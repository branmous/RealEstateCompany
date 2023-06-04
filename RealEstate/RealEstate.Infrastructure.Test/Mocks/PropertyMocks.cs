﻿using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Test.Mocks
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
    }
}
