using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveSharp.Waves
{
    public class Noise : WaveBase
    {
        static Random random = new Random();
        public override IEnumerator<double> GetEnumerator()
        {
            while (true)
                yield return random.NextDouble();
        }
    }
}
