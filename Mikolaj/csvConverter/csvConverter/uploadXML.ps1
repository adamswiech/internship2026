$sw = [System.Diagnostics.Stopwatch]::StartNew()

Write-Host "Program started" -ForegroundColor Cyan

[xml]$xml = New-Object xml
$xml.Load(("C:\Users\Vulcan\source\repos\internship2026\Mikolaj\csvConverter\csvConverter\personal_data.xml"))

$table = New-Object System.Data.DataTable
$table.TableName = "PersonalData"

Write-Host "Created local table" -ForegroundColor Blue

[void]$table.Columns.Add("FirstName",       [string])
[void]$table.Columns.Add("LastName",          [string])
[void]$table.Columns.Add("PhoneNumber",    [string])
[void]$table.Columns.Add("EmailAddress",      [string])
[void]$table.Columns.Add("Country",          [string])
[void]$table.Columns.Add("City",   [string])
[void]$table.Columns.Add("PostCode",        [string])
[void]$table.Columns.Add("Gender",         [string])
[void]$table.Columns.Add("Age",       [int])

foreach ($u in $xml.SelectNodes("//Person")) {
    $row = $table.NewRow()

    $row["FirstName"]    = $u.SelectSingleNode("PersonalData/FirstName").InnerText
    $row["LastName"]     = $u.SelectSingleNode("PersonalData/LastName").InnerText
    $row["Gender"]       = $u.SelectSingleNode("PersonalData/Gender").InnerText
    $row["Age"]          = $u.SelectSingleNode("PersonalData/Age").InnerText
    $row["Country"]      = $u.SelectSingleNode("PrivateAddress/Country").InnerText
    $row["City"]         = $u.SelectSingleNode("PrivateAddress/City").InnerText
    $row["PostCode"]     = $u.SelectSingleNode("PrivateAddress/PostCode").InnerText
    $row["PhoneNumber"]  = $u.SelectSingleNode("ContactData/PhoneNumber").InnerText
    $row["EmailAddress"] = $u.SelectSingleNode("ContactData/EmailAddress").InnerText

    [void]$table.Rows.Add($row)
}

Write-Host "$($table.Rows.Count) rows created"

$connectionString = "Server=(localdb)\MSSQLLocalDB;Database=mikolaj_db;Integrated Security=True;TrustServerCertificate=True;"
$connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
$connection.Open()

Write-Host "Connected to database." -ForegroundColor Green

$bulkCopy = New-Object System.Data.SqlClient.SqlBulkCopy($connection)
$bulkCopy.DestinationTableName = "dbo.PersonalData";

$bulkCopy.ColumnMappings.Add("FirstName", "FirstName");
$bulkCopy.ColumnMappings.Add("LastName", "LastName");
$bulkCopy.ColumnMappings.Add("Gender", "Gender");
$bulkCopy.ColumnMappings.Add("Age", "Age");
$bulkCopy.ColumnMappings.Add("Country", "Country");
$bulkCopy.ColumnMappings.Add("City", "City");
$bulkCopy.ColumnMappings.Add("PostCode", "PostCode");
$bulkCopy.ColumnMappings.Add("PhoneNumber", "PhoneNumber");
$bulkCopy.ColumnMappings.Add("EmailAddress", "EmailAddress");

try {
    $bulkCopy.WriteToServer($table)
}

finally {
    $bulkCopy.Close()
    $connection.Close()
}


$sw.Stop()
Write-Host "Execution time: $($sw.Elapsed.TotalSeconds) seconds" -ForegroundColor Yellow