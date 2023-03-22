using System;

namespace Elevator.Models.Interfaces
{
    public interface IPerson
    {
        int DestinationFloor { get; set; }
        Direction Direction { get; set; }
        Guid Id { get; set; }
        int OriginatingFloor { get; set; }
    }
}