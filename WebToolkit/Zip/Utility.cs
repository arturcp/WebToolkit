using Ionic.Zip;

namespace WebToolkit.Zip
{
    public static class Utility
    {
        /// <summary>
        /// Compress a list of files into one single zip file
        /// </summary>
        /// <param name="files">A list of files (a full path to them) to add to the .zip file.</param>
        /// <param name="fullPathToZipFile">The full path to the final .zip file. It will be created, so the application must have write permission on the folder.</param>
        public static void Compress(string[] files, string fullPathToZipFile)
        {
            using (ZipFile zip = new ZipFile())
            {
                foreach (string file in files)
                {
                    zip.AddFile(file);    
                }                
                zip.Save(fullPathToZipFile);
            }
        }

        /// <summary>
        /// Unzip files from a .zip, to a selected folder. It will override existing files on the destination folder.
        /// </summary>
        /// <param name="zipFileToUnpack">The full path to the .zip file, to be uncompressed.</param>
        /// <param name="fullPathToUnzippedFiles">The full path where the unzipped files should be written to. The application must have write permission on the folder.</param>
        public static void Extract(string zipFileToUnpack, string fullPathToUnzippedFiles)
        {
            using (ZipFile zip = ZipFile.Read(zipFileToUnpack))
            {
                foreach (ZipEntry entry in zip)
                {
                    entry.Extract(fullPathToUnzippedFiles, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }
    }
}
