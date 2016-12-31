using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveSharp.Waves
{
    public class Sinus : WaveBase
    {
        public IEnumerable<double> Frequency { get; set; }
        public IEnumerable<double> Phase { get; set; }

        public Sinus(double frequency)
        {
            this.Frequency = new Constant(frequency);
            this.Phase = new Constant(0);
        }
        public Sinus(WaveBase frequency, WaveBase phase)
        {
            this.Frequency = frequency;
            this.Phase = phase;
        }
        public Sinus(WaveBase frequency)
        {
            this.Frequency = frequency;
            this.Phase = new Constant(0);
        }
        public override IEnumerator<double> GetEnumerator()
        {
            var f = Frequency.GetEnumerator();
            var p = Phase.GetEnumerator();
            double angle = 0;
            var step = 2 * Math.PI / SampleRate;
            for (int i = 0; f.MoveNext() && p.MoveNext(); i++)
            {
                angle += f.Current * step;
                yield return Math.Sin(angle + p.Current);
            }
        }

    }
}
