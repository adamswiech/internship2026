USE [InterDB];
GO

CREATE or ALTER TRIGGER aD
ON dbo.people
Instead of INSERT
AS
BEGIN
	SET NOCOUNT ON;
    INSERT INTO dbo.people (
        first_name,
        middle_name,
        last_name,
        age,
        height_cm,
        weight_kg,
        city,
        country,
        favorite_number
    )
    SELECT 
        first_name,
        middle_name,
        last_name,
        age,
        height_cm,
        weight_kg,
        city,
        country,
        favorite_number
    FROM inserted
    WHERE first_name NOT LIKE 'a%';
END;
GO