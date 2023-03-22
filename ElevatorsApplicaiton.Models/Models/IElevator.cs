namespace ElevatorsApplicaiton.Models.Models
{
    public interface IElevator
    {
        string Direction { get; set; }
        bool enroute { get; set; }
        long id { get; set; }
        int peopleOnboard { get; set; }
        string statusOfElevator { get; set; }

        bool Validate_Capacity(Elevator elevator);
        bool Validate_Direction(string direction, Elevator elevator);
    }
}