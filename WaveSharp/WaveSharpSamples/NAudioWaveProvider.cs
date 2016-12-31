using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSharp.Waves;

namespace WaveSharpSamples
{
    public class NAudioWaveProvider : IWaveProvider
    {
        private WaveFormat waveFormat;
        IEnumerator<double> enumsource;
        public float Volume { get; set; } = 1;
        public NAudioWaveProvider(WaveBase wave) : this(44000, 1)
        {
            enumsource = wave.GetEnumerator();
        }

        public NAudioWaveProvider(int sampleRate, int channels)
        {
            SetWaveFormat(sampleRate, channels);
        }

        public void SetWaveFormat(int sampleRate, int channels)
        {
            this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            WaveBuffer waveBuffer = new WaveBuffer(buffer);
            int samplesRequired = count / 4;
            int samplesRead = Read(waveBuffer.FloatBuffer, offset / 4, samplesRequired);
            return samplesRead * 4;
        }
        public int Read(float[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;
            for (int n = 0; n < sampleCount; n++)
            {
                enumsource.MoveNext();
                buffer[n + offset] = Volume * (float)enumsource.Current;
            }
            return sampleCount;
        }
        public WaveFormat WaveFormat
        {
            get { return waveFormat; }
        }
    }

}
