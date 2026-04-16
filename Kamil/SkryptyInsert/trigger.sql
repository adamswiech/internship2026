
CREATE OR ALTER TRIGGER TR_Users_FilterAndClean
ON [dbo].[users]
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[users]
           ([Imie]
           ,[DrugieImie]
           ,[Nazwisko]
           ,[IsAdmin]
           ,[Kraj]
           ,[Wojewodztwo]
           ,[Miasto]
           ,[Ulica]
           ,[NrBloku]
           ,[NrMieszkania])
    SELECT 
        i.Imie, i.DrugieImie, i.Nazwisko, i.IsAdmin, 
        i.Kraj, i.Wojewodztwo, i.Miasto, i.Ulica, 
        i.NrBloku, i.NrMieszkania
    FROM inserted i
    WHERE i.Imie NOT LIKE 'n%'; 
END
go

Enable TRIGGER TR_Users_FilterAndClean ON users;


CREATE INDEX idx_users_KrajImie ON users (Kraj,Imie);



   select count(*) from users

  truncate table users

  select * from users u where u.Nazwisko like 'K%'
  

  drop index idx_users_Imie on users


select * from sys.indexes
where object_id = (select object_id from sys.objects where name = 'users')



ALTER INDEX idx_users_Imie ON dbo.users DISABLE;
ALTER INDEX idx_users_Nazwisko ON dbo.users DISABLE;
ALTER INDEX idx_users_ImieNazwisko ON dbo.users DISABLE;
ALTER INDEX idx_users_KrajImieNazwisko ON dbo.users DISABLE;
ALTER INDEX idx_users_KrajImie ON dbo.users DISABLE;
ALTER INDEX idx_users_NazwiskoImie ON dbo.users DISABLE;

ALTER INDEX idx_users_Kraj ON dbo.users DISABLE;

alter index idx_users_ImieNazwisko on dbo.users REBUILD
alter index idx_users_Imie on dbo.users REBUILD;

alter index all on dbo.users REBUILD;



select * from users u where u.Imie like 'N%'


select * from users where  LOWER(Imie) like '%' and LOWER(Kraj) like '%' and LOWER(Nazwisko) like '%' 
order by (select null) OFFSET 20 rows
FETCH NEXT 20 ROWS ONLY
OPTION (RECOMPILE);