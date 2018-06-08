
// workaround for "BadImageFormatException" issue - see http://msdn.microsoft.com/en-us/library/kz0ke5xt.aspx

var msiOpenDatabaseModeTransact = 1;
var msiViewModifyInsert = 1
var msiViewModifyUpdate = 2
var msiViewModifyAssign = 3
var msiViewModifyReplace = 4
var msiViewModifyDelete = 6

var filespec = WScript.Arguments(0);
var frameworkpath = WScript.Arguments(1);
var installer = WScript.CreateObject("WindowsInstaller.Installer");
var database = installer.OpenDatabase(filespec, msiOpenDatabaseModeTransact);

WScript.Echo("Updating file '" + filespec + "' to use a 64-bit custom action...");

Update64Bit();

database.Commit();
database = null;
installer = null;

function Update64Bit() {
    var sql;
    var view;
    var record;
    sql = "SELECT * FROM Binary WHERE `Name`='InstallUtil'";
    view = database.OpenView(sql);
    view.Execute();
    record = view.Fetch();
    if (record != null) {
        var dataCol = 2;
        record.SetStream(dataCol, frameworkpath + "\\InstallUtilLib.dll");
        view.Modify(msiViewModifyUpdate, record);
    }
    record = null;
    view.close();
    view = null;
}
