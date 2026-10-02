using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
namespace Business
{
    public class Person
    {
        
        public EnGender Gender { get; set; } = EnGender.Male;
        public int PersonId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public static Person FromDto(PersonDto personDto)
        {
            return new Person
            {
                Firstname = personDto.Firstname,
                Lastname = personDto.Lastname,
                Gender = personDto.Gender == 'M' ? EnGender.Male : EnGender.Female
            };
        }

        public static PersonDto ToDto(Person person)
        {
            return new PersonDto
            {
                PersonId = person.PersonId,
                Firstname = person.Firstname,
                Lastname = person.Lastname,
                Gender = person.Gender == EnGender.Male ? 'M' : 'F'
            };
        }

        public static PersonContactDto Test = new PersonContactDto();

        public Person()
        {
            PersonId = -1;
            Test.Email = "";
            Test.PhoneNumber = "12345";
            Test.ContactTypeId = (int)EnContactTypes.Normal;
        }



        public static int AddNewPerson()
        {
            return PersonData.AddNewPerson(ToDto(new Person()), Test);
        }
    }
}
