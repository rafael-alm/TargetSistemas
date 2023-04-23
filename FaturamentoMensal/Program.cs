
using FaturamentoMensal;
using System.Globalization;
using CalcPercPorEstado = FaturamentoMensal.CalculoDePercentualPorEstado;

var _faturamentos = new List<CalcPercPorEstado.FaturamentoMensalDTO>{
    new CalcPercPorEstado.FaturamentoMensalDTO("SP", 67836.43m),
    new CalcPercPorEstado.FaturamentoMensalDTO("RJ", 36678.66m),
    new CalcPercPorEstado.FaturamentoMensalDTO("MG", 29229.88m),
    new CalcPercPorEstado.FaturamentoMensalDTO("ES", 27165.48m),
    new CalcPercPorEstado.FaturamentoMensalDTO("Outros", 19849.53m),
    };

var _estatisticas = CalcPercPorEstado.Instanciar().Calcular(_faturamentos!);

Console.WriteLine("Resultado");
var brCulture = new CultureInfo("pt-BR");

foreach (var estatistica in _estatisticas)
    Console.WriteLine($"Estado: {estatistica.UF} - {estatistica.Valor.ToString("F2", brCulture)} %");






