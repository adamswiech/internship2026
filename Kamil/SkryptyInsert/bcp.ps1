sqllocaldb start MSSQLLocalDB
$info = sqllocaldb info MSSQLLocalDB

$pipe1 = ($info | Select-String "Instance pipe name").ToString().Split(":")[1].Trim()
$pipe2 = ($info | Select-String "Instance pipe name").ToString().Split(":")[2].Trim()
$pipe = $pipe1 + ":" + $pipe2

Write-Host "Using pipe: $pipe"

$processes = 1..10 | ForEach-Object {
    $i = $_
    Write-Host "Uruchamiam watek $i..."
    Start-Process -FilePath "bcp" `
        -ArgumentList "UsersDb.dbo.users in `"C:\Users\VULCAN\Documents\Main\GIT\internship2026\Kamil\SkryptyC#\Insert1000000\Insert1000000\output.csv`" -S `"$pipe`" -T -c -t `;` -F 2 -h `"FIRE_TRIGGERS`"" `
        -PassThru `
        -NoNewWindow
}

Write-Host "Czekam az wszystkie 10 procesow skonczy..."
$processes | ForEach-Object { $_.WaitForExit() }

Write-Host "USA uratowane! Wszystkie 10 importow zakonczone!"