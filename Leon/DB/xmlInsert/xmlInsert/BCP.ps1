
sqllocaldb start MSSQLLocalDB
$info = sqllocaldb info MSSQLLocalDB


$pipe1 = ($info | Select-String "Instance pipe name").ToString().Split(":")[1].Trim()
$pipe2 = ($info | Select-String "Instance pipe name").ToString().Split(":")[2].Trim()
$pipe = $pipe1 + ":" + $pipe2
write-host "Using pipe: $pipe"

bcp "InterDB.dbo.people" in "Data.csv" -S $pipe -T -c -t ";" #-h "FIRE_TRIGGERS"


sqllocaldb stop MSSQLLocalDB