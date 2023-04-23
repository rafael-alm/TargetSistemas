using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using CalculoDeEstatisticas = CalculoDeEstatisticasSobreFaturmanetoDeUmAno;

Console.Write("Processando...");

string arquivo = @"
[
	{
		""Dia"": 1,
		""Valor"": 22174.1664
	},
	{
		""Dia"": 2,
		""Valor"": 24537.6698
	},
	{
		""Dia"": 3,
		""Valor"": 26139.6134
	},
	{
		""Dia"": 4,
		""Valor"": 0.0
	},
	{
		""Dia"": 5,
		""Valor"": 0.0
	},
	{
		""Dia"": 6,
		""Valor"": 26742.6612
	},
	{
		""Dia"": 7,
		""Valor"": 0.0
	},
	{
		""Dia"": 8,
		""Valor"": 42889.2258
	},
	{
		""Dia"": 9,
		""Valor"": 46251.174
	},
	{
		""Dia"": 10,
		""Valor"": 11191.4722
	},
	{
		""Dia"": 11,
		""Valor"": 0.0
	},
	{
		""Dia"": 12,
		""Valor"": 0.0
	},
	{
		""Dia"": 13,
		""Valor"": 3847.4823
	},
	{
		""Dia"": 14,
		""Valor"": 373.7838
	},
	{
		""Dia"": 15,
		""Valor"": 2659.7563
	},
	{
		""Dia"": 16,
		""Valor"": 48924.2448
	},
	{
		""Dia"": 17,
		""Valor"": 18419.2614
	},
	{
		""Dia"": 18,
		""Valor"": 0.0
	},
	{
		""Dia"": 19,
		""Valor"": 0.0
	},
	{
		""Dia"": 20,
		""Valor"": 35240.1826
	},
	{
		""Dia"": 21,
		""Valor"": 43829.1667
	},
	{
		""Dia"": 22,
		""Valor"": 18235.6852
	},
	{
		""Dia"": 23,
		""Valor"": 4355.0662
	},
	{
		""Dia"": 24,
		""Valor"": 13327.1025
	},
	{
		""Dia"": 25,
		""Valor"": 0.0
	},
	{
		""Dia"": 26,
		""Valor"": 0.0
	},
	{
		""Dia"": 27,
		""Valor"": 25681.8318
	},
	{
		""Dia"": 28,
		""Valor"": 1718.1221
	},
	{
		""Dia"": 29,
		""Valor"": 13220.495
	},
	{
		""Dia"": 30,
		""Valor"": 8414.61
	}
]
";
var _faturamentos = JsonSerializer.Deserialize<List<CalculoDeEstatisticas.FaturamentoDoDiaDTO>>(arquivo);
var _estatisticas = CalculoDeEstatisticas.Instanciar().ProcessarCalculos(_faturamentos!);

Console.WriteLine("Resultado");
Console.WriteLine($"Media Anual: {_estatisticas.MediaAnual};");
Console.WriteLine($"Menor Valor De Faturamento Ocorrido: Dia - {_estatisticas.MenorValorDeFaturamentoOcorrido.Dia} | Valor - {_estatisticas.MenorValorDeFaturamentoOcorrido.Valor};");
Console.WriteLine($"Maior Valor De Faturamento Ocorrido: Dia - {_estatisticas.MaiorValorDeFaturamentoOcorrido.Dia} | Valor - {_estatisticas.MaiorValorDeFaturamentoOcorrido.Valor};");
Console.WriteLine($"Qtd De Dias Em Que o Valor do Faturamento Diario Foi Superior a Media Anual: {_estatisticas.QtdDeDiasEmQueValorFaturDiarioFoiSuperiorMediaAnual}.");

//.net 7
public class CalculoDeEstatisticasSobreFaturmanetoDeUmAno
{
    IEnumerable<FaturamentoDoDiaDTO> _faturamentos;

    private CalculoDeEstatisticasSobreFaturmanetoDeUmAno() { }

    public static CalculoDeEstatisticasSobreFaturmanetoDeUmAno Instanciar()
    {
        return new CalculoDeEstatisticasSobreFaturmanetoDeUmAno();
    }

    public EstatisticasDoFaturamentoDTO ProcessarCalculos(IEnumerable<FaturamentoDoDiaDTO> faturamentos)
    {
        _faturamentos = faturamentos ?? throw new ArgumentNullException(nameof(faturamentos));

        decimal _mediaAnual = CalcularMediaAnual();
        return CalcularEstatisticas(_mediaAnual);
    }

    public decimal CalcularMediaAnual()
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

    public EstatisticasDoFaturamentoDTO CalcularEstatisticas(decimal mediaAnual)
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
    public struct FaturamentoDoDiaDTO
    {
        public FaturamentoDoDiaDTO(int Dia, decimal Valor)
        {
            Dia = Dia;
            Valor = Valor;
        }

        public int Dia { get; init; }
        public Decimal Valor { get; init; }
    }
}

public struct EstatisticasDoFaturamentoDTO
{
    public EstatisticasDoFaturamentoDTO(CalculoDeEstatisticas.FaturamentoDoDiaDTO menorValorDeFaturamentoOcorrido, CalculoDeEstatisticas.FaturamentoDoDiaDTO maiorValorDeFaturamentoOcorrido, int qtdDeDiasEmQueValorFaturDiarioFoiSuperiorMediaAnual, decimal mediaAnual)
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


