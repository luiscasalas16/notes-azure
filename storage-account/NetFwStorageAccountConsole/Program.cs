using Azure.Identity;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using System;
using System.Diagnostics;
using System.IO;

namespace NetFwStorageAccountConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(".Net Framework Console");

                string result;

                try
                {
                    const string SampleFileContent = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

                    string containerName = $"{"sample-container"}-{DateTime.UtcNow.AddHours(-6).ToString("yyyy-MM-dd-HH-mm-ss-fffff")}";
                    string blobName = $"{"sample-file"}-{DateTime.UtcNow.AddHours(-6).ToString("yyyy-MM-dd-HH-mm-ss-fffff")}";
                    string filePath1 = Path.ChangeExtension(Path.GetTempFileName(), ".txt");
                    string filePath2 = Path.ChangeExtension(Path.GetTempFileName(), ".txt");

                    System.IO.File.WriteAllText(filePath1, SampleFileContent);

                    BlobServiceClient client = new BlobServiceClient(new Uri($"https://lcs16sa.blob.core.windows.net"), new DefaultAzureCredential());

                    BlobContainerClient container = client.CreateBlobContainer(containerName);

                    container.CreateIfNotExists();

                    BlobClient blob = container.GetBlobClient(blobName);

                    blob.Upload(filePath1);

                    blob.DownloadTo(filePath2);

                    if (System.IO.File.ReadAllText(filePath1) != System.IO.File.ReadAllText(filePath2))
                        throw new Exception("error");

                    BlobProperties properties = blob.GetProperties();

                    result = $"Container '{containerName}' Blob '{blobName}' MD5 '{Convert.ToBase64String(properties.ContentHash)}'";
                }
                catch (Exception ex)
                {
                    result = ex.ToString();
                }

                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());

                Console.WriteLine(ex.ToString());
            }

            Console.Write("Press enter to close this window . . .");
            Console.ReadLine();
        }
    }
}
