using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Business
{
    public class Person
    {
        private enum Mode { Add = 1, Update }
        private Mode _mode;
        public int PersonId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public EnGender Gender { get; set; } = EnGender.Male;
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
        }


        public Person(int personId)
        {
            PersonId = personId;
            _mode = Mode.Update;
        }

        private int _AddNewPerson()
        {
            return PersonData.AddNewPerson(ToDto(this), PersonContact.ToDto(contactInfo));
        }

        private bool _UpdatePerson()
        {
            return PersonData.UpdatePerson(ToDto(this), PersonContact.ToDto(contactInfo));
        }

        public bool Save()
        {
            if (_mode == Mode.Add)
            {
                PersonId = _AddNewPerson();
                if (PersonId > 0)
                {
                    _mode = Mode.Update;
                    return true;
                }
                else
                    return false;
            }

            return _UpdatePerson();
        }

        public static Person FindPersonById(int personId)
        {
            return FromDto(PersonData.FindPersonById(personId));
        }

        public static Person FindPersonByFirstname(string Firstname)
        {
            return FromDto(PersonData.FindPersonByFirstname(Firstname));
        }

        public static async Task<List<Person>> GetPeople()
        {
            List<Person> people = new List<Person>();

            List<PersonDto> peopleDto = await PersonData.GetPeople();
            foreach (PersonDto row in peopleDto)
            {
                people.Add(FromDto(row));
            }

            return people;
        }

        public string GetFullName() => Firstname + " " + Lastname;
    }
}
