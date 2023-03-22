using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Elevator.Models.Interfaces;

namespace Elevator.Models
{
    public class Floor : IFloor
    {
        public Floor(int id)
        {
            Id = id;
            PeopleWaiting = new List<IPerson>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public List<IPerson> PeopleWaiting { get; set; }
    }
}
