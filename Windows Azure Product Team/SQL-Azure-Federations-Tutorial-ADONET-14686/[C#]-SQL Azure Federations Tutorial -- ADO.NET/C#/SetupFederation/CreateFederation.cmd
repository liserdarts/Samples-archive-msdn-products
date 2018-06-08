rem -- load configuration data
SET AZUREDB_SERVERNAME=[SQLAzureServerName],1433
SET AZUREDB_COMPUTERNAME=[LogicalServerName]
SET AZURESQL_UID=[Username]
SET AZURESQL_PWD=[Password]
SET AZUREDB_NAME=AdventureWorks2

rem -- create Federation Root
sqlcmd -S %AZUREDB_SERVERNAME% -d master -U %AZURESQL_UID%@%AZUREDB_COMPUTERNAME% -P %AZURESQL_PWD% -i Setup\1-CreateDB.sql

rem -- create a Federation
sqlcmd -S %AZUREDB_SERVERNAME% -d %AZUREDB_NAME% -U %AZURESQL_UID%@%AZUREDB_COMPUTERNAME% -P %AZURESQL_PWD% -i Setup\2-CreateFederation.sql

pause
