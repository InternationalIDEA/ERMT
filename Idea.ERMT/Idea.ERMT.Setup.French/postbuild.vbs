Option Explicit
Const msiOpenDatabaseModeTransact = 1

' Connect to Windows Installer object
'On Error Resume Next

Dim installer : Set installer = Nothing
Set installer = Wscript.CreateObject("WindowsInstaller.Installer")

Dim openMode : openMode = msiOpenDatabaseModeTransact
Dim database : Set database = installer.OpenDatabase(WScript.Arguments(0), openMode)
Dim view : Set view = database.OpenView("UPDATE InstallExecuteSequence SET InstallExecuteSequence.Sequence = '1525' where Action= 'RemoveExistingProducts'")
view.Execute

database.Commit

Set view = Nothing
Set database = Nothing