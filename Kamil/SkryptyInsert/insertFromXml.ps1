[xml]$xml = New-Object xml
$xml.Load(("C:\Users\VULCAN\Documents\Main\GIT\internship2026\Kamil\SkryptyInsert\output.xml"))
Write-Host "123123"

# =============================================
# Load XML and create DataTable with proper mapping
# =============================================



$table = New-Object System.Data.DataTable
$table.TableName = "users"
Write-Host "test" -ForegroundColor Blue

[void]$table.Columns.Add("IsAdmin",       [bool])
[void]$table.Columns.Add("Imie",          [string])
[void]$table.Columns.Add("DrugieImie",    [string])
[void]$table.Columns.Add("Nazwisko",      [string])
[void]$table.Columns.Add("Kraj",          [string])
[void]$table.Columns.Add("Wojewodztwo",   [string])
[void]$table.Columns.Add("Miasto",        [string])
[void]$table.Columns.Add("Ulica",         [string])
[void]$table.Columns.Add("NrBloku",       [string])
[void]$table.Columns.Add("NrMieszkania",  [string])

Write-Host "test" -ForegroundColor Blue

foreach ($u in $xml.SelectNodes("//User")){
    $row = $table.NewRow()
    # Write-Host "UserId: $($u.SelectSingleNode("Nazwa").InnerText)" -ForegroundColor Blue

    $row["IsAdmin"] = [bool]($u.SelectSingleNode("IsAdmin").InnerText -eq $true)
    $nazwa = $u.SelectSingleNode("Nazwa")
    $row["Imie"]       = $nazwa.SelectSingleNode("Imie").InnerText
    $row["DrugieImie"] = $nazwa.SelectSingleNode("DrugieImie").InnerText
    $row["Nazwisko"]   = $nazwa.SelectSingleNode("Nazwisko").InnerText
    $adres = $u.SelectSingleNode("Adres")
    $row["Kraj"]       = $adres.SelectSingleNode("Kraj").InnerText
    $row["Wojewodztwo"]= $adres.SelectSingleNode("Wojewodztwo").InnerText 
    $row["Miasto"]     = $adres.SelectSingleNode("Miasto").InnerText
    $mieszkanie = $adres.SelectSingleNode("Mieszkanie")
    $row["Ulica"]        = $mieszkanie.SelectSingleNode("Ulica").InnerText  
    $row["NrBloku"]      = $mieszkanie.SelectSingleNode("NrBloku").InnerText
    $row["NrMieszkania"] = $mieszkanie.SelectSingleNode("NrMieszkania").InnerText

    [void]$table.Rows.Add($row)
    
}

Write-Host "Created $($table.Rows.Count) rows from XML"

$connectionString = 'Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UsersDb;Integrated Security=True'

$connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
$connection.Open()

$bulkCopy = New-Object System.Data.SqlClient.SqlBulkCopy($connection)

$bulkCopy.DestinationTableName = "dbo.users"

$bulkCopy.ColumnMappings.Add("IsAdmin", "IsAdmin")
$bulkCopy.ColumnMappings.Add("Imie", "Imie")
$bulkCopy.ColumnMappings.Add("DrugieImie", "DrugieImie")
$bulkCopy.ColumnMappings.Add("Nazwisko", "Nazwisko")
$bulkCopy.ColumnMappings.Add("Kraj", "Kraj")
$bulkCopy.ColumnMappings.Add("Wojewodztwo", "Wojewodztwo")
$bulkCopy.ColumnMappings.Add("Miasto", "Miasto")
$bulkCopy.ColumnMappings.Add("Ulica", "Ulica")
$bulkCopy.ColumnMappings.Add("NrBloku", "NrBloku")
$bulkCopy.ColumnMappings.Add("NrMieszkania", "NrMieszkania")
$bulkCopy.DestinationTableName = "dbo.users"

try {
    $bulkCopy.WriteToServer($table)
}
finally {
    $bulkCopy.Close()
    $connection.Close()
}

