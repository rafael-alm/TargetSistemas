using CalculoDeEstatisticas = FaturamentoDiario.CalculoDeEstatisticasSobreFaturmanetoDeUmAno;
internal struct EstatisticasDoFaturamentoDTO
{
    internal EstatisticasDoFaturamentoDTO(CalculoDeEstatisticas.FaturamentoDoDiaDTO menorValorDeFaturamentoOcorrido, CalculoDeEstatisticas.FaturamentoDoDiaDTO maiorValorDeFaturamentoOcorrido, int qtdDeDiasEmQueValorFaturDiarioFoiSuperiorMediaAnual, decimal mediaAnual)
    {
        MenorValorDeFaturamentoOcorrido = menorValorDeFaturamentoOcorrido;
        MaiorValorDeFaturamentoOcorrido = maiorValorDeFaturamentoOcorrido;
        QtdDeDiasEmQueValorFaturDiarioFoiSuperiorMediaAnual = qtdDeDiasEmQueValorFaturDiarioFoiSuperiorMediaAnual;
        MediaAnual = mediaAnual;
    }

    public CalculoDeEstatisticas.FaturamentoDoDiaDTO MenorValorDeFaturamentoOcorrido { get; init; }
    public CalculoDeEstatisticas.FaturamentoDoDiaDTO MaiorValorDeFaturamentoOcorrido { get; init; }
    public int QtdDeDiasEmQueValorFaturDiarioFoiSuperiorMediaAnual { get; init; }
    public decimal MediaAnual { get; init; }
}