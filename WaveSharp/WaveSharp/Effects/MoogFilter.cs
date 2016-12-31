using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSharp.Waves;

namespace WaveSharp.Effects
{
    public class MoogFilter : EffectBase
    {
        public double CutOffFrequency { get; private set; }
        public double Resolution { get; private set; }
        public MoogFilter(double cutOffFrequency, double resolution)
        {

        }
  
        internal override IEnumerable<double> process(WaveBase wave)
        {
            double y1 = 0, y2 = 0, y3 = 0, y4 = 0, oldx = 0, oldy1 = 0, oldy2 = 0, oldy3 = 0;
            double p, k, t1, t2, r, x;

            CutOffFrequency = 2 * CutOffFrequency / wave.SampleRate;
            p = CutOffFrequency * (1.8 - (0.8 * CutOffFrequency));
            k = 2 * Math.Sin(CutOffFrequency * Math.PI * 0.5) - 1;
            t1 = (1 - p) * 1.386249;
            t2 = 12 + t1 * t1;
            r = Resolution * (t2 + 6 * t1) / (t2 - 6 * t1);

            foreach (var item in wave)
            {
                x = item - r * y4;

                // four cascaded one-pole filters (bilinear transform)
                y1 = x * p + oldx * p - k * y1;
                y2 = y1 * p + oldy1 * p - k * y2;
                y3 = y2 * p + oldy2 * p - k * y3;
                y4 = y3 * p + oldy3 * p - k * y4;

                // clipper band limited sigmoid
                y4 -= (y4 * y4 * y4) / 6;

                oldx = x; oldy1 = y1; oldy2 = y2; oldy3 = y3;
                yield return y4;
            }
        }

    }
}
