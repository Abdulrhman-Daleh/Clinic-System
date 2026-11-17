using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class Person
    {
        private EnMode _mode;
        public virtual int PersonId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public EnGender Gender { get; set; } = EnGender.Male;
        public BindingList<PersonContact> Contacts { get; set; }
        public static async Task<Person> FromDto(PersonDto personDto)
        {
            if (personDto == null) return null;

            Person person = new Person(personDto.PersonId)
            {
                PersonId = personDto.PersonId,
                Firstname = personDto.Firstname,
                Lastname = personDto.Lastname,
                Gender = personDto.Gender == 'M' ? EnGender.Male : EnGender.Female,
                _mode = EnMode.Update
            };

            person.Contacts = await PersonContact.GetShortContactInfo(person.PersonId);

            return person;
        }
        public static PersonDto ToDto(Person person)
        {
            return new PersonDto
            {
                PersonId = person.PersonId,
                Firstname = person.Firstname,
                Lastname = person.Lastname,
                Gender = person.Gender == EnGender.Male ? 'M' : 'F',
            };
        }

        public PersonContact contactInfo = new PersonContact();
        public Person()
        {
            PersonId = -1;
            _mode = EnMode.Add;
            Contacts = new BindingList<PersonContact>();
        }
        public Person(int personId)
        {
            PersonId = personId;
            _mode = EnMode.Update;
        }
        private bool UpdatePerson()
        {
            return PersonData.UpdatePerson(ToDto(this), PersonContact.ToDto(contactInfo));
        }
        public virtual OperationResult Save()
        {
            if (_mode == EnMode.Add)
            {
                _mode = EnMode.Update;
                return new OperationResult("Person Info send", "PersonSend", true);
            }

            OperationResult result = Util.CanAccessForm(
              EnRole.Receptionist,
              EnRole.Owner);

            if (!result.IsSuccess)
                return result;

            if (Global.CurrentLoggedInStaff.PersonId != PersonId)
            {
                return new OperationResult(
                    "not allowed to update staff personal information.",
                    "Access Denied",
                    false);
            }

            if (UpdatePerson())
            {
                Auditor.Log(
                    "Person Updated",
                    $"Person: {Firstname} {Lastname} (PersonID {PersonId}) was updated by StaffID {Global.CurrentLoggedInStaff.StaffId}.",
                    $"PersonID {PersonId}");

                return new OperationResult(
                    "Person updated successfuly",
                    "Updated",
                    true);
            }

            return new OperationResult(
                "failed to save person info",
                "Failed",
                false);
        }
        public static async Task<Person> FindPersonById(int personId)
        {
            PersonDto personDto = await PersonData.FindPersonById(personId);

            return await FromDto(personDto);
        }
        public static async Task<Person> FindPersonByFirstname(string Firstname)
        {
            PersonDto personDto = await PersonData.FindPersonByFirstname(Firstname);

            return await FromDto(personDto);
        }
        public static async Task<BindingList<Person>> GetPeople()
        {
            BindingList<Person> people = new BindingList<Person>();

            if (!Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner).IsSuccess)
                return people;

            BindingList<PersonDto> peopleDto = await PersonData.GetPeople(Global.CurrentLoggedInStaff.PersonId);

            foreach (PersonDto row in peopleDto)
                people.Add(await FromDto(row));

            return people;
        }
        public string GetFullName() => Firstname + " " + Lastname;
        public char GetGenderChar() => Gender == EnGender.Male ? 'M' : 'F';
        public OperationResult IsPersonValidToSave()
        {
            if (Util.IsRequired(Firstname) && Util.IsRequired(Lastname) && Util.IsValidId((int)Gender))
                return new OperationResult("Form is valid to save", "valid", true);

            return new OperationResult("Form is invalid to save", "Invalid", false);
        }
        public OperationResult IsPersonRegisteredAsPatient()
        {
            OperationResult result = Util.CanAccessForm(
                EnRole.Receptionist,
                EnRole.Owner);

            if (!result.IsSuccess)
                return result;

            if (PersonData.IsPersonRegisteredAsPatient(PersonId))
                return new OperationResult(
                    "Person is already registered as patient",
                    "Not Allowed",
                    true);

            return new OperationResult(
                "person isn't a patient",
                "Allowed",
                false);
        }
        public OperationResult IsPersonRegisteredAsStaff()
        {
            OperationResult result = Util.CanAccessForm(EnRole.Owner);

            if (!result.IsSuccess)
                return result;

            if (PersonData.IsPersonRegisteredAsStaff(PersonId))
                return new OperationResult(
                    "Person is already registered as staff",
                    "Not Allowed",
                    true);

            return new OperationResult(
                "person isn't a staff",
                "Allowed",
                false);
        }
    }
}
