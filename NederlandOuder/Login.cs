using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht
{
    class Login
    {
        public static int gebrTypeId;
        public static int gebrId;
        public const int maxpoging = 3;
        public static List<string> users = new List<string>();
        public enum eLoginStatus { OK, FOUT, LEEG };
        //Functie voor inloggen met MAX 3 maal inloggen
        public static eLoginStatus LogStatus()
        {
            eLoginStatus status = eLoginStatus.LEEG;

            string[] line = System.IO.File.ReadAllLines(@"C:\Users\Public\Documents\" + Program.dataFile);
            foreach (string regel in line)
            {
                string[] data = regel.Split('|');
                users.Add(regel);
                while (data[1] == Program.userName)
                {
                    gebrId = Int32.Parse(data[0]);
                    gebrTypeId = Int32.Parse(data[3]);
                    int poging = 0;
                    while (poging < maxpoging)
                    {
                        poging++;
                        if (poging < maxpoging && poging != 1)
                        {
                            Console.WriteLine("Dit is poging {0} van {1}", poging, maxpoging);
                            status = eLoginStatus.FOUT;
                        }

                        if (poging == maxpoging)
                        {
                            status = eLoginStatus.FOUT;
                            Console.WriteLine("Dit is uw laatste poging.");
                        }

                        Console.WriteLine("Uw wachtwoord");
                        Program.userPass = Console.ReadLine();

                        if (data[2] == Program.userPass)
                        {
                            status = eLoginStatus.OK;
                            break;
                        }
                    }
                    break;
                }
            }
            return status;
        }
    }
}
