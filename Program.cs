/*ETML
 *Auteur : Killian Ganne
 *Date : 05.11.2024
 *Description : création d'un puissance 4 ou l'on peut choisir la taille du tableau
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace puissance4_KillianGanne
{
    internal class Program
    {


        static void Main(string[] args)
        {
            const int lineMax = 12;
            const int lineMine = 6;
            const int ColonMax = 15;
            const int ColonMine = 7;
            const int change = 4; // Le nombre de caractères par déplacement

            bool ingame = true;

            int lineValue;
            int columnValue;
            int left = 10;
            int top = 6;
            int turne = 2;
            int player = 1;

            string line = ""; //demande utilisateur
            string column = "";//demande utilisateur
            string input = "";


            Console.Title = "Puissance4"; //met le titre

             ShowTitle(); //appelle la méthode pour mettre le titre de bienvenue

            //affiche le texte qui explique le nombre de ligne max et min et combien de ligne 
            Console.Write("Merci d'entrer le nombre de ligne\nLa valueur doit être plus grande que ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("5 ");
            Console.ResetColor();
            Console.Write("est plus petite que ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("13");
            Console.ResetColor();
            Console.Write("Votre valuer : ");

            line = Console.ReadLine();


            do
            {
                if (!int.TryParse(line, out lineValue))
                {
                    Console.WriteLine("\nCe n'est pas un nombre\n");
                    Console.Write("Votre valuer : ");
                    line = Console.ReadLine();
                }
                if (lineValue < lineMine || lineValue > lineMax)
                {
                    Console.WriteLine("\nIl ne rentre pas dans les normes\n");
                    Console.Write("Votre valuer : ");
                    line = Console.ReadLine();
                }
            }
            while (lineValue == 0 || lineValue < lineMine || lineValue > lineMax);



            Console.Write("\nMerci d'entrer le nombre de column\nLa valueur doit être plus grande que ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("6 ");
            Console.ResetColor();
            Console.Write("est plus petite que ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("16");
            Console.ResetColor();
            Console.Write("Votre valuer : ");

            column = Console.ReadLine();


            do
            {
                if (!int.TryParse(column, out columnValue))
                {
                    Console.WriteLine("\nCe n'est pas un nombre\n");
                    Console.Write("Votre valuer : ");
                    column = Console.ReadLine();
                }
                if (columnValue < ColonMine || columnValue > ColonMax)
                {
                    Console.WriteLine("\nIl ne rentre pas dans les normes\n");
                    Console.Write("Votre valuer : ");
                    column = Console.ReadLine();
                }
            }
            while (columnValue == 0 || columnValue < ColonMine || columnValue > ColonMax);


            Console.Clear();

            ShowTitle();

            ShowTable(columnValue, lineValue);

            ShowRules(columnValue);

            Console.CursorVisible = false;

            // Placer le curseur à la nouvelle position et afficher le bloc
            Showplayer(left, top);

            int[,] tableau = new int[lineValue, columnValue];


             

            while (ingame)
            {
                // Lire la touche pressée
                var key = Console.ReadKey(true);

                if (turne % 2 == 0)
                {
                    player = 1;
                }
                else
                {
                    player = 2;
                }

                Console.ForegroundColor = ConsoleColor.Red;

                Console.ForegroundColor=Color(turne);

                // Effacer le bloc à la position actuelle
                Console.SetCursorPosition(left, top);
                Console.Write(" ");  // Effacer le précédent bloc

                // Vérifier si la touche fléchée droite a été pressée
                if (key.Key == ConsoleKey.RightArrow)
                {
                    left += change;  // Déplacement vers la droite
                }
                // Vérifier si la touche fléchée gauche a été pressée
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    left -= change;  // Déplacement vers la gauche
                }
                else if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter)
                {

                    int col = (left - 10) / change;


                    for (int row = lineValue - 1; row >= 0; row--)
                    {
                        if (tableau[row, col] == 0)
                        {
                            tableau[row, col] = player;


                            int positionTop = 10 + (row * 2);
                            int positionLeft = 10 + (col * change);


                            Console.SetCursorPosition(positionLeft, positionTop);
                            Console.Write('█');



                            turne++;


                            Console.ForegroundColor = Color(turne);
                            Showplayer(left, top);

                            // Vérification des 4 alignéss
                            // Vérification des 4 alignéss
                            if (VerifierQuatreAlignes(lineValue, columnValue, tableau))
                            {
                                Console.SetCursorPosition(8, (lineValue * 3) + 2);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Le Joueur {player} a gagné !");

                                ExitGame(input, lineValue);

                                ingame = false; // Fin de la partie
                                break;
                            }
                            break;
                        }
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    ExitGame(input, lineValue);
                }

                // Si on dépasse la fin de la ligne, revenir au début de la ligne
                if (left >= columnValue * change + 10)  // Si on dépasse la fin de la ligne
                {
                    left = 10;  // Revenir au début de la ligne
                }

                // Si on dépasse le début de la ligne, revenir à la fin de la ligne
                if (left < 10)  // Si on dépasse le début de la ligne (avant la position de départ)
                {
                    left = columnValue * change + 10 - change;  // Revenir à la fin de la ligne
                }

                Console.ForegroundColor = Color(turne);
                // Placer le curseur à la nouvelle position et afficher le bloc
                Showplayer(left, top);
            }
            

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="lineValue"></param>
        static void ExitGame(string input, int lineValue)
        {
            // écris un petit message pour recommencer
            Console.SetCursorPosition(8, (lineValue * 3) + 5);
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Voulez-vous recommencer ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("O/N :");
            input = Console.ReadLine();
            // regarde si on mets un o
            if (input == "o" || input == "O")
            {
                // relance le programme au main
                Console.Clear();
                Console.ResetColor();
                Main(null);
            }
            // regarde si on mets un N
            else if (input == "n" || input == "N")
            {
                // écris un petit message pour quitter
                Console.SetCursorPosition(8, (lineValue * 3) + 6);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Voulez-vous quitté ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O/N :");
                input = Console.ReadLine();
                // regarde si on mets un O
                if (input == "o" || input == "O")
                {
                    //quitte le programme apres 2s
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                }
                else
                {
                    //clear les 2 ligne ou il peux y avoir du texte
                    Console.SetCursorPosition(8, (lineValue * 3) + 5);
                    Console.Write("                                    ");
                    Console.SetCursorPosition(8, (lineValue * 3) + 6);
                    Console.Write("                                    ");
                }
            }
        }
        

        /// <summary>
        /// définie la couleur selon le joueur
        /// </summary>
        /// <param name="turne">le nombre de tour du jeu</param>
        /// <returns></returns>
        static ConsoleColor Color(int turne)
        {
            ConsoleColor colorBase = ConsoleColor.White;

            colorBase = (turne % 2 == 0) ? ConsoleColor.Red : ConsoleColor.Yellow;

            return colorBase;
        }


        /// <summary>
        /// affiche le titre
        /// </summary>
        static void ShowTitle()
        {
            Console.WriteLine("\t╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("\t║          Bienvenue dans le jeu Puissance 4           ║");
            Console.WriteLine("\t║              Réalisé par Killian Ganne               ║");
            Console.WriteLine("\t║                     04.11.2024                       ║");
            Console.WriteLine("\t╚══════════════════════════════════════════════════════╝\n");
        }


        /// <summary>
        /// affiche le joueur
        /// </summary>
        /// <param name="left">position à gauche</param>
        /// <param name="top">position en haut</param>
        static void Showplayer(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("█");
        }

        /// <summary>
        /// affiche le tableau
        /// </summary>
        /// <param name="columnValue">le nombre de colonne</param>
        /// <param name="lineValue">le nombre de ligne</param>
        static void ShowTable(int columnValue, int lineValue)
        {
            int i = 0;
            int x = 0;

            Console.Write("\t╔═══╦");
            for (i = 2; i < columnValue; i++)
            {
                Console.Write("═══╦");
            }
            Console.WriteLine("═══╗");


            Console.Write("\t║   ");
            for (i = 0; i < columnValue; i++)
            {
                Console.Write("║   ");
            }

            Console.Write("\n\t╚═══╩");
            for (i = 2; i < columnValue; i++)
            {
                Console.Write("═══╩");
            }
            Console.WriteLine("═══╝\n");



            Console.Write("\t╔═══╦");
            for (i = 2; i < columnValue; i++)
            {
                Console.Write("═══╦");
            }
            Console.WriteLine("═══╗");


            for (x = 1; x < lineValue; x++)
            {

                Console.Write("\t║   ");
                for (i = 0; i < columnValue; i++)
                {
                    Console.Write("║   ");
                }

                Console.Write("\n\t╠═══╬");
                for (i = 2; i < columnValue; i++)
                {
                    Console.Write("═══╬");
                }
                Console.WriteLine("═══╣");
            }

            Console.Write("\t║   ");
            for (i = 0; i < columnValue; i++)
            {
                Console.Write("║   ");
            }


            Console.Write("\n\t╚═══╩");
            for (i = 2; i < columnValue; i++)
            {
                Console.Write("═══╩");
            }
            Console.WriteLine("═══╝");
        }

        /// <summary>
        /// affiche le paragraphe de règle à côté du tableau
        /// </summary>
        /// <param name="columnValue">le nombre de colonne</param>
        static void ShowRules(int columnValue)
        {

            Console.SetCursorPosition((6 * columnValue) + 5, 7);
            Console.Write("Mode d'utilisation");
            Console.SetCursorPosition((6 * columnValue) + 5, 8);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("------------------");
            Console.ResetColor();
            Console.SetCursorPosition((6 * columnValue) + 5, 9);
            Console.Write("Déplacement \t Touches directionnelles");
            Console.SetCursorPosition((6 * columnValue) + 5, 10);
            Console.Write("Tir \t  \t Touches space ou enter");
            Console.SetCursorPosition((6 * columnValue) + 5, 11);
            Console.Write("Qutter \t Touches Escpae");
            Console.SetCursorPosition((6 * columnValue) + 5, 13);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Joueur 1 : █\t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" Joueur 2 : █");
            Console.ForegroundColor = ConsoleColor.Red;
        }

        /// <summary>
        /// vérifie si un joueur gagne
        /// </summary>
        /// <param name="lineValue">nombre de ligne</param>
        /// <param name="columnValue">nombre de colonne</param>
        /// <param name="tableau">le tableau à deux dimmension </param>
        /// <returns></returns>
        static bool VerifierQuatreAlignes(int lineValue, int columnValue, int[,] tableau)
        {
            // Vérification des lignes (horizontalement)
            for (int i = 0; i < lineValue; i++)
            {
                for (int j = 0; j < columnValue - 3; j++)
                {
                    if (tableau[i, j] != 0 && tableau[i, j] == tableau[i, j + 1] && tableau[i, j] == tableau[i, j + 2] && tableau[i, j] == tableau[i, j + 3])
                    {
                        return true;
                    }
                }
            }

            // Vérification des colonnes (verticalement)
            for (int i = 0; i < lineValue - 3; i++)
            {
                for (int j = 0; j < columnValue; j++)
                {
                    if (tableau[i, j] != 0 && tableau[i, j] == tableau[i + 1, j] && tableau[i, j] == tableau[i + 2, j] && tableau[i, j] == tableau[i + 3, j])
                    {
                        return true;
                    }
                }
            }

            // Vérification des diagonales descendantes
            for (int i = 0; i < lineValue - 3; i++)
            {
                for (int j = 0; j < columnValue - 3; j++)
                {
                    if (tableau[i, j] != 0 && tableau[i, j] == tableau[i + 1, j + 1] && tableau[i, j] == tableau[i + 2, j + 2] && tableau[i, j] == tableau[i + 3, j + 3])
                    {
                        return true;
                    }
                }
            }

            // Vérification des diagonales montantes
            for (int i = 3; i < lineValue; i++)
            {
                for (int j = 0; j < columnValue - 3; j++)
                {
                    if (tableau[i, j] != 0 && tableau[i, j] == tableau[i - 1, j + 1] && tableau[i, j] == tableau[i - 2, j + 2] && tableau[i, j] == tableau[i - 3, j + 3])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}