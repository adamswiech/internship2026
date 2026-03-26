using insert_db;

string[] firstNames = { "John", "Emma", "Luca", "Sophie", "Carlos", "Yuki", "Ahmed", "Anna", "James", "Maria" };
string[] lastNames = { "Smith", "Johnson", "Rossi", "Müller", "García", "Tanaka", "Hassan", "Kowalski", "Brown", "Silva" };
string[] phoneNumbers = { "+1-555-0101", "+44-7700-900", "+39-02-1234", "+49-30-5678", "+34-91-2345", "+81-3-1234", "+20-2-3456", "+48-22-5678", "+61-2-9876", "+55-11-9876" };
string[] emailAddresses = { "john.smith@email.com", "emma.j@email.com", "luca.rossi@email.com", "sophie.m@email.com", "carlos.g@email.com", "yuki.t@email.com", "ahmed.h@email.com", "anna.k@email.com", "james.b@email.com", "maria.s@email.com" };
string[] countries = { "USA", "UK", "Italy", "Germany", "Spain", "Japan", "Egypt", "Poland", "Australia", "Brazil" };
string[] cities = { "New York", "London", "Rome", "Berlin", "Madrid", "Tokyo", "Cairo", "Warsaw", "Sydney", "São Paulo" };
string[] postCodes = { "10001", "SW1A", "00100", "10115", "28001", "100-0001", "11511", "00-001", "2000", "01310" };
string[] genders = { "Male", "Female", "Male", "Female", "Male", "Female", "Male", "Female", "Male", "Female" };
int[] ages = { 28, 34, 45, 22, 31, 27, 39, 26, 52, 41 };

string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=mikolaj_db;Integrated Security=True;TrustServerCertificate=True;";

DatabaseHelper db = new DatabaseHelper(connectionString);
List<PersonalDataModel> people = new List<PersonalDataModel>();

Random random = new Random();
int iterator = 0;

while (iterator < 1000000)
{
    int index = random.Next(0, 10);

    PersonalDataModel person = new PersonalDataModel
    {
        firstName = firstNames[index],
        lastName = lastNames[index],
        phoneNumber = phoneNumbers[index],
        emailAddress = emailAddresses[index],
        country = countries[index],
        city = cities[index],
        postCode = postCodes[index],
        gender = genders[index],
        age = ages[index]
    };

    people.Add(person);
    iterator++;
}

Console.WriteLine("Inserting 1,000,000 records...");
db.Insert(people);
Console.WriteLine("Done!");