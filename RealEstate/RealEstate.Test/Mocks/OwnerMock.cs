using RealEstate.Domain.Entities;
using RealEstate.Presentation.DTOs;

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
        public static AuthDTO GetLogin()
        {
            return new AuthDTO
            {
                Email = "mock@mock.com",
                Password = "test"
            }; ;
        }

        public static AuthDTO GetLoginIncorrect()
        {
            return new AuthDTO
            {
                Email = "mock",
                Password = ""
            }; ;
        }

        public static RegisterDTO RegisterWithoutPhoto()
        {
            return new RegisterDTO
            {
                Address = "mock address",
                Birthday = new DateTime(2000, 1, 1),
                Email = "mock@mock.com",
                Name = "mock name",
                Password = "password",
                PasswordConfirm = "password",
            };
        }
    }
}
