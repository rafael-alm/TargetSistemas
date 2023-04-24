namespace SequenciaFibonacci
{
    internal sealed class CalcularSequenciaFibonacci
    {
        int _sequenciaAnterior;
        int _proximaSequencia;
        int _numeroFinal;
        List<int> _sequenciaDeFibonacci;

        private CalcularSequenciaFibonacci()
        {

        }

        internal static CalcularSequenciaFibonacci Instanciar()
        {
            return new CalcularSequenciaFibonacci();
        }

        internal List<int> Executar(int numeroFinal)
        {
            _numeroFinal = numeroFinal;

            iniciarListaDeSequenciaDeFibonacci();
            calcular();

            return _sequenciaDeFibonacci;
        }

        private void iniciarListaDeSequenciaDeFibonacci()
        {
            _sequenciaDeFibonacci = new List<int>();
            _sequenciaAnterior = 0;
            _proximaSequencia = 1;

            _sequenciaDeFibonacci.Add(_sequenciaAnterior);

            _proximaSequencia = _sequenciaAnterior + _proximaSequencia;
        }

        private void calcular()
        {
            _proximaSequencia = _sequenciaAnterior + _proximaSequencia;

            do
            {
                _sequenciaDeFibonacci.Add(_proximaSequencia);

                var soma = _sequenciaAnterior + _proximaSequencia;

                _sequenciaAnterior = _proximaSequencia;
                _proximaSequencia = soma;
            }
            while (_proximaSequencia <= _numeroFinal);


        }
    }
}
