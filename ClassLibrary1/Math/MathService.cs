using System;

namespace ClassLibrary1
{
    public class MathService
    {
        public int Count { get; private set; } = 0;
        private readonly IFactorialCalculator _factorialCalculator;

        public MathService(IFactorialCalculator factorialCalculator)
        {
            _factorialCalculator = factorialCalculator;
        }

        public int IncrementFactorial()
        {
            return _factorialCalculator.CalculateFactorial(Count++);
        }

        public string CreateFactorialMessage(int n)
        {
            try
            {
                return _factorialCalculator.CreateFactorialMessage(n);
            }
            catch (Exception)
            {
                return $"Unable to calculate factorial for {n}";
            }
        }
    }
}