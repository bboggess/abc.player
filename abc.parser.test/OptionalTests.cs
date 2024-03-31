using System.Security.Cryptography.X509Certificates;

namespace abc.parser.test;

public class OptionalTests
{
    [Test]
    public void Some_HasData_IsTrue()
    {
        var some = Optional<string>.Some("A value");

        var hasData = some.HasData;

        Assert.That(hasData);
    }

    [Test]
    public void None_HasData_IsFalse()
    {
        var none = Optional<string>.None();

        var hasData = none.HasData;

        Assert.That(hasData, Is.False);
    }

    [Test]
    public void Some_TryGetValue_ReturnsTrue()
    {
        var some = Optional<string>.Some("A value");

        var result = some.TryGetValue(out _);

        Assert.That(result);
    }

    [Test]
    public void Some_TryGetValue_GetsData()
    {
        var data = "A value";
        var some = Optional<string>.Some(data);

        var _ = some.TryGetValue(out string? result);

        Assert.That(result, Is.EqualTo(data));
    }

    [Test]
    public void None_TryGetValue_ReturnsFalse()
    {
        var none = Optional<string>.None();

        var result = none.TryGetValue(out _);

        Assert.That(result, Is.False);
    }

    [Test]
    public void None_TryGetValue_OutputsNull()
    {
        var none = Optional<string>.None();

        var _ = none.TryGetValue(out string? result);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Some_BindSome_GetsSome()
    {
        var data = "A value";
        var original = Optional<string>.Some(data);
        var transformedData = 7;
        Func<string, Optional<int>> transformer = s => Optional<int>.Some(s.Length);

        var newOptional = original.Bind(transformer);
        newOptional.TryGetValue(out int resultData);

        Assert.That(resultData, Is.EqualTo(transformedData));
    }

    [Test]
    public void Some_BindNone_GetsNone()
    {
        var data = "A value";
        var original = Optional<string>.Some(data);
        Func<string, Optional<int>> transformer = s => Optional<int>.None();

        var resultOptional = original.Bind(transformer);

        Assert.That(resultOptional.HasData, Is.False);
    }

    [Test]
    public void None_BindSome_GetsNone()
    {
        var original = Optional<string>.None();
        Func<string, Optional<int>> transformer = s => Optional<int>.Some(100);

        var resultOptional = original.Bind(transformer);

        Assert.That(resultOptional.HasData, Is.False);
    }

    [Test]
    public void None_BindNone_GetsNone()
    {
        var original = Optional<string>.None();
        Func<string, Optional<int>> transformer = s => Optional<int>.None();

        var resultOptional = original.Bind(transformer);

        Assert.That(resultOptional.HasData, Is.False);
    }
}
