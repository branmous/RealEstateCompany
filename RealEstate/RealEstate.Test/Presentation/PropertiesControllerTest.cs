using Moq;
using RealEstate.Application.Accounts;
using RealEstate.Domain.Interfaces.Services;
using RealEstate.Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Test.Presentation
{
    [TestFixture]
    public class PropertiesControllerTest
    {

        private Mock<IPropertyService> _propertyService;
        private Mock<IAccountService> _accountService;
        private PropertiesController _propertiesController;

        [SetUp]
        public void Setup()
        {
            _propertyService = new Mock<IPropertyService>();
            _accountService = new Mock<IAccountService>();
            _propertiesController = new PropertiesController(_propertyService.Object, _accountService.Object);
        }
    }
}
