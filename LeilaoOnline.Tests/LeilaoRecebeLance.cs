using Xunit;
using LeilaoOnline.Core;
namespace LeilaoOnline.Console;

public class LeilaoRecebeLance
{
    [Fact]
    public void NaoPermiteNovosLancesAposFimDoLeilao()
    {
        //Arrange - Cenario
        var modalidade = new MaiorValor();
        var leilao = new Leilao("Mulher com Sombrinha", modalidade);
        var interessado = new Interessada("interesado", leilao);
        var ana = new Interessada("ana", leilao);
        var joaquim = new Interessada("joaquim", leilao);
        leilao.IniciaPregao();
        leilao.RecebeLance(interessado, 1000);
        leilao.RecebeLance(ana, 10);
        leilao.TerminaPregao();

        //Act - metodo sobre teste
        leilao.RecebeLance(joaquim, 10000);

        //Assert
        Assert.Equal(2, leilao.Lances.Count());
    }

    [Fact]
    public void NãoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
    {
        //Arr
        var modalidade = new MaiorValor();
        var leilao = new Leilao("Mona Lisa", modalidade);
        var interessado = new Interessada("interessado", leilao);
        leilao.IniciaPregao();
        leilao.RecebeLance(interessado, 200);

        //Act
        leilao.RecebeLance(interessado, 300);

        //Ass
        Assert.Equal(1, leilao.Lances.Count());
    }
}
