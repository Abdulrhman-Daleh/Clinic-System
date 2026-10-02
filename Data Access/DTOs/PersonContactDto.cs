namespace Data_Access.DTOs
{
    public class PersonContactDto
    {
        public int ContactId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PersonId { get; set; }
        public int ContactTypeId { get; set; }
    }
}