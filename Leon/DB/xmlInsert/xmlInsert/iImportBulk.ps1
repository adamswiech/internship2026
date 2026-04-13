function Write-BatchToSql {
    param(
        [System.Data.DataTable]$Data,
        [System.Data.SqlClient.SqlConnection]$Connection
    )
    if ($Data.Rows.Count -eq 0) { return }
    $bulkCopy = New-Object System.Data.SqlClient.SqlBulkCopy($Connection)
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
        $bulkCopy.WriteToServer($Data)
        Write-Host "Batch inserted ($($Data.Rows.Count) rows)."
        $Data.Clear()
    }
    catch {
        Write-Host "Insert failed."
        throw
    }
    finally {
        $bulkCopy.Close()
    }
}







[xml]$xml = new-Object xml
$xml.Load("Data.xml")

$connectionString = "Server=(localdb)\MSSQLLocalDB;Database=InterDB;Integrated Security=True"

$table = New-Object System.Data.DataTable
$table.Columns.Add("first_name", [string])   | Out-Null
$table.Columns.Add("middle_name", [string])  | Out-Null
$table.Columns.Add("last_name", [string])    | Out-Null
$table.Columns.Add("age", [int])             | Out-Null
$table.Columns.Add("height_cm", [decimal])   | Out-Null
$table.Columns.Add("weight_kg", [decimal])   | Out-Null
$table.Columns.Add("city", [string])         | Out-Null
$table.Columns.Add("country", [string])      | Out-Null
$table.Columns.Add("favorite_number", [int]) | Out-Null

$connection = New-Object System.Data.SqlClient.SqlConnection $connectionString
$connection.Open()
Write-Host "Connected to database."

foreach ($p in $xml.SelectNodes("//Person")) {
    $row = $table.NewRow()
    $row["first_name"] = [string]$p.SelectSingleNode("first_name").InnerText
    $row["middle_name"] = [string]$p.SelectSingleNode("middle_name").InnerText
    $row["last_name"]  = [string]$p.SelectSingleNode("last_name").InnerText
    $row["age"]        = [int]$p.SelectSingleNode("age").InnerText
    $row["height_cm"]  = [decimal]$p.SelectSingleNode("height_cm").InnerText
    $row["weight_kg"]  = [decimal]$p.SelectSingleNode("weight_kg").InnerText
    $row["city"]       = [string]$p.SelectSingleNode("city").InnerText
    $row["country"]    = [string]$p.SelectSingleNode("country").InnerText
    $row["favorite_number"] = [int]$p.SelectSingleNode("favorite_number").InnerText
    $table.Rows.Add($row)
    if ($table.Rows.Count -ge 10000) {
        Write-BatchToSql -Data $table -Connection $connection
    }
}
Write-BatchToSql -Data $table -Connection $connection
$connection.Close()
Write-Host "Connection closed."