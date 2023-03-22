using System.Collections.Generic;

namespace Elevator.Models.Interfaces
{
    public interface IFloor
    {
        int Id { get; set; }
        List<IPerson> PeopleWaiting { get; set; }
    }
}