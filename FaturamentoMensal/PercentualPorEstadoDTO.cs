namespace FaturamentoMensal
{
    internal struct PercentualPorEstadoDTO
    {
        public PercentualPorEstadoDTO(string uF, decimal valor)
        {
            UF = uF;
            Valor = valor;
        }

        public string UF { get; init; }
        public decimal Valor { get; init; }
    }
}
