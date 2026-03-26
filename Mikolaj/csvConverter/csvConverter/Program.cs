using csvConverter;

string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=mikolaj_db;Integrated Security=True;TrustServerCertificate=True;";
CSV_converter converter = new CSV_converter(connectionString);

converter.fetchData();
converter.createCSV();
