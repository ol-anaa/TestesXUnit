using Xunit;
using LeilaoOnline.Core;

namespace LeilaoOnline.Tests;

public class LanceCtor
{
    [Fact]
    public void LancaArgumentExceptionDadoValorNegativo()
    {
        //Arr

        //Ass
        Assert.Throws<ArgumentException>(
            //Act
            () => new Lance(null, -100)
        );
    }
}
