using CPApp.Domains;

namespace CPApp.dtos
{
    public class CreatePersonalInformationDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string Residency { get; set; }
        public string IDNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public Dictionary<Guid, CustomQuestion> Questions { get; set; }
        public Dictionary<Guid, string> Answers { get; set; }
    }
}
