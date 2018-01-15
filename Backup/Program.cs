using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Downloader
{

    public struct Link
    {
        public string link, valore;

        public Link(string Link, string Valore)
        {
            link = Link;
            valore = Valore;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //La lista di url da scaricare
            List<Link> linkToDownload = new List<Link>();

            //Leggo il file di testo
            try
            {

                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(@"C:\Progetti\MyProgetti\Downloader\Downloader\bin\Link\Link.txt"))
                {
                    String line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    int i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        i++;
                        Console.WriteLine(line);
                        linkToDownload.Add(new Link(line, i + ".jpeg"));
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

           
            //linkToDownload.Add(new Link("http://www.carlsalter.com/download.asp?fileid=065100108121032049053048032085084073076073084065073082069032112097114116115046112100102", "Adly 150 UTILITAIRE parts"));

            //Ciclo e lancio il tread
            foreach (Link item in linkToDownload)
            {
                Thread paramThreadDownlaoder = new Thread(threadDownloaderParametrico);
                paramThreadDownlaoder.Start(item);
            }

            Console.WriteLine("Premi un tasto");
            Console.ReadLine();

        }

        private static void threadDownloaderParametrico(object o)
        {
            Link link = (Link)o;
            Console.WriteLine("threadDownloaderParametrico: {0}", link.valore);
            Utils.DownloadFile(link.link, link.valore.Trim());
        }
    }
}
