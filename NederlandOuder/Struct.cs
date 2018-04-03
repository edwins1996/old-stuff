using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht
{
    class Struct
    {
        //Hieronder aanmaken struct voor gebruikerstypes
        public struct gebrType
        {
            public int id;
            public string omschrijving;

            public gebrType(int id, string omschrijving)
            {
                this.id = id;
                this.omschrijving = omschrijving;
            }
        }
        //Hieronder aanmaken List<> voor gebruikerstypes
        public static List<gebrType> gebrTypes = new List<gebrType>();
        //Hieronder functie invoeren gebruikerstypes in List<>
        public static void Init()
        {
            gebrType type1 = new gebrType(1, "superuser");
            gebrType type2 = new gebrType(2, "huisarts");
            gebrType type3 = new gebrType(3, "patient");
            gebrType type4 = new gebrType(4, "secretariaat");
            gebrType type5 = new gebrType(5, "specialist");

            gebrTypes.Add(type1);
            gebrTypes.Add(type2);
            gebrTypes.Add(type3);
            gebrTypes.Add(type4);
            gebrTypes.Add(type5);

        }
        //Hieronder functie die gebruikerstypes retourneert
        public static List<gebrType> getTypes()
        {
            return gebrTypes;
        }
        //Hieronder functie voor uitlezen gebruikers, met de verschillende functies
        public static void showUsers()
        {
            string id = "ID", vrNm = "Voornaam", tsVg = "Tussenvoegsel", acNm = "Achternaam", wPl = "Woonplaats", pTel = "Telefoon", eMail = "E-mail", geBr = "Gebruikersnaam";
            if (Login.gebrTypeId == 3)
            {
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("|  {0,-5} | {1,-10}       | {2,-10}  |  {3,-22}  |", id, vrNm, tsVg, acNm);
                Console.WriteLine("-------------------------------------------------------------------------");

                foreach (string user in Login.users)
                {
                    string[] asUser = user.Split('|');
                    Console.WriteLine("|  {0,-5} | {1,-10}       | {2,-10}     |  {3,-22}  |", int.Parse(asUser[0]) - 1, asUser[4], asUser[5], asUser[6]);
                }

                Console.WriteLine("-------------------------------------------------------------------------");
            }
            else if (Login.gebrTypeId != 3)
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("|  {7,-5}  |  {6,-22}  |  {0,-10} | {1,-10} | {2,-22}  |  {3,-22}  |  {4,-15}  | {5,-22}  |",vrNm, tsVg, acNm, wPl, pTel, eMail, geBr, id);
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                foreach (string user in Login.users)
                {
                    string[] asUser = user.Split('|');
                    Console.WriteLine("|  {7,-5}  |  {6,-22}  |  {0,-10} | {1,-10}    | {2,-22}  |  {3,-22}  |  {4,-15}  | {5,-22}  |", asUser[4], asUser[5], asUser[6], asUser[7], asUser[8], asUser[9], asUser[1], int.Parse(asUser[0]) - 1);
                }

                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
        }
        //Hieronder functie voor uitlezen gebruikerstypes
        public static void showUserTypes()
        {
            foreach (gebrType str in gebrTypes)
                Console.WriteLine(str.id + " - " + str.omschrijving);
        }
    }
}
