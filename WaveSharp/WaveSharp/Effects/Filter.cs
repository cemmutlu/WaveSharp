using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSharp.Waves;

namespace WaveSharp.Effects
{

    public class Filter : EffectBase
    {
        public enum FilterType
        {
            LowPass,
            HighPass,
            BandPass
        }
        public FilterType Type { get; set; }
        public double Frequency { get; set; }
        public Filter(FilterType type, double frequency)
        {
            this.Type = type;
            this.Frequency = frequency;
        }
        internal override IEnumerable<double> process(WaveBase wave)
        {
            switch (Type)
            {
                case FilterType.LowPass: return new Wave(LowPassFilter(wave));
                case FilterType.HighPass: return new Wave(HighPassFilter(wave));
                case FilterType.BandPass: return new Wave(BandPassFilter(wave));
                default: return null;
            }
        }
        IEnumerable<double> LowPassFilter(WaveBase wave)
        {
            double dlyOut2 = 0, dlyOut1 = 0;
            double dlyIn2 = 0, dlyIn1 = 0;
            double sqr2 = 1.414213562;
            double c = 1.0 / Math.Tan((Math.PI / wave.SampleRate) * Frequency);
            double c2 = c * c;
            double csqr2 = sqr2 * c;
            double d = c2 + csqr2 + 1;
            double In0 = 1 / d;
            double In1 = In0 + In0;
            double In2 = In0;
            double Out1 = (2 * (1 - c2)) / d;
            double Out2 = (c2 - csqr2 + 1) / d;

            foreach (var item in wave)
            {
                double outt = (In0 * item)
                   + (In1 * dlyIn1)
                   + (In2 * dlyIn2)
                   - (Out1 * dlyOut1)
                   - (Out2 * dlyOut2);
                dlyOut2 = dlyOut1; dlyOut1 = outt;
                dlyIn2 = dlyIn1; dlyIn1 = item;
                yield return outt;
            }
        }
        IEnumerable<double> HighPassFilter(WaveBase wave)
        {
            double dlyOut2 = 0, dlyOut1 = 0;
            double dlyIn2 = 0, dlyIn1 = 0;
            double sqr2 = 1.414213562;
            double c = Math.Tan((Math.PI / wave.SampleRate) * Frequency);
            double c2 = c * c;
            double csqr2 = sqr2 * c;
            double d = c2 + csqr2 + 1;
            double In0 = 1 / d;
            double In1 = -(In0 + In0);
            double In2 = In0;
            double Out1 = (2 * (c2 - 1)) / d;
            double Out2 = (1 - csqr2 + c2) / d;

            foreach (var item in wave)
            {
                double outt = (In0 * item)
                        + (In1 * dlyIn1)
                        + (In2 * dlyIn2)
                        - (Out1 * dlyOut1)
                        - (Out2 * dlyOut2);
                dlyOut2 = dlyOut1; dlyOut1 = outt;
                dlyIn2 = dlyIn1; dlyIn1 = item;
                yield return outt;
            }
        }
        IEnumerable<double> BandPassFilter(WaveBase wave)
        {
            double dlyOut2 = 0, dlyOut1 = 0;
            double dlyIn2 = 0, dlyIn1 = 0;
            double c = 1 / Math.Tan((Math.PI / wave.SampleRate) * Frequency);
            double d = 1 + c;
            double In0 = 1 / d;
            double In1 = 0;
            double In2 = -In0;
            double Out1 = (-c * 2 * Math.Cos(2 * Math.PI * Frequency / wave.SampleRate)) / d;
            double Out2 = (c - 1) / d;
            foreach (var item in wave)
            {
                var outt = (In0 * item)
                        + (In1 * dlyIn1)
                        + (In2 * dlyIn2)
                        - (Out1 * dlyOut1)
                        - (Out2 * dlyOut2);
                dlyOut2 = dlyOut1;
                dlyOut1 = outt;
                dlyIn2 = dlyIn1;
                dlyIn1 = item;
                yield return outt;
            }
        }

        IEnumerable<double> FastLowPassFilter(WaveBase wave, double n)
        {
            double value = 0;
            foreach (var item in wave)
            {
                yield return value += (item - value) / n;
            }
        }
        IEnumerable<double> FastHighPassFilter(WaveBase wave, double n)
        {
            double value = 0;
            foreach (var item in wave)
            {
                yield return value += item - value * n;
            }
        }

        IEnumerable<double> LowPassFilter2(WaveBase wave, double cutoff_freq)
        {
            double RC = 1 / (cutoff_freq * 2 * Math.PI);
            double dt = 1 / 2048.0;
            double alpha = dt / (RC + dt);
            double curr = 0;
            foreach (var item in wave)
            {
                curr = curr + (alpha * (item - curr));
                yield return curr;
            }
        }

    }
}
