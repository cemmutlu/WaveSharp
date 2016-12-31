# WaveSharp

WaveSharp is a experimental .net library for synthesizing waves. 
It is designed to be easy to use and has quite a different usage from usual libraries. 
It uses lazy evaluation to generate waves and apply sound effects.

# Effects

- Clip
- Delay
- Echo
- Low Pass Filter
- High Pass Filter
- Band Pass Filter
- Moog Filter
- Reverb
- Mask

# Usage

Generate 1KHz Sinus Wave
```
//A wave is a infinite valued enumeration. Its values are calculated on realtime.
var sinus = new Sinus(1000);
```

Apply Reverb on a Wave
```
var reverb = wave ^ new Reverb(1,0.8);
```

Generate C3 Tone
```
var sinus = new Sinus(Note.C3.Frequency());
```

Create a melody
```
// melody object is a wave of frequencies. 
var notePerSecond=  4;
var melody = new Melody(notePerSecond, new Note[0]
        .Concat(new Note[] { Note.D4s, Note.G4, Note.A4s, Note.D5, Note.D5s, Note.G5, Note.D5s, Note.D5 }.Loop(2))
        .Concat(new Note[] { Note.C4, Note.D4s, Note.G4, Note.A4s, Note.C5, Note.G4, Note.D4s, Note.G4 }.Loop(2))
        .Concat(new Note[] { Note.G3s, Note.C4, Note.D4s, Note.G4, Note.G4s, Note.G4, Note.G4s, Note.G4 }.Loop(2))
        .Concat(new Note[] { Note.F3, Note.A4s, Note.D4, Note.F4, Note.A5s, Note.D5, Note.F5, Note.D5 }.Loop(2))
        .Loop(10));
        
// It needs to be converted to a speficic form of wave.
var wave = new Sinus(melody);
```
