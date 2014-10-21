using System;
using System.Windows.Forms;

namespace AudioGapClient
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new UI());
        }
    }
}