using Microsoft.EntityFrameworkCore.Query.Internal;
using RealEstate.Domain.Entities;
using RealEstate.Presentation.DTOs;

namespace RealEstate.Test.Mocks
{
    internal class PropertyMocks
    {
        public static List<Property> GetList()
        {
            var properties = new List<Property>
            {
                new Property {
                    //Id = 1,
                    Name = "My House",
                    Address = "Florida",
                    CodeInternal= "12345",
                    Price = 2000,
                    OwnerId = "211bd761-d46c-41b7-9c7f-301fb8239b73"
                },
                new Property {
                    //Id = 2,
                    Name = "My house 2",
                    Address = "Florida",
                    CodeInternal= "12345",
                    Price = 2000,
                    OwnerId = "211bd761-d46c-41b7-9c7f-301fb8239b73"
                }
            };
            return properties;
        }

        public static Property GetEntity()
        {
            return new Property
            {
                Id = 1,
                Name = "My House",
                Address = "Florida",
                CodeInternal = "12345",
                Price = 2000,
                OwnerId = "211bd761-d46c-41b7-9c7f-301fb8239b73"
            };
        }
        public static PropertyDTO GetDTO()
        {
            return new PropertyDTO
            {
                Name = "My House",
                Address = "Florida",
                CodeInternal = "12345",
                Price = 2000,
            };
        }

        public static PropertyPriceDTO GetPropertyPriceDTO()
        {
            return new PropertyPriceDTO
            {
                Price = 400
            };
        }

    }
}
