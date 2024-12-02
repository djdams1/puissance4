/*ETML
 *Auteur : Killian Ganne
 *Date : 05.11.2024
 *Description : création d'un puissance 4 ou l'on peut choisir la taille du tables
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
            const int LINE_MAX = 12;  //définie le max de ligne
            const int LINE_MINE = 6;  //définie le minimum de ligne
            const int COLOMN_MAX = 15;//définie le max de colonne
            const int COLOMN_MINE = 7;//définie le minimum de colonne
            const int CHANGE = 4;    // définie de combien on se déplace 

            bool ingame = true; //booléen pour savoir si on est en jeu 

            int lineValue;   //stoque le nombre de ligne pour le tableau
            int columnValue; //stoque le nombre de colonne pour le tableau
            int left = 10;   //définie la position du joueur à gauche au début, augmantera par la suite
            int top = 7;     //définie la position du joueur depuis le haut du tableau au début, augmantera par la suite
            int turne = 2;   //sert a savoir su cest le joueur rouge ou jaune
            int player = 1;  //sert a mettre le bon mumero dans le tableau

            string line = "";  //récupère le nombre de ligne
            string column = "";//récupère le nombre de colomn
            string input = ""; //récupère se que le joueur écrit


            Console.Title = "Puissance4"; //met le titre

             ShowTitle(); //appelle la méthode pour mettre le titre de bienvenue

            //affiche le texte qui explique le nombre de ligne max et min et combien de ligne le joueur veut
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
                //si il ne peut pas convertir line en nombre dire que c'est pas dans les norme et reposer la question.
                if (!int.TryParse(line, out lineValue)) //mettre line dasn lineValue
                {
                    Console.WriteLine("\nCe n'est pas un nombre\n");
                    Console.Write("Votre valuer : ");
                    line = Console.ReadLine();
                }
                if (lineValue < LINE_MINE || lineValue > LINE_MAX) //si le nombre est trop grand ou trop petit reposer la question
                {
                    Console.WriteLine("\nIl ne rentre pas dans les normes\n");
                    Console.Write("Votre valuer : ");
                    line = Console.ReadLine();
                }
            }
            while (lineValue == 0 || lineValue < LINE_MINE || lineValue > LINE_MAX); //reposer la question tant que le nombre rentre pas dans les normes


            //affiche le texte qui explique le nombre de colonne max et min et combien de colonne le joueur veut  
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
                //si il ne peut pas convertir colomn en nombre dire que c'est pas dans les norme et reposer la question.
                if (!int.TryParse(column, out columnValue))
                {
                    Console.WriteLine("\nCe n'est pas un nombre\n");
                    Console.Write("Votre valuer : ");
                    column = Console.ReadLine();
                }
                if (columnValue < COLOMN_MINE || columnValue > COLOMN_MAX) //si le nombre est trop grand ou trop petit reposer la question
                {
                    Console.WriteLine("\nIl ne rentre pas dans les normes\n");
                    Console.Write("Votre valuer : ");
                    column = Console.ReadLine();
                }
            }
            while (columnValue == 0 || columnValue < COLOMN_MINE || columnValue > COLOMN_MAX); //reposer la question tant que le nombre rentre pas dans les normes


            Console.Clear();

            ShowTitle();

            ShowTable(columnValue, lineValue);

            ShowRules(columnValue);

            //rend le cursor invisible
            Console.CursorVisible = false;

            // Placer le curseur à la nouvelle position et afficher le bloc
            Showplayer(left, top);

            //  crée le tableau
            int[,] tables = new int[lineValue, columnValue];


            //  boucle temps que le jeu est en cours
            while (ingame)
            {
                // Lire la touche pressée
                var key = Console.ReadKey(true);

                //  si le nombre est paire c'est le player 1 qui joue sinon c'est le 2
                if (turne % 2 == 0)
                {
                    player = 1;
                }
                else
                {
                    player = 2;
                }

                //mets la couleur pour le joueur, soit rouge pour le joueur 1 et jaune pour le joueur 2
                Console.ForegroundColor=Color(turne);

                // Effacer le bloc à la position actuelle
                Console.SetCursorPosition(left, top);
                Console.Write(" ");  // Effacer le précédent bloc

                // Vérifier si la touche fléchée droite a été pressée
                if (key.Key == ConsoleKey.RightArrow)
                {
                    left += CHANGE;  // Déplacement vers la droite
                }
                // Vérifier si la touche fléchée gauche a été pressée
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    left -= CHANGE;  // Déplacement vers la gauche
                }
                // Si la touche espace ou entrée est pressée, placer un jeton dans la colonne sélectionnée
                else if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter)
                {
                    // Calculer la colonne à partir de la position horizontale actuelle
                    int col = (left - 10) / CHANGE;

                    // Parcourir les lignes de la dernière vers la première pour placer la pièce
                    for (int row = lineValue - 1; row >= 0; row--)
                    {
                        // Vérifier si la case est vide (0 signifie case libre)
                        if (tables[row, col] == 0)
                        {
                            // Placer la pièce du player dans la case
                            tables[row, col] = player;

                            // Calculer la position de la pièce à afficher dans la console
                            int positionTop = 11 + (row * 2);  // Calcul de la position verticale
                            int positionLeft = 10 + (col * CHANGE);  // Calcul de la position horizontale

                            // Déplacer le curseur à la position calculée et afficher la pièce
                            Console.SetCursorPosition(positionLeft, positionTop);
                            Console.Write('█');


                            // Passer au tour suivant
                            turne++;


                            Console.ForegroundColor = Color(turne);// Mettre à jour la couleur du player
                            Showplayer(left, top);  // Afficher la position actuelle du player

                            // Vérification des 4 alignéss
                            // Vérification des 4 alignéss
                            if (VerifierQuatreAlignes(lineValue, columnValue, tables))
                            {
                                // Si un player a gagné, afficher un message et mettre fin au jeu
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
                if (left >= columnValue * CHANGE + 10)  // Si on dépasse la fin de la ligne
                {
                    left = 10;  // Revenir au début de la ligne
                }

                // Si on dépasse le début de la ligne, revenir à la fin de la ligne
                if (left < 10)  // Si on dépasse le début de la ligne (avant la position de départ)
                {
                    left = columnValue * CHANGE + 10 - CHANGE;  // Revenir à la fin de la ligne
                }

                Console.ForegroundColor = Color(turne);
                // Placer le curseur à la nouvelle position et afficher le bloc
                Showplayer(left, top);
            }
            

        }
        /// <summary>
        ///  sert a quitter le jeu ou à recommencer
        /// </summary>
        /// <param name="input">caractère qu'on écrit </param>
        /// <param name="lineValue">nombre de ligne</param>
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
        /// <param name="tables">le tableau à deux dimmension </param>
        /// <returns></returns>
        static bool VerifierQuatreAlignes(int lineValue, int columnValue, int[,] tables)
        {
            // Vérification des lignes (horizontalement)
            for (int i = 0; i < lineValue; i++)
            {
                for (int j = 0; j < columnValue - 3; j++)
                {
                    if (tables[i, j] != 0 && tables[i, j] == tables[i, j + 1] && tables[i, j] == tables[i, j + 2] && tables[i, j] == tables[i, j + 3])
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
                    if (tables[i, j] != 0 && tables[i, j] == tables[i + 1, j] && tables[i, j] == tables[i + 2, j] && tables[i, j] == tables[i + 3, j])
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
                    if (tables[i, j] != 0 && tables[i, j] == tables[i + 1, j + 1] && tables[i, j] == tables[i + 2, j + 2] && tables[i, j] == tables[i + 3, j + 3])
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
                    if (tables[i, j] != 0 && tables[i, j] == tables[i - 1, j + 1] && tables[i, j] == tables[i - 2, j + 2] && tables[i, j] == tables[i - 3, j + 3])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}