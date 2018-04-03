using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Eindopdracht
{
    class Menu
    {
        private static string backMessage = "\nDruk op <Escape> om terug te gaan naar het menu";
        private static string errorMessage = "U heeft een fout gemaakt bij het invoeren.\nDruk op <Enter> om het opnieuw te doen.";
        public static string functionMsg = "Druk op <Enter> om door te gaan of druk op een andere toets om terug te gaan.";
        public static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("*-----* MENU *-----*\n");
            Console.WriteLine("1. Persoonlijke gegevens");
            Console.WriteLine("2. Uitdraai patiëntenlijst");

            if (Login.gebrTypeId == 2 || Login.gebrTypeId == 1 || Login.gebrTypeId == 4)
                Console.WriteLine("3. Beheer gebruikersgegevens");

            if (Login.gebrTypeId == 5 || Login.gebrTypeId == 1)
            {
                if (Login.gebrTypeId == 1)
                {
                    Console.WriteLine("4. Beheer behandelingen");
                    Console.WriteLine("5. Beheer aandoeningen");
                }
                else if (Login.gebrTypeId == 5)
                {
                    Console.WriteLine("3. Beheer behandelingen");
                    Console.WriteLine("4. Beheer aandoeningen");
                }
            }

            Console.WriteLine("Esc. Afsluiten");

                uniOpt();
            

        }
        public static void uniOpt()
        {
            Char actie = ' ';
            Char esc = (char) ConsoleKey.Escape;
            while (actie == ' ')
            {

                ConsoleKeyInfo invoer = Console.ReadKey(true);
                if ((invoer.KeyChar >= '1') && (invoer.KeyChar <= '5') || (invoer.KeyChar == esc))
                {
                    actie = invoer.KeyChar;

                    switch (actie)
                    {
                        case '1':
                            //Menu-item 1: Persoonlijke Gegevens
                            Console.Clear();
                            Console.WriteLine("Persoonlijke gegevens\n\n");
                            foreach(string user in Login.users)
                            {
                                string[] asUser = user.Split('|');

                                while(asUser[0] == Login.gebrTypeId.ToString())
                                {
                                    Console.WriteLine("Gebruikersnaam : " + asUser[1]);
                                    Console.WriteLine("Voornaam       : " + asUser[4]);
                                    Console.WriteLine("Tussenvoegsel  : " + asUser[5]);
                                    Console.WriteLine("Achternaam     : " + asUser[6]);
                                    Console.WriteLine("Woonplaats     : " + asUser[7]);
                                    Console.WriteLine("Telefoonnummer : " + asUser[8]);
                                    Console.WriteLine("E-mailadres    : " + asUser[9]);
                                    foreach (Struct.gebrType type in Struct.gebrTypes)
                                    {
                                        while (int.Parse(asUser[3]) == type.id)
                                        {
                                            Console.WriteLine("Functie:       : " + type.omschrijving);
                                            break;
                                        }
                                    }
                                break;
                                }
                            }
                            Console.WriteLine(backMessage);
                            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                                ShowMenu();
                            else
                                goto case '1';
                            break;

                        case '2':
                            //Menu-item 2: Uitdraai patiëntenlijst
                            Console.Clear();
                            Console.WriteLine("Uitdraai patiëntenlijst\n\n");
                            Struct.showUsers();
                            Console.WriteLine(backMessage);

                            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                                ShowMenu();
                            else
                                goto case '2';
                            break;

                        case '3':
                            //Menu-item 3: Beheer gebruikersgegevens/beheer behandelingen
                            Console.Clear();
                            if (Login.gebrTypeId == 3)
                                ShowMenu();
                            else if(Login.gebrTypeId == 5)
                            {
                                Console.Clear();
                                Console.WriteLine("Beheer behandelingen\n\nOptie in aanmaak");
                                Console.WriteLine(backMessage);
                                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                                    ShowMenu();
                                else
                                    goto case '3';
                            }
                            else
                                userManagement();
                            break;

                        case '4':
                            //Menu-item 4: Beheer behandelingen/aandoeningen
                            Console.Clear();
                            if (Login.gebrTypeId == 2 || Login.gebrTypeId == 3 || Login.gebrTypeId == 4)
                                ShowMenu();
                            else {
                                if (Login.gebrTypeId == 1)
                                    Console.WriteLine("Beheer behandelingen\n\nOptie in aanmaak");
                                else if (Login.gebrTypeId == 5)
                                    Console.WriteLine("Beheer aandoeningen\n\nOptie in aanmaak");
                            }

                            Console.WriteLine(backMessage);
                            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                                ShowMenu();
                            else
                                goto case '4';
                            break;

                        case '5':
                            //Menu-item 5: Beheer aandoeningen
                            Console.Clear();
                            if (Login.gebrTypeId != 1)
                                ShowMenu();
                            else if (Login.gebrTypeId == 1)
                                Console.WriteLine("Beheer aandoeningen\n\nOptie in aanmaak");
                            Console.WriteLine(backMessage);
                            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                                ShowMenu();
                            else
                                goto case '5';
                            break;

                         case (char) ConsoleKey.Escape:
                            //Menu-item 6 Exit
                            Console.Clear();
                            Console.WriteLine("Druk op <Enter> om af te sluiten...\nWas dit niet uw bedoeling? Druk op <Escape> om terug te gaan naar het menu.");
                            ConsoleKeyInfo userInput = Console.ReadKey(true);
                            if (userInput.Key == ConsoleKey.Enter)
                            {
                                Console.WriteLine("Exiting...");
                                Environment.Exit(0);
                            }
                            else if (userInput.Key == ConsoleKey.Escape)
                                ShowMenu();
                            else if (userInput.Key != ConsoleKey.Escape && Console.ReadKey().Key != ConsoleKey.Enter)
                            {
                                Console.WriteLine("Exiting...");
                                Environment.Exit(0);
                            }
                            break;
                    }
                }
            }
        }
        //Hieronder bevind zich het gerbuikersbeheer menu.
        public static void userManagement()
        {
            Console.Clear();
            Console.WriteLine("*---* Gebruikersbeheer *---*\n");
            Console.WriteLine("1. Wijzigen personalia");
            if (Login.gebrTypeId != 2)
            {
                Console.WriteLine("2. Wijzigen functies");
                Console.WriteLine("3. Toevoegen gebruiker");
                Console.WriteLine("4. Verwijderen gebruiker");
                Console.WriteLine("5. Wijzigen wachtwoorden");                
            }
            if (Login.gebrTypeId != 2)
                Console.WriteLine("6. Terug naar hoofdmenu");
            else
                Console.WriteLine("2. Terug naar hoofdmenu");
            Char actie = ' ';
            while (actie == ' ')
            {

                ConsoleKeyInfo invoer = Console.ReadKey(true);
                if ((invoer.KeyChar >= '1') && (invoer.KeyChar <= '6'))
                {
                    actie = invoer.KeyChar;

                    switch (actie)
                    {
                        case '1':
                            //Gebruikersbeheer menu-item 1: Wijzigen personalia
                            int id = 0;

                            Console.Clear();
                            Console.WriteLine("Wijzigen personalia\n\n");
                            Console.WriteLine(functionMsg);
                            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                            {
                                Struct.showUsers();
                                Console.WriteLine("\nVoer het ID in van de gebruiker die u wilt wijzigen:");
                                try
                                {
                                    id = int.Parse(Console.ReadLine());
                                    if (id >= Login.users.Count)
                                    {
                                        Console.WriteLine(errorMessage);
                                        Console.ReadKey();
                                        goto case '1';
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine(errorMessage);
                                    Console.ReadKey();
                                    goto case '1';
                                }
                                Console.WriteLine("U heeft gekozen voor de volgende gebruiker: {0}", Login.users[id]);

                                Console.WriteLine("Nieuwe gebruikersnaam");
                                string newUsnm = Console.ReadLine();

                                Console.WriteLine("Nieuwe voornaam");
                                string newFirstname = Console.ReadLine();

                                Console.WriteLine("Nieuwe tussenvoegsel");
                                string newBetname = Console.ReadLine();

                                Console.WriteLine("Nieuwe achternaam");
                                string newLastname = Console.ReadLine();

                                Console.WriteLine("Nieuwe woonplaats");
                                string newVillage = Console.ReadLine();

                                Console.WriteLine("Nieuwe telefoonnummer");
                                string newTel = Console.ReadLine();

                                Console.WriteLine("Nieuwe e-mailadres");
                                string newMail = Console.ReadLine();

                                //schrijft nieuwe gegevens in gebruikersbestand
                                string[] userCh = Login.users[id].Split('|');
                                if (newFirstname != "" && newLastname != "" && newVillage != "" && newTel != "" && newMail != "")
                                {
                                    Login.users[id] = userCh[0] + "|" + newUsnm + "|" + userCh[2] + "|" + userCh[3] + "|" + newFirstname + "|" + newBetname + "|" + newLastname + "|" + newVillage + "|" + newTel + "|" + newMail;
                                    Console.WriteLine("\nGebruiker succesvol gewijzigd!\n" + backMessage);
                                }
                                else {
                                    Console.WriteLine(errorMessage);
                                    Console.ReadKey();
                                    goto case '1';
                                }
                                File.WriteAllLines(@"C:\Users\Public\Documents\" + Program.dataFile, Login.users);
                                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                                    userManagement();
                                else
                                    goto case '1';
                            }
                            else
                                userManagement();
                            break;

                        case '2':
                            //Gebruikersbeheer menu-item 2: Wijzigen functies
                            int uId = 0, fId = 0;

                            if (Login.gebrTypeId == 2)
                                ShowMenu();
                            Console.Clear();
                            Console.WriteLine("Wijzigen functies\n\n");
                            Console.WriteLine(functionMsg);
                            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                            {
                                Struct.showUsers();
                                Console.WriteLine("\nVoer het ID in van de gebruiker die u wilt wijzigen:");
                                try
                                {
                                    uId = int.Parse(Console.ReadLine());
                                }
                                catch
                                {
                                    Console.WriteLine(errorMessage);
                                    Console.ReadKey();
                                    goto case '2';
                                }
                                Console.Clear();
                                Console.WriteLine("U heeft gekozen voor de volgende gebruiker: {0}", Login.users[uId]);
                                Console.WriteLine("De functies waaruit gekozen kan worden zijn:\n ");
                                Struct.showUserTypes();
                                Console.WriteLine("\nKies het id van de door u gewenste functie");
                                try
                                {
                                    fId = int.Parse(Console.ReadLine());
                                    if ((fId > 0) && (fId <= 5))
                                    {
                                    }
                                    else
                                    {
                                        Console.WriteLine(errorMessage);
                                        Console.ReadKey();
                                        goto case '2';
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine(errorMessage);
                                    Console.ReadKey();
                                    goto case '2';
                                }
                                string[] charUser = Login.users[uId].Split('|');
                                Login.users[uId] = charUser[0] + "|" + charUser[1] + "|" + charUser[2] + "|" + fId + "|" + charUser[4] + "|" + charUser[5] + "|" + charUser[6] + "|" + charUser[7] + "|" + charUser[8] + "|" + charUser[9];

                                File.WriteAllLines(@"C:\Users\Public\Documents\" + Program.dataFile, Login.users);
                                Console.WriteLine("Functie succesvol gewijzigd!\n" + backMessage);
                                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                                    userManagement();
                                else
                                    goto case '2';
                            }
                            else
                                userManagement();
                            break;

                        case '3':
                            //Gebruikersbeheer menu-item 3: Toevoegen gebruikers
                            int idCount = 0, nId = 0;

                            if (Login.gebrTypeId == 2)
                                userManagement();
                            Console.Clear();
                            Console.WriteLine("Gebruiker toevoegen\n\n");
                            Console.WriteLine(functionMsg);

                            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                            {
                                Console.WriteLine("\nVoer een gebruikersnaam in:");
                                string nGebr = Console.ReadLine();

                                Console.WriteLine("\nVoer een wachtwoord in:");
                                string nWw = Console.ReadLine();

                                Console.WriteLine("\nVoer een voornaam in:");
                                string nVnaam = Console.ReadLine();

                                Console.WriteLine("\nVoer een tussenvoegsel in: (leeg laten indien geen tussenvoegsel)");
                                string nTvoeg = Console.ReadLine();

                                Console.WriteLine("\nVoer een achternaam in:");
                                string nAnaam = Console.ReadLine();

                                Console.WriteLine("\nVoer een woonplaats in:");
                                string nWplaats = Console.ReadLine();

                                Console.WriteLine("\nVoer een telefoonnummer in:");
                                string nTel = Console.ReadLine();

                                Console.WriteLine("\nVoer een e-mailadres in:");
                                string nEmail = Console.ReadLine();

                                foreach(string item in Login.users)
                                {
                                    idCount++;

                                    string[] idCheck = item.Split('|');
                                    if(int.Parse(idCheck[0]) - 1 != idCount)                                    
                                        nId = idCount + 1;                                                                
                                }
                                Console.WriteLine("\nKies een functie\nDe functies waaruit gekozen kan worden zijn:\n ");
                                Struct.showUserTypes();
                                Console.WriteLine("\n");
                                try
                                {
                                    Console.WriteLine("Voer het nummer van één van de functies in");
                                    fId = int.Parse(Console.ReadLine());
                                    if ((fId > 0) && (fId <= 5))
                                    {
                                    }
                                    else
                                    {
                                        Console.WriteLine(errorMessage);
                                        Console.ReadKey();
                                        goto case '3';
                                    }                                    
                                }
                                catch
                                {
                                    Console.WriteLine(errorMessage);
                                    Console.ReadKey();
                                    goto case '3';
                                }                                
                                string newUser = nId + "|" + nGebr + "|" + nWw + "|" + fId.ToString() + "|" + nVnaam + "|" + nTvoeg + "|" + nAnaam + "|" + nWplaats + "|" + nTel + "|" + nEmail;
                                if (nGebr != "" && nWw != "" && nVnaam != "" && nAnaam != "" && nWplaats != "" && nTel != "" && nEmail != "")
                                {
                                    Login.users.Add(newUser);                                    
                                    File.WriteAllLines(@"C:\Users\Public\Documents\" + Program.dataFile, Login.users);
                                    Console.WriteLine("\nGebruiker succesvol aangemaakt!\n" + backMessage);
                                }
                                else
                                {
                                    Console.WriteLine(errorMessage);
                                    Console.ReadKey();
                                    goto case '3';
                                }
                                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                                        userManagement();
                                    else
                                        goto case '3';
                            }
                            else
                                userManagement();
                            break;

                        case '4':
                            //Gebruikersbeheer menu-item 4: Verwijderen gebruikers
                            int vId = 0;

                            if (Login.gebrTypeId == 2)
                                userManagement();
                            Console.Clear();
                            Console.WriteLine("Gebruiker verwijderen\n\n");
                            Console.WriteLine(functionMsg);

                            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                            {
                                Console.Clear();
                                foreach (string user in Login.users)
                                {
                                    string[] uData = user.Split('|');
                                    while (Login.gebrId != int.Parse(uData[0]))
                                    {
                                        Console.WriteLine(int.Parse(uData[0]) - 1 + "|" + uData[1]);
                                        break;
                                    }
                                }
                                Console.WriteLine("\nVoer het ID in van de gebruiker die u wilt verwijderen:");
                                try
                                {
                                    vId = int.Parse(Console.ReadLine());
                                    if ((vId >= 0) && (vId < Login.users.Count) && (vId != (Login.gebrId - 1)))
                                    {
                                    }
                                    else
                                    {
                                        Console.WriteLine(errorMessage);
                                        Console.ReadKey();
                                        goto case '4';
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine(errorMessage);
                                    Console.ReadKey();
                                    goto case '4';
                                }
                                Login.users.RemoveAt(vId);
                                File.WriteAllLines(@"C:\Users\Public\Documents\" + Program.dataFile, Login.users);
                                Console.WriteLine("\nGebruiker succesvol verwijderd!\n" + backMessage);
                                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                                    userManagement();
                                else
                                    goto case '4';
                            }
                            else
                                userManagement();
                            break;

                        case '5':
                            //Gebruikersbeheer menu-item 5: Wijzigen wachtwoord
                            int iUid;

                            if (Login.gebrTypeId == 2)
                                userManagement();
                            Console.Clear();
                            Console.WriteLine("Wijzigen wachtwoord\n\n");
                            Console.WriteLine(functionMsg);
                            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                            {
                                Struct.showUsers();
                                Console.WriteLine("\nKies de gebruiker waarvan u het wachtwoord wilt wijzigen");
                                try
                                {
                                    iUid = int.Parse(Console.ReadLine());
                                    if ((iUid >= 0) && (iUid < Login.users.Count) && (iUid != (Login.gebrId - 1)))
                                    {

                                    }
                                    else
                                    {
                                        Console.WriteLine(errorMessage);
                                        Console.ReadKey();
                                        goto case '5';
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine(errorMessage);
                                    Console.ReadKey();
                                    goto case '5';
                                }
                                Console.WriteLine("U heeft gekozen voor : {0}", Login.users[iUid]);

                                string[] userWw = Login.users[iUid].Split('|');
                                Console.WriteLine("Voer het huidige wachtwoord in:");
                                string oldPass = Console.ReadLine();
                                if (oldPass == userWw[2])
                                {
                                    Console.WriteLine("Voer het nieuwe wachtwoord in:");
                                    string newPass1 = Console.ReadLine();
                                    Console.WriteLine("Voer nogmaals het nieuwe wachtwoord in:");
                                    string newPass2 = Console.ReadLine();
                                    if (newPass1 == newPass2)
                                    {
                                        Login.users[iUid] = userWw[0] + "|" + userWw[1] + "|" + newPass2 + "|" + userWw[3] + "|" + userWw[4] + "|" + userWw[5] + "|" + userWw[6] + "|" + userWw[7] + "|" + userWw[8] + "|" + userWw[9];
                                        File.WriteAllLines(@"C:\Users\Public\Documents\" + Program.dataFile, Login.users);
                                        Console.WriteLine("\nWachtwoord succesvol gewijzigd!\n" + backMessage);
                                        if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                                            userManagement();
                                        else
                                            goto case '5';
                                    }
                                    else
                                    {
                                        Console.WriteLine(errorMessage);
                                        Console.ReadKey();
                                        goto case '5';
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(errorMessage);
                                    Console.ReadKey();
                                    goto case '5';
                                }
                            }
                            else
                                userManagement();
                            break;

                        case '6':
                            //Gebruikersbeheer menu-item 6: Terug naar Menu
                            if (Login.gebrTypeId == 2)
                                userManagement();
                            ShowMenu();
                            break;
                    }
                }
            }
        }
    }
}
