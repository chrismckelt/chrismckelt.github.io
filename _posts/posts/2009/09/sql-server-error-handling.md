---
title: "SQL Server error handling"
date: "2009-09-11"
categories: 
  - "sql"
---

CREATE PROCEDURE <procName>
/\*^
\* Procedure:     usp\_TryCatchSkeleton
^\*/
AS

BEGIN

   SET NOCOUNT ON

   BEGIN TRY

      --Do work

   END TRY

   BEGIN CATCH
/\* Note that catching and rethrowing an exception is a lossy operation. ERROR\_PROCEDURE() etc will be reset. \*/
      DECLARE @errorMessage   NVARCHAR(4000);
      DECLARE @errorSeverity  INT;
      DECLARE @errorState     INT;

      SELECT  @errorMessage   = ERROR\_MESSAGE(),
              @errorSeverity  = ERROR\_SEVERITY() ,
              @errorState     = ERROR\_STATE();

      --Perform required recovery actions.

      RAISERROR ( @errorMessage, @errorSeverity, @errorState );
      
      RETURN 1;

   END CATCH
   
   RETURN 0;
END

.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }
