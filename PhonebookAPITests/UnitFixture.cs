using PhonebookAPI.Models;
using PhonebookAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;

namespace PhonebookAPITests
{
    public class UnitFixture
    {
        public Mock<IRepository> mockRepo;
        public UnitFixture()
        {
            mockRepo = new Mock<IRepository>();
    }
        
           
        
    }
}
