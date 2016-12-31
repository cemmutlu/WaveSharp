using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSharp.Waves;

namespace WaveSharp.Effects
{
    public class Delay : EffectBase
    {
        public float Seconds { get; private set; }
        public Delay(float seconds)
        {
            this.Seconds = seconds;
        }
        internal override IEnumerable<double> process(WaveBase wave)
        {
            int count = (int)(wave.SampleRate * Seconds);
            for (int i = 0; i < count; i++)
                yield return 0;
            foreach (var item in wave)
                yield return item;
        }
    }
}
