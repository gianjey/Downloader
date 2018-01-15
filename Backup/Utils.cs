using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Downloader.Properties;

namespace Downloader
{
    public class Utils
    {
        public static string ConcatUrls(string firstPart, string secondPart)
        {
            if (firstPart.EndsWith("/") == false)
            {
                if (secondPart.StartsWith("/") == false)
                {
                    return firstPart + "/" + secondPart;
                }
                return firstPart + secondPart;
            }
            if (secondPart.StartsWith("/") == true)
            {
                firstPart = firstPart.TrimEnd(new char[] { '/' });
            }
            return firstPart + secondPart;
        }

        public static bool? boolValue(string outVal)
        {
            if (String.IsNullOrEmpty(outVal))
                return null;
            else
            {
                string valore = outVal.Trim().ToUpper();
                if (valore != "NO" && valore != "SI")
                    throw new SystemException("Boolean non valido");
                else
                    return valore == "SI";
            }
        }

        public static int? ToNullableInt32(string s)
        {
            int i;
            if (Int32.TryParse(s, out i)) return i;
            return null;
        }

        public static decimal? ToNullableDecimal(string s)
        {
            decimal retOut;
            if (decimal.TryParse(s, out retOut)) return retOut;
            return null;

        }

        public static decimal ToDecimal(string s)
        {
            decimal retOut;
            decimal.TryParse(s, out retOut);
            return retOut;


        }

        public static bool DownloadImage(string urlImage, string ImageName)
        {
            //Scarico l'immagine 
            string ImagesDir = Path.GetFullPath(Settings.Default.ImagesDirectory);
            //Controllo l'esistenza della directory specificata
            if (!Directory.Exists(ImagesDir))
            {
                DirectoryInfo currPath = Directory.GetParent(ImagesDir);
                if (currPath.FullName.Equals(currPath.Parent.FullName, StringComparison.CurrentCultureIgnoreCase))
                {
                    currPath = currPath.Parent;
                }
                if (Directory.Exists(currPath.FullName))
                {
                    Directory.CreateDirectory(ImagesDir);
                }
                else
                {
                    throw new DirectoryNotFoundException("Path specificato non valido.");
                }
            }

            string ImageSavePath = Path.Combine(ImagesDir, ImageName);
            if (File.Exists(ImageSavePath))
                return true;

            try
            {
                Console.Write("Download immagine: " + urlImage);
                WebRequest requestPic = WebRequest.Create(urlImage);

                using (WebResponse resp = requestPic.GetResponse())
                {
                    using (Stream ResponseStream = resp.GetResponseStream())
                    {
                        FileStream writeStream = new FileStream(ImageSavePath, FileMode.Create, FileAccess.Write);
                        Byte[] readBuffer = new Byte[256];
                        // Read from buffer
                        int count = ResponseStream.Read(readBuffer, 0, 256);
                        while (count > 0)
                        {
                            // get string
                            writeStream.Write(readBuffer, 0, count);
                            count = ResponseStream.Read(readBuffer, 0, 256);
                        }
                        // Release the response object resources.
                        ResponseStream.Close();
                        writeStream.Close();
                    }
                }

                Console.Write(" - Scaricata");
                Console.WriteLine();
                return true;

            }
            catch (Exception ex)
            {
                Console.Write(" - Errore download immagine {0}, errore:{1}", urlImage, ex.Message);
                Console.WriteLine();
                return false;
            }
        }

        public static bool DownloadFile(string urlFile, string FileName)
        {
            //Scarico l'immagine 
            string FilesDir = Path.GetFullPath(Settings.Default.FilesDirectory);
            //Controllo l'esistenza della directory specificata
            if (!Directory.Exists(FilesDir))
            {
                DirectoryInfo currPath = Directory.GetParent(FilesDir);
                if (currPath.FullName.Equals(currPath.Parent.FullName, StringComparison.CurrentCultureIgnoreCase))
                {
                    currPath = currPath.Parent;
                }
                if (Directory.Exists(currPath.FullName))
                {
                    Directory.CreateDirectory(FilesDir);
                }
                else
                {
                    throw new DirectoryNotFoundException("Path specificato non valido.");
                }
            }

            string FilesSavePath = Path.Combine(FilesDir, FileName);
            // Se non voglio l'aggiornamento scommenta queste righe
            //if (File.Exists(FilesSavePath))
            //    return true;

            try
            {
                Console.WriteLine("Download file: " + urlFile);
                WebRequest requestFile = WebRequest.Create(urlFile);

                using (WebResponse resp = requestFile.GetResponse())
                {
                    using (Stream ResponseStream = resp.GetResponseStream())
                    {
                        FileStream writeStream = new FileStream(FilesSavePath, FileMode.Create, FileAccess.Write);
                        Byte[] readBuffer = new Byte[256];
                        // Read from buffer
                        int count = ResponseStream.Read(readBuffer, 0, 256);
                        while (count > 0)
                        {
                            // get string
                            writeStream.Write(readBuffer, 0, count);
                            count = ResponseStream.Read(readBuffer, 0, 256);
                        }
                        // Release the response object resources.
                        ResponseStream.Close();
                        writeStream.Close();
                    }
                }

                Console.WriteLine(urlFile + " - Scaricato come - " + FileName );
                Console.WriteLine();
                return true;

            }
            catch (Exception ex)
            {
                Console.Write(" - Errore download file {0}, errore:{1}", urlFile, ex.Message);
                Console.WriteLine();
                return false;
            }
        }
    }
}



    
