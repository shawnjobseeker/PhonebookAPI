using PhonebookAPI.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using PhonebookAPI.Controllers;
using Moq;
using System.Threading.Tasks;

namespace PhonebookAPITests
{
    
    public class UnitTest : IClassFixture<UnitFixture>
    {
        UnitFixture fix;
        
        public UnitTest(UnitFixture fixture)
        {
            fix = fixture;
        }
        [Fact]
        public void TestAdd()
        {
            fix.mockRepo.Setup(repo => repo.Add(It.IsAny<Entry>())).Verifiable();
            var controller = new DefaultController(fix.mockRepo.Object);
            var result = controller.Create(new Entry());
            Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("GetPhoneNumber", ((CreatedAtRouteResult)result).RouteName);
            fix.mockRepo.Verify();
            result = controller.Create(null);
            Assert.IsType<BadRequestResult>(result);

        }
        
        
    }
}
