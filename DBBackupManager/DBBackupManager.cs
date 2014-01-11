using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.IO;
using System.Security.AccessControl;

namespace Ultrasonicsoft.Products.BackupManager
{
    public class DBBackupManager
    {
        private string logFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\\DBBackupStatus.log";

        public void TakeDailyBackupDatabase(string backupFolderName, string backupDate, string databaseName, string dbServerName, bool skipBackupCheck = false)
        {
            try
            {
                if (false == IsDatabaseBackupTaken())
                {
                    string backupFullPath = backupFolderName + System.IO.Path.DirectorySeparatorChar.ToString() +
                                        databaseName + "-" + DateTime.Now.ToString("yyyyMMdd") + ".bak";

                    SetupBackupFolder(backupFolderName);

                    BackupDatabase(backupFullPath, dbServerName, databaseName);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public  void SetupBackupFolder(string backupFolderName)
        {
            System.IO.DirectoryInfo di = null;
            if (Directory.Exists(backupFolderName) == false)
            {
                di = Directory.CreateDirectory(backupFolderName);

            }
            else
            {
                di = new DirectoryInfo(backupFolderName);
            }
            FileSystemAccessRule fsar = new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow);
            DirectorySecurity ds = null;

            ds = di.GetAccessControl();
            ds.AddAccessRule(fsar);
            di.SetAccessControl(ds);
        }

        public void BackupDatabase(string backupFile, string dbServerName, string databaseName)
        {
            try
            {
                ServerConnection con = new ServerConnection(dbServerName);
                Server server = new Server(con);
                Backup source = new Backup();
                source.Action = BackupActionType.Database;
                source.Database = databaseName;
                BackupDeviceItem destination = new BackupDeviceItem(backupFile, DeviceType.File);
                source.Devices.Add(destination);
                source.SqlBackup(server);
                con.Disconnect();

                //Log database backup date to file
                string backupDate = DateTime.Now.ToString("yyyyMMdd");
                File.WriteAllText(logFile, backupDate);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private bool IsDatabaseBackupTaken()
        {
            bool result = false;
            try
            {
                if (File.Exists(logFile) == false)
                {
                    File.WriteAllText(logFile, string.Empty);
                }
                string logStatus = File.ReadAllText(logFile);

                if (string.IsNullOrEmpty(logStatus))
                {
                    return false;
                }
                if (logStatus.ToString().Equals(DateTime.Now.ToString("yyyyMMdd")))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return result;
        }

        internal static void LogException(Exception ex)
        {
            try
            {
                string errorFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\\Errors.log";
                StringBuilder message = new StringBuilder();
                message.AppendFormat(Environment.NewLine);
                message.AppendFormat("==================================================================================================================================");
                message.AppendFormat(Environment.NewLine);
                message.AppendFormat("Date Time: {0}", DateTime.Now.ToString());
                message.AppendFormat(Environment.NewLine);
                message.AppendFormat("Error Message:{0}", ex.Message);
                message.AppendFormat(Environment.NewLine);
                message.AppendFormat("Stack Trace:{0}", ex.StackTrace);

                File.AppendAllText(errorFile, message.ToString());
            }
            catch (Exception exx)
            {
                // MessageBox.Show(exx.Message);
            }
        }

        internal static void LogMessage(string logMessage)
        {
            try
            {
                string errorFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\\Errors.log";
                StringBuilder message = new StringBuilder();
                message.AppendFormat(Environment.NewLine);
                message.AppendFormat("==================================================================================================================================");
                message.AppendFormat(Environment.NewLine);
                message.AppendFormat("Date Time: {0}", DateTime.Now.ToString());
                message.AppendFormat(Environment.NewLine);
                message.AppendFormat("Error Message:{0}", logMessage);

                File.AppendAllText(errorFile, message.ToString());
            }
            catch (Exception exx)
            {
              //  MessageBox.Show(exx.Message);
            }
        }
    }
}
