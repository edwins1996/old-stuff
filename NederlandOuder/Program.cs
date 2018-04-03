using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Eindopdracht
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);
        //Functie voor het maximaliseren van het scherm
        private static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }

        public static string userName;
        public static string userPass;
        public static string dataFile = "gebruikersoverzicht.Txt";

        static void Main(string[] args)
        {
            //Maximaliseert scherm
            Maximize();
            Struct.Init();
            Inloggen:
            Console.WriteLine("Welkom bij het Elektronisch Patiënten Dossier\nDruk op enter om in te loggen...");
            Console.ReadKey();
            Console.WriteLine("Vul uw gebruikersnaam in");
            userName = Console.ReadLine();
            Login.eLoginStatus status = Login.LogStatus();

            if (status == Login.eLoginStatus.OK)
            {
                Console.Clear();
                Console.WriteLine("Ingelogd als, {0}", userName);
                Menu.ShowMenu();
            }
            else if (status == Login.eLoginStatus.FOUT)
            {
                Console.WriteLine("U heeft uw wachtwoord driemaal fout ingevoerd.\nDruk op een toets om het programma af te sluiten.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else if (status == Login.eLoginStatus.LEEG)
            {
                Console.WriteLine("Gebruikersnaam is onjuist\nDruk op <Enter> om opnieuw in te loggen...");
                Console.ReadKey();
                Console.Clear();
                goto Inloggen;
            }
            Console.ReadKey();
        }
    }
}
