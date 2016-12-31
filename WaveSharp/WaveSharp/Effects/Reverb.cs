using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSharp.Waves;

namespace WaveSharp.Effects
{
    public class Reverb : EffectBase
    {
        public double Seconds { get; private set; }
        public double Decay { get; private set; }
        public Reverb(double seconds, double decay)
        {
            this.Seconds = seconds;
            this.Decay = decay;
        }


        internal override IEnumerable<double> process(WaveBase wave)
        {
            int count = (int)(wave.SampleRate * Seconds);
            Queue<double> q = new Queue<double>();
            foreach (var item in wave.Take(count))
            {
                q.Enqueue(item);
                yield return item;
            }
            foreach (var item in wave.Skip(count))
            {
                var t = item + Decay * q.Dequeue();
                q.Enqueue(t);
                yield return t;
            }
            foreach (var item in q)
            {
                yield return item * Decay;
            }
        }
    }
}
