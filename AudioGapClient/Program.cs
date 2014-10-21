using System;
using System.Windows.Forms;

namespace AudioGap.Client
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