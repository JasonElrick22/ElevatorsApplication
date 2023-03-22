using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Elevator.Models.Interfaces;

namespace Elevator.Models
{
    public class Elevator : IElevator
    {
        public Elevator(Guid id)
        {
            Id = id;
            RidersOnBoard = new List<IPerson>();
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public int Location { get; set; }

        [Required]
        public int Enroute { get; set; }

        [Required]
        public string StatusOfElevator { get; set; }

        [Required]
        public string Direction { get; set; }

        [Required]
        public List<IPerson> RidersOnBoard { get; set; }

        //public static SetRiders(List<IPerson> _riders)  //JE - Older. REFACTOR or Remove.  example: DevOps task 123
        //{
        //    #region travellingRiders
        //    Random rnd = new Random();

        //    foreach (IPerson _rider in _riders)
        //    {
        //        RidersOnBoard.Add(_rider);

        //        //IPerson _rider = new Person(_rider.OriginatingFloor, _rider.DestinationFloor);
        //        //_rider.Direction = Elevator.Models.Direction.Up;    // TO DO: Random up or down, WaitingRiders

        //    }

        //    #endregion
        //}

    }

}
