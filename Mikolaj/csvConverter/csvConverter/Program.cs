using csvConverter;

string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=mikolaj_db;Integrated Security=True;TrustServerCertificate=True;";

List<PersonalDataModel> personalData = new List<PersonalDataModel>();
CSV_converter converter = new CSV_converter(connectionString);


long elementsCount = converter.countElements();

Console.WriteLine(elementsCount);

//converter.fetchData();
