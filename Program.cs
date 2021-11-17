using System;
using System.Collections.Generic;
using System.IO;

namespace LA_1300
{
    class Program
    {
        static void Main(string[] args)
        {

            //Variablen
            List<string> name = new List<string>();
            List<string> priority1 = new List<string>();
            List<string> priority2 = new List<string>();
            List<string> priority3 = new List<string>();
            int position = 0;
            string textForSaving = "";
            string userInput;
            string coursFromFile;
            string pathToFileForCours = @"..\..\..\Kurse.csv";
            string allowedChars = "qwertzuiopasdfghjklyxcvbnmüöäQWERTZUIOPASDFGHJKLYXCVBNMÜÖÄ";
            char charChecker;
            string[] workshops = new string[0];
            //inputs[position] = userInput;
            string again = "j";

            // Welche kurse gibt es
            // if input nicht gleich array --> falsche eingabe
            while (true)
            {
                try
                {
                    Console.WriteLine("Wollen sie Eigene Kurse Einfügen? [y/n]");
                    userInput = Console.ReadLine();
                    if (userInput == "y")
                    {
                        //Custom courses
                        Console.WriteLine("Wie viele Kurse wollen sie machen? ");
                        string[] temp = new string[Convert.ToInt32(Console.ReadLine())];
                        for (int i = 0; i < temp.Length; i++)
                        {
                            Console.WriteLine("Bitte geben sie den " + (i + 1) + ". Kurs ein:");
                            temp[i] = Console.ReadLine();
                            if (i == 0)
                            {
                                textForSaving = temp[i];
                            }
                            else
                            {
                                textForSaving = textForSaving + "\r\n" + temp[i];
                            }
                        }
                        //Speichern in file
                        Console.WriteLine("Wollen sie Eigene Kurse Einfügen? [y/n]");
                        userInput = Console.ReadLine();
                        if (userInput == "y")
                        {
                            File.WriteAllText(pathToFileForCours, textForSaving);
                        }
                        workshops = temp;
                        break;
                    }
                    //Kurse laden
                    else if (userInput == "n")
                    {
                        coursFromFile = File.ReadAllText(pathToFileForCours);
                        string[] tempSecond = coursFromFile.Split("\r\n");
                        workshops = tempSecond;
                        break;
                    }
                    else
                    {
                        throw new Exception();
                    }
                    
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Ein Fehler ist aufgetreten, wiederholen Sie Ihre Eingabe.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            // Einleitung
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Herzlich willkommen auf unsere Seite.");

            while (true)
            {
                // Namen eingeben
                Console.WriteLine("Bitte geben Sie Ihren namen ein...");
                while (true)
                {
                    try
                    {

                        Console.Write("Vorname: ");
                        string firstname = Console.ReadLine();
                        for (int i = 0; i < firstname.Length; i++)
                        {
                            charChecker = firstname[i];
                            if (allowedChars.IndexOf(charChecker) < 0)
                            {
                                throw new Exception();
                            }
                        }

                        Console.Write("Nachname: ");
                        string lastname = Console.ReadLine();
                        for (int i = 0; i < lastname.Length; i++)
                        {
                            charChecker = lastname[i];
                            if (allowedChars.IndexOf(charChecker) < 0)
                            {
                                throw new Exception();
                            }
                        }

                        Console.WriteLine(firstname + "." + lastname + "@stud.bbbaden.ch");
                        name.Add(firstname + "." + lastname + "@stud.bbbaden.ch"); 
                        Console.ReadLine();
                        break;

                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Ein Fehler ist aufgetreten, wiederholen Sie Ihre Eingabe.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                // Kurs wählen
                Console.WriteLine("Sie können sich in drei Kurse einschreiben. Schreiben Sie ihren Favoriten als erstes.");
                Console.WriteLine("In welchen der folgenden Kurse möchten Sie sich einschreiben?");
                Console.WriteLine("");


                for (int i = 0; i < workshops.Length; i++)
                {
                    Console.WriteLine(i + 1 + ". " + workshops[i]);
                }
                Console.WriteLine("");

                priority1.Add(AskCours("Ihre erste Wahl ist: ", workshops));
                priority2.Add(AskCours("Ihre zweite Wahl ist: ", workshops));
                priority3.Add(AskCours("Ihre dritte Wahl ist: ", workshops));
                position++;


                Console.WriteLine("--- Möchten Sie weitere Teilnehmer angeben? [j/n]");
                again = Console.ReadLine();
                if (again == "n")
                {
                    int[] splitUp = new int[name.Count];
                    int countOfPersons;

                    // Erste einteilung
                    for (int i = 0; i < name.Count; i++)
                    {
                        for (int e = 0; e < workshops.Length; e++)
                        {
                            if (priority1[i] == workshops[e])
                            {
                                splitUp[i] = e;
                            }
                        }
                    }

                    //mehr als 20 Teilnehmer überprüfen und ändern
                    for (int i = 0; i < workshops.Length; i++)
                    {
                        countOfPersons = 0;

                        for (int e = 0; e < splitUp.Length; e++)
                        {
                            if (splitUp[e] == i)
                            {
                                countOfPersons++;
                            }
                        }

                        if (countOfPersons < 4 && countOfPersons != 0)
                        {
                            for (int e = 0; e < splitUp.Length; e++)
                            {
                                if (priority2[e] == workshops[i])
                                {
                                    splitUp[e] = i;
                                }
                            }
                        }

                        if (countOfPersons > 20)
                        {
                            for (int e = 0; e < splitUp.Length; e++)
                            {
                                if (splitUp[e] == i && countOfPersons > 20)
                                {
                                    for (int a = 0; a < workshops.Length; a++)
                                    {
                                        if (priority2[e] == workshops[a])
                                        {
                                            splitUp[i] = a;
                                        }
                                    }
                                    countOfPersons = countOfPersons - 1;
                                }
                            }
                        }

                    }

                    // Das Gleiche einfach mit dritter Priorität
                    for (int i = 0; i < workshops.Length; i++)
                    {
                        countOfPersons = 0;

                        for (int e = 0; e < splitUp.Length; e++)
                        {
                            if (splitUp[e] == i)
                            {
                                countOfPersons++;
                            }
                        }

                        if (countOfPersons < 4 && countOfPersons != 0)
                        {
                            for (int e = 0; e < splitUp.Length; e++)
                            {
                                if (priority3[e] == workshops[i])
                                {
                                    splitUp[e] = i;
                                }
                            }
                        }

                        if (countOfPersons > 20)
                        {
                            for (int e = 0; e < splitUp.Length; e++)
                            {
                                if (splitUp[e] == i && countOfPersons > 20)
                                {
                                    for (int a = 0; a < workshops.Length; a++)
                                    {
                                        if (priority3[e] == workshops[a])
                                        {
                                            splitUp[i] = a;
                                        }
                                    }
                                    countOfPersons = countOfPersons - 1;
                                }
                            }
                        }

                    }

                    //Auflissten
                    for (int i = 0; i < name.Count; i++)
                    {
                        Console.WriteLine(workshops[i] + ":");
                        for (int e = 0; e < splitUp.Length; e++)
                        {
                            if (splitUp[e] == i)
                            {
                                Console.WriteLine(name[e]);
                            }
                        }                       
                    }
                    break;
                }
            }
        }

        //Kursnachfrage
        public static string AskCours(string question, string[] allCourses)
        {
            int counter = 0;
            string cours;
            while (true)
            {
                try
                {
                    counter = 0;
                    Console.Write(question);
                    cours = Console.ReadLine();

                    for (int i = 0; i < allCourses.Length; i++)
                    {
                        if (cours != allCourses[i])
                        {
                            counter++;
                        }
                    }

                    if (counter == allCourses.Length)
                    {
                        throw new Exception();
                    }

                    break;
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Dies ist kein Kurs.");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
            return cours;
        }
    }
}
