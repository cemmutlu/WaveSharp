using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSharp.Waves;

namespace WaveSharp.Effects
{
    public class Mask : EffectBase
    {
        public WaveBase MaskWave { get; set; }
        public Mask(WaveBase maskwave)
        {
            this.MaskWave = maskwave;
        }

        internal override IEnumerable<double> process(WaveBase wave)
        {
            var em = MaskWave.GetEnumerator();
            var ew = wave.GetEnumerator();
            while (em.MoveNext() && ew.MoveNext())
            {
                if (em.Current < ew.Current)
                    yield return em.Current;
                else if (-em.Current > ew.Current)
                    yield return -em.Current;
                else
                    yield return ew.Current;
            }
        }
    }
}
