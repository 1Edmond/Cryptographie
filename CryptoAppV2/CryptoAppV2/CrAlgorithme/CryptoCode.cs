using CryptoAppV2.Custom;
using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CryptoAppV2.CrAlgorithme
{

    public class CryptoCode
    {
        private static CryptoFonction MesFonctions = new CryptoFonction();
        UserModele userModele = App.UserModeleManager.GetByName(UserSettings.UserModele);

        public string CodageAffine(string message, int a = 1, int b = 3, string modele = "Modèle de base")
        {
            int valCar = 0;
            string code = String.Empty;
            var temp = App.UserModeleManager.GetByName(modele);
            message = message.ToUpper();
            foreach (char car in message)
            {
                valCar = (temp.Valeur.Getkey(car) * a + b) % temp.Valeur.Keys.Count;
                while (valCar < 0)
                    valCar += temp.Valeur.Keys.Count;
                code += temp.Valeur[valCar];
            }
            return code;
        } // Etape faite
        public string DecodageAffine(string message, int a = 1, int b = 3, string modele = "Modèle de base")
        {
            int valCar = 0;
            string decode = String.Empty;
            var temp = App.UserModeleManager.GetByName(modele);
            a = MesFonctions.InverseModulo(a, temp.Valeur.Keys.Count);
            message.ToUpper();
            foreach (char car in message)
            {
                valCar = (a * (temp.Valeur.Getkey(car) - b)) % temp.Valeur.Keys.Count;
                while (valCar < 0)
                    valCar = valCar + temp.Valeur.Keys.Count;
                decode += temp.Valeur[valCar]; ;

            }
            return decode;
        } // Etape faite
        public string VernamCryptoSysteme(string message, string cle)
        {
            string result = "";
            for (int i = message.Length - 1; i >= 0; i--)
                result += (int.Parse($"{message[i]}") + int.Parse($"{cle[i]}")) % 2;
            char[] resultat = result.ToCharArray();
            Array.Reverse(resultat);
            return new string(resultat);
        }
        public string CodageVigenere(string message, string cle)
        {
            var result = "";
            message = message.ToUpper();
            string[] newCle = cle.Split(',');
            for (int i = 0; i < message.Length; i++)
            {

                Int32 temp = Convert.ToInt32(newCle[i % newCle.Length]);
                result += CodageAffine(message[i].ToString(), 1, int.Parse(temp.ToString()));


            }

            return result;
        } // Etape faite
        public string DecodageVigenere(string message, string cle)
        {
            var result = "";
            string[] newCle = cle.Split(',');
            for (int i = 0; i < message.Length; i++)
            {
                try
                {
                    Int32 temp = Convert.ToInt32(newCle[i % newCle.Length]);
                    result += DecodageAffine(message[i].ToString(), 1, int.Parse(temp.ToString()));

                }
                catch
                {
                    var temp = MesFonctions.Chiffrement(newCle[i % newCle.Length]);
                    result += DecodageAffine(message[i].ToString(), 1, temp[0]);
                }
            }



            return result;
        } // Etape faite
        public string CodageHill(string message, string matrice, string special = "x", string modele = "Modèle de base")
        {
            string result = "";
            var Blocs = new List<String>();
            var newMatrice = MesFonctions.TransformationEnMatrice(matrice);
            var Realmatrice = new List<String>();
            userModele = App.UserModeleManager.GetByName(modele);
            while (message.Length % newMatrice.Keys.Count != 0)
                message += special;
            foreach (KeyValuePair<int, List<string>> keyValuePair in newMatrice)
                Realmatrice.Add(String.Join(",", keyValuePair.Value));
            for (int i = 0; i < message.Length; i += newMatrice.Keys.Count)
            {
               // var chiffrement = MesFonctions.Chiffrement(message.Substring(i, newMatrice.Keys.Count));
                var tem = new List<int>();
                var bloc = message.Substring(i, newMatrice.Keys.Count);
                foreach (char item in bloc)
                    tem.Add(userModele.Valeur.Getkey(item));
                Blocs.Add($"{String.Join(",", tem)}");
            }
            for (int i = 0; i < Blocs.Count; i++)
            {
                var bloc = Blocs[i].Split(',').ToList();
                string correspondantBloc = "";
                for (int u = 0; u < bloc.Count; u++)
                {
                    int correspondant = 0;
                    var MatriceLigne = Realmatrice[u].Split(',').ToList();
                    for (int j = 0; j < MatriceLigne.Count; j++)
                        correspondant += int.Parse(bloc[j]) * int.Parse(MatriceLigne[j].ToString());
                    correspondant = correspondant % userModele.Valeur.Keys.Count;
                    while (correspondant < 0)
                        correspondant += userModele.Valeur.Keys.Count;
                    if (u != MatriceLigne.Count - 1)
                        correspondantBloc += $"{correspondant},";
                    else
                        correspondantBloc += $"{correspondant}";

                }
                correspondantBloc.Split(',').ToList().ForEach(x =>
                {
                    result += $"{userModele.Valeur[(int.Parse(x))]}";
                });
            }
            return result;
        }
        public string DecodageHill(string message, string matrice, string special = "x", string modele = "Modèle de base")
        {
            int compteur = 0;
            (string a, string b, string c, string d) = ("", "", "", "");
            var temp = MesFonctions.TransformationEnMatrice(matrice);
            userModele = App.UserModeleManager.GetByName(modele);
            foreach (KeyValuePair<int, List<String>> keyValuePair in temp)
            {
                var Ligne = keyValuePair.Value;
                if (compteur == 0)
                    (a, b) = (Ligne[0], Ligne[1]);
                else
                    (c, d) = (Ligne[0], Ligne[1]);
                compteur++;
            }
            var InverseDet = MesFonctions.InverseModulo(int.Parse(MesFonctions.MatriceDeterminant(matrice)), userModele.Valeur.Keys.Count);
            string MatriceInverse = $"{InverseDet * int.Parse(d)},{InverseDet * (-int.Parse(b))};{InverseDet * (-int.Parse(c))},{InverseDet * int.Parse(a)}";
            string result = CodageHill(message, MatriceInverse, special, userModele.Nom);
            return result;
        }
        public string CodageRSA(string message, string n, string e) => (Math.Pow(int.Parse(message), int.Parse(e)) % int.Parse(n)).ToString();
        public string DecodageRSA(string message, string n, string e) => MesFonctions.ExponentiationModulaire(int.Parse(message), int.Parse(MesFonctions.RSAClePrive(n, e)), int.Parse(n)).ToString();
        public string CodageBinaireMerkleHellman(string message, string cle, string n, string m)
        {
            string code = "";
            List<string> PublicCle = new List<string>();
            var newCle = cle.Split(',');
            foreach (var car in newCle)
            {
                int val = int.Parse(car.ToString());
                PublicCle.Add($"{val * int.Parse(m) % int.Parse(n)}");
            }
            Console.WriteLine(PublicCle.Count);
            for (int i = 0; i < message.Length; i += PublicCle.Count)
            {
                string bloc = message.Substring(i, PublicCle.Count);
                int somme = 0;
                for (int u = 0; u < bloc.Length; u++)
                    somme += int.Parse(bloc[u].ToString()) * int.Parse(PublicCle[u].ToString());
                if (i != message.Length - PublicCle.Count)
                    code += somme + ",";
                else
                    code += somme;
            }
            return code;
        } // Etape faite
        public string FormeBinaire(List<string> cle, List<string> message)
        {
            var binaire = new Char[cle.Count];
            for (int i = 0; i < binaire.Length; i++)
                if (message.Contains(cle[i]))
                    binaire[i] = '1';
                else
                    binaire[i] = '0';
            return new String(binaire);
        }
        public string DecodageMerkleHellman(string message, string cle, string n, string m)
        {
            var newMessage = message.Split(',').ToList();
            var newCle = cle.Split(',').ToList();
            string solution = "";
            int newM = MesFonctions.InverseModulo(int.Parse(m), int.Parse(n));
            newMessage.ForEach(x =>
            {
                int correspondant = int.Parse(x) * newM % int.Parse(n);
                var resolution = MesFonctions.SacADos(cle, correspondant);
                foreach (string element in resolution)
                    solution += FormeBinaire(newCle, element.Split(',').ToList());
            });
            return solution;
        } // Etape faite
    }
}
