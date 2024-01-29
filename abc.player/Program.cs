// See https://aka.ms/new-console-template for more information
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.MusicTheory;

var output = OutputDevice.GetAll().First();
var pattern = new PatternBuilder()
    .SetNoteLength(MusicalTimeSpan.Quarter)
    .Note(Melanchall.DryWetMidi.MusicTheory.Note.Get(NoteName.C, 4))
    .Repeat(4)
    .Build();

var playback = pattern.GetPlayback(TempoMap.Default, new Melanchall.DryWetMidi.Common.FourBitNumber(0), output);
playback.Start();

SpinWait.SpinUntil(() => !playback.IsRunning);
output.Dispose();
playback.Dispose();