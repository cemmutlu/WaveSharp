using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveSharp.Waves
{
    public class Triangular : WaveBase
    {
        public IEnumerable<double> Frequency { get; set; }
        public IEnumerable<double> Phase { get; set; }

        public Triangular(IEnumerable<double> frequency, IEnumerable<double> phase)
        {
            this.Frequency = frequency;
            this.Phase = phase;
        }
        public Triangular(IEnumerable<double> frequency)
        {
            this.Frequency = frequency;
            this.Phase = new Constant(0);
        }
        public Triangular(double frequency)
        {
            this.Frequency = new Constant(frequency);
            this.Phase = new Constant(0);
        }
        public override IEnumerator<double> GetEnumerator()
        {
            var f = Frequency.GetEnumerator();
            var p = Phase.GetEnumerator();
            double value = 0;
            double currentPhase = 0;
            for (int i = 0; f.MoveNext() && p.MoveNext(); i++)
            {
                var turn = f.Current / SampleRate + (p.Current - currentPhase);
                currentPhase = p.Current;
                value += turn;
                while (value > 1) value--;
                if (value < 0.25)
                    yield return value * 4;// Up /
                else if (value < 0.5)
                    yield return 2 - 4 * value;// Down /\
                else if (value < 0.75)
                    yield return -4 * value + 2;// Down \
                else
                    yield return -4 + 4 * value;//Up ^v
            }
        }

    }
}
