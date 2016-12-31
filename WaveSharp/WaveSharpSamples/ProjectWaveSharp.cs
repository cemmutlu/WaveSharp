using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using WaveSharp;
using WaveSharp.Waves;

namespace WaveSharpSamples
{
    public partial class ProjectWaveSharp : Form
    {
        WaveOut waveOut = new WaveOut();
        MethodInfo[] samples;
        NAudioWaveProvider provider;
        public ProjectWaveSharp()
        {
            InitializeComponent();
            samples = typeof(Samples).GetMethods()
                .Where(x => x.IsStatic)
                .ToArray();
            listBoxSamples.DataSource = samples.Select(x => x.Name).ToArray();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            waveOut.Stop();
            waveOut = new WaveOut();
            if (listBoxSamples.SelectedItem != null)
            {
                var wave = samples[listBoxSamples.SelectedIndex].Invoke(null, null) as WaveBase;
                provider = new NAudioWaveProvider(wave);
                provider.Volume = trackBarVolume.Value / 100f;
                provider.SetWaveFormat(wave.SampleRate, 1); // 16kHz mono
                waveOut.Init(provider);
                waveOut.Play();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            waveOut.Stop();
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            provider.Volume = trackBarVolume.Value / 100f;
        }
    }
}
