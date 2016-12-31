using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveSharp.Waves
{
    public class Melody : WaveBase
    {
        public IEnumerable<double> Frequency { get; set; }
        public IEnumerable<Note> Notes { get; set; }
        public Melody(double frequency, params Note[] notes)
        {
            this.Frequency = new Constant(frequency);
            this.Notes = notes;
        }
        public Melody(double frequency, IEnumerable<Note> notes)
        {
            this.Frequency = new Constant(frequency);
            this.Notes = notes;
        }
        public Melody(IEnumerable<double> frequency, params Note[] notes)
        {
            this.Frequency = frequency;
            this.Notes = notes;
        }
        public Melody(IEnumerable<double> frequency, IEnumerable<Note> notes)
        {
            this.Frequency = frequency;
            this.Notes = notes;
        }


        /// <summary>
        /// Returns frequencies of notes with given frequency.
        /// </summary>
        /// <example>
        /// var f = FromNotes(WaveFactory.Constant(2),Note.C4,Note.C5,Note.C7,Note.C6);//Creates a wave that plays 2 note per second.
        /// var wave = WaveFactory.Sinus(f);
        /// </example>
        /// <returns>Frequencies of given notes</returns>
        public override IEnumerator<double> GetEnumerator()
        {
            double value = 0;
            var eFrequency = Frequency.GetEnumerator();
            var eNote = Notes.GetEnumerator();
            double notecursor = 1;
            while (true)
            {
                if (notecursor >= 1)
                {
                    if (!eNote.MoveNext()) break;
                    notecursor--;
                }
                else
                {
                    if (!eFrequency.MoveNext()) break;
                    yield return eNote.Current.Frequency();
                    notecursor += eFrequency.Current / SampleRate;
                }
            }
        }
    }
}
