using Serilog;
using System;
using System.Configuration;
using System.IO.Ports;
using System.Windows.Forms;

namespace CementControl
{
    public partial class MainWindow : Form
    {

        static Autofac.IContainer Container { get; set; }

        private static WeigthScaleControl weigthScaleControl;
        private static PowerSupplyControl powerSupplyControl;

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
        CementControl.WeigthScaleControl.DataReceived(object, string) in WeigthScaleControl.cs
        CementControl.SerialConnection.DataReceivedHandler(object, System.IO.Ports.SerialDataReceivedEventArgs) in SerialConnection.cs
        [External Code]
        System.InvalidOperationException: 'Cross-thread operation not valid: Control 'labelReadWeight' accessed from a thread other than the thread it was created on.'
         */


        public MainWindow()
        {
            InitializeComponent();


            // Initialize logging
            App.ConfigureLogging();

            // Initialize app
            //  Container = App.ConfigureDependencyInjection();

            Log.Debug("TEst");

            InitConfigFileItems();
            InitWeightScale();
            //InitPowerSupply();


        }


        private void InitConfigFileItems()
        {
            _scaleComPort = ConfigurationManager.AppSettings["app:weightScalePort"];
            _powerSupplyComPort = ConfigurationManager.AppSettings["app:powerSupplyPort"];
            _isWeightScaleTestMode = Convert.ToBoolean(ConfigurationManager.AppSettings["app:scaleTestMode"]);
        }


        private void InitPowerSupply()
        {
            iSerialConnection = new SerialConnection();


            powerSupplyControl = new PowerSupplyControl(iSerialConnection);
            powerSupplyControl.OpenConnection(_powerSupplyComPort, 9600, Parity.None, StopBits.One, 8, Handshake.None, "\n");
            powerSupplyControl.OnDataRead += ReadVoltageSetting;
        }


        private void ReadVoltageSetting(object sender, string reading)
        {
            Log.Debug($"Reading form power supply: {reading}");
        }




        private void InitWeightScale()
        {
            if (_isWeightScaleTestMode)
            {
                iSerialConnection = new SerialConnectionScaleTest();
            }
            else
            {
                iSerialConnection = new SerialConnection();
                iSerialConnection.SetReadType(ReadMode.ReadTillSlashRSlashN);
            }

            weigthScaleControl = new WeigthScaleControl(iSerialConnection);
            weigthScaleControl.OpenConnection(_scaleComPort, 9600, Parity.None, StopBits.One, 8, Handshake.None, "\r");
            weigthScaleControl.OnDataRead += DisplayWeight;
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

            SetText($"{weight.ToString():F1}");
            Log.Debug($"Reading scale: {weight}");

            // Clean this up
            if (_isRunningCement)
            {
                if (weight <= _targetLoadCement)
                {
                    powerSupplyControl.TurnOff();
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
                powerSupplyControl.TurnOn();
                _isRunningCement = true;
                _isStartLoadingCement = false;
                weight_loaded.Text = $"{_currentlyLoadedCement.ToString():F1}";
            }

        }


        private void readWeightTimer_Tick(object sender, EventArgs e)
        {
            Log.Debug("Read weight timer triggered.");
            if (weigthScaleControl != null)
            {
                weigthScaleControl.ReadWeight();
            }
        }

        private void startLoadWeight_Click(object sender, EventArgs e)
        {
            _isStartLoadingCement = true;
        }


    }
}
