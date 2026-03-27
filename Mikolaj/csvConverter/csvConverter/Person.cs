namespace csvConverter
{
    public class Person
    {
        public int Id { get; set; }
        public PersonalInfo PersonalData { get; set; }
        public Address PrivateAddress { get; set; }
        public Contact ContactData { get; set; }
    }

    public class PersonalInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }

    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }

    public class Contact
    {
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
