using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSharp.Waves;

namespace WaveSharp.Effects
{
    public abstract class EffectBase
    {
        public WaveBase Apply(WaveBase wave)
        {
            return new Wave(process(wave));
        }
        internal abstract IEnumerable<double> process(WaveBase wave);
    }
}
