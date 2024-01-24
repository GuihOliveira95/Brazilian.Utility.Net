using AutoFixture;
using Brazilian.Utility.Net.Domain.Utility.Entities;
using Brazilian.Utility.Net.Domain.Utility.Integration;
using Brazilian.Utility.Net.Domain.Utility.Services;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Brazilian.Utility.Net.Tests.Utility
{
    public class UtilityTest
    {
        [Fact]
        public async Task Utility_GetIpva_ReturnTrue()
        {
            //Arrange
            var fixture = new Fixture();
            var licensePlate = fixture.Create<string>();
            var ipvaApi = new Mock<IIpvaApi>();
            var vehycleUtility = fixture.Create<VehycleUtility>();

            ipvaApi.Setup(method => method.GetIpvaValues(It.IsAny<string>()))
                .ReturnsAsync(vehycleUtility);

            var utilityService = new UtilityService(ipvaApi.Object);


            //Act
            var utilityResponse = await utilityService.GetIPVAAsync(licensePlate);


            //Asserts
            ipvaApi.Verify(ipvaApi => ipvaApi.GetIpvaValues(licensePlate), Times.Once,"Double Called");
            utilityResponse.Data.Should().BeEquivalentTo(vehycleUtility,"founded a different return");
        }
    }
}