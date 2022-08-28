using CryptoAppV2.Custom;
using CryptoAppV2.Data;
using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CryptoAppV2.ViewModel
{
    public class UserModeleVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<UserModele> Modeles { get; private set; }
        public UserModeleManager manager = App.UserModeleManager;
        public UserModeleVm()
        {
            var dos =  manager.GetAll();
            Modeles = new ObservableCollection<UserModele>(dos);
        }
        public ICommand DeleteCommand => new Command(async (obj) =>
        {
            var modele = obj as UserModele;
            if(modele.Nom != "Modèle de base")
            {
                Modeles.Remove(modele);
                var result = await manager.Delete(modele);
                if (result)
                {
                    var dos = manager.GetAll();
                    if (dos != null)
                    {
                        dos.Sort((x, y) => { return x.DateAjout.Date.CompareTo(y.DateAjout.Date); });
                        Modeles = new ObservableCollection<UserModele>(dos);
                    }
                }
                OnPropertyChanged("Modeles");
                InputValidationColor = Color.FromHex("#4ba3c3");
                InputValidation = $"Bien, Le modèle {modele.Nom} a bien été supprimé.";
            }
            else
            {
                InputValidation = "Vous ne pouvez pas supprimer le modèle de base.";
                InputValidationColor = Color.Red;
            }
            OnPropertyChanged("InputValidationColor");
            OnPropertyChanged("InputValidation");

        });
        UserModele selectedModel = App.UserModeleManager.GetByName(UserSettings.UserModele);
        public UserModele SelectedItem { get 
            {
                return selectedModel;
            } 
            set {
                if(SelectedItem != value)
                {
                    selectedModel = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }

            } 
        }
        public ICommand RefreshCommand => new Command(() =>
        {
            IsRefreshing = true;
            OnPropertyChanged(nameof(IsRefreshing));
            var dos = manager.GetAll();
            if (dos != null)
                Modeles = new ObservableCollection<UserModele>(dos);
            IsRefreshing = false;
            OnPropertyChanged(nameof(Modeles));
            OnPropertyChanged(nameof(IsRefreshing));
        });
        public string InputValidation { get; set; }
        public Color InputValidationColor { get; set; }
        public bool IsOk = true;
        public bool IsRefreshing = false;
        public string NewModeleNomInput { get; set; } = String.Empty;
        public string NewModeleValueInput { get; set; } = String.Empty;
        public ICommand AddCommand => new Command(async () =>
        {
            InputValidationColor = Color.FromHex("#4ba3c3");
            InputValidation = $"Bien, Votre modèle {NewModeleNomInput} a bien été ajouté.";
            IsOk = true;
            if (String.IsNullOrEmpty(NewModeleNomInput) || String.IsNullOrWhiteSpace(NewModeleNomInput) || String.IsNullOrEmpty(NewModeleValueInput) || String.IsNullOrWhiteSpace(NewModeleValueInput) || !NewModeleValueInput.IsModele())
            {
                IsOk = false;
                InputValidationColor = Color.Red;
                InputValidation = "Erreur, revoyez le format de votre entrée.";
            }

            if (IsOk)
            {
                var modele = new UserModele() 
                { 
                    Nom = NewModeleNomInput ,
                    DateAjout = DateTime.Now,
                    ModeleValue = NewModeleValueInput.ToUpper() ,
                };
                if (!manager.Exist(modele.Nom))
                {
                    await manager.Add(modele);
                    Modeles.Add(modele);
                    OnPropertyChanged("Modeles");
                }
                else
                {
                    InputValidationColor = Color.Red;
                    InputValidation = $"Erreur le modèle {NewModeleNomInput} existe déjà.";
                }

            }
            NewModeleNomInput = "";
            NewModeleValueInput = "";
            OnPropertyChanged("NewModeleNomInput");
            OnPropertyChanged("NewModeleValueInput");
            OnPropertyChanged("InputValidationColor");
            OnPropertyChanged("InputValidation");
        });
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
            else
                return ;
        }
    }
}
