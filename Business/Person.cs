using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System.ComponentModel;
using System.Threading.Tasks;
namespace Business
{
    public class Person
    {
        private enum Mode { Add = 1, Update }
        private Mode _mode;
        public virtual int PersonId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public EnGender Gender { get; set; } = EnGender.Male;
        public BindingList<PersonContact> Contacts { get; set; }
        public static Person FromDto(PersonDto personDto)
        {
            if (personDto == null) return null;

            return new Person(personDto.PersonId)
            {
                PersonId = personDto.PersonId,
                Firstname = personDto.Firstname,
                Lastname = personDto.Lastname,
                Gender = personDto.Gender == 'M' ? EnGender.Male : EnGender.Female,
                _mode = Mode.Update
            };
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
            _mode = Mode.Add;
            Contacts = new BindingList<PersonContact>();
        }

        private async void _LoadPersonContacts()
        {
            Contacts = await PersonContact.GetShortContactInfo(this.PersonId);
        }

        public Person(int personId)
        {
            PersonId = personId;
            _mode = Mode.Update;
            _LoadPersonContacts();
        }

        private bool _UpdatePerson()
        {
            return PersonData.UpdatePerson(ToDto(this), PersonContact.ToDto(contactInfo));
        }

        public virtual OperationResult Save()
        {
            if (_mode == Mode.Add)
            {
                _mode = Mode.Update;
                return new OperationResult("Person Info send", "PersonSend", true);
            }

            if (_UpdatePerson())
            {
                Auditor.Log("Person Updated", $"Person: {Firstname} {Lastname} (PersonID {PersonId}) was updated by StaffID {Global.CurrentLoggedInStaff.StaffId}.", $"PersonID {PersonId}");
                return new OperationResult("Person updated successfuly", "Updated", true);
            }

            return new OperationResult("failed to save person info", "Failed", false);
        }

        public static Person FindPersonById(int personId)
        {
            return FromDto(PersonData.FindPersonById(personId));
        }

        public static Person FindPersonByFirstname(string Firstname)
        {
            return FromDto(PersonData.FindPersonByFirstname(Firstname));
        }

        public static async Task<BindingList<Person>> GetPeople()
        {
            BindingList<Person> people = new BindingList<Person>();

            BindingList<PersonDto> peopleDto = await PersonData.GetPeople();
            foreach (PersonDto row in peopleDto)
            {
                people.Add(FromDto(row));
            }

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
            if (PersonData.IsPersonRegisteredAsPatient(PersonId))
                return new OperationResult("Person is already registered as patient", "Not Allowed", true);

            return new OperationResult("person isn't a patient", "Allowed", false);
        }

        public OperationResult IsPersonRegisteredAsStaff()
        {
            if (PersonData.IsPersonRegisteredAsStaff(PersonId))
                return new OperationResult("Person is already registered as staff", "Not Allowed", true);

            return new OperationResult("person isn't a staff", "Allowed", false);
        }
    }
}
