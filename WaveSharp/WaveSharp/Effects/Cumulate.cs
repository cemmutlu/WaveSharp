using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSharp.Waves;

namespace WaveSharp.Effects
{
    public class Cumulate:EffectBase
    {
        internal override IEnumerable<double> process(WaveBase wave)
        {
            double sum = 0;
            foreach (var item in wave)
            {
                sum += item;
                yield return sum;
            }
        }
    }
}
