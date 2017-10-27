using System;

namespace ClassLibrary1
{
    public interface IFactorialCalculator
    {
        int CalculateFactorial(int n);
        string CreateFactorialMessage(int n, string messagePrefix = "Factorial result is ");
    }

    public class FactorialCalculator : IFactorialCalculator
    {
        public int CalculateFactorial(int n)
        {
            if (n < 0) throw new ArgumentException("n must be >= 0");
            if (n == 0) return 1;
            return n * CalculateFactorial(n - 1);
        }

        public string CreateFactorialMessage(int n, string messagePrefix = "Factorial result is ")
        {
            int result = CalculateFactorial(n);
            return messagePrefix ?? "" + result;
        }
    }
}