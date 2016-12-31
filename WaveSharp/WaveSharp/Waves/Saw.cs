using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveSharp.Waves
{
    public class Saw : WaveBase
    {
        public IEnumerable<double> Frequency { get; set; }
        public IEnumerable<double> Phase { get; set; }

        public Saw(IEnumerable<double> frequency, IEnumerable<double> phase)
        {
            this.Frequency = frequency;
            this.Phase = phase;
        }
        public Saw(IEnumerable<double> frequency)
        {
            this.Frequency = frequency;
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
                yield return value;
            }
        }

    }
}
