using System.Text.Json.Nodes;
namespace AspireApp1.Server
{
    public class SchemaInterface
    {
        public string Key { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;

        List<SchemaInterface> schemaInterfaces = new List<SchemaInterface>
    {
        new SchemaInterface { Key = "faktura", type = "faktura" },
        new SchemaInterface { Key = "podmiot", type = "podmiot" },
        new SchemaInterface { Key = "faWiersz", type = "faWiersz" }
    };


    }
}
