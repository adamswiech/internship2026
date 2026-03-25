

string path = @"C:\Users\VULCAN\Documents\Main\GIT\internship2026\Kamil\GPS\GPS\GPS\miasta.txt";

string outputPath = @"C:\Users\VULCAN\Documents\Main\GIT\internship2026\Kamil\GPS\GPS\GPS\output.txt"; 

// Read first 100 lines and write them to output file
var first100Lines = File.ReadLines(path).Take(100);

File.WriteAllLines(outputPath, first100Lines);

Console.WriteLine("First 100 lines written successfully.");