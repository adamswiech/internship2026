# W.I.P.


[xml]$xml = Get-Content "Data.xml"

$connectionString = "Server=(localdb)\MSSQLLocalDB;Database=InterDB;Integrated Security=True"

$table = New-Object System.Data.DataTable

$table.Columns.Add("first_name", [string])   | Out-Null
$table.Columns.Add("middle_name", [string])   | Out-Null
$table.Columns.Add("last_name", [string])    | Out-Null
$table.Columns.Add("age", [int])             | Out-Null
$table.Columns.Add("height_cm", [decimal])   | Out-Null
$table.Columns.Add("weight_kg", [decimal])   | Out-Null
$table.Columns.Add("city", [string])         | Out-Null
$table.Columns.Add("country", [string])      | Out-Null
$table.Columns.Add("favorite_number", [int]) | Out-Null


foreach ($p in $xml.People.Person) {

    $row = $table.NewRow()

    $row["first_name"] = [string]$p.Name.first_name
    $row["middle_name"] = [string]$p.Name.middle_name
    $row["last_name"]  = [string]$p.Name.last_name
    $row["age"]        = [int]$p.Stats.age
    $row["height_cm"]  = [decimal]$p.Stats.height_cm
    $row["weight_kg"]  = [decimal]$p.Stats.weight_kg
    $row["city"]       = [string]$p.Location.city
    $row["country"]    = [string]$p.Location.country
    $row["favorite_number"] = [int]$p.Stats.favorite_number

    $table.Rows.Add($row)
}


$connection = New-Object System.Data.SqlClient.SqlConnection $connectionString
$connection.Open()

Write-Host "Connected to database."

$bulkCopy = New-Object System.Data.SqlClient.SqlBulkCopy($connection)

$bulkCopy.DestinationTableName = "dbo.people"


$bulkCopy.ColumnMappings.Add("first_name", "first_name") | Out-Null
$bulkCopy.ColumnMappings.Add("middle_name", "middle_name") | Out-Null
$bulkCopy.ColumnMappings.Add("last_name", "last_name") | Out-Null
$bulkCopy.ColumnMappings.Add("age", "age") | Out-Null
$bulkCopy.ColumnMappings.Add("height_cm", "height_cm") | Out-Null
$bulkCopy.ColumnMappings.Add("weight_kg", "weight_kg") | Out-Null
$bulkCopy.ColumnMappings.Add("city", "city") | Out-Null
$bulkCopy.ColumnMappings.Add("country", "country") | Out-Null
$bulkCopy.ColumnMappings.Add("favorite_number", "favorite_number") | Out-Null

try {
    $bulkCopy.WriteToServer($table)
    Write-Host "insert successfull."
}
catch {
    Write-Host "insert failed."
    throw
}
finally {
    $bulkCopy.Close()
    $connection.Close()
    Write-Host "Connection closed."
}