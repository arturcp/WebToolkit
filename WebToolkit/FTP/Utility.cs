using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;

namespace WebToolkit.FTP
{
    public class Utility
    {
        private string UserName { get; set; }
        private string Password { get; set; }
        private Uri Host { get; set; }
        private int Port { get; set; }

        public Utility(string host, string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
            this.Host = new Uri(host);
        }

        public Utility(string host, int port, string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
            this.Host = new Uri(host);
            this.Port = port;            
        }

        /// <summary>
        /// List files on a FTP.
        /// </summary>
        /// <param name="path">FTP folder, such as "files/2011". Don't start or end with a slash, it will be put automatically.</param>
        /// <returns>Status of the operation.</returns>
        public List<FTPFile> GetFiles(string path)
        {
            List<FTPFile> files = new List<FTPFile>();
            if (Host.Scheme == Uri.UriSchemeFtp)
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(string.Format("{0}/{1}", Host, path));
                request.Credentials = new NetworkCredential(UserName, Password);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                try
                {

                    Stream responseStream = response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        string result = reader.ReadToEnd();
                        result = result.Replace("\r\n","|");
                        string[] lines = result.Split('|');
                        foreach (string item in lines)
                        {
                            if (!string.IsNullOrEmpty(item))
                                files.Add(new FTPFile(item));
                        }                        
                    }
                }
                finally
                {
                    response.Close();
                }                
            }
            return files;
        }

        /// <summary>
        /// Rename a file on a FTP.
        /// </summary>
        /// <param name="path">FTP folder, such as "files/2011". Don't start or end with a slash, it will be put automatically.</param>
        /// <param name="originalFileName">Name of the file to be renamed, such as 'TextFile1.txt'.</param>
        /// <param name="newFileName">New name to the file, such as 'TextFile2.txt'.</param>
        /// <returns>Status of the operation.</returns>
        public bool RenameFile(string path, string originalFileName, string newFileName)
        {
            bool status = false;
            if (Host.Scheme == Uri.UriSchemeFtp)
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(string.Format("{0}/{1}/{2}", Host, path, originalFileName));
                request.Credentials = new NetworkCredential(UserName, Password);
                request.Method = WebRequestMethods.Ftp.Rename;
                request.RenameTo = newFileName;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                status = response.StatusCode == FtpStatusCode.CommandOK; 
            }

            return status;
        }

        /// <summary>
        /// Upload a file to a folder in an FTP
        /// </summary>
        /// <param name="destinationPath">Destination folder on FTP. For example, "files/2011". Don't start or end with a slash, it will be put automatically.</param>
        /// <param name="filePath">Full path to the file to be uploaded.</param>
        /// <returns>Status of the operation.</returns>
        public bool UploadFile(string destinationPath, string filePath)
        {
            bool status = false;
            if (Host.Scheme == Uri.UriSchemeFtp)
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(string.Format("{0}/{1}/{2}", Host, destinationPath, Path.GetFileName(filePath)));
                request.Credentials = new NetworkCredential(UserName, Password);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                
                StreamReader sourceStream = new StreamReader(filePath);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                status = response.StatusCode == FtpStatusCode.CommandOK;
            }

            return status;
        }

        /// <summary>
        /// Delete a remote file on a FTP.
        /// </summary>
        /// <param name="path">FTP folder, such as "files/2011". Don't start or end with a slash, it will be put automatically.</param>
        /// <param name="fileName">Name of the file to be delete, such as 'TextFile2.txt'.</param>
        /// <returns>Status of the operation.</returns>
        public bool DeleteFile(string path, string fileName)
        {
            bool status = false;
            if (Host.Scheme == Uri.UriSchemeFtp)
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(string.Format("{0}/{1}/{2}", Host, path, fileName));
                request.Credentials = new NetworkCredential(UserName, Password);
                request.Method = WebRequestMethods.Ftp.DeleteFile;


                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                status = response.StatusCode == FtpStatusCode.CommandOK;
            }
            return status;
        }

        public bool CreateDirectory(string path, string directoryName)
        {
            bool status = false;
            if (Host.Scheme == Uri.UriSchemeFtp)
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(string.Format("{0}/{1}/{2}", Host, path, directoryName));
                request.Credentials = new NetworkCredential(UserName, Password);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;


                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                status = response.StatusCode == FtpStatusCode.CommandOK;
            }
            return status;
        }
    }
}
