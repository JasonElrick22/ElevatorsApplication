using Elevator.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace ElevatorConsole.Fatctory
{
    public static class Factory
    {
        public static IElevator CreateElevator()
        {
            return new Elevator.Models.Elevator(Guid.NewGuid());
        }

        public static IFloor CreateFloor(int floorToCreate)
        {
            return new Elevator.Models.Floor(floorToCreate);
        }

        public static IPerson CreatePerson(int sourceFloor, int Enroute)
        {
            return new Elevator.Models.Person(sourceFloor, Enroute);
        }

    }
}
