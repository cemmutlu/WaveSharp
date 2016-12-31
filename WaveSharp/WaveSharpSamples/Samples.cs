using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSharp.Effects;
using WaveSharp.Waves;

namespace WaveSharp
{
    class Samples
    {
        public static WaveBase Alarm()
        {
            return new Sinus(3000 + 500 * new Sinus(10));
        }

        public static WaveBase Siren()
        {
            return new Sinus(750 + new Triangular(0.2) * 250 );
        }

        public static WaveBase SimpleChord()
        {
            return Note.C4.GetMinorTriadChord()
                .Select(x => new Melody(0.1, x))//Select 10 seconds long single note melodies
                .Select(x => new Sinus(x) * 0.1)
                .Combine();
        }

        public static WaveBase Yay()
        {
            var bass_osc = new Triangular(500) * 0.24 + new Sinus(16000);
            var mul = (new Sinus(1.1337) + 1) * ((new Sinus(0.42) * 1) + 2);
            return new Sinus(0, bass_osc * mul * 0.5f ^ new Cumulate()) * 0.1;
        }

        public static WaveBase CombinedChords()
        {
            var chords = new Note[][] { Note.C4.GetMinorTriadChord(), Note.A3.GetMinorTriadChord(), Note.D4.GetMinorTriadChord(), Note.B3.GetMinorTriadChord() };
            return Enumerable.Range(0, 3)//Channels
            .Select(channel => new Melody(2f, chords.Select(y => y[channel]).ToArray()))
            .Select(channel => new Sinus(channel) * 0.25).Combine();
        }

        public static WaveBase SimpleMelody()
        {
            double nps = 4;//note per second

            var melody1 = new Melody(nps, new Note[] { }
                          .Concat(new Note[] { Note.D4s, Note.G4, Note.A4s, Note.D5, Note.D5s, Note.G5, Note.D5s, Note.D5 }.Loop(2))
                          .Concat(new Note[] { Note.C4, Note.D4s, Note.G4, Note.A4s, Note.C5, Note.G4, Note.D4s, Note.G4 }.Loop(2))
                          .Concat(new Note[] { Note.G3s, Note.C4, Note.D4s, Note.G4, Note.G4s, Note.G4, Note.G4s, Note.G4 }.Loop(2))
                          .Concat(new Note[] { Note.F3, Note.A4s, Note.D4, Note.F4, Note.A5s, Note.D5, Note.F5, Note.D5 }.Loop(2)).Loop(10));

            var melody2 = new Melody(nps, new Note[] { }
                       .Concat(new Note[] { Note.D3s, Note.D3s, Note.D3s, Note.D3s, Note.D3s, Note.D3s, Note.D3s, Note.D3 }.Loop(2))
                       .Concat(new Note[] { Note.C3, Note.C3, Note.C3, Note.C3, Note.C3, Note.C3, Note.C3, Note.D3 }.Loop(2))
                       .Concat(new Note[] { Note.G2s, Note.G2s, Note.G2s, Note.G2s, Note.G2s, Note.G2s, Note.G2s, Note.G2 }.Loop(2))
                       .Concat(new Note[] { Note.F2, Note.F2, Note.F2, Note.F2, Note.G2s, Note.F2, Note.G2s, Note.D3 }.Loop(2)).Loop(10));

            var channel1 = new Square(melody1) ^ new Reverb(0.01, 0.8);
            var channel2 = new Sinus(melody2) ^ new Reverb(0.05, 0.5);
            return channel1 * 0.1 + channel2 * 0.15;
        }


    }
}

