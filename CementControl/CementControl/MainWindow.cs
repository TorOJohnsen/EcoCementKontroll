using Serilog;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CementControl.DataAccess;
using CementControl.Execution;
using CementControl.Models;
using Microsoft.VisualBasic.FileIO;

namespace CementControl
{
    public partial class MainWindow : Form
    {

        //static Autofac.IContainer Container { get; set; }

        private readonly DataFileHandle _dataHandle;
        private readonly string _dataLogFile;
        
        private static HandlerRs232WeigthScale _handlerRs232WeigthScale;
        private static HandlerRs232PowerSupply _handlerRs232PowerSupply;

        private static SerialPortConfigParameters _weightScaleConfig;
        private static SerialPortConfigParameters _powerSupplyConfig;

        private ISerialConnection _scaleSerialConnection;
        private ISerialConnection _psSerialConnection;

        private static ExecuteLoading _execute;

        private string _scaleComPort;
        private string _powerSupplyComPort;

        private bool _isWeightScaleTestMode;
        private bool _isPowerSupplyTestMode;

        private bool _isOnlyConnectingAndLoggingWeight = false;

        private readonly ILogger _logger;



        delegate void SetTextCallbackCurrentWeightDisplay(string text);
        delegate void SetTextCallbackCurrentLoadedCementDisplay(string text);
        delegate void SetCallbackUpdateRunButtonStatus(bool isEnabled);
        delegate void SetCallbackUpdateStopLoadWeightButtonStatus(bool isEnabled);
        delegate void SetbuttonConnectSerialStatus(bool isEnabled);
        delegate void EnableSystemReadTimer(bool isEnabled);



        /*
        This exception was originally thrown at this call stack:
        [External Code]
        CementControl.MainWindow.DisplayWeight(object, double) in MainWindow.cs
        CementControl.HandlerRs232WeigthScale.DataReceived(object, string) in HandlerRs232WeigthScale.cs
        CementControl.SerialConnection.DataReceivedHandler(object, System.IO.Ports.SerialDataReceivedEventArgs) in SerialConnection.cs
        [External Code]
        System.InvalidOperationException: 'Cross-thread operation not valid: Control 'labelReadWeight' accessed from a thread other than the thread it was created on.'
         */


