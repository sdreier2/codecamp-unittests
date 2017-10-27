using Autofac.Extras.Moq;
using ClassLibrary1;
using System;
using Xunit;

namespace UnitTestClassLibrary
{
    public class FactorialCalculatorTests
    {
        #region Test1
        [Fact]
        public void Test1()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                FactorialCalculator factorialCalculator = mock.Create<FactorialCalculator>();
                int result = factorialCalculator.CalculateFactorial(4);
                Assert.Equal(24, result);
            }
        }
        #endregion Test1

        #region Test2
        [Theory]
        [InlineData(-2, null)]
        [InlineData(-1, null)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 6)]
        [InlineData(4, 24)]
        [InlineData(5, 120)]
        public void Test2(int n, int? expectedResult)
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                FactorialCalculator factorialCalculator = mock.Create<FactorialCalculator>();
                if (n < 0)
                {
                    Assert.Throws<ArgumentException>(() => factorialCalculator.CalculateFactorial(n));
                }
                else
                {
                    Assert.Equal(expectedResult, factorialCalculator.CalculateFactorial(n));
                }
            }
        }
        #endregion Test2
    }
}
