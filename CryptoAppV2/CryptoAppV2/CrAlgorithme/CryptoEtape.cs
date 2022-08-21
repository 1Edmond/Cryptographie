using CryptoAppV2.Custom;
using CryptoAppV2.Model;
using CryptoAppV2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CryptoAppV2.CrAlgorithme
{
    public class CryptoEtape
    {
        private readonly CryptoFonction MesFonctions = new CryptoFonction();
        private readonly CryptoCode Crypto = new CryptoCode();

        public CryptoEtape()
        {

        }
        public List<Etape> CodageAffine(string message, int a = 1, int b = 3, string modele = "Modèle de base")
        {
            List<Etape> result = new List<Etape>();
            var Usermodele = App.UserModeleManager.GetByName(modele);
            int LaBase = Usermodele.Valeur.Keys.Count;
            message = message.ToUpper();
            foreach (char c in message)
            {
                var CorrespondCar = Usermodele.Valeur.Getkey(c);
                var MyTemp1 = Crypto.CodageAffine(c.ToString(), a, b,modele);
                var car = Convert.ToChar(MyTemp1);
                var CorrespondCode = Usermodele.Valeur.Getkey(Convert.ToChar(car));
                //var resultTem = (CorrespondCar * a + b) % LaBase;
                result.Add(new Etape() { Info = $"Correspondant de {c} = {CorrespondCar} : {CorrespondCar} x {a} + {b} % {LaBase} = {CorrespondCode} => {MyTemp1}" });
            }
            return result;
        }
        public List<Etape> DecodageAffine(string message, int a = 1, int b = 3, string modele = "Modèle de base")
        {
            List<Etape> result = new List<Etape>();
            var Usermodele = App.UserModeleManager.GetByName(modele);
            int LaBase = App.UserModeleManager.GetByName(modele).Valeur.Keys.Count;
            message = message.ToUpper();
            foreach (char c in message)
            {
                var d = MesFonctions.InverseModulo(a, LaBase);
                var CorrespondCar = Usermodele.Valeur.Getkey(c);
                var CorrespondCode = Usermodele.Valeur.Getkey(Convert.ToChar(Crypto.DecodageAffine(c.ToString(), a, b, modele)));
                var MyTemp = Crypto.DecodageAffine(c.ToString(), a, b, modele);
                result.Add(new Etape { Info = $"Correspondant de {c} = {CorrespondCar} : {CorrespondCar} x {d} - {b} % {LaBase} =  {CorrespondCode} : {MyTemp}" });
            }
            return result;
        }
        public List<Etape> DecodageVigenere(string message, string cle)
        {
            var result = new List<Etape>();
            var newCle = cle.Split(',');
            for (int i = 0; i < message.Length; i++)

                if (int.TryParse(newCle[i % newCle.Length].ToString(), out var ce))
                {
                    var chiffrementMessage = MesFonctions.Chiffrement(message[i].ToString())[0];
                    var chiffremetnSoluion = MesFonctions.Chiffrement(Crypto.DecodageVigenere(message[i].ToString(), newCle[i % newCle.Length]))[0];
                    result.Add(new Etape
                    {
                        Info = $"Correspondant de {message[i]} = {chiffrementMessage} : {chiffrementMessage} - {newCle[i % newCle.Length]} % 26 =" +
                        $" {chiffremetnSoluion} => {Crypto.DecodageVigenere(message[i].ToString(), newCle[i % newCle.Length])} "
                    });
                }
                else
                {
                    var chiffrementMessage = MesFonctions.Chiffrement(message[i].ToString())[0];
                    var chiffremetnSoluion = MesFonctions.Chiffrement(Crypto.DecodageVigenere(message[i].ToString(), newCle[i % newCle.Length]))[0];
                    result.Add(new Etape
                    {
                        Info = $"Correspondant de {message[i]} = {chiffrementMessage} : {chiffrementMessage} - {MesFonctions.Chiffrement(newCle[i % newCle.Length])[0]} % 26 =" +
                        $" {chiffremetnSoluion} => {Crypto.DecodageVigenere(message[i].ToString(), newCle[i % newCle.Length])} "
                    });
                }
            return result;
        }
        public List<Etape> CodageVigenere(string message, string cle)
        {
            var result = new List<Etape>();
            var newCle = cle.Split(',');
            for (int i = 0; i < message.Length; i++)
            {
                if (int.TryParse(newCle[i % newCle.Length].ToString(), out var ce))
                {
                    var chiffrementMessage = MesFonctions.Chiffrement(message[i].ToString())[0];

                    result.Add(new Etape
                    {
                        Info = $"Correspondant de {message[i]} = {chiffrementMessage} : {chiffrementMessage} + {newCle[i % newCle.Length]} % 26 =" +
                        $" {MesFonctions.Chiffrement(Crypto.CodageVigenere(message[i].ToString(), newCle[i % newCle.Length]))[0]} => {Crypto.CodageVigenere(message[i].ToString(), newCle[i % newCle.Length])} "
                    });
                }
                else
                {
                    var chiffrementMessage = MesFonctions.Chiffrement(message[i].ToString())[0];
                    result.Add(new Etape
                    {
                        Info = $"Correspondant de {message[i]} = {chiffrementMessage} : {chiffrementMessage} + {MesFonctions.Chiffrement(newCle[i % newCle.Length])[0]} % 26 =" +
                        $" {MesFonctions.Chiffrement(Crypto.CodageVigenere(message[i].ToString(), newCle[i % newCle.Length]))[0]} => {Crypto.CodageVigenere(message[i].ToString(), newCle[i % newCle.Length])} "
                    });
                }
            }

            return result;
        }
        public List<Etape> MerkleHelmanClePublique(string cle, string n, string m)
        {
            List<Etape> result = new List<Etape>();
            var newCle = cle.Split(',').ToList();
            var temp = MesFonctions.MerkleHellmanClePublique(cle, n, m);
            var publiqueCle = temp.Split(',').ToList();
            result.Add(new Etape { Info = $"La clé publique est H = ({temp})." });
            for (int i = 0; i < newCle.Count; i++)
                result.Add(new Etape { Info = $"{newCle[i]} x {int.Parse(m)} % {int.Parse(n)} = {int.Parse(newCle[i]) * int.Parse(m)} % {int.Parse(n)} => {publiqueCle[i]}" });
            return result;
        }
        public List<Etape> CodageMerkleHellman(string message, string cle, string n, string m)
        {
            var result = new List<Etape>(MerkleHelmanClePublique(cle, n, m));
            var newCle = MesFonctions.MerkleHellmanClePublique(cle, n, m).Split(',').ToList();
            for (int i = 0; i < message.Length; i += newCle.Count)
            {
                string bloc = message.Substring(i, newCle.Count);
                result.Add(new Etape { Info = $"# Bloc {bloc} :" });
                int somme = 0;
                string etape = "";
                for (int u = 0; u < bloc.Length; u++)
                {
                    somme += int.Parse(bloc[u].ToString()) * int.Parse(newCle[u].ToString());
                    int temp = int.Parse(bloc[u].ToString()) * int.Parse(newCle[u].ToString());
                    if (u != bloc.Length - 1)
                        etape += $"{bloc[u]} * {newCle[u]} = {temp}; ";
                    else
                        etape += $"{bloc[u]} * {newCle[u]} = {temp}.";
                }
                etape += $"\nLa somme des éléments du bloc {bloc} donne {somme}";
                result.Add(new Etape() { Info = etape });
            }
            // result.Add(new Etape() { Info = $"La solution est donc {Crypto.CodageBinaireMerkleHellman(message, cle, n, m)}." });
            return result;
        }
        public List<Etape> DecodageMerkleHellman(string message, string cle, string n, string m)
        {
            var result = new List<Etape>()
            {
                new Etape {Info = $"L'inverse de {m} % {n} est {MesFonctions.InverseModulo(int.Parse(m),int.Parse(n))}."},
                new Etape {Info = $"La clé étant ({cle}), l'on transforme en binaire chaque résultat suivant cette clé."},
            };
            string solution = "";
            int newM = MesFonctions.InverseModulo(int.Parse(m), int.Parse(n));
            var newCle = cle.Split(',').ToList();
            var newMessage = message.Split(',').ToList();
            newMessage.ForEach(x =>
            {
                int correspondant = int.Parse(x) * newM % int.Parse(n);
                string temp = "";
                var resolution = MesFonctions.SacADos(cle, correspondant);
                foreach (string element in resolution)
                {
                    temp = Crypto.FormeBinaire(newCle, element.Split(',').ToList());
                    solution += temp;
                }
                result.Add(new Etape()
                {
                    Info = $"# Le message {x} : \n {x} x {newM} % {n} = {int.Parse(x) * newM} % {n} => {correspondant} "
                });
                result.Add(new Etape()
                {
                    Info = $"{correspondant} = {String.Join("", MesFonctions.SacADos(cle, correspondant)).Replace(",", " + ")} => {temp} suivant la clé"
                });

            });
            /*  result.Add(new Etape
              {
                  Info = $"La solution est donc {Crypto.DecodageMerkleHellman(message, cle, n, m)}"
              });*/

            return result;
        }
        public List<Etape> ExponentiationModulaire(int nbr, int exposant, int module)
        {
            int z = 1;
            var binaire = MesFonctions.Binaire(exposant);
            var result = new List<Etape>()
            {
                new Etape() { Info = $"Le bianire de {exposant} donne {String.Join("",binaire)}"},
                new Etape { Info = $"Au départ z = 1,"}
            };
            foreach (var element in binaire)
            {
                string temp = "";
                if (element == 0)
                {
                    int secondz = z;
                    z = (z * z) % module;
                    temp = $"{element} : z = {secondz}² % {module} <=> {secondz * secondz} % {module} => z = {z}";
                }
                else
                {
                    int secondz = z;
                    z = (z * z * nbr) % module;
                    temp = $"{element} : z = ({secondz}² x {nbr}) % {module} <=> {secondz * secondz * nbr} % {module} => z = {z}";
                }
                result.Add(new Etape { Info = temp });
            }

            return result;
        }
        public List<Etape> CodageRSA(string message, string n, string e) => new List<Etape>() {
            new Etape {Info = $"La formule de codage est c = (message ˆ e) % n"},
            new Etape {Info = $"Ce qui donne "},
            new Etape { Info = $"{Crypto.CodageRSA(message, n, e)} = {message} ˆ {e} % {n}" },
        };
        public List<Etape> DecodageRSA(string message, string n, string e)
        {
            var temp = MesFonctions.RSAPEtQ(n).Split(',');
            var d = MesFonctions.InverseModulo(int.Parse(e), (int.Parse(temp.First()) - 1) * (int.Parse(temp.Last()) - 1));
            var solution = Crypto.DecodageRSA(message, n, e);
            var result = new List<Etape>()
            {
                new Etape { Info = $"n = {n} = p x q => p et q correspondent à {String.Join(",", temp)}"},
                new Etape { Info = $"φ(n = {n}) = ({temp.First()} - 1) x ({temp.Last()} - 1) => {(int.Parse(temp.First()) - 1) * (int.Parse(temp.Last()) - 1)}" },
                new Etape { Info = $"La clé privée est d = {d} obtenu en calculant l'inverse de e = {e} modulo φ(n = {n}) = {(int.Parse(temp.First()) - 1) * (int.Parse(temp.Last()) - 1)} "},
                new Etape { Info = $"La solution est donc égale à {solution} car, "},
                new Etape { Info = $"{solution} = {message} ˆ {d} % {n}"},
            };

            return result;
        }
        public List<Etape> BonneCleAffine(string n) => new List<Etape>() {  new Etape { Info = "φ(n) x n - 1" } ,
                                        new Etape { Info = $"φ(n={n}) = {MesFonctions.Phie(n)}" },
                                        new Etape { Info = $"{MesFonctions.Phie(n)} x {n} - 1 = {MesFonctions.AffineBonneCle(n)}" }
                               };
        public List<Etape> RSAPriveEtape(string n, string e)
        {
            var petq = MesFonctions.RSAPEtQ(n);
            var p = $"({petq.Split(',').ToList()[0]} - 1)";
            var q = $"({petq.Split(',').ToList()[1]} - 1)";
            var phie = MesFonctions.Phie(n);
            var result = new List<Etape>()
            {
                new Etape { Info = $"La décomposition en facteur premier p et q de {n} donne {petq}"},
                new Etape { Info = $"φ(n) = (p - 1) x (q - 1). => φ({n}) = {p} x {q}."},
                new Etape { Info = $"φ(n) = {phie}."},
                new Etape { Info = $"La clé privé d est l'inverse de e % φ(n),"},
                new Etape { Info = $"d = inverse de {e} % {phie}."},
                new Etape { Info = $"d = {MesFonctions.RSAClePrive(n,e)}."},
            };

            return result;
        }
        public List<Etape> EtapeDiviseur(string nbr)
        {
            var diviseur = MesFonctions.Diviseur(int.Parse(nbr));
            var etapes = new List<Etape>()
            {
                new Etape { Info = $"Le cardinal d'un ensemble est le nombre d'éléments de cet ensemble."},
                new Etape { Info = $"Soit D l'ensemble des diviseurs de {nbr}."},
                new Etape { Info = $"Car(D({nbr})) = {diviseur.Count}."},
                new Etape { Info = $"D({nbr}) = "+ "{" + $"{String.Join(", ",diviseur)}" + "}"},
            };
            diviseur.ForEach(x =>
            {
                etapes.Add(new Etape { Info = $"{nbr} % {x} = {int.Parse(nbr) % x}." });
            });
            return etapes;
        }
        public List<Etape> Chiffrement(string text, int nbr)
        {
            var result = new List<Etape>();
            foreach (var car in text)
                result.Add(new Etape { Info = $"{car} en base {nbr} => {String.Join(", ", MesFonctions.Chiffrement(car.ToString(), nbr))}." });
            return result;
        }
        public List<Etape> SacADos(string text, int nbr1)
        {
            var Etaperesult = new List<Etape>();
            var result = MesFonctions.SacADos(text, nbr1);
            if (result.Count > 0)
                foreach (var element in result)
                    Etaperesult.Add(new Etape { Info = $"{String.Join(" + ", element.Split(',')) + $" = {nbr1}"}" });
            return Etaperesult;
        }
        public List<Etape> RSASignatureEtape(string message, string n, string e)
        {
            var d = MesFonctions.RSAClePrive(n, e);
            var sol = MesFonctions.RSASignature(message, n, e);
            var result = new List<Etape>()
            {
                new Etape { Info = $"La clé privée est {d}."},
                new Etape { Info = $"La formule est sig(m) = m ˆ d % n,"},
                new Etape { Info = $"avec d la clé privée."},
                new Etape { Info = $"Ce qui donne sig({message}) = {message} ˆ {d} % {n}."},
                new Etape { Info = $"sig({message}) = {sol}."},
            };

            return result;
        }
        public List<Etape> MatriceDeterminant(string matrice)
        {
            var LaMatrice = MesFonctions.TransformationEnMatrice(matrice);
            (string a, string b, string c, string d, string e, string f, string g, string h, string i) = ("", "", "", "", "", "", "", "", "");
            int compteur = 0;
            if (LaMatrice.Keys.Count == 2)
                foreach (KeyValuePair<int, List<String>> keyValuePair in LaMatrice)
                {
                    var Ligne = keyValuePair.Value;
                    if (compteur == 0)
                        (a, b) = (Ligne[0], Ligne[1]);
                    else
                        (c, d) = (Ligne[0], Ligne[1]);
                    compteur++;
                }
            else
                foreach (KeyValuePair<int, List<String>> keyValuePair in LaMatrice)
                {
                    var Ligne = keyValuePair.Value;
                    if (compteur == 0)
                        (a, b, c) = (Ligne[0], Ligne[1], Ligne[2]);
                    else
                        if (compteur == 1)
                        (d, e, f) = (Ligne[0], Ligne[1], Ligne[2]);
                    else
                        (g, h, i) = (Ligne[0], Ligne[1], Ligne[2]);
                    compteur++;
                }
            var result = new List<Etape>();
            if (LaMatrice.Keys.Count == 2)
            {


                var ligne1 = matrice.Split(';')[0];
                var ligne2 = matrice.Split(';')[1];
                result.Add(new Etape { Info = $"La matrice saisie est : \n{String.Join(" ", ligne1.Split(','))}\n{String.Join(" ", ligne2.Split(','))}" });
                result.Add(new Etape { Info = $"Le déterminant est obtenu en faisant {a} x {d} - {b} x {c}," });
                result.Add(new Etape { Info = $"nous avons ainsi {int.Parse(a) * int.Parse(d)} - {int.Parse(b) * int.Parse(c)}" });
            }
            else
            {
                var ligne1 = matrice.Split(';')[0];
                var ligne2 = matrice.Split(';')[1];
                var ligne3 = matrice.Split(';')[2];
                result.Add(new Etape { Info = $"La matrice saisie est : \n{String.Join(" ", ligne1.Split(','))}\n{String.Join(" ", ligne2.Split(','))}\n{String.Join(" ", ligne3.Split(','))}" });
                result.Add(new Etape { Info = $"Le déterminant est obtenu en faisant [({a} x {e} x {i}) + ({b} x {f} x {g}) + ({c} x {d} x {h})] - [({b} x {d} x {i}) + ({a} x {f} x {h}) + ({c} x {e} x {g})]" });
                result.Add(new Etape
                {
                    Info = $"Nous avons ainsi ({int.Parse(a) * int.Parse(e) * int.Parse(i)} + {int.Parse(b) * int.Parse(f) * int.Parse(g)} + {int.Parse(c) * int.Parse(d) * int.Parse(h)}) - " +
                    $"({int.Parse(b) * int.Parse(d) * int.Parse(i)} + {int.Parse(a) * int.Parse(f) * int.Parse(h)} + {int.Parse(c) * int.Parse(e) * int.Parse(g)} )"
                });
                int gauche = int.Parse(a) * int.Parse(e) * int.Parse(i) + int.Parse(b) * int.Parse(f) * int.Parse(g) + int.Parse(c) * int.Parse(d) * int.Parse(h);
                int droite = int.Parse(b) * int.Parse(d) * int.Parse(i) + int.Parse(a) * int.Parse(f) * int.Parse(h) + int.Parse(c) * int.Parse(e) * int.Parse(g);
                result.Add(new Etape { Info = $"Ce qui nous donne ({gauche}) - ({droite})" });
            }

            return result;
        }
        public List<Etape> MatriceInverse(string matrice, string n)
        {
            var det = MesFonctions.MatriceDeterminant(matrice);
            int compteur = 0;
            (string a, string b, string c, string d) = ("", "", "", "");
            var temp = MesFonctions.TransformationEnMatrice(matrice);
            foreach (KeyValuePair<int, List<String>> keyValuePair in temp)
            {
                var Ligne = keyValuePair.Value;
                if (compteur == 0)
                    (a, b) = (Ligne[0], Ligne[1]);
                else
                    (c, d) = (Ligne[0], Ligne[1]);
                compteur++;
            }
            var InverseDet = MesFonctions.InverseModulo(int.Parse(MesFonctions.MatriceDeterminant(matrice)), int.Parse(n));
            string MatriceInverse = $"{InverseDet * int.Parse(d)},{InverseDet * (-int.Parse(b))};{InverseDet * (-int.Parse(c))},{InverseDet * int.Parse(a)}";
            var result = new List<Etape>(MatriceDeterminant(matrice))
            {
                new Etape {Info = $"L'inverse du déterminant {det} est {InverseDet}"},
                new Etape {Info = $"L'inverse de la matrice est obtenu en multipliant le déterminant par la matrice,"},
                new Etape {Info = $"{d},{- int.Parse(b)};{- int.Parse(c)},{int.Parse(a)}"}
            };
            return result;
        }
        public List<Etape> Binaire(string binaire)
        {
            var sol = MesFonctions.Binaire(int.Parse(binaire));
            var suite = new List<String>();
            for (int i = 0; i < sol.Count; i++)
            {
                if (i != sol.Count - 1)
                    suite.Add($",{Math.Pow(2, i)}");
                else
                    suite.Add($"{Math.Pow(2, i)}");
            }
            suite.Reverse();
            var temp = MesFonctions.SacADos(String.Join("", suite), int.Parse(binaire));
            var result = new List<Etape>()
            {
                new Etape {Info = $"Prenons la suite ({String.Join("", suite)}), et une somme s = {binaire},"},
                new Etape {Info = $"cherchons la somme des éléments de la suite ayant s comme résultat,"},
                new Etape {Info = $"ensuite mettons un 1 aux positions des éléments ayant donné s,"},
                new Etape {Info = $"et un 0 aux autres éléments."},
                new Etape {Info = $"s : {binaire} = {String.Join(" + ", temp[0].Split(','))}."},
                new Etape {Info = $"Ce qui nous donne {String.Join("", sol)}"},

            };
            return result;
        }
        public List<Etape> RSAPEtQ(string n)
        {
            var result = new List<Etape>(EtapeDiviseur(n))
            {
                new Etape {Info = $"Ceux qui sont premiers par le reste sont {MesFonctions.RSAPEtQ(n)}"},
                new Etape {Info = $"La solution est donc {MesFonctions.RSAPEtQ(n)}"},
            };
            result.Insert(0, new Etape { Info = $"Les diviseurs de {n} sont : {String.Join(", ", MesFonctions.Diviseur(int.Parse(n)))}." });
            result.Insert(result.Count - 2, new Etape { Info = $"Retirons 1 et {n}" });

            return result;
        }
        public List<Etape> FrequenceCaractere(string n)
        {
            var result = new List<Etape>();
            var Lste = new List<int>();
            for (int i = 0; i < n.Length; i++)
            {
                if (n[i] != ' ' && !Lste.Contains(MesFonctions.Chiffrement(n[i].ToString())[0]))
                {
                    result.Add(new Etape { Info = $"{n[i]} se répète {n.Count(x => (x.ToString().ToLower() == n[i].ToString().ToLower()))} fois" });
                    Lste.Add(MesFonctions.Chiffrement(n[i].ToString())[0]);
                    n = n.Replace(n[i], ' ');
                }
            }

            return result;
        }
        public List<Etape> DecompositionFacteur(string n)
        {
            var result = new List<Etape>();
            var decomp = MesFonctions.DecompositonEnFacteurPremier(n).Split(',').ToList();
            decomp.ForEach(x =>
            {
                result.Add(new Etape { Info = $"{n} % {x} = {int.Parse(n) % int.Parse(x)} or {x} est premier." });
            });
            return result;
        }
        public List<Etape> CodageElGammal(string message, string alpha, string a, string p, string k)
        {
            int beta = MesFonctions.ExponentiationModulaire(int.Parse(alpha), int.Parse(a), int.Parse(p));
            int c1 = MesFonctions.ExponentiationModulaire(int.Parse(alpha), int.Parse(k), int.Parse(p));
            int c2 = MesFonctions.ExponentiationModulaire(int.Parse(message) * beta, int.Parse(k), int.Parse(p));
            return new List<Etape>()
            {
                new Etape { Info = $"La clé publique est (p,α,ꞵ) avec β = αˆa mod p."},
                new Etape { Info = $"β = {alpha}ˆ{a} % p => β = {beta},"},
                new Etape { Info = $"c1 = αˆk mod p : c1 = {alpha}ˆ{k} % {p} => c1 = {c1},"},
                new Etape { Info = $"c2 = mβˆk mod p : c2 = {message} x {beta}ˆ{k} % {p} => c2 = {c2},"},
                new Etape { Info = $"Le message codé est constituer de c1 et c2."},
            };
        } // A supprimer
        public List<Etape> CodageDeHill(string message, string matrice, string special = "x", string modele = "Modèle de base")
        {
            var NewMatrice = MesFonctions.TransformationEnMatrice(matrice);
            var userModele = App.UserModeleManager.GetByName(modele);
            string n = $"{userModele.Valeur.Keys.Count}";
            var result = new List<Etape>()
            {
                new Etape { Info = $"Le dégré de la matrice {matrice} est {NewMatrice.Keys.Count},"},
                new Etape { Info = $"L'on chiffre alors par bloc de {NewMatrice.Keys.Count},"},
            };
            if (message.Length % NewMatrice.Keys.Count != 0)
            {
                result.Add(new Etape { Info = $"Le message {message} n'est pas assez long pour chiffrer par bloc de {NewMatrice.Keys.Count}" });
                result.Add(new Etape { Info = $"Le caractère {special} à été ajouté pour pouvoir chiffrer le message" });
                while (message.Length % NewMatrice.Keys.Count != 0)
                    message += special;
            }
            var Blocs = new List<String>();
            for (int i = 0; i < message.Length; i += NewMatrice.Keys.Count)
            {
              //  var chiffrement = MesFonctions.Chiffrement(message.Substring(i, NewMatrice.Keys.Count));
                var tem = new List<int>();
                var bloc = message.Substring(i, NewMatrice.Keys.Count);
                foreach (char item in bloc)
                    tem.Add(userModele.Valeur.Getkey(item));
                Blocs.Add($"{String.Join(",", tem)}");
            }
            result.Add(new Etape { Info = $"Les différents blocs sont : {String.Join("; ", Blocs)}." });
            var RealMatrice = NewMatrice.Values.ToList();
            var ChiffrementListe = new List<int>();
            var ListeResult = new List<string>();
            for (int i = 0; i < Blocs.Count; i++)
            {
                var bloc = Blocs[i].Split(',').ToList();
                int correspondantBloc = 0;
                string temp = "";
                result.Add(new Etape { Info = $"Prenons le bloc {String.Join(",", bloc)}" });
                for (int u = 0; u < bloc.Count; u++)
                {
                    var tempMatrice = RealMatrice[u];
                    correspondantBloc = (int.Parse(bloc[u]) * int.Parse(tempMatrice[u]) + int.Parse(bloc[(u + 1) % bloc.Count]) * int.Parse(tempMatrice[(u + 1) % bloc.Count])) % int.Parse(n);
                    while (correspondantBloc < 0)
                        correspondantBloc += int.Parse(n);
                    result.Add(new Etape { Info = $"c{u + 1} = ({bloc[u]} x {tempMatrice[u]} + {bloc[(u + 1) % bloc.Count]} x {tempMatrice[(u + 1) % bloc.Count]}) % {n} => {correspondantBloc}" });
                    if (!ChiffrementListe.Contains(correspondantBloc))
                        ChiffrementListe.Add(correspondantBloc);
                    if (u != bloc.Count - 1)
                        temp = $"{correspondantBloc},";
                    else
                        temp = $"{correspondantBloc}; ";
                    ListeResult.Add(temp);

                }
            }
            result.Add(new Etape { Info = $"Les résultats trouvés sont : {String.Join("", ListeResult)} " });
            ChiffrementListe.ForEach(x =>
            {
                result.Add(new Etape { Info = $"Le correspondant de {x} est {userModele.Valeur[x]}" });
            });


            return result;
        }
        public List<Etape> DecodageDeHill(string message, string matrice, string special = "x", string modele = "Modèle de base")
        {
            int compteur = 0;
            (string a, string b, string c, string d) = ("", "", "", "");
            var temp = MesFonctions.TransformationEnMatrice(matrice);
            foreach (KeyValuePair<int, List<String>> keyValuePair in temp)
            {
                var Ligne = keyValuePair.Value;
                if (compteur == 0)
                    (a, b) = (Ligne[0], Ligne[1]);
                else
                    (c, d) = (Ligne[0], Ligne[1]);
                compteur++;
            }
            var userModele = App.UserModeleManager.GetByName(modele);
            var det = int.Parse(MesFonctions.MatriceDeterminant(matrice));
            var InverseDet = MesFonctions.InverseModulo(det, userModele.Valeur.Keys.Count);
            string MatriceInverse = $"{InverseDet * int.Parse(d)},{InverseDet * (-int.Parse(b))};{InverseDet * (-int.Parse(c))},{InverseDet * int.Parse(a)}";
            var result = new List<Etape>(CodageDeHill(message, MatriceInverse, special, modele));
            result.Insert(0, new Etape
            {
                Info = $"Le déterminant de la matrice {matrice} est {MesFonctions.MatriceDeterminant(matrice)}."
            });
            result.Insert(1, new Etape
            {
                Info = $"L'inverse du déterminant d = {det} de la matrice {matrice} est {InverseDet}."
            });
            result.Insert(2, new Etape
            {
                Info = $"L'inverse de la matrice {matrice} est {MatriceInverse}."
            });

            return result;
        }

        public List<Etape> ChiffrementModele(string text, string nom)
        {
            var result = new List<Etape>();
            var modele = App.UserModeleManager.GetByName(nom);
            for (int i = 0; i < text.Length; i++)
                if (i != text.Length - 1)
                    result.Add( new Etape() { Info = $"{text[i]} => {modele.Valeur.Getkey(text[i])}," });
                else
                    result.Add( new Etape() { Info = $"{text[i]} => {modele.Valeur.Getkey(text[i])}" });
            result.Insert(0, new Etape()
            {
                Info = $"Le modèle choisi est {modele.Nom}, sa base est {modele.NbrElement}"
            });
            return result;
        }
    }
}
