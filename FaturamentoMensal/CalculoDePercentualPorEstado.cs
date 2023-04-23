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

            //Optei pelo lambda pq tem poucos registros e não tem condições
            var _totalFaturamentol = faturamentos.Sum(x => x.Valor);
            return faturamentos.Select(x => new PercentualPorEstadoDTO(x.UF, (x.Valor * 100) / _totalFaturamentol));
        }

        // struct --> para desocupar a memoria sem precisar do garbage collection
        internal struct FaturamentoMensalDTO
        {
            public FaturamentoMensalDTO(string uf, decimal valor)
            {
                UF = uf;
                Valor = valor;
            }

            public string UF { get; init; }
            public Decimal Valor { get; init; }
        }
    }
}