        public MainWindow()
        {

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            InitializeComponent();

            // db context
            _dataLogFile = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\ECO\Datafiles\CementLog.{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            _dataHandle = new DataFileHandle(_dataLogFile);

            // Initialize logging
            App.ConfigureLogging();
            _logger = Log.ForContext<MainWindow>();
            _logger.Information("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~  New Run  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            // Init GUI

            // Initialize app
            //  Container = App.ConfigureDependencyInjection();

            //Log.Debug("Test");

            InitConfigFileItems();

            _logger.Information("Tool initiation MainWindow completed");

        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;

            try
            {
                string unHandledExceptionLogFile =
                    $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\ECO\Logfiles\ExceptionLog.{DateTime.Now:yyyyMMdd_HHmmss}.log";
                StringBuilder sb = new StringBuilder();
                sb.Append($"-- Exception --{Environment.NewLine}"); 
                sb.Append(e.Message);
                sb.Append($"{Environment.NewLine}");
                sb.Append(e); 
                File.AppendAllText(unHandledExceptionLogFile, sb.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show(e.ToString(), "Feil i programmet");
            }
        }



        private void CopyDataFoldersToGoogleDrive()
        {

            var sourceDir = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\ECO\";
            string destDir = ConfigurationManager.AppSettings["app:googleDriveFolder"];

            if (destDir == "")
            {
                destDir = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Google Drive\CementLastingData\";
            }

            _logger.Information($"Copy data to GoogleDrive: {sourceDir} to {destDir}");

            try
            {
                //FileSystem.CopyDirectory(sourceDir, destDir, UIOption.AllDialogs);
                FileSystem.CopyDirectory(sourceDir, destDir, false);
            }
            catch (Exception e)
            {
                _logger.Error($"Copy data to GoogleDrive: {e.Message}");
                _logger.Error($"{e}");
            }
        }




        public void InitiateSystemInterfacesAndExecution()
        {
            if (_isOnlyConnectingAndLoggingWeight == false)
            {
                ConfigurePowerSupplyComPort();
                InitPowerSupply();
            }
            else
            {
                _handlerRs232PowerSupply = null;
            }

            ConfigureWeightScaleComPort();
            InitWeightScale();

        }


        public void InitiateSystemExecution()
        {

            _logger.Information($"Starting execution, logging to {_dataLogFile}");
            var cdb = new CementDataServices(_dataHandle);

            _execute = new ExecuteLoading(cdb, _handlerRs232WeigthScale, _handlerRs232PowerSupply);
            _execute.CementLoadFinished += CompletedLoad;

        }



        private void UpdateLabel(Label label, string message, Color color)
        {
            label.Text = message;
            label.ForeColor = color;
        }





        private void InitConfigFileItems()
        {

            _logger.Information($"Initiate config files items");

            _scaleComPort = ConfigurationManager.AppSettings["app:weightScalePort"];
            _powerSupplyComPort = ConfigurationManager.AppSettings["app:powerSupplyPort"];
            _isWeightScaleTestMode = Convert.ToBoolean(ConfigurationManager.AppSettings["app:scaleTestMode"]);
            _isPowerSupplyTestMode = Convert.ToBoolean(ConfigurationManager.AppSettings["app:powerSupplyTestMode"]);

            var cfgPwrSupply = new SerialPortConfig(ConfigurationManager.AppSettings["app:pwrSupplySerialConfig"]);
            var cfgWeightScale = new SerialPortConfig(ConfigurationManager.AppSettings["app:scaleSerialConfig"]);

            _powerSupplyConfig = cfgPwrSupply.GetConnectionObject();
            _weightScaleConfig = cfgWeightScale.GetConnectionObject();

        }


        private void ConfigurePowerSupplyComPort()
        {

            _logger.Information($"Configure connection to power supply for Silo");

            if (_powerSupplyComPort == "")
            {
                string title = "Silo tilkobling";
                string message1 = "Dra ut plugg for silo tilkobling om denne er tilkoblet, trykk deretter OK";
                string message2 = "Plug inn silo tilkobling og trykk OK";
                MessageBox.Show(message1, title);
                

                var disc = new DiscoverSerialConnections(_powerSupplyConfig, title, message2);
                disc.Run();

            }
            else
            {
                _powerSupplyConfig.ComPort = _powerSupplyComPort;
            }

        }


        private void ConfigureWeightScaleComPort()
        {
            _logger.Information($"Configure connection to weight ");

            if (_scaleComPort == "")
            {
                string title = "Vekt tilkobling";
                string message1 = "Dra up plugg for vekt tilkobling om denne er tilkoblet, trykk deretter OK";
                string message2 = "Plug inn vekt tilkobling og trykk OK";

                MessageBox.Show(message1, title);

                var disc = new DiscoverSerialConnections(_weightScaleConfig, title, message2);
                disc.Run();

            }
            else
            {
                _weightScaleConfig.ComPort = _scaleComPort;
            }
        }




        private void InitWeightScale()
        {
            _logger.Information($"Initiate connection to weight scale");

            Color color = Color.Green;
            
            if (_isWeightScaleTestMode)
            {
                _scaleSerialConnection = new SerialConnectionScaleTest();
                color = Color.DarkOrange;
            }
            else
            {
                _scaleSerialConnection = new SerialConnection();
            }

            try
            {
                _handlerRs232WeigthScale = new HandlerRs232WeigthScale(_scaleSerialConnection);
                _handlerRs232WeigthScale.OpenConnection(_weightScaleConfig.ComPort, _weightScaleConfig.BaudRate, _weightScaleConfig.Parity,
                    _weightScaleConfig.StopBits, _weightScaleConfig.DataBits, _weightScaleConfig.Handshake,
                    _weightScaleConfig.NewLine, _weightScaleConfig.ReadMode);

                _logger.Information($"Opened port to Scale");

                _handlerRs232WeigthScale.OnDataRead += DisplayWeight;

                UpdateLabel(label_weight, $"Vekt tilkobling, {_weightScaleConfig.ComPort}", color);

            }
            catch (Exception e)
            {
                _handlerRs232WeigthScale = null;
                _logger.Error($"Exception InitPowerSupply: {e.Message}");
            }
        }



        private void InitPowerSupply()
        {
            _logger.Information($"Initiate connection to power supply for Silo");

            Color color = Color.Green;

            if (_isPowerSupplyTestMode)
            {
                _psSerialConnection = new SerialConnectionPsTest();
                color = Color.DarkOrange;
            }
            else
            {
                _psSerialConnection = new SerialConnection();
            }


            try
            {
                _handlerRs232PowerSupply = new HandlerRs232PowerSupply(_psSerialConnection);
                _handlerRs232PowerSupply.OpenConnection(_powerSupplyConfig.ComPort, _powerSupplyConfig.BaudRate, _powerSupplyConfig.Parity,
                    _powerSupplyConfig.StopBits, _powerSupplyConfig.DataBits, _powerSupplyConfig.Handshake,
                    _powerSupplyConfig.NewLine, _powerSupplyConfig.ReadMode);

                _logger.Information($"Opened port to Silo");

                _handlerRs232PowerSupply.OnDataRead += ReadVoltageSetting;

                UpdateLabel(label_powerSupply, $"Silo tilkobling, {_powerSupplyConfig.ComPort}", color);

            }
            catch (Exception e)
            {
                _handlerRs232PowerSupply = null;
                _logger.Error($"Exception InitPowerSupply: {e.Message}");
            }
        }


        private void ReadVoltageSetting(object sender, string reading)
        {
            try
            {
                var readingV = Convert.ToDouble(reading, CultureInfo.InvariantCulture);
                _execute?.UpdateVoltageSetting(readingV);
                Log.Debug($"Reading form power supply: {reading}");

            }
            catch (Exception e)
            {
                Log.Error($"Reading form power supply: {reading}, {e.Message}");
            }

        }


        private void SetTextCurrentWeight(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.labelReadWeight.InvokeRequired)
            {
                SetTextCallbackCurrentWeightDisplay d = new SetTextCallbackCurrentWeightDisplay(SetTextCurrentWeight);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelReadWeight.Text = text;
            }
        }



        private void SetTextLoadedCementWeight(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.weight_loaded.InvokeRequired)
            {
                SetTextCallbackCurrentLoadedCementDisplay d = new SetTextCallbackCurrentLoadedCementDisplay(SetTextLoadedCementWeight);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.weight_loaded.Text = text;
            }
        }



        private void DisplayWeight(object sender, double weight)
        {

            SetTextCurrentWeight($"{weight:F1}");
            _logger.Debug($"Reading scale: {weight}");

            _execute.UpdateCurrentWeight(weight);
            _execute.Execution();
            _execute.SaveData();

            SetTextLoadedCementWeight($"{_execute.GetCurrentlyLoadedCement():F1}");
        }


        private void readWeightTimer_Tick(object sender, EventArgs e)
        {
            Log.Debug("Read weight timer triggered.");
            _handlerRs232WeigthScale?.ReadWeight();
            _handlerRs232PowerSupply?.GetVoltage();
        }

        private void startLoadWeight_Click(object sender, EventArgs e)
        {
            // TODO
            double cementToBeLoaded = 0.0;
            string readFromGui = desiredCementLoad.Text;
            readFromGui = readFromGui.Replace(",", ".");

            try
            {
                desiredCementLoad.ForeColor = Color.Black;
                cementToBeLoaded = Convert.ToDouble(desiredCementLoad.Text);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error converting {readFromGui} desired weight to double {ex.Message}");
                desiredCementLoad.ForeColor = Color.Red;
                return;
            } 

            string description = textBoxDescription.Text;

            _execute.StartLoading(cementToBeLoaded, description);
            SetStartLoadButtonEnabledState(false);
        }


        private void CompletedLoad(object sender, EventArgs e)
        {
            SetStartLoadButtonEnabledState(true);
        }



        private void SetStartLoadButtonEnabledState(bool isEnabled)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.startLoadWeight.InvokeRequired)
            {
                SetCallbackUpdateRunButtonStatus d = new SetCallbackUpdateRunButtonStatus(SetStartLoadButtonEnabledState);
                this.Invoke(d, new object[] { isEnabled });
            }
            else
            {
                this.startLoadWeight.Enabled = isEnabled;
            }
        }




