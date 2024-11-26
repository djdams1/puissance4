
// auteur : Damien Rochat
// date   : 05.11.2024
// lieu   : ETML VENNES
// description : c'est un puissance 4 ou les joueurs peuvent changer le nombre de casses


using System;
using System.Diagnostics;
using System.Threading; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puissance4_Rochat_Damien
{
    internal class Program
    {

        static void Main(string[] args)
        {



            // constante pour definire le max de colonne
            const byte MAX_COLONNE = 16;
            // constante pour definire le min de colonne
            const byte MIN_COLONE = 6;
            // constante pour definire le max de Ligne
            const byte MAX_LIGNE = 13;
            // constante pour definire le min de Ligne
            const byte MIN_LIGNE = 5;
            // constante pour definire le nombre de casse a changer
            const int CHANGE = 4;


            // Booléin utile pour quitté la premier boucle 
            bool isok = false;
            // Booléin utile pour quitté la deuxième boucle 
            bool isok2 = false;
            // Booléin utile pour savoir si je suis en game
            bool ingame = true;


            //  Récuperer ce que le joueur écris
            string input = "";


            //  sert dans la creation du tableau (== a colonne)
            int i = 1;
            //  sert dans la creation du tableau (== a ligne)
            int x = 1;
            //  sert a savoir su cest le joueur rouge ou jaune
            int tour = 2;
            //  sert a stoqué le nombre de ligne pour la construction
            int ligne = 0;
            //  sert a stoqué le nombre de colonne pour la construction
            int colonne = 0;
            //  déclaration des varibale de possition pour la metode ShowPlayer et pour déplacer le joueur
            int left = 10;
            int top = 7;
            int joueur = 1;


            //  sert a afficher le message en haut
            ShowTitle();



            //  sert a afficher le massage de consigne pour les lignes
            Console.WriteLine("Merci d'entrer le nombre de lignes");
            Console.Write("La valeur doit être plus grande que ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(MIN_LIGNE);
            Console.ResetColor();
            Console.Write(" est plus petite que ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(MAX_LIGNE);
            Console.ResetColor();




            //   chek si le nombre dinner est dans les normes ou pas
            while (!isok)
            {
                Console.Write("Votre Valeur : ");
                input = Console.ReadLine();
                // && = et 
                if (int.TryParse(input, out ligne) && ligne > MIN_LIGNE && ligne < MAX_LIGNE)
                {
                    isok = true;
                }
                else
                {
                    if (!int.TryParse(input, out ligne))
                    {
                        Console.WriteLine("\nEntrez un chiffre valide.");
                    }
                    else
                    {
                        Console.Write("La valeur doit être plus grande que ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(MIN_LIGNE);
                        Console.ResetColor();
                        Console.Write(" est plus petite que ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(MAX_LIGNE);
                        Console.ResetColor();
                    }
                    Console.ResetColor();
                }
            }
            Console.Clear();
            //  sert a afficher le message en haut
            ShowTitle();
            //  sert a afficher le massage de consigne pour les colonnes
            Console.WriteLine("Merci d'entrer le nombre de colonne");
            Console.Write("La valeur doit être plus grande que ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(MIN_COLONE);
            Console.ResetColor();
            Console.Write(" est plus petite que ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(MAX_COLONNE);
            Console.ResetColor();

            //   chek si le nombre dinner est dans les normes ou pas
            while (!isok2)
            {
                Console.Write("Votre Valeur : ");
                input = Console.ReadLine();
                if (int.TryParse(input, out colonne) && colonne > MIN_COLONE && colonne < MAX_COLONNE)
                {
                    isok2 = true;
                }
                else
                {
                    if (!int.TryParse(input, out colonne))
                    {
                        Console.WriteLine("\nEntrez un chiffre valide.");
                    }
                    else
                    {
                        Console.WriteLine("Merci d'entrer le nombre de colonne");
                        Console.Write("La valeur doit être plus grande que ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(MIN_COLONE);
                        Console.ResetColor();
                        Console.Write(" est plus petite que ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(MAX_COLONNE);
                        Console.ResetColor();
                    }
                    Console.ResetColor();
                }


            }
            Console.Clear();

            int[,] tableau = new int[ligne, colonne];


            for (i = 0; i < ligne; i++)
            {
                for (int j = 0; j < colonne; j++)
                {
                    tableau[i, j] = 0;
                }
            }
;
            ShowTitle();
            CreatTable(x, i, colonne, ligne, tableau);
            color(tour);
            ShowPlayer(left, top);



            Console.SetCursorPosition((6 * colonne) + 5, 7);
            Console.Write("Mode d'utilisation");
            Console.SetCursorPosition((6 * colonne) + 5, 8);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("------------------");
            Console.ResetColor();
            Console.SetCursorPosition((6 * colonne) + 5, 9);
            Console.Write("Déplacement \t Touches directionnelles");
            Console.SetCursorPosition((6 * colonne) + 5, 10);
            Console.Write("Tir \t  \t Touches space ou enter");
            Console.SetCursorPosition((6 * colonne) + 5, 11);
            Console.Write("Qutter \t Touches Escpae");
            Console.SetCursorPosition((6 * colonne) + 5, 13);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Joueur 1 : █\t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" Joueur 2 : █");
            Console.ForegroundColor = ConsoleColor.Magenta;


            while (ingame)
            {
                if (tour % 2 == 0)
                {
                    joueur = 1;
                }
                else
                {
                    joueur = 2;
                }

                var key = Console.ReadKey(intercept: true);
                color(tour);

                Console.SetCursorPosition(left, top);
                Console.Write(" ");

                if (key.Key == ConsoleKey.RightArrow)
                {
                    left += CHANGE;
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    left -= CHANGE;
                }
                else if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter)
                {
                    int col = (left - 10) / CHANGE;

                    for (int row = ligne - 1; row >= 0; row--)
                    {
                        if (tableau[row, col] == 0)
                        {
                            tableau[row, col] = joueur;

                            int positionTop = 11 + (row * 2);
                            int positionLeft = 10 + (col * CHANGE);

                            Console.SetCursorPosition(positionLeft, positionTop);
                            Console.Write('█');
                            tour++;
                            color(tour);
                            ShowPlayer(left, top);
                            // Vérification des 4 alignéss
                            // Vérification des 4 alignéss
                            if (CheckWin(ligne, colonne, tableau))
                            {
                                Console.SetCursorPosition(8, (ligne * 3) + 4);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Le Joueur {joueur} a gagné !");
                                ingame = false; // Fin de la partie
                                ExitGame(input, ligne);
                                break;
                            }
                            break;
                        }
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    ExitGame(input, ligne);
                }

                if (left >= colonne * CHANGE + 10)
                {
                    left = 10;
                }
                // Si on dépasse le début de la ligne, revenir à la fin de la ligne
                if (left < 10)
                {
                    left = colonne * CHANGE + 10 - CHANGE;
                }
                color(tour);
                // Placer le curseur à la nouvelle position et afficher le bloc
                ShowPlayer(left, top);
            }
            Console.Read();
        }
        /// <summary>
        /// modele pour mettre partout
        /// </summary>
        static void ShowTitle()
        {
            Console.Title = "Puissance 4";
            Console.SetCursorPosition(0, 0);
            // Les couleurs de dégradé (par exemple, du rouge au violet)
            ConsoleColor[] gradientColors = new ConsoleColor[] {
            ConsoleColor.Red, ConsoleColor.DarkRed, ConsoleColor.DarkMagenta,
            ConsoleColor.Magenta
        };
            // Affichage du titre avec un dégradé
            string[] lines = new string[]
            {
        "\t╔══════════════════════════════════════╗",
        "\t║ Bienvenue dans le jeu du puissance 4 ║",
        "\t║     Réalisé par Damien Rochat        ║",
        "\t╚══════════════════════════════════════╝"
            };
            int lineIndex = 0;
            foreach (string line in lines)
            {
                Console.SetCursorPosition(0, lineIndex);
                // Applique un dégradé en fonction de l'index de la ligne
                for (int i = 0; i < line.Length; i++)
                {
                    Console.ForegroundColor = gradientColors[(i * gradientColors.Length) / line.Length];
                    Console.Write(line[i]);
                }
                Console.ResetColor();
                lineIndex++;
            }
            Console.SetCursorPosition(0, 6);
        }
        /// <summary>
        /// Sert a afficher le joueur dans la case
        /// </summary>
        /// <param name="left">permet de decaler a gauche le joueur</param>
        /// <param name="top">permet de decaler vers le bas le joueur</param>
        static void ShowPlayer(int left, int top)
        {
            //écris le █ au cordonée defini par les variable
            Console.SetCursorPosition(left, top);
            Console.Write("█");
            Console.CursorVisible = false;
        }
        /// <summary>
        /// sert a quitter le jeu
        /// </summary>
        /// <param name="input">sert a recuperer ce que le user écris</param>
        /// <param name="ligne">sert a connaitre le nombre de ligne</param>
        static void ExitGame(string input, int ligne)
        {
            // écris un petit message pour recommencer
            Console.SetCursorPosition(8, (ligne * 3) + 5);
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
                Main(null);
            }
            // regarde si on mets un N
            else if (input == "n" || input == "N")
            {
                // écris un petit message pour quitter
                Console.SetCursorPosition(8, (ligne * 3) + 6);
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
                    Console.SetCursorPosition(8, (ligne * 3) + 5);
                    Console.Write("                                    ");
                    Console.SetCursorPosition(8, (ligne * 3) + 6);
                    Console.Write("                                    ");
                }
            }
        }
        /// <summary>
        /// Permete de créé la table
        /// </summary>
        /// <param name="x">nombre d'iteration pour les lignes</param>
        /// <param name="i">nombre d'iteration pour les colonnes</param>
        /// <param name="colonne">nombre de colonne</param>
        /// <param name="ligne">nombre de ligne</param>
        static void CreatTable(int x, int i, int colonne, int ligne, int[,] tableau)
        {
            Console.Write("\t╔");
            for (i = 1; i < colonne; i++)
            {
                Console.Write("═══╦");
            }
            Console.WriteLine("═══╗");
            Console.Write("\t║");
            for (i = 1; i < colonne; i++)
            {
                Console.Write("   ║");
            }
            Console.WriteLine("   ║");

            Console.Write("\t╚");
            for (i = 1; i < colonne; i++)
            {
                Console.Write("═══╩");
            }
            Console.WriteLine("═══╝");


            Console.WriteLine("");
            Console.Write("\t╔");

            for (i = 1; i < colonne; i++)
            {
                Console.Write("═══╦");
            }
            Console.WriteLine("═══╗");

            for (x = 0; x < ligne - 1; x++)
            {
                Console.Write("\t║   ");
                for (i = 0; i < colonne; i++)
                {
                    Console.Write("║   ");
                }
                Console.Write("\n\t╠═══╬");
                for (i = 2; i < colonne; i++)
                {
                    Console.Write("═══╬");
                }
                Console.WriteLine("═══╣");
            }
            Console.Write("\t║");
            for (i = 1; i < colonne; i++)
            {
                Console.Write("   ║");
            }
            Console.WriteLine("   ║");
            Console.Write("\t╚");
            for (i = 1; i < colonne; i++)
            {
                Console.Write("═══╩");
            }
            Console.WriteLine("═══╝");
        }
        /// <summary>
        /// sert a changer la couleur en fonction du joueur
        /// </summary>
        /// <param name="tour">sert a savoir si cest le tour 1 ou 2</param>
        static void color(int tour)
        {
            if (tour % 2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

            }
        }
        /// <summary>
        /// check dans le tableau si il y a 4 nombre les meme l'un a coté des autre
        /// </summary>
        /// <param name="ligne">ligne entrer par le user</param>
        /// <param name="colonne">colonne entrer par le user</param>
        /// <param name="tableau">c'est le tableau ou on stock la ou il y a les pion des joueurs</param>
        /// <returns></returns>
        static bool CheckWin(int ligne, int colonne, int[,] tableau)
        {
            // Vérification des lignes (horizontalement)
            for (int i = 0; i < ligne; i++)
            {
                for (int j = 0; j < colonne - 3; j++)
                {
                    if (tableau[i, j] != 0 && tableau[i, j] == tableau[i, j + 1] && tableau[i, j] == tableau[i, j + 2] && tableau[i, j] == tableau[i, j + 3])
                    {
                        return true;
                    }
                }
            }

            // Vérification des colonnes (verticalement)
            for (int i = 0; i < ligne - 3; i++)
            {
                for (int j = 0; j < colonne; j++)
                {
                    if (tableau[i, j] != 0 && tableau[i, j] == tableau[i + 1, j] && tableau[i, j] == tableau[i + 2, j] && tableau[i, j] == tableau[i + 3, j])
                    {
                        return true;
                    }
                }
            }

            // Vérification des diagonales descendantes
            for (int i = 0; i < ligne - 3; i++)
            {
                for (int j = 0; j < colonne - 3; j++)
                {
                    if (tableau[i, j] != 0 && tableau[i, j] == tableau[i + 1, j + 1] && tableau[i, j] == tableau[i + 2, j + 2] && tableau[i, j] == tableau[i + 3, j + 3])
                    {
                        return true;
                    }
                }
            }

            // Vérification des diagonales montantes
            for (int i = 3; i < ligne; i++)
            {
                for (int j = 0; j < colonne - 3; j++)
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
