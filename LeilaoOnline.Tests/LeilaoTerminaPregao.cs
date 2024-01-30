using Xunit;
using LeilaoOnline.Core;
namespace LeilaoOnline.Console;
public class LeilaoTerminaPregao
{
    [Theory]
    [InlineData(1100, new double[] { 800, 1000, 1100, 200 })]
    [InlineData(1100, new double[] { 200, 800, 1000, 1100 })]
    [InlineData(200,  new double[] { 200 })]
    public void RetornaMaiorValorEClienteVencedorDadoLeilaoComPeloMenosUmLance(double esperado, double[] ofertas)
    {
        //Arrange - Cenario
        IModalidadeAvaliacao modalidade = new MaiorValor();
        var leilao = new Leilao("Van Gogh", modalidade);
        var interessado = new Interessada("Interessado", leilao);
        var ana = new Interessada("ana", leilao);
        leilao.IniciaPregao();

        for (int i = 0; i < ofertas.Length; i++) { 
            double valor = ofertas[i];
            if((i%2) == 0)
            {
                leilao.RecebeLance(interessado, valor);
            }
            else
            {
                leilao.RecebeLance(ana, valor);
            }
        }

        //Act - metodo sobre teste
        leilao.TerminaPregao();

        //Assert
        Assert.Equal(esperado, leilao.Ganhador.Valor);
    }

    [Fact]
    public void RetornaZeroEClienteNullDadoLeilaoSemLances()
    {
        //Arrange - Cenario
        var modalidade = new MaiorValor();
        var leilao = new Leilao("Mulher com sombrinha", modalidade);
        leilao.IniciaPregao();

        //Act - metodo sobre teste
        leilao.TerminaPregao();

        //Assert
        Assert.Equal(0, leilao.Ganhador.Valor);
        Assert.Equal(null, leilao.Ganhador.Cliente);
    }

    [Fact]
    public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
    {
        //Arr
        var modalidade = new MaiorValor();
        var leilao = new Leilao("Mulher com sombrinha", modalidade);

        //Ass
        Assert.Throws<System.InvalidOperationException>(
            //Act - metodo sobre teste
            //Como se fosse um delegate
            () =>  leilao.TerminaPregao()
        );
    }

    [Theory]
    [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250})]
    public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidalidade(
        double valorDestino, 
        double valorEsperado, 
        double[] ofertas)
    {
        //Arr
        IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
        var leilao = new Leilao("Girassóis", modalidade);
        var interessado = new Interessada("interessado", leilao);
        var ana = new Interessada("Ana", leilao);
        leilao.IniciaPregao();

        for (int i = 0; i < ofertas.Length; i++)
        {
            double valor = ofertas[i];
            if (i % 2 == 0)
            {
                leilao.RecebeLance(interessado, valor);
            }
            else
            {
                leilao.RecebeLance(ana, valor);
            }
        }

        //Act
        leilao.TerminaPregao();

        //Ass
        Assert.Equal(1250, leilao.Ganhador.Valor);
    }
}
