[xml]$xml = Get-Content "Data.xml"

$connectionString = "Server=(localdb)\MSSQLLocalDB;Database=InterDB;Integrated Security=True"

$connection = New-Object System.Data.SqlClient.SqlConnection
$connection.ConnectionString = $connectionString

using($connection) {
    $connection.Open()
    # Clear table
    #$cmd = $connection.CreateCommand()
    #$cmd.CommandText = "TRUNCATE TABLE dbo.people"
    #$cmd.ExecuteNonQuery()

    foreach ($p in $xml.People.Person) {

        $sql = @"
    INSERT INTO dbo.people
    (id, first_name, middle_name, last_name, age, height_cm, weight_kg, city, country, favorite_number)
    VALUES
    ($($p.id), '$($p.Name.first_name)', '$($p.Name.middle_name)', '$($p.Name.last_name)',
     $($p.Stats.age), $($p.Stats.height_cm), $($p.Stats.weight_kg),
     '$($p.Location.city)', '$($p.Location.country)', $($p.Stats.favorite_number))
    "@

        $cmd = $connection.CreateCommand()
        $cmd.CommandText = $sql
        $cmd.ExecuteNonQuery()
    }

}

