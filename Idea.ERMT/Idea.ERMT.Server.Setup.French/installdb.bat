REM Restore DB Backup
"%ProgramFiles%\Microsoft SQL Server\90\Tools\binn\sqlcmd" -S .\IDEAERMT -i attachDB.sql -v database="IDEA" -v root="%CD%" -v programfiles="%PROGRAMFILES%"
REM Start service
net start "IDEA ERMT Service"
