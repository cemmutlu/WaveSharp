using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveSharp.Waves
{
    public static class WaveExtensions
    {
        public static WaveBase Combine(this IEnumerable<WaveBase> waves)
        {
            return new Wave(combine(waves));
        }
        static IEnumerable<double> combine(this IEnumerable<WaveBase> waves)
        {
            var enumerators = waves.Select(x => x.GetEnumerator()).ToArray();
            while (enumerators.All(x => x.MoveNext()))
                yield return enumerators.Sum(x => x.Current); 
        }
    }
}
