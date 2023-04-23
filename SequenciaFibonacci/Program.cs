//.net 7
using SequenciaFibonacci;

Console.Write("Informe um número inteiro:");

int numeroDeEntreda = Convert.ToInt32(Console.ReadLine());

var sequenciaDefibonacci = CalcularSequenciaFibonacci.Instanciar().Executar(numeroDeEntreda);

Console.WriteLine($"Sequência de Fibonacci: {string.Join(", ", sequenciaDefibonacci)}");

