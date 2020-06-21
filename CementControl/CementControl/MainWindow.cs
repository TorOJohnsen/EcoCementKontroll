using Serilog;
using System;
using System.Configuration;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using CementControl.DataAccess;
using CementControl.Models;

namespace CementControl
{
    public partial class MainWindow : Form
    {

        static Autofac.IContainer Container { get; set; }

        private CementContext db;

        private static HandlerRs232WeigthScale _handlerRs232WeigthScale;
        private static HandlerRs232PowerSupply _handlerRs232PowerSupply;

        private static SerialPortConfigParameters _weightScaleConfig;
        private static SerialPortConfigParameters _powerSupplyConfig;



        private static ISerialConnection iSerialConnection;

        private string _scaleComPort;
        private string _powerSupplyComPort;

        private bool _isWeightScaleTestMode;

        private bool _isStartLoadingCement = false;
        private bool _isRunningCement = false;
        private double _targetLoadCement;
        private double _currentlyLoadedCement;
        private double _startingWeigth;

        delegate void SetTextCallback(string text);
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
            db = new CementContext();

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


            var cdb = new CementDataServices(db);
            cdb.SaveCementData(new CementData(DateTime.Now, "DUDE", 23.5, 1));



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
                string message = "Silo kontroller";
                string title = "Plug inn silo kontroller ... ";
                MessageBox.Show(message, title);

                var disc = new DiscoverSerialConnections(_powerSupplyConfig);
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
                string message = "Vekt kontroller";
                string title = "Plug inn vekt  ... ";
                MessageBox.Show(message, title);

                var disc = new DiscoverSerialConnections(_weightScaleConfig);
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
            
            UpdateLabel(label_powerSupply, $"Silo tilkobling, {_powerSupplyConfig.ComPort}", Color.Green);

        }


        private void ReadVoltageSetting(object sender, string reading)
        {
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




        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.labelReadWeight.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelReadWeight.Text = text;
            }
        }




        private void DisplayWeight(object sender, double weight)
        {

            SetText($"{weight:F1}");
            Log.Debug($"Reading scale: {weight}");

            // Clean this up
            if (_isRunningCement)
            {
                if (weight <= _targetLoadCement)
                {
                    _handlerRs232PowerSupply.TurnOff();
                    _isRunningCement = false;
                }

                _currentlyLoadedCement = _startingWeigth - weight;
                weight_loaded.Text = $"{_currentlyLoadedCement.ToString():F1}";

            }
            
            
            if (_isStartLoadingCement)
            {
                _targetLoadCement = weight - Convert.ToDouble(desiredCementLoad.Text);
                _startingWeigth = weight;
                _currentlyLoadedCement = 0.0;
                _handlerRs232PowerSupply.TurnOn();
                _isRunningCement = true;
                _isStartLoadingCement = false;
                weight_loaded.Text = $"{_currentlyLoadedCement.ToString():F1}";
            }

        }


        private void readWeightTimer_Tick(object sender, EventArgs e)
        {
            Log.Debug("Read weight timer triggered.");
            if (_handlerRs232WeigthScale != null)
            {
                _handlerRs232WeigthScale.ReadWeight();
            }
        }

        private void startLoadWeight_Click(object sender, EventArgs e)
        {
            _isStartLoadingCement = true;
        }


    }
}
