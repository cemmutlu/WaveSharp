using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSharp.Waves;

namespace WaveSharp.Effects
{
    public class Echo : EffectBase
    {
        public float Seconds { get; private set; }
        public float Decay { get; private set; }
        public Echo(float seconds,float decay)
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
                q.Enqueue(item);
                yield return item + Decay * q.Dequeue();
            }
            foreach (var item in q)
            {
                yield return item * Decay;
            }
        }
    }
}
