using System.Linq;

namespace LeilaoOnline.Core;

public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
{
    private double ValorDestino { get; }

    public OfertaSuperiorMaisProxima(double valorDestino)
    {
        ValorDestino = valorDestino;
    }

    public Lance Avalia(Leilao leilao)
    {
        //Oferta superior mais próxima
        return leilao.Lances
            .DefaultIfEmpty(new Lance(null, 0))
            .Where(l => l.Valor > ValorDestino)
            .OrderBy(l => l.Valor)
            .FirstOrDefault();
    }
}
