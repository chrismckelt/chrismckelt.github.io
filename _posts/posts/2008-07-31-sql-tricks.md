---
layout: post
category: posts
title: "SQL Tricks"
date: "2008-07-31"
tags: sql
---

#### Delete all tables

EXEC sp\_MSforeachtable @command1 = "DROP TABLE ?"

#### Grant execute permissions to all stored procedures to a specific user

    `CREATE PROC grantexecutepermission( @UserName NVARCHAR(250)) AS DECLARE curse CURSOR  FOR SELECT name FROM   sysobjects WHERE  TYPE = 'P'`

    OPEN curse 
    DECLARE  @proc VARCHAR(100) 
    DECLARE  @stmt NVARCHAR(200) 
    FETCH NEXT FROM curse INTO @proc 
    WHILE @@FETCH\_STATUS \= 0 BEGIN SET @stmt \= 'grant execute on ' + @proc + ' to ' + @UserName

    EXEC sp\_executesql @STMT
    PRINT @stmt

    FETCH NEXT FROM curse INTO @proc END
    CLOSE curse
    DEALLOCATE curse

 
#### Check if column exists before adding


    IF NOT EXISTS (select \* from Information\_SCHEMA.columns 
    WHERE Table\_name='ExampleTable' and column\_name='ExampleColumn')

    BEGIN
     ALTER TABLE ExampleTable
     ADD ExampleColumn nvarchar(350)
    END
  

#### Use a cursor to print out column values to a pre-formatted string

    SET NOCOUNT ON
    DECLARE @BTDocumentId int
    DECLARE @BTDocumentVersionNo int
    DECLARE myCursor CURsOR FOR
    SELECT Id as BTDocumentId, VersionNo as BTDocumentVersionNo from document where istemplate = 1

    OPEN myCursor
    FETCH NEXT FROM myCursor INTO @BTDocumentId, @BTdocumentVersionNo
    WHILE @@FETCH\_STATUS = 0
    BEGIN
        PRINT 'BTVersionId=' +  CAST(@BTDocumentId AS VARCHAR(500)) + ' AND BTDocumentVersionNo=' + CAST(@BTDocumentVersionNo AS VARCHAR(500)) + ', '
        FETCH NEXT FROM myCursor INTO @BTDocumentId, @BTdocumentVersionNo
    END
    CLOSE myCursor
    DEALLOCATE myCursor

#### Reseed a table

    DBCC CHECKIDENT ("Risk", RESEED, 920617);
#### Drop column – remove foreign key reference first

    IF EXISTS(SELECT \* FROM INFORMATION\_SCHEMA.TABLE\_CONSTRAINTS WHERE CONSTRAINT\_SCHEMA='dbo' AND CONSTRAINT\_NAME='FK\_Coverage\_CobCode' AND TABLE\_NAME='Coverage')

    BEGIN
      ALTER TABLE dbo.Coverage DROP CONSTRAINT FK\_Coverage\_CobCode;
    END

    IF EXISTS (select \* from Information\_SCHEMA.columns
    WHERE Table\_name='Coverage' and column\_name='CobCodeId')
    BEGIN
        ALTER TABLE Coverage
        DROP COLUMN CobCodeId
    END

#### CSV import

    IF Object _id('tempdb..#csv') IS NOT NULL
    DROP TABLE #csv

     CREATE TABLE #csv
       (
	       positionId         NVARCHAR(150) COLLATE sql_latin1_general_cp1_ci_as,
	       salesforceno        NVARCHAR(150) COLLATE sql_latin1_general_cp1_ci_as,
      )

    DECLARE @FileName NVARCHAR (450)

    SET @FileName ='C:\\Temp\\DATA.tab'

    DECLARE @SqlStatement NVARCHAR(4000)

    SET @SqlStatement = ' BULK INSERT #csv  FROM ''' + @FileName + '''     WITH      (          FIELDTERMINATOR = ''\\t'',          ROWTERMINATOR = ''\\n''      ) '

    EXEC Sp\_executesql    @SqlStatement
    DECLARE @Total INT
    SET @Total = (SELECT Count(positionId) FROM   #csv)
    PRINT 'Total Imported Rows'
    PRINT @Total
    \-- CHANGE to your DB database -----------------
    use YourDB
    DECLARE @Id NVARCHAR(150)
    DECLARE @SfId NVARCHAR(150)
    WHILE (SELECT Count(\*)
    FROM   #csv) > 0
      BEGIN
     SELECT TOP 1 @Id = positionId,
			   @SfId = salesforceno
      FROM   #csv

    UPDATE useraccount
      SET    positionId = @SfId
      WHERE  positionId = @Id
      PRINT 'OLD AdviserNo = '
      PRINT @Id
      PRINT 'NEW SalesForce PositionId ='
      PRINT @SfId
      DELETE #csv
      WHERE  positionId = @Id
    END 
      
#### Delete all objects in the database

```
    DECLARE @Sql NVARCHAR(500) DECLARE @Cursor CURSOR
    SET @Cursor = CURSOR FAST_FORWARD FOR
    SELECT DISTINCT sql = 'ALTER TABLE [' + tc2.TABLE_NAME + '] DROP [' + rc1.CONSTRAINT_NAME + ']'
    FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc1
    LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc2 ON tc2.CONSTRAINT_NAME =rc1.CONSTRAINT_NAME

    OPEN @Cursor FETCH NEXT FROM @Cursor INTO @Sql

    WHILE (@@FETCH_STATUS = 0)
    BEGIN
    Exec SP_EXECUTESQL @Sql
    FETCH NEXT FROM @Cursor INTO @Sql
    END

    CLOSE @Cursor DEALLOCATE @Cursor
    GO

    EXEC sp_MSForEachTable 'DROP TABLE ?'
    GO
```
