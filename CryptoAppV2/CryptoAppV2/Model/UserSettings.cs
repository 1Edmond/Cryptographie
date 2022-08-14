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
                    return "";
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

        private const string Password = "UserPassword";
        public static string UserPassword
        { 
            get 
            {
                if (!Application.Current.Properties.ContainsKey(Password))
                    return "";
                return (string)Application.Current.Properties[Password];
            } 
            set
            {
                if (Application.Current.Properties.ContainsKey(Password))
                    Application.Current.Properties[Password] = value;
                else
                    Application.Current.Properties.Add(Password, value);
                Application.Current.SavePropertiesAsync();
            }
        }

        private const string Contact = "UserContact";
        public static string UserContact
        { 
            get 
            {
                if (!Application.Current.Properties.ContainsKey(Contact))
                    return "";
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

        private const string Pseudo = "UserPseudo";
        public static string UserPseudo
        { 
            get 
            {
                if (!Application.Current.Properties.ContainsKey(Pseudo))
                    return "";
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



    }
}
