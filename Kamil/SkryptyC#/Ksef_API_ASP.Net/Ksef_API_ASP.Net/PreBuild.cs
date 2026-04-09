using Newtonsoft.Json.Linq;

namespace Ksef_API_ASP.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pre-build event: Updating launchSettings.json with environment variables...");
            static void UpdatePort()
            {
                string envPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, ".env");
                DotNetEnv.Env.Load(envPath);

                var port = Environment.GetEnvironmentVariable("PORTAPI") ?? "8080";
                var host = Environment.GetEnvironmentVariable("HOSTAPI") ?? "localhost";

                var json = File.ReadAllText(Path.Combine("Properties", "launchSettings.json"));
                var obj = JObject.Parse(json);

                obj["profiles"]["https"]["applicationUrl"] = $"https://{host}:{port}";

                File.WriteAllText(Path.Combine("Properties", "launchSettings.json"), obj.ToString());
            }
            UpdatePort();
        }
    }
}
