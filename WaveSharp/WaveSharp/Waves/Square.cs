using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveSharp.Waves
{
    public class Square : WaveBase
    {
        public IEnumerable<double> Frequency { get; set; }
        public IEnumerable<double> Phase { get; set; }

        public Square(IEnumerable<double> frequency, IEnumerable<double> phase)
        {
            this.Frequency = frequency;
            this.Phase = phase;
        }
        public Square(IEnumerable<double> frequency)
        {
            this.Frequency = frequency;
            this.Phase = new Constant(0);
        }
        public override IEnumerator<double> GetEnumerator()
        {
            var f = Frequency.GetEnumerator();
            var p = Phase.GetEnumerator();
            double value = 0;
            double lastPhase = 0;
            for (int i = 0; f.MoveNext() && p.MoveNext(); i++)
            {
                value += f.Current / SampleRate + (p.Current - lastPhase);
                lastPhase = p.Current;
                while (value > 1) value--;
                if (value < 0.5)
                    yield return 0;
                else
                    yield return 1;
            }
        }

    }
}
