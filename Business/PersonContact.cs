using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class PersonContact
    {
        private EnMode _mode;
        public (EnContactTypes ContactTypeId, string ContactTypeName) ContactType { get; set; }
        public int ContactId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PersonId { get; set; }
        public static PersonContact FromDto(PersonContactDto contactDto)
        {
            if (contactDto == null)
                return null;

            return new PersonContact()
            {
                Email = contactDto.Email,
                PhoneNumber = contactDto.PhoneNumber,
                PersonId = contactDto.PersonId,
                ContactId = contactDto.ContactId,
                ContactType = (contactDto.ContactType.ContactTypeId, contactDto.ContactType.ContactTypeName),
                _mode = EnMode.Update
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
            _mode = EnMode.Add;
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
        public OperationResult Save()
        {
            OperationResult result = Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner);

            if (!result.IsSuccess)
                return result;

            if (Global.CurrentLoggedInStaff == null)
                return new OperationResult("person is not logged in", "Not logged", false);

            string performedBy = $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            switch (_mode)
            {
                case EnMode.Add:

                    if (AddNewContact())
                    {
                        _mode = EnMode.Update;

                        Auditor.Log(
                            "Contact Added",
                            $"Contact type '{ContactType.ContactTypeName}' for PersonID {PersonId} was added by {performedBy}.",
                            $"ContactID {ContactId}");

                        return new OperationResult(
                            "Contact added successfully.",
                            "Added",
                            true);
                    }

                    return new OperationResult(
                        "Failed to add contact.",
                        "Failed",
                        false);

                case EnMode.Update:

                    if (UpdateContact())
                    {
                        Auditor.Log(
                            "Contact Updated",
                            $"Contact type '{ContactType.ContactTypeName}' for PersonID {PersonId} was updated by {performedBy}.",
                            $"ContactID {ContactId}");

                        return new OperationResult(
                            "Contact updated successfully.",
                            "Updated",
                            true);
                    }

                    return new OperationResult(
                        "Failed to update contact.",
                        "Failed",
                        false);
            }

            return new OperationResult(
                "Invalid contact operation mode.",
                "Failed",
                false);
        }
        public static async Task<BindingList<PersonContact>> GetShortContactInfo(int personId)
        {
            BindingList<PersonContact> contacts = new BindingList<PersonContact>();

            if (!Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner).IsSuccess)
                return contacts;

            foreach (PersonContactDto c in await PersonContactData.GetShortContactInfo(personId))
                contacts.Add(FromDto(c));

            return contacts;
        }
    }
}
