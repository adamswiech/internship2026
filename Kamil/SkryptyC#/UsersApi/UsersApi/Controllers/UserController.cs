using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Text.Json;
using UsersApi.Modles;

namespace UsersApi.Models
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UsersDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0";

        [HttpPost("GetUsers")]
        public ActionResult<List<UserDTO>> GetUsers([FromBody] Dictionary<string, JsonElement> body)
        {
            int offset = body.TryGetValue("offset", out var o) && o.TryGetInt32(out int off) ? off : 0;
            string? imieFilter = body.TryGetValue("imieFilter", out var i) ? i.GetString() : null;
            string? nazwiskoFilter = body.TryGetValue("nazwiskoFilter", out var n) ? n.GetString() : null;
            string? krajFilter = body.TryGetValue("krajFilter", out var k) ? k.GetString() : null;

            List<UserDTO> userList = new List<UserDTO>();
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = $"select * from users where  LOWER(Imie) like '{imieFilter?.ToLower()}%' and LOWER(Kraj) like '{krajFilter?.ToLower()}%' and LOWER(Nazwisko) like '{nazwiskoFilter?.ToLower()}%' ";

                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        userList.Add(new UserDTO
                        {
                            Imie = reader["Imie"]?.ToString() ?? "",
                            DrugieImie = reader["DrugieImie"]?.ToString() ?? "",
                            Nazwisko = reader["Nazwisko"]?.ToString() ?? "",
                            IsAdmin = reader["IsAdmin"] != DBNull.Value && Convert.ToBoolean(reader["IsAdmin"]),
                            Kraj = reader["Kraj"]?.ToString() ?? "",
                            Wojewodztwo = reader["Wojewodztwo"]?.ToString() ?? "",
                            Miasto = reader["Miasto"]?.ToString() ?? "",
                            Ulica = reader["Ulica"]?.ToString() ?? "",
                            NrBloku = reader["NrBloku"]?.ToString() ?? "",
                            NrMieszkania = reader["NrMieszkania"]?.ToString() ?? ""
                        });
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return NoContent();
                }
            }
            return userList;

        }

        [HttpPost("GetRowCount")]
        public ActionResult<int> GetRowCount([FromBody] Dictionary<string, JsonElement> body)
        {
            string? imieFilter = body.TryGetValue("imieFilter", out var i) ? i.GetString() : null;
            string? nazwiskoFilter = body.TryGetValue("nazwiskoFilter", out var n) ? n.GetString() : null;
            string? krajFilter = body.TryGetValue("krajFilter", out var k) ? k.GetString() : null;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = $"select count(*) as 'rowCount' from users where LOWER(Imie) like '{imieFilter?.ToLower()}%' and LOWER(Kraj) like '{krajFilter?.ToLower()}%' and LOWER(Nazwisko) like '{nazwiskoFilter?.ToLower()}%';";

                    string rowcount = "";
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                        rowcount = reader["rowCount"]?.ToString() ?? "0";

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return NoContent();
                }
                return Int32.Parse(rowcount);
            }

        }
    }
}
