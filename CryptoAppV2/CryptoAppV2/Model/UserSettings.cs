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
                UserProfile = (string)Application.Current.Properties[Profile];
                return UserProfile;
            } 
            set
            {
                if (Application.Current.Properties.ContainsKey(Profile))
                {
                    UserProfile = value;
                    Application.Current.Properties[Profile] = UserProfile;
                }
                else
                    Application.Current.Properties.Add(Profile,value);
                Application.Current.SavePropertiesAsync();
            }
            }
        
        private const string Modele = "UserModele";
        public static string UserModele
        { 
            get 
            {
                if (!Application.Current.Properties.ContainsKey(Modele))
                {
                    Application.Current.Properties.Add(Modele, "BaseModele");
                    Application.Current.SavePropertiesAsync();
                }
                UserModele = (string)Application.Current.Properties[Modele];
                return UserModele;
            } 
            set
            {
                if (Application.Current.Properties.ContainsKey(Modele))
                {
                    UserModele = value;
                    Application.Current.Properties[Modele] = UserModele;
                }
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
                return UserName;
            } 
            set
            {
                if (Application.Current.Properties.ContainsKey(Name))
                {
                    UserName = value;
                    Application.Current.Properties[Name] = UserName;
                }
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
                return UserPassword;
            } 
            set
            {
                if (Application.Current.Properties.ContainsKey(Password))
                {
                    UserPassword = value;
                    Application.Current.Properties[Password] = UserPassword;
                }
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
                return UserContact;
            } 
            set
            {
                if (Application.Current.Properties.ContainsKey(Contact))
                {
                    UserContact = value;
                    Application.Current.Properties[Contact] = UserContact;
                }
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
                return UserPseudo;
            } 
            set
            {
                if (Application.Current.Properties.ContainsKey(Pseudo))
                {
                    UserPseudo = value;
                    Application.Current.Properties[Pseudo] = UserPseudo;
                }
                else
                    Application.Current.Properties.Add(Pseudo, value);
                Application.Current.SavePropertiesAsync();
            }
            }



    }
}
