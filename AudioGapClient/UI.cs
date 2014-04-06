using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.BassWasapi;

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
            var info = new BASS_WASAPI_DEVICEINFO();
            var Devices = new int[BassWasapi.BASS_WASAPI_GetDeviceCount()]; //List of devices

            for (int n = 0; BassWasapi.BASS_WASAPI_GetDeviceInfo(n, info); n++)
            {
                if (info.SupportsRecording && info.IsEnabled)
                {
                    AudioDeviceList.Items.Add(info.ToString());
                    Devices[n] = n;
                    AudioDeviceList.SelectedItem = info.ToString();
                }
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Start Bass and BassWASAPI
                Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
                BassWasapi.BASS_WASAPI_Init(0, 44100, 2, BASSWASAPIInit.BASS_WASAPI_AUTOFORMAT, 0f, 0f, null,
                    IntPtr.Zero);
            }
            catch (Exception exc)
            {
                MessageBox.Show("BASS Failed to Init. \n " + exc);
            }


                    var Device = AudioDeviceList.SelectedIndex;
                    var _wasapi = new BassWasapiHandler(Device, false, 44100, 2, 0f, 0f); //Device
                    var _wasapiOutput = new BassWasapiHandler(Device, false, 48000, 2, 0f, 0f);

                    // init and start WASAPI
                    _wasapi.Init();
                    if (_wasapi.DeviceMute)
                        _wasapi.DeviceMute = false;
                    _wasapi.Start();

                    // setup a full-duplex stream
                    _wasapi.SetFullDuplex(0, BASSFlag.BASS_STREAM_DECODE, false);

                    var stream = _wasapi.OutputChannel;
                    // and assign it to an output
                    _wasapiOutput.AddOutputSource(stream, BASSFlag.BASS_DEFAULT);

                    //Start the output stream
                    _wasapiOutput.Init();
                    _wasapiOutput.Start();
            
        }
    }
}
