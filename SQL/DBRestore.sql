--RESTORE DATABASE HODBConnection FROM DISK='c:\sandb\HODBConnection.bak'
--WITH 
 --  MOVE 'HODBConnection' TO 'c:\sandb_Hotel\HODBConnection.mdf',
 --  MOVE 'HODBConnection_log' TO 'c:\sandb_Hotel\HODBConnection_log.ldf'

   --RESTORE FILELISTONLY FROM DISK='c:\sandb\HODBConnection.bak'

   ---
   DECLARE @MDFFilePath NVARCHAR(255) = 'c:\sandb\HODBConnection.mdf';
	DECLARE @LDFFilePath NVARCHAR(255) = 'c:\sandb\HODBConnection_log.ldf';

	CREATE DATABASE HODBConnection_NEW
	ON (FILENAME = 'c:\sandb\HODBConnection.mdf')
	LOG ON (FILENAME = 'c:\sandb\HODBConnection_log.ldf')
	FOR ATTACH;