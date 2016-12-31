using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSharp.Waves;

namespace WaveSharp.Effects
{
    public class Clip:EffectBase
    {
        public float Value { get; set; }
        public Clip(float value)
        {
            this.Value = value;
        }

       
        internal override IEnumerable<double> process(WaveBase wave)
        {
            foreach (var item in wave)
            {
                yield return item > Value ? Value : item < -Value ? -Value : item;
            }
        }
    }
}
