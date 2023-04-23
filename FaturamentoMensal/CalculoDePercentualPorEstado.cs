namespace FaturamentoMensal
{
    internal sealed class CalculoDePercentualPorEstado
    {
        private CalculoDePercentualPorEstado() { }

        internal static CalculoDePercentualPorEstado Instanciar()
        {
            return new CalculoDePercentualPorEstado();
        }

        internal IEnumerable<PercentualPorEstadoDTO> Calcular(IEnumerable<FaturamentoMensalDTO> faturamentos)
        {
            var listaDePercentual = new List<PercentualPorEstadoDTO>();

            var _totalFaturamentol = faturamentos.Sum(x => x.Valor);
            return faturamentos.Select(x => new PercentualPorEstadoDTO(x.UF, (x.Valor * 100) / _totalFaturamentol));

            foreach (var faturamento in faturamentos)
            {
                listaDePercentual.Add(new PercentualPorEstadoDTO(faturamento.UF, (faturamento.Valor * 100) / _totalFaturamentol));
            }

            return listaDePercentual;
        }

        // struct --> para desocupar a memoria sem precisar do garbage collection
        internal struct FaturamentoMensalDTO
        {
            public FaturamentoMensalDTO(string uF, decimal valor)
            {
                UF = uF;
                Valor = valor;
            }

            public string UF { get; init; }
            public Decimal Valor { get; init; }
        }
    }
}
