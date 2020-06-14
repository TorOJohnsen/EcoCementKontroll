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
        private static SerialConnectionScaleTest serialConnectionScaleTest;

        private string _scaleComPort;
        private string _powerSupplyComPort;


        public MainWindow()
        {
            InitializeComponent();

            // Initialize logging
            App.ConfigureLogging();

            // Initialize app
            Container = App.ConfigureDependencyInjection();

            Log.Debug("TEst");

            InitConfigFileItems();
            InitWeightScale();


        }


        private void InitConfigFileItems()
        {
            _scaleComPort = ConfigurationManager.AppSettings["app:weightScalePort"];
            _powerSupplyComPort = ConfigurationManager.AppSettings["app:powerSupplyPort"];
        }


        private void InitWeightScale()
        {
            serialConnectionScaleTest = new SerialConnectionScaleTest();
            weigthScaleControl = new WeigthScaleControl(serialConnectionScaleTest);
            weigthScaleControl.OpenConnection(_scaleComPort, 9600, Parity.None, StopBits.One, 8, Handshake.None, "\r");
            weigthScaleControl.OnDataRead += DisplayWeight;
        }


        private void DisplayWeight(object sender, double weight)
        {
            label_weight.Text = weight.ToString();
        }




        private void readWeightTimer_Tick(object sender, EventArgs e)
        {
            Log.Debug("Read weight timer triggered.");
            if (weigthScaleControl != null)
            {
                weigthScaleControl.ReadWeight();
            }
        }
    }
}
