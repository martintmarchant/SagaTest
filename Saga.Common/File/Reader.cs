using System;
using System.IO;

namespace Saga.Common.File
{
    public static class Reader
    {
        /// <summary>
        /// Reads the content of a file into a string array
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <returns></returns>
        public static string[] ContentToStringArray(string fullFilePath, string rowTerminator = "\r\n")
        {
            string result;
            try
            {
                // reads file with shared lock
                using (var fileStream = new FileStream(
                    fullFilePath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite))
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Issue reading file {fullFilePath}.",ex);
            }

            return result.Split(rowTerminator);
        }
    }
}
