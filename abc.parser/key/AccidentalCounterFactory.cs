using abc.parser.model;

namespace abc.parser.key;

internal static class AccidentalCounterFactory
{
    /// <summary>
    /// Determines the proper strategy for counting accidentals based on information about
    /// the key signature.
    /// </summary>
    /// <param name="mode">The mode the key is using</param>
    /// <param name="accidental">The accidental used in describing the tonic</param>
    /// <returns>A counter that can be used to count accidentals in the key.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Invalid enum values for either argument</exception>
    public static IAccidentalCounter GetAccidentalCounter(Mode mode, Accidental accidental)
    {
        return mode switch
        {
            Mode.Major
                => accidental switch
                {
                    Accidental.Sharp
                        => new AccidentalCounter(new MajorSharpStepper(), AccidentalType.Sharp),
                    Accidental.Flat
                        => new AccidentalCounter(new MajorFlatStepper(), AccidentalType.Flat),
                    Accidental.Natural
                        => new CompositeAccidentalCounter(
                            new List<IAccidentalCounter>()
                            {
                                new AccidentalCounter(
                                    new MajorSharpStepper(),
                                    AccidentalType.Sharp
                                ),
                                new AccidentalCounter(new MajorFlatStepper(), AccidentalType.Flat),
                            }
                        ),
                    _ => throw new ArgumentOutOfRangeException(nameof(accidental)),
                },
            Mode.Minor
                => accidental switch
                {
                    Accidental.Sharp
                        => new AccidentalCounter(new MinorSharpStepper(), AccidentalType.Sharp),
                    Accidental.Flat
                        => new AccidentalCounter(new MinorFlatStepper(), AccidentalType.Flat),
                    Accidental.Natural
                        => new CompositeAccidentalCounter(
                            new List<IAccidentalCounter>()
                            {
                                new AccidentalCounter(
                                    new MinorSharpStepper(),
                                    AccidentalType.Sharp
                                ),
                                new AccidentalCounter(new MinorFlatStepper(), AccidentalType.Flat),
                            }
                        ),
                    _ => throw new ArgumentOutOfRangeException(nameof(accidental)),
                },
            _ => throw new ArgumentOutOfRangeException(nameof(mode)),
        };
    }
}
