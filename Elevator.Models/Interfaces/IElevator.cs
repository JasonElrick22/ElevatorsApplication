using System;
using System.Collections.Generic;

namespace Elevator.Models.Interfaces
{
    public interface IElevator
    {
        string Direction { get; set; }
        int Enroute { get; set; }
        Guid Id { get; set; }
        int Location { get; set; }
        List<IPerson> RidersOnBoard { get; set; }
        string StatusOfElevator { get; set; }
    }
}