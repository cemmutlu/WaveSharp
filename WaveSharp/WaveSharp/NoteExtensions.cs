using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveSharp
{
    public static class NoteExtensions
    {
        public static int Octave(this Note note)
        {
            return (int)note / 12;
        }
        public static double Frequency(this Note note)
        {
            return 16.352 * Math.Pow(2, (int)note / (double)12);
        }

        public static Note[] GetAugmentedTriadChord(this Note note)
        {
            return new Note[] { note, note.Sharpen(4), note.Sharpen(8) };
        }
        public static Note[] GetMajorTriadChord(this Note note)
        {
            return new Note[] { note, note.Sharpen(4), note.Sharpen(7) };
        }
        public static Note[] GetMinorTriadChord(this Note note)
        {
            return new Note[] { note, note.Sharpen(3), note.Sharpen(7) };
        }
        public static Note[] GetDiminishedTriadChord(this Note note)
        {
            return new Note[] { note, note.Sharpen(3), note.Sharpen(6) };
        }

        public static Note SetOctave(this Note note, int octave)
        {
            return (Note)((int)note % 12 + 12 * octave);
        }
        public static Note Sharpen(this Note note, int halfstep)
        {
            return (Note)((int)note + halfstep);
        }

        public static IEnumerable<Note> Loop(this IEnumerable<Note> notes, int loop)
        {
            for (int i = 0; i < loop; i++)
                foreach (var item in notes)
                    yield return item;
        }

    }
}
