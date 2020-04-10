using System;
using System.Collections.Generic;
using System.Text;

namespace NiceLinq.Tests.Models
{
    class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
    }
    class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Car> Cars { get; set; }

    }
    static class FleetFactory
    {
        public static Owner CreateFleet()
        {
            var cars = new List<Car>
            {
                new Car{Id = 1, Model = "T001"},
                new Car{Id = 2, Model = "T001"},
                new Car{Id = 3, Model = "X001"},
                new Car{Id = 4, Model = "T001"},
                new Car{Id = 5, Model = "Z001"},
                new Car{Id = 6, Model = "Z001"},
                new Car{Id = 7, Model = "Z001"},
                new Car{Id = 8, Model = "T001"},
                new Car{Id = 9, Model = "T001"},
                new Car{Id = 10, Model = "X001"},
                new Car{Id = 11, Model = "T001"},
            };
            var owner = new Owner { Id = 1, Cars = cars, Name = "Ali" };
            return owner;
        }
    }
}
