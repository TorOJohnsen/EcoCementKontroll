using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;

namespace CementControl
{
    public partial class MainWindow : Form
    {

        static Autofac.IContainer Container { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            // Initialize logging
            App.ConfigureLogging();

            // Initialize app
            Container = App.ConfigureDependencyInjection();

            Log.Debug("TEst");


        }

        private void readWeightTimer_Tick(object sender, EventArgs e)
        {
            Log.Debug("Read weight timer triggered.");
        }
    }
}
