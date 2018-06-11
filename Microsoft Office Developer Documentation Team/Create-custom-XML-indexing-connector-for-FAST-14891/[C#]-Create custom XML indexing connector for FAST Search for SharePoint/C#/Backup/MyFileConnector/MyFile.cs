using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Security.AccessControl;

namespace MyFileConnector
{
    /// <summary>
    /// Defines the entity to be crawled
    /// </summary>
    public class MyFile
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime LastModified { get; set; }
        public Byte[] SecurityDesciptor { get; set; }
        public string Extension { get; set; }
    }

    public class MyFolder
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime LastModified { get; set; }
        public Byte[] SecurityDesciptor { get; set; }
    }

    /// <summary>
    /// Implements the methods that return data from the back-end
    /// </summary>
    public class MyFileProxy : IDisposable
    {
        private string path;

        public void connect(string folderpath)
        {
            this.path = folderpath;

        }

        /// <summary>
        /// Returns the start address as the root folder
        /// </summary>
        /// <returns></returns>
        [Browsable(true)]
        public MyFolder[] GetRootFolders()
        {
            List<MyFolder> myFolders = new List<MyFolder>();

            MyFolder myfolder = new MyFolder();
            myfolder.Path = this.path;
            DirectoryInfo di = new DirectoryInfo(path);
            myfolder.Name = di.Name;
            myfolder.LastModified = di.LastWriteTimeUtc;
            DirectorySecurity sec = di.GetAccessControl();
            myfolder.SecurityDesciptor = new Byte[sec.GetSecurityDescriptorBinaryForm().Length];
            myfolder.SecurityDesciptor = sec.GetSecurityDescriptorBinaryForm();

            myFolders.Add(myfolder);
            return myFolders.ToArray();
        }

        [Browsable(true)]
        public MyFolder[] GetSubFolders(string parentFolderPath)
        {
            List<MyFolder> myFolders = new List<MyFolder>();
            foreach (string dirpath in Directory.GetDirectories(parentFolderPath))
            {
                MyFolder myfolder = new MyFolder();
                myfolder.Path = dirpath;
                DirectoryInfo di = new DirectoryInfo(dirpath);
                myfolder.Name = di.Name;
                myfolder.LastModified = di.LastWriteTimeUtc;
                DirectorySecurity sec = di.GetAccessControl();
                myfolder.SecurityDesciptor = new Byte[sec.GetSecurityDescriptorBinaryForm().Length];
                myfolder.SecurityDesciptor = sec.GetSecurityDescriptorBinaryForm();

                myFolders.Add(myfolder);
            }
            return myFolders.ToArray();
        }

        [Browsable(true)]
        public MyFolder GetSpecifiedFolder(string folderpath)
        {
            MyFolder myfolder = new MyFolder();
            myfolder.Path = folderpath;
            DirectoryInfo di = new DirectoryInfo(folderpath);
            myfolder.Name = di.Name;
            myfolder.LastModified = di.LastWriteTimeUtc;
            DirectorySecurity sec = di.GetAccessControl();
            myfolder.SecurityDesciptor = new Byte[sec.GetSecurityDescriptorBinaryForm().Length];
            myfolder.SecurityDesciptor = sec.GetSecurityDescriptorBinaryForm();
            return myfolder;
        }



        /// <summary>
        /// The finder method implementation that returns all objects
        /// </summary>
        /// <returns>All files for the given start address</returns>
        [Browsable(true)]
        public MyFile[] GetAllFiles(string folderpath)
        {
            List<MyFile> myfiles = new List<MyFile>();

            foreach (string filepath in Directory.GetFiles(folderpath))
            {
                MyFile myfile = new MyFile();
                myfile.Path = filepath;
                FileInfo fi = new FileInfo(filepath);
                myfile.Extension = fi.Extension.TrimStart(new char[] { '.' });
                myfile.LastModified = fi.LastWriteTimeUtc;
                myfile.Name = fi.Name;
                FileSecurity sec = fi.GetAccessControl();
                myfile.SecurityDesciptor = new Byte[sec.GetSecurityDescriptorBinaryForm().Length];
                myfile.SecurityDesciptor = sec.GetSecurityDescriptorBinaryForm();
                myfiles.Add(myfile);
            }

            return myfiles.ToArray();
        }

        /// <summary>
        /// Implementation of the SpecificFinder stereotype
        /// </summary>
        /// <param name="filepath">The path of the file</param>
        /// <returns>The File object</returns>
        [Browsable(true)]
        public MyFile GetSpecifiedFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                FileInfo fi = new FileInfo(filepath);
                MyFile myfile = new MyFile();
                myfile.Path = filepath;
                myfile.Extension = fi.Extension.TrimStart(new char[] {'.'});
                myfile.LastModified = fi.LastWriteTimeUtc;
                myfile.Name = fi.Name;
                FileSecurity sec = fi.GetAccessControl();
                myfile.SecurityDesciptor = new Byte[sec.GetSecurityDescriptorBinaryForm().Length];
                myfile.SecurityDesciptor = sec.GetSecurityDescriptorBinaryForm();
                return myfile;

            }
            return null;
        }

        /// <summary>
        /// Implementation of the streamaccessor stereotype
        /// </summary>
        /// <param name="filepath">Path of the file</param>
        /// <returns>A stream to the file data</returns>
        [Browsable(true)]
        public FileStream GetFileData(string filepath)
        {
            return new FileStream(filepath, FileMode.Open, FileAccess.Read);
        }

        #region IDisposable Members

        public void Dispose() { }

        #endregion
    }
}
