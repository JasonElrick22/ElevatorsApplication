using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Elevator.Models.Interfaces;
using ElevatorConsole.Interfaces;
using IElevatorConsole;

namespace ElevatorConsole
{
    internal class Program
    { 
        public static void Main(string[] args)
        {

            var pickupCount = 0;
            var stepCount = 0;
            var random = new Random();

            // Create building
            int iFloors = int.Parse(ConfigurationManager.AppSettings["floorsInBuilding"]);
            int iElevators = int.Parse(ConfigurationManager.AppSettings["elevatorsInBuilding"]);
            int numberOfRequests = int.Parse(ConfigurationManager.AppSettings["MoveRequests"]);

            IControlSystem system = new ControlSystem(iElevators); 

            while (pickupCount < numberOfRequests)
            {
                var originatingFloor = random.Next(1, iFloors + 1);
                var destinationFloor = random.Next(1, iFloors + 1);
                if (originatingFloor != destinationFloor)
                {
                    system.Pickup(originatingFloor, destinationFloor);
                    pickupCount++;
                }
            }

            while (system.AnyOutstandingPickups())
            {
                system.Step();  
                stepCount++;
                Console.WriteLine("Movement : {0}", stepCount.ToString());

                //Status report at Elevator level.  
                //foreach (IElevator elevator in system.Elevators)
                //{
                //    Console.WriteLine("Elevator ID: {0} CurrentFloor: {1} Status: {3}, Direction:{2}, Number of Passengers: {4}, CurrentAction:{5}", elevator.Id.ToString(), elevator.Location, elevator.Direction, elevator.StatusOfElevator, elevator.RidersOnBoard.Count(), elevator.Enroute);
                //}
                Console.WriteLine();
            }

            #region Report on Building -- JE Redundant
            //Ask each floor to report, with waiting riders

            //foreach (IFloor floor in system.Floors) 
            //{
            //    Console.WriteLine("Floor : {0}, Waiting Passengers: {1} -- ", floor.Id, floor.PeopleWaiting.Count());
            //    foreach(IPerson person in floor.PeopleWaiting)
            //    {
            //        Console.WriteLine("Person ID:{0}", person.Id);  // JE Add destination
            //    }
            //}
            #endregion

            Console.WriteLine("People moved {0} ", pickupCount);
            Console.WriteLine("Movements {0} ", stepCount);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    public enum Direction
    {
        Up = 1,
        Down = -1
    }

    public enum Status
    {
        Idle,
        Full,
        Moving
    }

}
