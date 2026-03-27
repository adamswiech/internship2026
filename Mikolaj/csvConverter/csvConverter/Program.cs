using csvConverter;

string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=mikolaj_db;Integrated Security=True;TrustServerCertificate=True;";
CSV_converter converter = new CSV_converter(connectionString);
List<Thread> threads = new List<Thread>();

int threadsCount = 2; //MAIN THREAD + EVERY ADDITIONAL THREAD

long elementsCount = converter.countElements() / threadsCount;
converter.fetchData(0, elementsCount, 1);

for (int i = 1; i <= (threadsCount - 1); i++)
{
    int localCounter = i;
    Thread t = new Thread(() => converter.fetchData(elementsCount * localCounter, elementsCount, localCounter + 1));

    threads.Add(t);
    t.Start();
}

foreach (var t in threads)
    t.Join();