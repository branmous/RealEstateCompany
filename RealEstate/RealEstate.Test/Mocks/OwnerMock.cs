using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Test.Mocks
{
    internal class OwnerMock
    {
        public static Owner GetEntity()
        {
            return new Owner
            {
                Id = "211bd761-d46c-41b7-9c7f-301fb8239b73",
                Name = "Mock Name",
                Email = "mock@mock.com",
                UserName = "mock@mock.com",
                Address = "Florida",
                Birthday = DateTime.Now,
            };
        }
    }
}
