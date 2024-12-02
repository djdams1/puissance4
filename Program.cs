
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
            const byte MAX_COLUMN = 16;
            // constante pour definire le min de colonne
            const byte MIN_COLUMN = 6;
            // constante pour definire le max de Ligne
            const byte MAX_LINE = 13;
            // constante pour definire le min de Ligne
            const byte MIN_LINE = 5;
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


            
            //  sert a savoir su cest le joueur rouge ou jaune
            int turne = 2;
            //  sert a stoqué le nombre de ligne pour la construction
            int line = 0;
            //  sert a stoqué le nombre de colonne pour la construction
            int column = 0;
            //  déclaration des varibale de possition pour la metode ShowPlayer et pour déplacer le joueur depuis la gauche
            int left = 10;
            //  déclaration des varibale de possition pour la metode ShowPlayer et pour déplacer le joueur depuis le haut
            int top = 7;
            //  sert a mettre le bon mumero dans le tableau
            int player = 1;
            //  sert a calculer la position vartical 
            int positionLeft;
            //  sert a calculer la position horizontal 
            int positionTop;
            int col;


            //  sert a afficher le message en haut
            ShowTitle();



            //  sert a afficher le massage de consigne pour les lines
            Console.WriteLine("Merci d'entrer le nombre de lignes");
            Console.Write("La valeur doit être plus grande que ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(MIN_LINE);
            Console.ResetColor();
            Console.Write(" est plus petite que ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(MAX_LINE);
            Console.ResetColor();
            //   chek si le nombre donner est dans les normes ou pas
            while (!isok)
            {
                Console.Write("Votre Valeur : ");
                input = Console.ReadLine();
                // && = et 
                if (int.TryParse(input, out line) && line > MIN_LINE && line < MAX_LINE)
                {
                    isok = true;
                }
                else
                {
                    if (!int.TryParse(input, out line))
                    {
                        Console.WriteLine("\nEntrez un chiffre valide.");
                    }
                    else
                    {
                        Console.Write("La valeur doit être plus grande que ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(MIN_LINE);
                        Console.ResetColor();
                        Console.Write(" est plus petite que ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(MAX_LINE);
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
            Console.Write(MIN_COLUMN);
            Console.ResetColor();
            Console.Write(" est plus petite que ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(MAX_COLUMN);
            Console.ResetColor();

            //   chek si le nombre donner est dans les normes ou pas
            while (!isok2)
            {
                Console.Write("Votre Valeur : ");
                input = Console.ReadLine();
                if (int.TryParse(input, out column) && column > MIN_COLUMN && column < MAX_COLUMN)
                {
                    isok2 = true;
                }
                else
                {
                    if (!int.TryParse(input, out column))
                    {
                        Console.WriteLine("\nEntrez un chiffre valide.");
                    }
                    else
                    {
                        Console.WriteLine("Merci d'entrer le nombre de colonne");
                        Console.Write("La valeur doit être plus grande que ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(MIN_COLUMN);
                        Console.ResetColor();
                        Console.Write(" est plus petite que ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(MAX_COLUMN);
                        Console.ResetColor();
                    }
                    Console.ResetColor();
                }
            }
            Console.Clear();

            //  init le tableau
            int[,] tableau = new int[line, column];

            //  affiche le message en haut
            ShowTitle();
            //  créé le tableau
            CreatTable(column, line, tableau);
            //  mets la couleur juste pour le premier player
            Console.ForegroundColor = Color(turne);
            // affiche le jeton
            ShowPlayer(left, top);


            //  affiche les regles
            Console.SetCursorPosition((6 * column) + 5, 7);
            Console.Write("Mode d'utilisation");
            Console.SetCursorPosition((6 * column) + 5, 8);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("------------------");
            Console.ResetColor();
            Console.SetCursorPosition((6 * column) + 5, 9);
            Console.Write("Déplacement \t Touches directionnelles");
            Console.SetCursorPosition((6 * column) + 5, 10);
            Console.Write("Tir \t  \t Touches space ou enter");
            Console.SetCursorPosition((6 * column) + 5, 11);
            Console.Write("Qutter \t Touches Escpae");
            Console.SetCursorPosition((6 * column) + 5, 13);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Joueur 1 : █\t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" Joueur 2 : █");
            Console.ForegroundColor = ConsoleColor.Magenta;

            //  boucle temps que le jeu est en cours
            while (ingame)
            {
                //  si le nombre est paire c'est le player 1 qui joue sinon c'est le 2
                if (turne % 2 == 0)
                {
                    player = 1;
                }
                else
                {
                    player = 2;
                }
                //  on mets dans key la touche que le player appuie
                var key = Console.ReadKey();

                //  mets la couleur juste pour le player
                Console.ForegroundColor = Color(turne);
                //  mets le curseur dans dans la casse en fenction de left et top
                Console.SetCursorPosition(left, top);
                //  efface l'encien jeton
                Console.Write(" ");

                // Si la touche flèche droite est pressée, déplacer le player vers la droite
                if (key.Key == ConsoleKey.RightArrow)
                {
                    left += CHANGE; // Déplacer le player de 4 unités vers la droite
                }
                // Si la touche flèche gauche est pressée, déplacer le player vers la gauche
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    left -= CHANGE; // Déplacer le player de 4 unités vers la gauche
                }
                // Si la touche espace ou entrée est pressée, placer un jeton dans la colonne sélectionnée
                else if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter)
                {
                    // Calculer la colonne à partir de la position horizontale actuelle
                    col = (left - 10) / CHANGE;

                    // Parcourir les lignes de la dernière vers la première pour placer la pièce
                    for (int row = line - 1; row >= 0; row--)
                    {
                        // Vérifier si la case est vide (0 signifie case libre)
                        if (tableau[row, col] == 0)
                        {
                            tableau[row, col] = player; // Placer la pièce du player dans la case

                            // Calculer la position de la pièce à afficher dans la console
                            positionTop = 11 + (row * 2); // Calcul de la position verticale
                            positionLeft = 10 + (col * CHANGE); // Calcul de la position horizontale

                            // Déplacer le curseur à la position calculée et afficher la pièce
                            Console.SetCursorPosition(positionLeft, positionTop);
                            Console.Write('█');
                            turne++; // Passer au tour suivant
                            Console.ForegroundColor = Color(turne);// Mettre à jour la couleur du player
                            ShowPlayer(left, top); // Afficher la position actuelle du player

                            // Vérification si un player a gagné (4 pièces alignées)
                            if (CheckWin(line, column, tableau))
                            {
                                // Si un player a gagné, afficher un message et mettre fin au jeu
                                Console.SetCursorPosition(8, (line * 3) + 4);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Le player {player} a gagné !");
                                ingame = false; // Fin de la partie
                                ExitGame(input, line); // Quitter le jeu
                                break; // Sortir de la boucle dès qu'un gagnant est trouvé
                            }
                            break; // Sortir de la boucle dès que la pièce est placée
                        }
                    }
                }
                // Si la touche Échap est pressée, quitter le jeu
                else if (key.Key == ConsoleKey.Escape)
                {
                    ExitGame(input, line); // Quitter le jeu
                }

                // Si la position horizontale du player dépasse la largeur, réinitialiser à la position de départ (column 1)
                if (left >= column * CHANGE + 10)
                {
                    left = 10; // Réinitialiser la position à la première colonne
                }
                // Si la position horizontale est inférieure à la première colonne, réinitialiser à la dernière colonne
                if (left < 10)
                {
                    left = column * CHANGE + 10 - CHANGE; // Réinitialiser à la dernière colonne
                }
                Console.ForegroundColor = Color(turne); ; // Mettre à jour la couleur du player
                // Afficher le player à la nouvelle position après chaque mouvement
                ShowPlayer(left, top);
            }
            Console.Read(); // Attendre que l'utilisateur appuie sur une touche avant de fermer le programme
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
        /// Sert a afficher le player dans la case
        /// </summary>
        /// <param name="left">permet de decaler a gauche le player</param>
        /// <param name="top">permet de decaler vers le bas le player</param>
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
        /// <param name="line">sert a connaitre le nombre de ligne</param>
        static void ExitGame(string input, int line)
        {
            // écris un petit message pour recommencer
            Console.SetCursorPosition(8, (line * 3) + 5);
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
                Console.SetCursorPosition(8, (line * 3) + 6);
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
                    //clear les 2 line ou il peux y avoir du texte
                    Console.SetCursorPosition(8, (line * 3) + 5);
                    Console.Write("                                    ");
                    Console.SetCursorPosition(8, (line * 3) + 6);
                    Console.Write("                                    ");
                }
            }
        }
        /// <summary>
        /// Permete de créé la table
        /// </summary>
        /// <param name="x">nombre d'iteration pour les lines</param>
        /// <param name="i">nombre d'iteration pour les columns</param>
        /// <param name="column">nombre de column</param>
        /// <param name="line">nombre de line</param>
        static void CreatTable( int column, int line, int[,] tableau)
        {
            int x, i;
            Console.Write("\t╔");
            for (i = 1; i < column; i++)
            {
                Console.Write("═══╦");
            }
            Console.WriteLine("═══╗");
            Console.Write("\t║");
            for (i = 1; i < column; i++)
            {
                Console.Write("   ║");
            }
            Console.WriteLine("   ║");

            Console.Write("\t╚");
            for (i = 1; i < column; i++)
            {
                Console.Write("═══╩");
            }
            Console.WriteLine("═══╝");


            Console.WriteLine("");
            Console.Write("\t╔");

            for (i = 1; i < column; i++)
            {
                Console.Write("═══╦");
            }
            Console.WriteLine("═══╗");

            for (x = 0; x < line - 1; x++)
            {
                Console.Write("\t║   ");
                for (i = 0; i < column; i++)
                {
                    Console.Write("║   ");
                }
                Console.Write("\n\t╠═══╬");
                for (i = 2; i < column; i++)
                {
                    Console.Write("═══╬");
                }
                Console.WriteLine("═══╣");
            }
            Console.Write("\t║");
            for (i = 1; i < column; i++)
            {
                Console.Write("   ║");
            }
            Console.WriteLine("   ║");
            Console.Write("\t╚");
            for (i = 1; i < column; i++)
            {
                Console.Write("═══╩");
            }
            Console.WriteLine("═══╝");
        }
        /// <summary>
        /// sert a changer la couleur en fonction du player
        /// </summary>
        /// <param name="turne">sert a savoir si cest le turne 1 ou 2</param>
        static ConsoleColor Color(int turne)
        {
            ConsoleColor colorBase = ConsoleColor.White;

            colorBase = (turne % 2 == 0) ? ConsoleColor.Red : ConsoleColor.Yellow;

            return colorBase;
        }
        /// <summary>
        /// check dans le tableau si il y a 4 nombre les meme l'un a coté des autre
        /// </summary>
        /// <param name="line">line entrer par le user</param>
        /// <param name="column">colonne entrer par le user</param>
        /// <param name="tableau">c'est le tableau ou on stock la ou il y a les pion des players</param>
        /// <returns></returns>
        static bool CheckWin(int line, int column, int[,] tableau)
        {
            // Vérification des lines (horizontalement)
            // Parcours des lines du tableau pour vérifier une séquence de 4 éléments identiques horizontalement.
            for (int i = 0; i < line; i++)
            {
                // Parcours des colonnes, en s'assurant qu'il y a suffisamment de place pour une séquence de 4 éléments
                for (int j = 0; j < column - 3; j++)
                {
                    // Vérification si les 4 éléments consécutifs sont identiques et non nuls
                    if (tableau[i, j] != 0 && tableau[i, j] == tableau[i, j + 1] && tableau[i, j] == tableau[i, j + 2] && tableau[i, j] == tableau[i, j + 3])
                    {
                        return true; // Retourne true si une séquence gagnante est trouvée
                    }
                }
            }

            // Vérification des colonnes (verticalement)
            // Parcours des colonnes du tableau pour vérifier une séquence de 4 éléments identiques verticalement.
            for (int i = 0; i < line - 3; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    // Vérification si les 4 éléments consécutifs sont identiques et non nuls
                    if (tableau[i, j] != 0 && tableau[i, j] == tableau[i + 1, j] && tableau[i, j] == tableau[i + 2, j] && tableau[i, j] == tableau[i + 3, j])
                    {
                        return true; // Retourne true si une séquence gagnante est trouvée
                    }
                }
            }

            // Vérification des diagonales descendantes
            // Parcours des cases pour vérifier une séquence de 4 éléments identiques sur une diagonale descendante
            for (int i = 0; i < line - 3; i++)
            {
                for (int j = 0; j < column - 3; j++)
                {
                    // Vérification si les 4 éléments consécutifs sur la diagonale sont identiques et non nuls
                    if (tableau[i, j] != 0 && tableau[i, j] == tableau[i + 1, j + 1] && tableau[i, j] == tableau[i + 2, j + 2] && tableau[i, j] == tableau[i + 3, j + 3])
                    {
                        return true; // Retourne true si une séquence gagnante est trouvée
                    }
                }
            }

            // Vérification des diagonales montantes
            // Parcours des cases pour vérifier une séquence de 4 éléments identiques sur une diagonale montante
            for (int i = 3; i < line; i++)
            {
                for (int j = 0; j < column - 3; j++)
                {
                    // Vérification si les 4 éléments consécutifs sur la diagonale sont identiques et non nuls
                    if (tableau[i, j] != 0 && tableau[i, j] == tableau[i - 1, j + 1] && tableau[i, j] == tableau[i - 2, j + 2] && tableau[i, j] == tableau[i - 3, j + 3])
                    {
                        return true; // Retourne true si une séquence gagnante est trouvée
                    }
                }
            }
            return false; // Retourne false si aucune séquence gagnante n'est trouvée
        }
    }
}
