using System.Linq;

namespace LeilaoOnline.Core;

public class Leilao
{
    private IList<Lance> _lances;
    private Interessada _ultimoClienteLance;
    private IModalidadeAvaliacao _avaliacao;
    public IEnumerable<Lance> Lances => _lances;
    public string Peca { get; }
    public Lance Ganhador { get; private set; }
    public EstadoLeilao Estado { get; set; }


    public double ValorDestino { get; }

    public Leilao(string peca, IModalidadeAvaliacao avaliacao)
    {
        Peca = peca;
        _lances = new List<Lance>();
        Estado = EstadoLeilao.LeilaoAntesDoPregao;
        _avaliacao = avaliacao;
    }

    private bool NovoLanceAceito(Interessada cliente, double valor)
    {
        return (Estado != EstadoLeilao.LeilaoFinalizado) && (cliente != _ultimoClienteLance);
    }

    public void RecebeLance(Interessada cliente, double valor)
    {
        if(NovoLanceAceito(cliente, valor))
        {
            _lances.Add(new Lance(cliente, valor));
            _ultimoClienteLance = cliente;
        }
    }

    public void IniciaPregao()
    {
        Estado = EstadoLeilao.LeilaoEmAndamento;
    }

    public void TerminaPregao()
    {
        if(Estado != EstadoLeilao.LeilaoEmAndamento)
        {
            throw new InvalidOperationException();
        }

        Ganhador = _avaliacao.Avalia(this);
        Estado = EstadoLeilao.LeilaoFinalizado;
    }
}

public enum EstadoLeilao
{
    LeilaoEmAndamento,
    LeilaoFinalizado,
    LeilaoAntesDoPregao
}
