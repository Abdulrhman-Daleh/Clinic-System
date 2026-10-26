using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class PersonContact
    {
        private enum Mode { Add = 1, Update = 2 };
        private Mode _mode;
        public (EnContactTypes ContactTypeId, string ContactTypeName) ContactType { get; set; }
        public int ContactId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PersonId { get; set; }

        public static PersonContact FromDto(PersonContactDto contactDto)
        {
            return new PersonContact()
            {
                Email = contactDto.Email,
                PhoneNumber = contactDto.PhoneNumber,
                PersonId = contactDto.PersonId,
                ContactId = contactDto.ContactId,
                ContactType = (contactDto.ContactType.ContactTypeId, contactDto.ContactType.ContactTypeName)
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
                ContactType = (contact.ContactType.ContactTypeId, contact.ContactType.ContactTypeName)
            };

        }

        public PersonContact()
        {
            ContactId = -1;
            ContactType = ((EnContactTypes)0, "None");
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
                        Auditor.Log("Contact Added", $"Contact type '{ContactType.ContactTypeName}' for PersonID {PersonId} was added by StaffID {Global.CurrentLoggedInStaff.StaffId}.", $"ContactID {ContactId}");
                        return true;
                    }
                    else
                        return false;

                case Mode.Update:
                    if (UpdateContact())
                    {
                        Auditor.Log("Contact Updated", $"Contact type '{ContactType.ContactTypeName}' for PersonID {PersonId} was updated by StaffID {Global.CurrentLoggedInStaff.StaffId}.", $"ContactID {ContactId}");
                        return true;
                    }
                    return false;
            }

            return false;
        }

        public static async Task<BindingList<PersonContact>> GetShortContactInfo(int personId)
        {
            BindingList<PersonContact> contacts = new BindingList<PersonContact>();

            foreach (PersonContactDto c in await PersonContactData.GetShortContactInfo(personId))
                contacts.Add(FromDto(c));

            return contacts;
        }

    }
}
