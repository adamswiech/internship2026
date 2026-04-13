[xml]$xml = New-Object xml
$xmlPath = Join-Path $PSScriptRoot "export.xml"
if (-not (Test-Path $xmlPath)) {
	throw "Nie znaleziono pliku XML: $xmlPath"
}
$xml.Load($xmlPath)
Write-Host "loadd"

$userNodes = $xml.SelectNodes("//User")
Write-Host ("users in xml: " + $userNodes.Count) -ForegroundColor Yellow

$table = New-Object System.Data.DataTable
$Table.TableName = "dane"
Write-Host "created table" -ForegroundColor Blue

[void]$table.Columns.Add("id", [int])
[void]$table.Columns.Add("firstname", [string])
[void]$table.Columns.Add("lastname", [string])
[void]$table.Columns.Add("city", [string])
[void]$table.Columns.Add("email", [string])
Write-Host "added columns" -ForegroundColor Blue

foreach($u in $userNodes)
{
	$row = $table.NewRow()
	$row["id"] = [int]$u.Attributes["id"].Value
	$row["firstname"] = [string]$u.SelectSingleNode("Name/FirstName").InnerText
	$row["lastname"] = [string]$u.SelectSingleNode("Name/LastName").InnerText
	$row["city"] = [string]$u.SelectSingleNode("Location").InnerText
	$row["email"] = [string]$u.SelectSingleNode("ContactInfo/Email").InnerText
	$table.Rows.Add($row)
}
Write-Host ("added rows: " + $table.Rows.Count) -ForegroundColor Blue

$connS = 'Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True'
$conn = New-Object System.Data.SqlClient.SqlConnection($connS)
$conn.Open()

$existsCmd = $conn.CreateCommand()
$existsCmd.CommandText = "SELECT CASE WHEN OBJECT_ID('dbo.dane','U') IS NULL THEN 0 ELSE 1 END"
$tableExists = [int]$existsCmd.ExecuteScalar()
if ($tableExists -ne 1) {
	throw "Tabela docelowa dbo.dane nie istnieje w bazie: $($conn.Database)"
}

$countBeforeCmd = $conn.CreateCommand()
$countBeforeCmd.CommandText = "SELECT COUNT(*) FROM dbo.dane"
$countBefore = [int]$countBeforeCmd.ExecuteScalar()
Write-Host ("rows in dbo.dane before insert: " + $countBefore) -ForegroundColor Yellow

$bulkCopy = New-Object System.Data.SqlClient.SqlBulkCopy($conn)
$bulkCopy.DestinationTableName = "dbo.dane"

$bulkCopy.ColumnMappings.Add("id", "id")
$bulkCopy.ColumnMappings.Add("firstname", "firstname")
$bulkCopy.ColumnMappings.Add("lastname", "lastname")
$bulkCopy.ColumnMappings.Add("city", "city")
$bulkCopy.ColumnMappings.Add("email", "email")

try {
 
    $bulkCopy.WriteToServer($table)

	$countAfterCmd = $conn.CreateCommand()
	$countAfterCmd.CommandText = "SELECT COUNT(*) FROM dbo.dane"
	$countAfter = [int]$countAfterCmd.ExecuteScalar()
	Write-Host ("rows in dbo.dane after insert: " + $countAfter) -ForegroundColor Green
	Write-Host ("inserted rows: " + ($countAfter - $countBefore)) -ForegroundColor Green
 
}
 
finally {
 
    $bulkCopy.Close()
 
    $conn.Close()
 
}