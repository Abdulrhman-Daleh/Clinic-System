using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;

namespace Business
{
    public class PersonContact
    {
        private enum Mode { Add = 1, Update = 2 };
        private Mode _mode;
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
            if (contact == null)
                return ToDto(new PersonContact());

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
            _mode = Mode.Add;
        }

        private bool AddNewContact()
        {
            ContactId = PersonContactData.AddNewContact(ToDto(this));

            return ContactId > 0;
        }

        private bool UpdateContact()
        {
            return PersonContactData.UpdateContact(ToDto(this));
        }


        public bool Save()
        {
            switch (_mode)
            {
                case Mode.Add:
                    if (AddNewContact())
                    {
                        _mode = Mode.Update;
                        return true;
                    }
                    else
                        return false;

                case Mode.Update:
                    return UpdateContact();
            }

            return false;
        }

    }
}
