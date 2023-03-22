using System.ComponentModel.DataAnnotations;
using System;
using System.Net.Mail;
using Microsoft.AspNetCore.Builder;

namespace ElevatorsApplicaiton.Models.Models
{
    public class Elevator : IElevator
    {
        [Key]  //ID Column
        public Int64 id { get; set; }

        [Required]
        public string statusOfElevator { get; set; }

        [Required]
        public string Direction { get; set; }

        [Required]
        public int peopleOnboard { get; set; }

        [Required]
        public bool enroute { get; set; }

        //public string CallElevator(int floorRequest, int peopleWaiting)
        //{
        //    {
        //        string fullName = LastName;
        //        if (!string.IsNullOrWhiteSpace(FirstName))
        //        {
        //            if (!string.IsNullOrWhiteSpace(fullName))
        //            {
        //                fullName += ", ";
        //            }
        //            fullName += FirstName;
        //        }
        //        return enroute;
        //    }
        //}

        public bool Validate_Capacity(Elevator elevator)
        {
            var isEnroute = false;
            int maxCapacity = 5;

            if (elevator != null)
            {

                //check capacity available  // JE POSSIBLE CHECK WEIGHT HERE
                if (elevator.peopleOnboard <= maxCapacity)
                {
                    isEnroute = true;
                }
            }

            return isEnroute;
        }

        public bool Validate_Direction(string direction, Elevator elevator)
        {
            var isEnroute = false;
            int maxCapacity = 5;

            if (elevator != null)
            {
                // Check direction
                if (Direction == direction)
                {
                    isEnroute = true;
                }
            }

            return isEnroute;
        }
    }
}
