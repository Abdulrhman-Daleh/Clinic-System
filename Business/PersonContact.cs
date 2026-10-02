using AppEnums.cs;
using Data_Access.DTOs;

namespace Business
{
    public class PersonContact
    {
        public EnContactTypes ContactTypeId { get; set; }
        public int ContactId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PersonId { get; set; }

        public static PersonContact FromDto(PersonContactDto contactDto)
        {
            return new PersonContact()
            {
                Email = null,
                PhoneNumber = null,
                PersonId = -1
            };
        }

        public static PersonContactDto ToDto(PersonContact contact)
        {
            return new PersonContactDto()
            {
                ContactId = contact.ContactId,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                PersonId = contact.PersonId,
                ContactTypeId = (int)contact.ContactTypeId
            };

        }

        public PersonContact()
        {
            ContactId = -1;
            ContactTypeId = EnContactTypes.Normal;
        }
    }
}
