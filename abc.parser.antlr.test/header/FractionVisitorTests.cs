﻿using abc.parser.antlr.visitor;
using abc.parser.model;

namespace abc.parser.antlr.test.header;

internal class FractionVisitorTests
{
    [Test]
    public void ParsesFractions(
        [Random(1, int.MaxValue, 5)] int numer,
        [Random(1, int.MaxValue, 5)] int denom
    )
    {
        var stringUnderTest = $"{numer}/{denom}";
        var parser = SetupHelpers.SetUpParser(stringUnderTest);
        var visitor = new FractionVisitor();

        var outcome = visitor.Visit(parser.fraction());

        var expected = new Ratio(numer, denom);
        Assert.That(outcome, Is.EqualTo(expected));
    }
}
