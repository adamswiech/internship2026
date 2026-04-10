






# =============================================
# Load XML and create DataTable with proper mapping
# =============================================

Write-Host "test" -ForegroundColor Blue
[xml]$xml = Get-Content "output.xml"
Write-Host "test" -ForegroundColor Blue


$table = New-Object System.Data.DataTable
$table.TableName = "users"

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


foreach ($u in $xml.Descendants("User")) {   
    $row = $table.NewRow()
    Write-Host "UserId: $($u.Element("Id"))" -ForegroundColor Blue

    $row["IsAdmin"] = [bool]($u.Element("IsAdmin") -eq $true)
    $nazwa = $u.Element("Nazwa")
    $row["Imie"]       = $nazwa?.Element("Imie").Value
    $row["DrugieImie"] = $nazwa?.Element("DrugieImie").Value
    $row["Nazwisko"]   = $nazwa?.Element("Nazwisko").Value
    $adres = $u.Element("Adres")
    $row["Kraj"]       = $adres?.Element("Kraj").Value    
    $row["Wojewodztwo"]= $adres?.Element("Wojewodztwo").Value 
    $row["Miasto"]     = $adres?.Element("Miasto").Value   
    $mieszkanie = $adres?.Element("Mieszkanie")
    $row["Ulica"]        = $mieszkanie?.Element("Ulica").Value   
    $row["NrBloku"]      = $mieszkanie?.Element("NrBloku").Value  
    $row["NrMieszkania"] = $mieszkanie?.Element("NrMieszkania").Value 

    [void]$table.Rows.Add($row)
}

Write-Host "Created $($table.Rows.Count) rows from XML"

$connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UsersDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Application Name=\""SQL Server Management Studio\"";Command Timeout=0";
            
$bulkCopy = New-Object System.Data.SqlClient.SqlBulkCopy($connectionString)
$bulkCopy.DestinationTableName = "dbo.users"
$bulkCopy.BulkCopyTimeout = 0

try {
    $bulkCopy.WriteToServer($table)
    Write-Host "Bulk copy completed successfully! $($table.Rows.Count) rows inserted." -ForegroundColor Green
}
catch {
    Write-Host "Error during bulk copy: $($_.Exception.Message)" -ForegroundColor Red
}
finally {
    $bulkCopy.Close()
}