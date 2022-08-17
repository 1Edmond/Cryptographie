using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CryptoAppV2.Model
{
    public class MySearchHandler : SearchHandler
    {
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
            var temp = string.IsNullOrWhiteSpace(newValue) ? null : App.UserHistoriqueManager.GetByValue(newValue).Result;
            ItemsSource = temp;
        }
        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

        }
    }
}
