using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace AudioGapClient
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
        }

        private void UI_Load(object sender, EventArgs e)
        {
            MMDeviceEnumerator deviceEnum = new MMDeviceEnumerator();
            MMDeviceCollection deviceCol = deviceEnum.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            
            Collection<MMDevice> devices = new Collection<MMDevice>();
            foreach (var device in deviceCol)
            {
                devices.Add(device);
            }

            AudioDeviceList.DataSource = devices;
            AudioDeviceList.DisplayMember = "FriendlyName";
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Network.connect(null, (MMDevice)AudioDeviceList.SelectedItem);
        }
    }
}
