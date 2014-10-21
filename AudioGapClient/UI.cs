using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using NAudio.CoreAudioApi;

namespace AudioGap.Client
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
        }

        void UI_Load(object sender, EventArgs e)
        {
            var deviceEnum = new MMDeviceEnumerator();
            var deviceCol = deviceEnum.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

            var deviceList = deviceCol.ToList();

            AudioDeviceList.DataSource = deviceList;
            AudioDeviceList.DisplayMember = "FriendlyName";
        }

        void ConnectButton_Click(object sender, EventArgs e)
        {
            // TODO: this button can be clicked multiple times (and the result isn't pretty)
            Network.Connect(new IPEndPoint(IPAddress.Parse(ServerIP.Text), 11000), (MMDevice)AudioDeviceList.SelectedItem);
        }
    }
}
