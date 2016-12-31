using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveSharp.Waves
{
    public class Constant:WaveBase
    {
        public double Value { get; set; }
        public Constant(double value=0)
        {
            this.Value = value;
        }
        public override IEnumerator<double> GetEnumerator()
        {
            while (true)
                yield return Value;
        }

       
    }
}
