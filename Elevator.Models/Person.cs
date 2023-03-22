using System;
using System.Collections.Generic;
using System.Text;
using Elevator.Models.Interfaces;

namespace Elevator.Models
{
    public class Person : IPerson
    {
        public Person(int originatingFloor, int destinationFloor)
        {
            OriginatingFloor = originatingFloor;
            DestinationFloor = destinationFloor;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public int OriginatingFloor { get; set; }
        public int DestinationFloor { get; set; }
        public Direction Direction
        {
            get { return OriginatingFloor < DestinationFloor ? Direction.Up : Direction.Down; }
            set { }
        }
    }
    public enum Direction
    {
        Up = 1,
        Down = -1
    }
}
