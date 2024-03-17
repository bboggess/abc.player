using abc.parser.model;

namespace abc.parser.exception;

public class InvalidKeyException : Exception
{
    public InvalidKeyException(KeyTonic tonic, Mode mode)
        : base($"Invalid key signature. Tonic: {tonic}, Mode: {mode}") { }
}
