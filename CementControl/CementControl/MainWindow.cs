using Serilog;
using System;
using System.Configuration;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using CementControl.DataAccess;
using CementControl.Execution;
using CementControl.Models;

namespace CementControl
{
    public partial class MainWindow : Form
    {

        //static Autofac.IContainer Container { get; set; }

        private DataFileHandle dataHandle;

        private static HandlerRs232WeigthScale _handlerRs232WeigthScale;
        private static HandlerRs232PowerSupply _handlerRs232PowerSupply;

        private static SerialPortConfigParameters _weightScaleConfig;
        private static SerialPortConfigParameters _powerSupplyConfig;

        private static ExecuteLoading _execute;

        private static ISerialConnection iSerialConnection;

        private string _scaleComPort;
        private string _powerSupplyComPort;

        private bool _isWeightScaleTestMode;

        delegate void SetTextCallbackCurrentWeightDisplay(string text);
        delegate void SetTextCallbackCurrentLoadedCementDisplay(string text);
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
            InitializeComponent();

            // db context
            dataHandle = new DataFileHandle(@"c:\temp\dude.txt");

            // Initialize logging
            App.ConfigureLogging();


            // Init GUI

            // Initialize app
            //  Container = App.ConfigureDependencyInjection();

            Log.Debug("Test");

            InitConfigFileItems();

            ConfigurePowerSupplyComPort();
            ConfigureWeightScaleComPort();


            InitWeightScale();
            InitPowerSupply();


            var cdb = new CementDataServices(dataHandle);


            _execute = new ExecuteLoading(cdb, _handlerRs232WeigthScale, _handlerRs232PowerSupply);
            _execute.CementLoadFinished += CompletedLoad;
        }

 

        private void IntiGui()
        { 
            UpdateLabel(label_weight, "Vekt tilkobling .. venter", Color.Red);
            UpdateLabel(label_powerSupply, "Silo tilkobling .. venter", Color.Red);
        }

        
        private void UpdateLabel(Label label, string message, Color color)
        {
            label.Text = message;
            label.ForeColor = color;
        }





        private void InitConfigFileItems()
        {
            _scaleComPort = ConfigurationManager.AppSettings["app:weightScalePort"];
            _powerSupplyComPort = ConfigurationManager.AppSettings["app:powerSupplyPort"];
            _isWeightScaleTestMode = Convert.ToBoolean(ConfigurationManager.AppSettings["app:scaleTestMode"]);

            var cfgPwrSupply = new SerialPortConfig(ConfigurationManager.AppSettings["app:pwrSupplySerialConfig"]);
            var cfgWeightScale = new SerialPortConfig(ConfigurationManager.AppSettings["app:scaleSerialConfig"]);

            _powerSupplyConfig = cfgPwrSupply.GetConnectionObject();
            _weightScaleConfig = cfgWeightScale.GetConnectionObject();

        }


        private void ConfigurePowerSupplyComPort()
        {

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


        private void InitPowerSupply()
        {
            iSerialConnection = new SerialConnection();


            _handlerRs232PowerSupply = new HandlerRs232PowerSupply(iSerialConnection);
            _handlerRs232PowerSupply.OpenConnection(_powerSupplyConfig.ComPort, _powerSupplyConfig.BaudRate, _powerSupplyConfig.Parity, 
                                                    _powerSupplyConfig.StopBits, _powerSupplyConfig.DataBits, _powerSupplyConfig.Handshake, 
                                                    _powerSupplyConfig.NewLine, _powerSupplyConfig.ReadMode);

            _handlerRs232PowerSupply.OnDataRead += ReadVoltageSetting;
            
            // Initialize to off
            _handlerRs232PowerSupply.SetVoltage(0.0);

            UpdateLabel(label_powerSupply, $"Silo tilkobling, {_powerSupplyConfig.ComPort}", Color.Green);



        }


        private void ReadVoltageSetting(object sender, string reading)
        {
            var readingV = Convert.ToDouble(reading);
            _execute.UpdateVoltageSetting(readingV);
            Log.Debug($"Reading form power supply: {reading}");
        }




        private void InitWeightScale()
        {
            Color color = Color.Green;
            if (_isWeightScaleTestMode)
            {
                iSerialConnection = new SerialConnectionScaleTest();
                color = Color.DarkOrange;
            }
            else
            {
                iSerialConnection = new SerialConnection();
            }

            _handlerRs232WeigthScale = new HandlerRs232WeigthScale(iSerialConnection);
            _handlerRs232WeigthScale.OpenConnection(_weightScaleConfig.ComPort, _weightScaleConfig.BaudRate, _weightScaleConfig.Parity,
                                                    _weightScaleConfig.StopBits, _weightScaleConfig.DataBits, _weightScaleConfig.Handshake,
                                                    _weightScaleConfig.NewLine, _weightScaleConfig.ReadMode);
            _handlerRs232WeigthScale.OnDataRead += DisplayWeight;

            UpdateLabel(label_weight, $"Vekt tilkobling, {_weightScaleConfig.ComPort}", color);


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
            if (this.labelReadWeight.InvokeRequired)
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
            Log.Debug($"Reading scale: {weight}");

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
            double cementToBeLoaded = Convert.ToDouble(desiredCementLoad.Text);
            string description = textBoxDescription.Text;

            _execute.StartLoading(cementToBeLoaded, description);
            startLoadWeight.Enabled = false;
        }


        private void CompletedLoad(object sender, EventArgs e)
        {
            startLoadWeight.Enabled = true;
        }



        // Exit Handler
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _handlerRs232WeigthScale?.ClosePort();
            
            _handlerRs232PowerSupply?.SetVoltage(0.0);
            _handlerRs232PowerSupply?.ClosePort();
        }

        private void stopLoadWeight_Click(object sender, EventArgs e)
        {
            _execute.StopLoading();
        }
    }
}
