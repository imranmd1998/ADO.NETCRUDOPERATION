create procedure [dbo].[getEmployee]
AS
Begin
SELECT [id]
      ,[Ename]
      ,[Age]
      ,[Salary]
  FROM .[dbo].[Employee]
End

exec [dbo].[getEmployee]
