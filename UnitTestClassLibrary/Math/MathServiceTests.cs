using Autofac.Extras.Moq;
using ClassLibrary1;
using Moq;
using System;
using Xunit;

namespace UnitTestClassLibrary
{
    public class MathServiceTests
    {
        #region Test3
        [Fact]
        public void Test3()
        {
            int factorialResult = 24;
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<IFactorialCalculator>()
                    .Setup(f => f.CalculateFactorial(It.IsAny<int>()))
                    .Returns((int n) =>
                    {
                        return factorialResult;
                    });

                MathService math = mock.Create<MathService>();

                Assert.Equal(factorialResult, math.IncrementFactorial());
                Assert.Equal(1, math.Count);

                Assert.Equal(factorialResult, math.IncrementFactorial());
                Assert.Equal(2, math.Count);

                mock.Mock<IFactorialCalculator>()
                    .Verify(f => f.CalculateFactorial(It.IsAny<int>()), Times.Exactly(2));

                mock.Mock<IFactorialCalculator>()
                    .Verify(f => f.CalculateFactorial(It.Is<int>(i => i == 0)), Times.Exactly(1));

                mock.Mock<IFactorialCalculator>()
                    .Verify(f => f.CalculateFactorial(It.Is<int>(i => i == 1)), Times.Once);
            }
        }
        #endregion Test3

        #region Test4
        [Fact]
        public void Test4()
        {
            int factorialParam = 4;
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<IFactorialCalculator>()
                    .Setup(f => f.CreateFactorialMessage(It.IsAny<int>(), It.IsAny<string>()))
                    .Returns((int n, string message) =>
                    {
                        return $"Factorial result is {n}";
                    });

                mock.Mock<IFactorialCalculator>()
                    .Setup(f => f.CreateFactorialMessage(-1, It.IsAny<string>()))
                    .Returns((int n, string message) =>
                    {
                        return "This shouldn't happen";
                    });

                MathService math = mock.Create<MathService>();
                string result = math.CreateFactorialMessage(factorialParam);
                Assert.Contains("Factorial result is ", result);

                mock.Mock<IFactorialCalculator>()
                    .Verify(f => f.CreateFactorialMessage(It.Is<int>(i => i == factorialParam), It.IsAny<string>()), Times.Once);

                mock.Mock<IFactorialCalculator>()
                    .Verify(f => f.CreateFactorialMessage(It.Is<int>(i => i < 0), It.IsAny<string>()), Times.Never);
            }
        }
        #endregion Test4

        #region Test5
        [Fact]
        public void Test5()
        {
            int factorialParam = 4;
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<IFactorialCalculator>()
                    .Setup(f => f.CreateFactorialMessage(It.IsAny<int>(), It.IsAny<string>()))
                    .Throws(new ArgumentException());

                MathService math = mock.Create<MathService>();
                string result = math.CreateFactorialMessage(factorialParam);
                Assert.Contains("Unable to calculate factorial", result);

                mock.Mock<IFactorialCalculator>()
                    .Verify(f => f.CreateFactorialMessage(It.Is<int>(i => i == factorialParam), It.IsAny<string>()), Times.Once);
            }
        }
        #endregion Test5
    }
}