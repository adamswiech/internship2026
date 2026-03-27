using csvConverter;
using System.Diagnostics;

string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=mikolaj_db;Integrated Security=True;TrustServerCertificate=True;";
CSV_converter converter = new CSV_converter(connectionString);
//PersonalDataModel personalDataModel = new PersonalDataModel();
void multithreadingCSV()
{
    List<Thread> threads = new List<Thread>();
    List<string> filesPaths = new List<string>();
    Stopwatch stopwatch = new Stopwatch();

    int threadsCount = 10; //MAIN THREAD + EVERY ADDITIONAL THREAD
    long elementsCount = converter.countElements() / threadsCount;
    stopwatch.Start();

    Console.WriteLine("Timer started!");

    converter.fetchData(0, elementsCount, 1);
    filesPaths.Add($"{Path.GetFullPath($@"..\..\..\out1.csv")}");

    for (int i = 1; i <= (threadsCount - 1); i++)
    {
        int localCounter = i;
        filesPaths.Add($"{Path.GetFullPath($@"..\..\..\out{localCounter}.csv")}");

        CSV_converter converter_t = new CSV_converter(connectionString);
        Thread t = new Thread(() => converter_t.fetchData(elementsCount * localCounter, elementsCount, localCounter + 1));

        threads.Add(t);
        t.Start();
    }

    foreach (var t in threads)
    {
        t.Join();
    }

    void mergeFilesCSV()
    {
        string destFilePath = Path.GetFullPath(@"..\..\..\output.csv");
        File.WriteAllText(destFilePath, "");

        using (TextWriter tw = new StreamWriter(destFilePath, true))
        {
            foreach (string filePath in filesPaths)
            {
                using (TextReader tr = new StreamReader(filePath))
                {
                    tw.WriteLine(tr.ReadToEnd());
                    tr.Close();
                    tr.Dispose();
                }

                Console.WriteLine("File Processed : " + filePath);
            }

            tw.Close();
            tw.Dispose();
        }
    }

    mergeFilesCSV();

    stopwatch.Stop();
    TimeSpan elapsedTime = stopwatch.Elapsed;

    Console.WriteLine($"Elapsed time for {threadsCount} threads: {elapsedTime}");
}

converter.fetchData(0, 0, 0);


