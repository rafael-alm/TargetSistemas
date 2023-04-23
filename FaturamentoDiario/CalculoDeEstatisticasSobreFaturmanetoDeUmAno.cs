namespace FaturamentoDiario
{
    internal sealed class CalculoDeEstatisticasSobreFaturmanetoDeUmAno
    {
        IEnumerable<FaturamentoDoDiaDTO> _faturamentos;

        private CalculoDeEstatisticasSobreFaturmanetoDeUmAno() { }

        internal static CalculoDeEstatisticasSobreFaturmanetoDeUmAno Instanciar()
        {
            return new CalculoDeEstatisticasSobreFaturmanetoDeUmAno();
        }

        internal EstatisticasDoFaturamentoDTO ProcessarCalculos(IEnumerable<FaturamentoDoDiaDTO> faturamentos)
        {
            _faturamentos = faturamentos ?? throw new ArgumentNullException(nameof(faturamentos));

            decimal _mediaAnual = CalcularMediaAnual();
            return CalcularEstatisticas(_mediaAnual);
        }

        internal decimal CalcularMediaAnual()
        {
            decimal _faturamentoAnual = default;
            int diasValidos = default;

            foreach (var faturamento in _faturamentos)
            {
                if (faturamento.Valor > 0)
                {
                    diasValidos++;
                    _faturamentoAnual += faturamento.Valor;
                }
            }

            return diasValidos == 0 ? 0 : _faturamentoAnual / diasValidos;
        }

        internal EstatisticasDoFaturamentoDTO CalcularEstatisticas(decimal mediaAnual)
        {
            int _qtdDeDiasEmQueValorFaturDiarioFoiSuperiorMediaAnual = default;
            FaturamentoDoDiaDTO _menorValorDeFaturamentoOcorrido = default;
            FaturamentoDoDiaDTO _maiorValorDeFaturamentoOcorrido = default;

            foreach (var faturamento in _faturamentos)
            {
                if (faturamento.Valor > 0)
                {
                    if (faturamento.Valor > mediaAnual)
                        _qtdDeDiasEmQueValorFaturDiarioFoiSuperiorMediaAnual++;

                    if (_menorValorDeFaturamentoOcorrido.Valor > faturamento.Valor || _menorValorDeFaturamentoOcorrido.Valor == default)
                        _menorValorDeFaturamentoOcorrido = faturamento;

                    if (_maiorValorDeFaturamentoOcorrido.Valor < faturamento.Valor)
                        _maiorValorDeFaturamentoOcorrido = faturamento;
                }
            }

            return new EstatisticasDoFaturamentoDTO(_menorValorDeFaturamentoOcorrido, _maiorValorDeFaturamentoOcorrido, _qtdDeDiasEmQueValorFaturDiarioFoiSuperiorMediaAnual, mediaAnual);
        }

        // struct --> para desocupar a memoria sem precisar do garbage collection
        internal struct FaturamentoDoDiaDTO
        {
            public FaturamentoDoDiaDTO(int dia, decimal valor)
            {
                Dia = dia;
                Valor = valor;
            }

            public int Dia { get; init; }
            public Decimal Valor { get; init; }
        }
    }
}
