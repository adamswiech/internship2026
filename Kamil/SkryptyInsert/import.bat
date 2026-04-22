@echo off
setlocal enabledelayedexpansion

:: ================== CONFIGURE THESE ==================
set SERVER=(localdb)\MSSQLLocalDB         :: your server name (e.g. .\SQLEXPRESS or MYPC\SQL2022)
set DATABASE=UsersDb    :: database where the table exists
set XMLFILE=C:\Users\VULCAN\Documents\Main\GIT\internship2026\Kamil\SkryptyInsert\output.xml   :: full path to your XML file
set USER=
set PASS=
:: ====================================================

echo Starting XML import (1,000,000 rows)...

:: Build the SQL query in a temporary file to avoid all escaping problems
(
echo DECLARE @xml XML;
echo.
echo SELECT @xml = BulkColumn 
echo FROM OPENROWSET(BULK '%XMLFILE%', SINGLE_BLOB) AS x;
echo.
echo INSERT INTO dbo.Users (Id, IsAdmin, Imie, DrugieImie, Nazwisko, Kraj, Wojewodztwo, Miasto, Ulica, NrBloku, NrMieszkania)
echo SELECT 
echo     x.value('(Id/text())[1]',           'INT')               AS Id,
echo     x.value('(IsAdmin/text())[1]',      'BIT')               AS IsAdmin,
echo     x.value('(Nazwa/Imie/text())[1]',   'NVARCHAR(100)')    AS Imie,
echo     x.value('(Nazwa/DrugieImie/text())[1]', 'NVARCHAR(100)') AS DrugieImie,
echo     x.value('(Nazwa/Nazwisko/text())[1]', 'NVARCHAR(100)')   AS Nazwisko,
echo     x.value('(Adres/Kraj/text())[1]',   'NVARCHAR(100)')     AS Kraj,
echo     x.value('(Adres/Wojewodztwo/text())[1]', 'NVARCHAR(100)') AS Wojewodztwo,
echo     x.value('(Adres/Miasto/text())[1]', 'NVARCHAR(100)')     AS Miasto,
echo     x.value('(Adres/Mieszkanie/Ulica/text())[1]', 'NVARCHAR(200)') AS Ulica,
echo     x.value('(Adres/Mieszkanie/NrBloku/text())[1]', 'NVARCHAR(20)') AS NrBloku,
echo     x.value('(Adres/Mieszkanie/NrMieszkania/text())[1]', 'NVARCHAR(20)') AS NrMieszkania
echo FROM @xml.nodes('/Users/User') AS u(x);
) > "C:\Users\VULCAN\Documents\Main\GIT\internship2026\Kamil\SkryptyInsert\tmp.sql"

:: Run the query
if "%USER%"=="" (
    sqlcmd -S "%SERVER%" -d "%DATABASE%" -E -i "C:\Users\VULCAN\Documents\Main\GIT\internship2026\Kamil\SkryptyInsert\tmp.sql"
)
echo.
echo Import completed successfully!
echo You can check the number of rows with: SELECT COUNT(*) FROM dbo.Users;

