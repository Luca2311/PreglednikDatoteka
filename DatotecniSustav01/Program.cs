using System;
using System.IO;

namespace DatotecniSustav01
{
    class Program
    {
        static void Main(string[] args)
        {
            //prihvaca ulazni argument na prvoj poziciji
            string direktorij = args[0];
            DirectoryInfo dir = new DirectoryInfo(direktorij);

            //provjerava ako direktorij postoji i ako postoji izvršava zadani kod
            if (Directory.Exists(direktorij))
            {
                 var direktoriji = dir.GetDirectories();
            var datoteke = dir.GetFiles();
            long velicina = 0;
            List<DirectoryInfo> dirs = new List<DirectoryInfo>(direktoriji);
            List<FileInfo> files = new List<FileInfo>(datoteke);

            Console.WriteLine("+------------------+-------------+---------+------------------------------------------+");
            Console.WriteLine("| Veličina       B |          KB |      MB | Nazivi direktorija/datoteka              |");
            Console.WriteLine("+------------------+-------------+---------+------------------------------------------+");
            foreach (DirectoryInfo d in dirs)
            {
                long velicinaDir = 0;
                FileInfo[] fileInfos = d.GetFiles();

                //petlja za racunanje ukupne veličine datoteka u direktoriju
                foreach (FileInfo f in fileInfos)
                {
                    velicinaDir += f.Length;
                }

                Console.WriteLine("|{0, 15} B | {1, 8} KB | {2, 4} MB | {3,40} |",
                    velicinaDir,
                    //velicina u KB
                    velicinaDir / 1024,
                    //velicina u MB
                    velicinaDir / (1024 * 1024),
                    d.Name);
            }

            foreach (FileInfo d in files)
            {
                velicina += d.Length;
                Console.WriteLine("|{0, 15} B | {1, 8} KB | {2, 4} MB | {3,40} |",
                    d.Length,
                    //velicina u KB
                    d.Length / 1024,
                    //Velicina u MB
                    d.Length / (1024 * 1024),
                    d.FullName);
            }
            Console.WriteLine("+------------------+-------------+---------+------------------------------------------+");
            Console.WriteLine("|{0, 15} B | {1, 8} KB | {2, 4} MB |                                          |",
                velicina,
                velicina / 1024,
                velicina / (1024 * 1024));
            Console.WriteLine("+------------------+-------------+---------+------------------------------------------+");


            //Vaš dio koda nisam dirala, u sljedećem pull requestu budem probala taj dio srediti
           Console.SetCursorPosition(1, 3);
            Console.Write(">");
            int brojRedova = datoteke.Length + 6;

            int cekanjeTreperenje = 500;
            Console.CursorVisible = false;
            int pokazivacY = 3;
            while (true)
            {
                System.Threading.Thread.Sleep(cekanjeTreperenje);
                Console.SetCursorPosition(1, pokazivacY);
                Console.Write(" ");
                System.Threading.Thread.Sleep(cekanjeTreperenje);
                Console.SetCursorPosition(1, pokazivacY);
                Console.Write(">");

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pritisnutaTipka = Console.ReadKey(true);
                    if (pritisnutaTipka.Key == ConsoleKey.DownArrow)
                    {
                        pokazivacY++;
                    }
                }
            }
            }
            //ukoliko zadani direktorij ne postoji,ispisi mi sve diskove koji postoje na racunalu
            else
            {
                 //Dohvat svih logickih diskova i omogucena fleksibilnost tablice
                DriveInfo[] diskovi = DriveInfo.GetDrives();
                int najveceIme = 0;
                foreach (DriveInfo d in diskovi)
                {
                    if (d.IsReady)
                    {
                        if (d.VolumeLabel.Length > najveceIme)
                        {
                            najveceIme = d.VolumeLabel.Length;
                        }

                    }
                }
                //PadRight metodu sam saznala od profesora Kolca
                Console.Write("+------------------+---------------+---------+-----------+"); Console.Write("-".PadRight(najveceIme, '-')); Console.WriteLine("+");
                Console.Write("| Oznaka diska     |Ukupna veličina|Slobodno |     %     |"); Console.Write("Naziv Diska".PadRight(najveceIme)); Console.WriteLine("+");
                Console.Write("+------------------+---------------+---------+-----------+"); Console.Write("-".PadRight(najveceIme, '-')); Console.WriteLine("+");
                //ispis oznake diska,ukupne veličine,slobodnog prostora za svaki dostupni disk
                foreach (DriveInfo d in diskovi)
                {
                    //za zdravi disk ispiši sve podatke
                    if (d.IsReady)
                    {

                        Console.Write("|{0, 16}  | {1, 10} GB | {2, 4} GB | {3, 7} % |",
                        d.Name,
                        d.TotalSize / (1024 * 1024 * 1024),
                        d.TotalFreeSpace / (1024 * 1024 * 1024),
                        Math.Round(((double)d.TotalFreeSpace / (double)d.TotalSize) * 100, 2));
                        Console.Write("{0}".PadRight((najveceIme + 3) - d.VolumeLabel.Length), d.VolumeLabel); Console.WriteLine("|");
                    }
                    //izbjegavanje exceptiona kod nedostupnosti diska
                    else
                    {
                        Console.Write("|{0, 16}  | {1, 12}  | {2, 6}  | {3, 8}  |", d.Name, "n/a", "n/a", "n/a");
                        Console.Write("n/a".PadRight((najveceIme))); Console.WriteLine("|");
                    }
                }
                Console.Write("+------------------+---------------+---------+-----------+"); Console.Write("-".PadRight((najveceIme), '-')); Console.WriteLine("+");


            }

            // Console.SetCursorPosition(0, brojRedova);
        } //Main
    }
}
