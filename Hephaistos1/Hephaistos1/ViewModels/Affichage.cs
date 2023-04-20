using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hephaistos1.ViewModels
{
    public class Affichage : ObservableObject
    {
        public int SizeX;
        public int SizeY;
        public int[,] Board;
        public Affichage()
        {
            Board = new int[SizeX,SizeY] ;
            for (int x = 0; x < SizeY; x++)
            {
                for (int y = 0; y < SizeX; y++)
                {
                    int tile = Board[x, y];
                }
            }
        }
        public void Taille()
        {
            Console.WriteLine("Veuillez rentrer la largeur de votre Map : ");
            string largeur = Console.ReadLine();
            while (!IsInteger(largeur))
            {
                Console.WriteLine("La valeur rentrer n'est pas un entier");
                Console.WriteLine("Veuillez rentrer la largeur de votre Map : ");
                largeur = Console.ReadLine();
            }
            SizeX = Int32.Parse(largeur);
            Console.WriteLine("Veuillez rentrer la longueur de votre Map : ");
            string longueur = Console.ReadLine();
            while (!IsInteger(longueur))
            {
                Console.WriteLine("La valeur rentrer n'est pas un entier");
                Console.WriteLine("Veuillez rentrer la longueur de votre Map : ");
                longueur = Console.ReadLine();
            }
            SizeY = Int32.Parse(longueur);
        }

        public bool IsInteger(string input)
        {
            int number;
            return int.TryParse(input, out number);
        }

        public void BoutonQuadrillage()
        {

        }
    }
}
