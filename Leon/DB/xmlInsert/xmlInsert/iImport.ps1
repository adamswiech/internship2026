# slow, one by one with transaction

[xml]$xml = Get-Content "DataXXS.xml"

$connectionString = "Server=(localdb)\MSSQLLocalDB;Database=InterDB;Integrated Security=True"

$connection = New-Object System.Data.SqlClient.SqlConnection $connectionString
$connection.Open()

Write-Host "Connected to database."

$transaction = $connection.BeginTransaction()

try {
    foreach ($p in $xml.People.Person) {

        $cmd = $connection.CreateCommand()
        $cmd.Transaction = $transaction

        $cmd.CommandText = @"
INSERT INTO dbo.people
(first_name, middle_name, last_name, age, height_cm, weight_kg, city, country, favorite_number)
VALUES
(@first_name, @middle_name, @last_name, @age, @height_cm, @weight_kg, @city, @country, @favorite_number)
"@

        $cmd.Parameters.AddWithValue("@first_name", [string]$p.Name.first_name) | Out-Null
        $cmd.Parameters.AddWithValue("@middle_name", [string]$p.Name.middle_name) | Out-Null
        $cmd.Parameters.AddWithValue("@last_name", [string]$p.Name.last_name) | Out-Null
        $cmd.Parameters.AddWithValue("@age", [int]$p.Stats.age) | Out-Null
        $cmd.Parameters.AddWithValue("@height_cm", [decimal]$p.Stats.height_cm) | Out-Null
        $cmd.Parameters.AddWithValue("@weight_kg", [decimal]$p.Stats.weight_kg) | Out-Null
        $cmd.Parameters.AddWithValue("@city", [string]$p.Location.city) | Out-Null
        $cmd.Parameters.AddWithValue("@country", [string]$p.Location.country) | Out-Null
        $cmd.Parameters.AddWithValue("@favorite_number", [int]$p.Stats.favorite_number) | Out-Null

        $cmd.ExecuteNonQuery()
    }

    $transaction.Commit()
    Write-Host "inserted successfully."
}
catch {
    $transaction.Rollback()
    Write-Host "Error"
    throw
}
finally {
    $connection.Close()
    Write-Host "Connection closed."
}