        //SetCallbackUpdateStopLoadWeightButtonStatus
        private void SetStopLoadButtonEnabledState(bool isEnabled)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.startLoadWeight.InvokeRequired)
            {
                SetCallbackUpdateStopLoadWeightButtonStatus d = new SetCallbackUpdateStopLoadWeightButtonStatus(SetStopLoadButtonEnabledState);
                this.Invoke(d, new object[] { isEnabled });
            }
            else
            {
                this.stopLoadWeight.Enabled = isEnabled;
            }
        }




        private void SetButtonConnectSerialEnabledState(bool isEnabled)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.buttonConnectSerial.InvokeRequired)
            {
                SetbuttonConnectSerialStatus d = new SetbuttonConnectSerialStatus(SetButtonConnectSerialEnabledState);
                this.Invoke(d, new object[] { isEnabled });
            }
            else
            {
                this.buttonConnectSerial.Enabled = isEnabled;
            }
        }


        private void EnableSystemReadTimerEnabledState(bool isEnabled)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.buttonConnectSerial.InvokeRequired)
            {
                EnableSystemReadTimer d = new EnableSystemReadTimer(EnableSystemReadTimerEnabledState);
                this.Invoke(d, new object[] { isEnabled });
            }
            else
            {
                this.readWeightTimer.Enabled = isEnabled;
            }
        }




        // Exit Handler
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            _handlerRs232WeigthScale?.ClosePort();
            
            _handlerRs232PowerSupply?.SetVoltage(0.0);
            _handlerRs232PowerSupply?.ClosePort();
            
            _dataHandle?.Close();
            
            CopyDataFoldersToGoogleDrive();
        }


        private void stopLoadWeight_Click(object sender, EventArgs e)
        {
            _execute.StopLoading();
        }

        
        private void buttonConnectSerial_Click(object sender, EventArgs e)
        {
            _logger.Information("Entering COM port setup section");


            ReadConfiguredPortsFromGui();

            CheckIOnlyLoggingWeight();

            InitiateSystemInterfacesAndExecution();

            // Disable button
            if (_handlerRs232WeigthScale != null && (_isOnlyConnectingAndLoggingWeight || _handlerRs232PowerSupply != null))
            {
                SetButtonConnectSerialEnabledState(false);
                _logger.Information("Set up system execution");
                InitiateSystemExecution();

                _handlerRs232PowerSupply?.TurnOff();
                // Turn on timer and enable buttons
                _logger.Information("Enable system execution timer");
                EnableSystemReadTimerEnabledState(true);
                SetStartLoadButtonEnabledState(true);
                SetStopLoadButtonEnabledState(true);

                _logger.Information("Successfully set up COM port setup section");
            }
            else
            {
                _logger.Error(
                    $"Feil ved oppsett av COM porter porter Silo: {_handlerRs232PowerSupply} og vekt: {_handlerRs232WeigthScale}");
            }
        }

        private void CheckIOnlyLoggingWeight()
        {
            _isOnlyConnectingAndLoggingWeight = checkBoxOnlyLogWeight.Checked;
            if (_isOnlyConnectingAndLoggingWeight)
            {
                _logger.Information("Not connecting silo, only logging weight data");
            }
        }

        private void ReadConfiguredPortsFromGui()
        {

            string userEnteredPortPowerSupply = textBoxPowerSupplyComPort.Text;
            string userEnteredPortScale = textBoxScaleComPort.Text;

            textBoxPowerSupplyComPort.Enabled = false;
            textBoxScaleComPort.Enabled = false;

            if (userEnteredPortPowerSupply != "")
            {
                _powerSupplyComPort = userEnteredPortPowerSupply.ToUpper();
                _logger.Information($"User configured COM port for Silo (PS): {_powerSupplyComPort}");
            }

            if (userEnteredPortScale != "")
            {
                _scaleComPort = userEnteredPortScale.ToUpper();
                _logger.Information($"User configured COM port for Scale: {_scaleComPort}");
            }
        }

        private void buttonDeviceManager_Click(object sender, EventArgs e)
        {
            Process.Start("devmgmt.msc");
        }
    }
}
