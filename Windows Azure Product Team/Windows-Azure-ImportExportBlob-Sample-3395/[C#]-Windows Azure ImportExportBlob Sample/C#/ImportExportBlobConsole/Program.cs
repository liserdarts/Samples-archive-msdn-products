//*********************************************************
//
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

// USAGE
//
// Import contents of container to local directory:
// ImportExportBlob -o import -d <local-dest-dir> -c <container-name> 
//
// Export contents of local directory to container:
// ImportExportBlob -o export -d <local-source-dir>
//
// Get help:
// ImportExportBlob [-h] 

namespace ImportExportBlob
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;

    /// <summary>
    /// A console application that exports a local directory and its files to a Windows Azure storage services
    /// container and blob, or that imports a container and its blob to a local directory.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Console application entry point.
        /// </summary>
        /// <param name="args">The command line argument list.</param>
        private static void Main(string[] args)
        {
            try
            {
                // Check for the usual help conditions
                if (args.Length == 0 ||
                    args.Contains("-h") ||
                    args.Contains("/h") ||
                    args.Contains("-?") ||
                    args.Contains("/?"))
                {
                    OutputHelp();
                    Environment.Exit(1);
                }

                string error = "Error: command-line arguments incorrectly specified. Type " +
                    "ImportExportBlob -h for guidelines.";

                // Ensure an even number of arguments
                if (args.Length % 2 != 0)
                {
                    Console.WriteLine(error);
                    Environment.Exit(1);
                }

                // Return name-value pairs containing command-line values
                NameValueCollection argCollection = ProcessCommandLineArgs(args);

                // Perform requested operation
                string order = (argCollection["-o"] ?? string.Empty).ToLower();

                if (order == "export")
                {
                    if (argCollection["-d"] == null || argCollection["-c"] != null)
                    {
                        Console.WriteLine(error);
                        Environment.Exit(1);
                    }

                    ExportDirectoryToContainer(Path.GetFullPath(argCollection["-d"]).TrimEnd('\\'));
                }
                else if (order == "import")
                {
                    if (argCollection["-d"] == null || argCollection["-c"] == null)
                    {
                        Console.WriteLine(error);
                        Environment.Exit(1);
                    }

                    ImportContainerToDirectory(
                        argCollection["-c"].ToLower(), 
                        Path.GetFullPath(argCollection["-d"]).TrimEnd('\\'));
                }
                else
                {
                    Console.WriteLine(error);
                    Environment.Exit(1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Processes the command line arguments and adds them to a collection of name-value pairs.
        /// Does not perform error checking on command-line syntax.
        /// </summary>
        /// <param name="args">The argument list passed in to Main.</param>
        /// <returns>The command line arguments as name-value pairs.</returns>
        private static NameValueCollection ProcessCommandLineArgs(string[] args)
        {
            NameValueCollection argCollection = new NameValueCollection();

            for (int i = 0; i < args.Length; i += 2)
            {
                argCollection.Add(args[i], args[i + 1]);
            }

            return argCollection;
        }

        /// <summary>
        /// Exports the user's directory to a container of the same name in their storage account.
        /// </summary>
        /// <param name="dirPath">The full path to the directory to export.</param>
        private static void ExportDirectoryToContainer(string dirPath)
        {
            // Retrieve storage account information from app.config file (uses dev storage by default).
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageAccountConnectionString"]);

            // Create client object for Blob service.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get container name corresponding to top-level folder name  
            string containerName = MakeContainerName(Path.GetFileName(dirPath));
            Console.WriteLine("Uploading blobs to container " + containerName);

            // Get named container or create if it does not exist.
            CloudBlobContainer container = GetContainer(blobClient, containerName, true);

            // Create a blob for each file in the top-level directory and all subdirectories.
            foreach (string fileName in Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories))
            {
                // Exclude hidden, system, and temp files
                if (0 == (new FileInfo(fileName).Attributes & 
                    (FileAttributes.Hidden | FileAttributes.System | FileAttributes.Temporary)))
                {
                    // Construct the blob reference from the top level folder path and file name.
                    CloudBlob blob = container.GetBlobReference(fileName.Substring(dirPath.Length + 1));

                    // Upload the file to the blob.
                    try
                    {
                        blob.UploadFile(fileName);
                        Console.WriteLine("Uploaded new blob " + blob.Uri);
                    }
                    catch (StorageClientException e)
                    {
                        Console.WriteLine("Could not upload blob named " + blob.Uri);
                        Console.WriteLine("Error message: " + e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the specified container, optionally creating it if it does not already exist.
        /// </summary>
        /// <param name="blobClient">The blob client the container goes in.</param>
        /// <param name="containerName">The name of the blob container.</param>
        /// <param name="createIfNotExists">If true, create the container if not found.</param>
        /// <returns>The specified container.</returns>
        /// <exception cref="ArgumentException"><para>The container specified by <paramref name="containerName"/> does not exist in the storage account, when <paramref name="createIfNotExists"/> is false.</para></exception>
        private static CloudBlobContainer GetContainer(
            CloudBlobClient blobClient, string containerName, bool createIfNotExists)
        {
            // create reference to container
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Create the container if it does not already exist, per the flag.
            if (createIfNotExists)
            {
                container.CreateIfNotExist();
            }
            else
            {
                try
                {
                    container.FetchAttributes();
                }
                catch (StorageClientException e)
                {
                    if (e.ErrorCode == StorageErrorCode.ResourceNotFound)
                    {
                        throw new ArgumentException(
                            "Container " + containerName + " does not exist in this storage account.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return container;
        }

        /// <summary>
        /// Imports the blobs in the specified container to the specified directory.
        /// </summary>
        /// <param name="containerName">The blob container to import from.</param>
        /// <param name="dirParentPath">The full path to the directory to copy the blobs to.</param>
        /// <exception cref="ArgumentException"><para>The directory specified by argument <paramref name="dirParentPath"/> or a subdirectory could not be created.</para></exception>
        private static void ImportContainerToDirectory(string containerName, string dirParentPath)
        {
            // Check whether specified local directory exists, and if not, create it.
            try
            {
                Directory.CreateDirectory(dirParentPath);
            }
            catch (IOException e)
            {
                throw new ArgumentException("Could not create directory " + dirParentPath, e);
            }

            // Retrieve storage account information from app.config file.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageAccountConnectionString"]);

            // Create client object for Blob service.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get the specified container if it exists.
            CloudBlobContainer container = GetContainer(blobClient, containerName, false);

            // Set options on request to blob service, to control how blobs are returned by listing operation.
            BlobRequestOptions options = new BlobRequestOptions();

            // Do not include metadata, snapshots, or uncommitted blobs in listing. 
            // Including this setting for completeness, as default value is BlobListingDetails.None.
            options.BlobListingDetails = BlobListingDetails.None;

            // Ignore delimiter and return all blobs, rather than aggregating blobs according to the 
            // delimiter character. This way, we know each item corresponds to a file.
            options.UseFlatBlobListing = true;

            // Enumerate blobs in this container.
            foreach (CloudBlob blob in container.ListBlobs(options))
            {
                // Get path to destination file.
                string relativePath = blob.Uri.AbsoluteUri.Substring(container.Uri.AbsoluteUri.Length + 1);
                string fileName = Path.GetFullPath(Path.Combine(dirParentPath, relativePath));
                string dirName = Path.GetDirectoryName(fileName);

                // Create the directory for the file, if necessary
                try
                {
                    Directory.CreateDirectory(dirName);
                }
                catch (IOException)
                {
                    throw new ArgumentException(
                        "An error occurred while attempting to create directory " + dirName);
                }

                FileInfo file = new FileInfo(fileName);

                // Check whether file already exists (before it is created).
                bool fileExists = file.Exists;

                // Download the blob to the destination file.
                blob.DownloadToFile(fileName);

                if (fileExists)
                {
                    Console.WriteLine("Overwrote existing file " + fileName + " from blob " + blob.Uri);
                }
                else
                {
                    Console.WriteLine("Created new file " + fileName + " from blob " + blob.Uri);
                }
            }
        }

        /// <summary>
        /// Create a blob container name from a string.
        /// Container names must be between 3 and 63 characters long, 
        /// must consist of lower-case alphanumeric and dash characters, 
        /// and every dash must be preceded and followed by an alphanumeric.
        /// The container name is prefixed with "blob" to handle zero-length names.
        /// </summary>
        /// <param name="name">The input name to be normalized.</param>
        /// <returns>The normalized blob container name.</returns>
        private static string MakeContainerName(string name)
        {
            // Prefix container names with "blob" for zero-length names (drive root).
            StringBuilder nameBuilder = new StringBuilder();
            nameBuilder.Append("blob");

            if (!string.IsNullOrEmpty(name))
            {
                for (int i = 0; i < name.Length; i++)
                {
                    // Copy characters that are digits, letters, 
                    // and non-initial, non-sequential dashes.
                    if (char.IsLetterOrDigit(name[i]) ||
                        ((i > 0) && ('-' == name[i]) && ('-' != name[i - 1])))
                    {
                        nameBuilder.Append(name[i]);
                    }
                }
            }

            // Fold result to lower case.
            string containerName = nameBuilder.ToString().ToLower();

            // Copy only up to 63 characters to output.
            if (containerName.Length > 63)
            {
                containerName = containerName.Substring(0, 63);
            }

            // Eliminate any trailing dash in output and return the result.
            return containerName.TrimEnd('-');
        }

        /// <summary>
        /// Writes help text to the console.
        /// </summary>
        private static void OutputHelp()
        {
            string help = "ImportExportBlob help:\n" +
                "This console application either exports the contents of a local directory \n" +
                "to a container, or imports the contents of a container to a local directory.\n" +
                "You can round-trip cleanly between the two.\n" +
                "\n" +
                "Command-line arguments (specify in order):\n" +
                "-o\tSpecify \"import\" or \"export\" to indicate which operation to perform.\n" +
                "-d\tSpecify the local source directory for the export case, or the local\n" +
                "\tdestination directory for the import case.\n" +
                "-c\tFor the import case only, specify the container to import to a local\n" +
                "\tdestination directory.\n" +
                "\n" +
                "Example export: ImportExportBlob -o export -d c:\\myblobs \n" +
                "Example import: ImportExportBlob -o import -d c:\\myblobs -c blobmyblobs \n"; 
                
            Console.WriteLine(help);
        }
    }
}