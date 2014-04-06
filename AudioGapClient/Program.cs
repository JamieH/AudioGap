using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;

namespace AudioGapClient
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            BassNet.Registration("swkauker@yahoo.com", "2X2832371834322");

            Application.EnableVisualStyles();
            Application.Run(new UI()); // or whatever
        }
    }
}