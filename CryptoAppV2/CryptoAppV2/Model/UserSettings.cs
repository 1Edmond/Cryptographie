using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CryptoAppV2.Model
{
    public static class UserSettings
    {
        private const string Profile = "ProfileImage";
        public static string UserProfile { 
            get 
            {
                if (!Application.Current.Properties.ContainsKey(Profile))
                {
                    Application.Current.Properties.Add(Profile, "default.png");
                    Application.Current.SavePropertiesAsync();
                }
                return (string)Application.Current.Properties[Profile];
            } 
            set
            {
                if (Application.Current.Properties.ContainsKey(Profile))
                    Application.Current.Properties[Profile] = value;
                else
                    Application.Current.Properties.Add(Profile,value);
                Application.Current.SavePropertiesAsync();
            }
            }
        
        private  const string Modele = "UserModele";
        public static string UserModele
        { 
            get 
            {
                if (!Application.Current.Properties.ContainsKey(Modele))
                {
                    Application.Current.Properties.Add(Modele, "Modèle de base");
                    Application.Current.SavePropertiesAsync();
                }
                return (string)Application.Current.Properties[Modele];
            }
            set
            {
                if (Application.Current.Properties.ContainsKey(Modele))
                    Application.Current.Properties[Modele] = value;
                else
                    Application.Current.Properties.Add(Modele, value);
                Application.Current.SavePropertiesAsync();
            }
            }
         
        private const string Name = "UserName";
        public static string UserName
        { 
            get 
            {
                if (!Application.Current.Properties.ContainsKey(Name))
                {
                    Application.Current.Properties.Add(Name, "");
                    Application.Current.SavePropertiesAsync();
                }
                return (string)Application.Current.Properties[Name];
            } 
            set
            {
                if (Application.Current.Properties.ContainsKey(Name))
                    Application.Current.Properties[Name] = value;
                else
                    Application.Current.Properties.Add(Name, value);
                Application.Current.SavePropertiesAsync();
            }
            }

        private const string Contact = "UserContact";
        public static string UserContact
        { 
            get 
            {
                if (!Application.Current.Properties.ContainsKey(Contact))
                {
                    Application.Current.Properties.Add(Contact, "");
                    Application.Current.SavePropertiesAsync();
                }
                return (string)Application.Current.Properties[Contact];
            } 
            set
            {
                if (Application.Current.Properties.ContainsKey(Contact))
                    Application.Current.Properties[Contact] = value;
                else
                    Application.Current.Properties.Add(Contact, value);
                Application.Current.SavePropertiesAsync();
            }
            }

        private const string Pseudo = "UserPrenom";
        public static string UserPrenom
        { 
            get 
            {
                if (!Application.Current.Properties.ContainsKey(Pseudo))
                {
                    Application.Current.Properties.Add(Pseudo, "");
                    Application.Current.SavePropertiesAsync();
                }
                return (string)Application.Current.Properties[Pseudo];
            } 
            set
            {
                if (Application.Current.Properties.ContainsKey(Pseudo))
                    Application.Current.Properties[Pseudo] = value;
                else
                    Application.Current.Properties.Add(Pseudo, value);
                Application.Current.SavePropertiesAsync();
            }
            }

        private const string InscriptionId = "InscriptionId";
        public static string UserInscriptionId {
            get
            {
                if (!Application.Current.Properties.ContainsKey(InscriptionId))
                {
                    Application.Current.Properties.Add(InscriptionId, "");
                    Application.Current.SavePropertiesAsync();
                }
                return (string)Application.Current.Properties[InscriptionId];
            }
            set
            {
                if (Application.Current.Properties.ContainsKey(InscriptionId))
                    Application.Current.Properties[InscriptionId] = value;
                else
                    Application.Current.Properties.Add(InscriptionId, value);
                Application.Current.SavePropertiesAsync();
            }

        }
        
        private const string InscriptionValide = "InscriptionValide";
        public static string UserInscriptionValide
        {
            get
            {
                if (!Application.Current.Properties.ContainsKey(InscriptionValide))
                {
                    Application.Current.Properties.Add(InscriptionValide, "");
                    Application.Current.SavePropertiesAsync();
                }
                return (string)Application.Current.Properties[InscriptionValide];
            }
            set
            {
                if (Application.Current.Properties.ContainsKey(InscriptionValide))
                    Application.Current.Properties[InscriptionValide] = value;
                else
                    Application.Current.Properties.Add(InscriptionValide, value);
                Application.Current.SavePropertiesAsync();
            }

        }
        
        private const string InscriptionTest = "InscriptionTest";
        public static string UserInscriptionTest
        {
            get
            {
                if (!Application.Current.Properties.ContainsKey(InscriptionTest))
                {
                    Application.Current.Properties.Add(InscriptionTest, "");
                    Application.Current.SavePropertiesAsync();
                }
                return (string)Application.Current.Properties[InscriptionTest];
            }
            set
            {
                if (Application.Current.Properties.ContainsKey(InscriptionTest))
                    Application.Current.Properties[InscriptionTest] = value;
                else
                    Application.Current.Properties.Add(InscriptionTest, value);
                Application.Current.SavePropertiesAsync();
            }

        }

    }
}
