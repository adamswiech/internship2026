sqllocaldb start MSSQLLocalDB

$info = sqllocaldb info MSSQLLocalDB

$pipe1 = ($info | Select-String "Instance pipe name").ToString().Split(":")[1].Trim()
$pipe2 = ($info | Select-String "Instance pipe name").ToString().Split(":")[2].Trim()
$pipe = $pipe1 + ":" + $pipe2

Write-Host "Using pipe: $pipe"
bcp "mikolaj_db.dbo.PersonalData" in "C:\Users\Vulcan\source\repos\internship2026\Mikolaj\csvConverter\csvConverter\out1.csv" -S $pipe -T -c -t ";" -F 2 -h "FIRE_TRIGGERS"

sqllocaldb stop MSSQLLocalDB 