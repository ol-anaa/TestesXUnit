using System.Linq;

namespace LeilaoOnline.Core;


public class MaiorValor : IModalidadeAvaliacao
{
    public Lance Avalia(Leilao leilao)
    {
        //Maior valor
        return leilao.Lances
              .DefaultIfEmpty(new Lance(null, 0))
              .OrderBy(v => v.Valor)
              .LastOrDefault();
    }
}
