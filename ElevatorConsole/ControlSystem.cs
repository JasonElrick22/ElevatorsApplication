using Elevator.Models;
using Elevator.Models.Interfaces;
using ElevatorConsole.Fatctory;
using ElevatorConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IElevatorConsole
{

    public class ControlSystem : IControlSystem
    {
        public List<IElevator> Elevators { get; set; }
        public List<IPerson> WaitingRiders { get; set; }

        public ControlSystem(int numberOfElevators)
        {
            Elevators = Enumerable.Range(0, numberOfElevators).Select(eid => Factory.CreateElevator()).ToList();
            WaitingRiders = new List<IPerson>();
        }

        public IElevator GetStatus(Guid elevatorId)
        {
            return Elevators.First(e => e.Id == elevatorId);
        }

        public void Update(Guid elevatorId, int location, int Enroute)
        {
            UpdateElevator(elevatorId, e =>
            {
                e.Location = location;
                e.Enroute = Enroute;
            });
        }

        public void Pickup(int pickupFloor, int destinationFloor)
        {
            WaitingRiders.Add(new Person(pickupFloor, destinationFloor));
        }

        private void UpdateElevator(Guid elevatorId, Action<IElevator> update)
        {
            Elevators = Elevators.Select(e =>
            {
                if (e.Id == elevatorId) update(e);
                return e;
            }).ToList();
        }

        public void Step()
        {
            var busyElevatorIds = new List<Guid>();
            // unload elevators
            Elevators = Elevators.Select(e =>
            {
                var disembarkingRiders = e.RidersOnBoard.Where(r => r.DestinationFloor == e.Location).ToList();
                if (disembarkingRiders.Any())
                {
                    busyElevatorIds.Add(e.Id);
                    e.RidersOnBoard = e.RidersOnBoard.Where(r => r.DestinationFloor != e.Location).ToList();
                }

                return e;
            }).ToList();

            // Embark passengers to available elevators
            WaitingRiders.GroupBy(r => new { r.OriginatingFloor, r.Direction }).ToList().ForEach(waitingFloor =>
            {
                var availableElevator =
                    Elevators.FirstOrDefault(
                        e =>
                            e.Location == waitingFloor.Key.OriginatingFloor &&
                            (e.Direction == waitingFloor.Key.Direction.ToString() || !e.RidersOnBoard.Any()));
                if (availableElevator != null)
                {
                    busyElevatorIds.Add(availableElevator.Id);
                    var embarkingPassengers = waitingFloor.ToList();
                    UpdateElevator(availableElevator.Id, e => e.RidersOnBoard.AddRange(embarkingPassengers));
                    WaitingRiders = WaitingRiders.Where(r => embarkingPassengers.All(er => er.Id != r.Id)).ToList();
                }
            });


            Elevators.ForEach(e =>
            {
                var isBusy = busyElevatorIds.Contains(e.Id);
                int destinationFloor;
                if (e.RidersOnBoard.Any())
                {
                    var closestDestinationFloor =
                        e.RidersOnBoard.OrderBy(r => Math.Abs(r.DestinationFloor - e.Location))
                            .First()
                            .DestinationFloor;
                    destinationFloor = closestDestinationFloor;
                }
                else if (e.Enroute == e.Location && WaitingRiders.Any())
                {
                    destinationFloor = WaitingRiders.GroupBy(r => new { r.OriginatingFloor }).OrderBy(g => g.Count()).First().Key.OriginatingFloor;
                }
                else
                {
                    destinationFloor = e.Enroute;
                }

                var floorNumber = isBusy
                    ? e.Location
                    : e.Location + (destinationFloor > e.Location ? 1 : -1);

                Update(e.Id, floorNumber, destinationFloor);
                Console.WriteLine("Elevator ID:{0} CurrentFloor:{1} Status:{3}, Direction:{2}, Number of Passengers: {4}, Enroute To:{5}", e.Id.ToString(), e.Location, e.Direction, e.StatusOfElevator, e.RidersOnBoard.Count(), e.Enroute);

            });
        }

        public bool AnyOutstandingPickups()
        {
            return WaitingRiders.Any();
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
