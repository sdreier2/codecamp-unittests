using Autofac.Extras.Moq;
using ClassLibrary1.Delivery;
using Moq;
using System;
using Xunit;

namespace UnitTestClassLibrary.Delivery
{
    public class DeliveryDateServiceTests
    {
        #region Test6
        [Fact]
        public void Test6()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                DeliveryDateService service = mock.Create<DeliveryDateService>();
                string result = service.GetNextAvailableDeliveryDate();
                string expectedResult = DateTime.Now.AddDays(1).ToString("MM/dd/yyyy");
                //string expectedResult = DateTime.Now.AddDays(2).ToString("MM/dd/yyyy");

                Assert.Equal(expectedResult, result);
            }
        }
        #endregion Test6

        #region Test7
        [Fact]
        public void Test7()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                //10AM
                DateTime deliveryDate = DateTime.Today.AddHours(10);
                mock.Mock<IDateTimeHelper>().Setup(x => x.GetCurrentDateTime())
                    .Returns(deliveryDate);

                DeliveryDateService service = mock.Create<DeliveryDateService>();
                string result = service.GetNextAvailableDeliveryDate();
                string expectedResult = deliveryDate.AddDays(1).ToString("MM/dd/yyyy");

                Assert.Equal(expectedResult, result);

                mock.Mock<IDateTimeHelper>().Verify(x => x.GetCurrentDateTime(), Times.Once);
            }
        }
        #endregion Test7

        #region Test8
        [Fact]
        public void Test8()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                //8PM
                DateTime deliveryDate = DateTime.Today.AddHours(20);
                mock.Mock<IDateTimeHelper>().Setup(x => x.GetCurrentDateTime())
                    .Returns(deliveryDate);

                DeliveryDateService service = mock.Create<DeliveryDateService>();
                string result = service.GetNextAvailableDeliveryDate();
                string expectedResult = deliveryDate.AddDays(2).ToString("MM/dd/yyyy");

                Assert.Equal(expectedResult, result);

                mock.Mock<IDateTimeHelper>().Verify(x => x.GetCurrentDateTime(), Times.Once);
            }
        }
        #endregion Test8
    }
}