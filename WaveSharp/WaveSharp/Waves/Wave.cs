using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveSharp.Waves
{
    public class Wave : WaveBase
    {
        public IEnumerable<double> Source { get; set; }
        public Wave(IEnumerable<double> source)
        {
            this.Source = source;
        }
        public override IEnumerator<double> GetEnumerator()
        {
            return Source.GetEnumerator();
        }
    }
}
