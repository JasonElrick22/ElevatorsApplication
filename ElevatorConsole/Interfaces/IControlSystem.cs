using Elevator.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace ElevatorConsole.Interfaces
{
    public interface IControlSystem
    {
        List<IElevator> Elevators { get; set; }
        List<IPerson> WaitingRiders { get; set; }
        bool AnyOutstandingPickups();
        IElevator GetStatus(Guid elevatorId);
        void Pickup(int pickupFloor, int destinationFloor);
        void Step();
        void Update(Guid elevatorId, int floorNumber, int goalFloorNumber);
    }
}