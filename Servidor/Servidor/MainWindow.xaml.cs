using Servidor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Servidor
{

    public class SocketS
    {
        public Socket socket { get; set; }


        public SocketS()
        {
            this.socket = socket;
        }
    }

    // SocketT2H = SocketS
    //_serverSocket = socketServidor

    public partial class MainWindow : Window
    {

        




        public MainWindow()
        {
            InitializeComponent();
            ManejoBWorker();
        }


        public void ManejoBWorker()
        {

            BackgroundWorker bw = new BackgroundWorker();
            SocketServer ss = new SocketServer();
            bw.WorkerReportsProgress = true;
            bw.DoWork += ss.Conectar;
            bw.ProgressChanged += metodoProgress;
            bw.RunWorkerCompleted += metodoCompleted;
            bw.RunWorkerAsync();

        }

        public void metodoDoWork(object sender, DoWorkEventArgs e)
        {

        }

        public void metodoProgress(object sender, ProgressChangedEventArgs e)
        {
            


        }

        public void metodoCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string resultado = (string)e.Result;


            ManejoBWorker();
        }
    }
    
}
