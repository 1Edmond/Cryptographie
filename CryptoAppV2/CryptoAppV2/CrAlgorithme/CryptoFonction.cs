using CryptoAppV2.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoAppV2.CrAlgorithme
{
    public class CryptoFonction
    {
        public List<int> Binaire(int nbr)
        {
            string binaire = string.Empty;
            int temp;
            while (nbr > 0)
            {
                temp = nbr % 2;
                nbr /= 2;
                binaire = temp.ToString() + binaire;
            }
            var list = new List<int>();
            binaire.ToList().ForEach(w =>
            {
                list.Add(int.Parse($"{w}"));
            });
            return list;
        }
        public List<int> Compose(List<int> list)
        {
            var newList = new List<int>();
            list.ForEach(w =>
            {
                newList.Add((w + 1) % 2);
            });
            return newList;
        }
        public List<int> Diviseur(int nbr)
        {
            var list = new List<int>();
            if (nbr > 0)
                for (int i = 1; i <= nbr; i++)
                {
                    if (nbr % i == 0)
                        list.Add(i);
                }
            else
                for (int i = nbr; i <= -1; i++)
                {
                    if (nbr % i == 0)
                        list.Add(i);
                }
            return list;
        }
        public int ExponentiationModulaire(int nbr, int exposant, int module)
        {
            int z = 1;
            var binaire = Binaire(exposant);
            foreach (var element in binaire)
                if (element == 0)
                    z = (z * z) % module;
                else
                    z = (z * z * nbr) % module;

            return z;
        } // Etape faite
        public int InverseModulo(int nbr, int module)
        {
            while (nbr < 0)
                nbr += module;
            for (int j = 1; j <= module; j++)
                if (nbr * j % module == 1)
                    return j;
            return -1;
        }
        public bool Premier(int nbr) => Diviseur(nbr).Count() == 2 ? true : false;
        public bool PremierEntreEux(int nbr1, int nbr2)
        {
            bool test = true;
            Diviseur(nbr1).ForEach(x =>
            {
                if (Diviseur(nbr2).Contains(x) && x != 1)
                    test = false;
            });

            if (nbr1 == 1 || nbr2 == 1)
                test = true;

            return test;
        }
        public List<int> Chiffrement(string text, int LaBase = 26)
        {
            List<int> list = new List<int>();

            foreach (char ch in text)
                if (ch >= 'a' && ch <= 'z')
                    list.Add((ch - 97) % LaBase);
                else if (ch >= 'A' && ch <= 'Z')
                    list.Add((ch - 65) % LaBase);
            return list;
        }
        public List<string> SacADos(string suite, int nbr)
        {
            var list = suite.Split(',').ToList();
            List<string> solutions = new List<string>();
            var combinaison = produceList(list);
            foreach (List<string> s in combinaison)
            {
                int somme = 0;
                foreach (var temp in s)
                    somme += int.Parse(temp);
                if (somme == nbr)
                    solutions.Add(string.Join(",", s));
            }
            return solutions;
        }
        public string RSAPEtQ(string n)
        {
            int newN = int.Parse(n);
            string result = "";
            for (int i = 1; i <= newN; i++)
            {
                for (int j = 1; j <= newN; j++)
                {
                    if (i * j == newN && Premier(i) && Premier(j))
                        result = $"{i},{j}";
                }
            }
            return result;
        }
        public string MerkleHellmanClePublique(string cle, string n, string m)
        {
            var newCle = cle.Split(',').ToList();
            string[] PubliqueCle = new string[newCle.Count];
            for (int i = 0; i < newCle.Count; i++)
                PubliqueCle[i] = (int.Parse(newCle[i]) * int.Parse(m) % int.Parse(n)).ToString();
            return String.Join(",", PubliqueCle);
        } // Etape faite
        private IEnumerable<int> constructSetFromBits(int i)
        {
            for (int n = 0; i != 0; i /= 2, n++)
            {
                if ((i & 1) != 0)
                    yield return n;
            }
        }
        private IEnumerable<List<string>> produceEnumeration(List<string> maliste)
        {
            for (int i = 0; i < (1 << maliste.Count); i++)
            {
                yield return
                    constructSetFromBits(i).Select(n => maliste[n]).ToList();
            }
        }
        public List<List<string>> produceList(List<string> maliste)
        {
            return produceEnumeration(maliste).ToList();
        }
        public string RSAClePrive(string n, string e) => InverseModulo(int.Parse(e), RSAPEtQ(n).Split(',').ToList().ProduitRSaList()).ToString();
        public Dictionary<int, List<string>> TransformationEnMatrice(string matrice)
        {
            var MatrcieLigne = matrice.Split(';').ToList();
            var newMatrice = new Dictionary<int, List<string>>();
            MatrcieLigne.ForEach(x =>
            {
                newMatrice[newMatrice.Count + 1] = x.Split(',').ToList();
            });
            return newMatrice;
        }
        public string AffineBonneCle(string n)
        {
            int phie = 1;
            DecompositonEnFacteurPremier(n).Split(',').ToList().ForEach(x =>
            {
                phie *= int.Parse(x) - 1;
            });
            return $"{int.Parse(Phie(n)) * int.Parse(n) - 1}";
        }
        public string Phie(string n)
        {
            int phie = 1;
            DecompositonEnFacteurPremier(n).Split(',').ToList().ForEach(x =>
            {
                phie *= int.Parse(x) - 1;
            });
            return $"{phie}";
        }
        public string DecompositonEnFacteurPremier(string n)
        {
            string result = "";
            var diviseur = Diviseur(int.Parse(n));
            diviseur.ForEach(x =>
            {
                if (Premier(x))
                    result += $"{x},";
            });
            if (result.Equals(""))
                return result; // Modiification aportée
            return result.Remove(result.Length - 1);
        }
        public string RSAPhi(String texte) => texte.Split(',').ToList().ProduitRSaList().ToString();
        public string RSASignature(string message, string n, string e) =>
            ExponentiationModulaire(int.Parse(message), int.Parse(RSAClePrive(n, e)), int.Parse(n)).ToString();
        public string MatriceDeterminant(string matrice)
        {
            string result = String.Empty;
            var LaMatrice = TransformationEnMatrice(matrice);
            var TempMatrice = LaMatrice.Values.ToList();
            var NewMatrice = new List<String>();
            if (LaMatrice.Keys.Count == 2)
            {
                var LigneUne = TempMatrice[0].ToList();
                var LignrDeux = TempMatrice[1].ToList();
                result = $"{int.Parse(LigneUne[0]) * int.Parse(LignrDeux[1]) - int.Parse(LigneUne[1]) * int.Parse(LignrDeux[0])}";

            }
            else if (LaMatrice.Keys.Count == 3)
            {
                TempMatrice.ForEach(x =>
                {
                    NewMatrice.Add($"{String.Join(",", x)},{x[0]},{x[1]}");
                });
                NewMatrice.ForEach(x => Console.WriteLine(x));
                int sommeDroite = 0;
                for (int v = 0; v < 3; v++)
                {
                    int i = 0;
                    int temp = 1;
                    for (int u = 0; u < 3; u++)
                    {
                        int ProduitDroite = int.Parse(NewMatrice[u].Split(',').ToList()[i + v]);
                        i++;
                        temp *= ProduitDroite;
                    }
                    sommeDroite += temp;
                }
                int sommeGauche = 0;
                for (int v = 0; v < 3; v++)
                {
                    int i = 0;
                    int temp = 1;
                    for (int u = 0; u < 3; u++)
                    {
                        var element = NewMatrice[u].Split(',').ToList();
                        element.Reverse();
                        int ProduitDroite = int.Parse(element[i + v]);
                        i++;
                        temp *= ProduitDroite;
                    }
                    sommeGauche += temp;

                }
                result = $"{sommeDroite - sommeGauche}";

            }
            return result;
        }

        public bool IsSquareMatrice(string LaMatrice)
        {
            bool result = true;
            var matrice = TransformationEnMatrice(LaMatrice);
            int LigneElement = matrice.Keys.Count;
            foreach (KeyValuePair<int, List<string>> element in matrice)
                if (element.Value.Count != LigneElement)
                    result = false;
            return result;
        }

        public string MatriceInverse(string matrice, string n)
        {
            int compteur = 0;
            (string a, string b, string c, string d) = ("", "", "", "");
            var temp = TransformationEnMatrice(matrice);
            foreach (KeyValuePair<int, List<String>> keyValuePair in temp)
            {
                var Ligne = keyValuePair.Value;
                if (compteur == 0)
                    (a, b) = (Ligne[0], Ligne[1]);
                else
                    (c, d) = (Ligne[0], Ligne[1]);
                compteur++;
            }
            var InverseDet = InverseModulo(int.Parse(MatriceDeterminant(matrice)), int.Parse(n));
            string MatriceInverse = $"{InverseDet * int.Parse(d)},{InverseDet * (-int.Parse(b))};{InverseDet * (-int.Parse(c))},{InverseDet * int.Parse(a)}";


            return InverseDet < 0 ? "" : MatriceInverse;
        }

         public string ChiffrementModele(string text, string nom)
        {
            var modele = App.UserModeleManager.GetByName(nom);
            var result = "";
            for (int i = 0; i < text.Length; i++)
            
                if(i  != text.Length -1)
                    result += $"{modele.Valeur.Getkey(text[i])},";
                else
                    result += $"{modele.Valeur.Getkey(text[i])}";
            return result;
        }
    }
}
