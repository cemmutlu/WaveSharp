using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSharp.Effects;

namespace WaveSharp.Waves
{
    public abstract class WaveBase : IEnumerable<double>
    {
        public int SampleRate { get; set; } = 44000;
        public abstract IEnumerator<double> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static WaveBase operator +(WaveBase wave1, WaveBase wave2)
        {
            return new Wave(wave1.Zip(wave2, (x, y) => x + y));
        }
        public static WaveBase operator -(WaveBase wave1, WaveBase wave2)
        {
            return new Wave(wave1.Zip(wave2, (x, y) => x - y));
        }
        public static WaveBase operator *(WaveBase wave1, WaveBase wave2)
        {
            return new Wave(wave1.Zip(wave2, (x, y) => x * y));
        }
        public static WaveBase operator *(WaveBase wave1, double coef)
        {
            return new Wave(wave1.Select(x => x * coef));
        }
        public static WaveBase operator *(double coef, WaveBase wave1)
        {
            return new Wave(wave1.Select(x => x * coef));
        }
        public static WaveBase operator +(WaveBase wave1, double coef)
        {
            return new Wave(wave1.Select(x => x + coef));
        }
        public static WaveBase operator -(WaveBase wave1, double coef)
        {
            return new Wave(wave1.Select(x => x - coef));
        }

        public static WaveBase operator ^(WaveBase wave, EffectBase effect)
        {
            return effect.Apply(wave);
        }

        public static implicit operator WaveBase(double x)
        {
            return new Constant(x);
        }
        public static implicit operator WaveBase(int x)
        {
            return new Constant(x);
        }

    }
}
