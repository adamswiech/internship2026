
sqllocaldb start MSSQLLocalDB

$info = sqllocaldb info MSSQLLocalDB


$pipe = ($info | Select-String "Instance pipe name").ToString().Split(":")[1].Trim()

bcp "InterDB.dbo.people" in "Data.csv" -S $pipe -T -c -t ";"


sqllocaldb stop MSSQLLocalDB