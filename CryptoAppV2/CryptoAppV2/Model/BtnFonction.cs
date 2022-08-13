using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CryptoAppV2.Model
{
    public class BtnFonction
    {
        public string Text { get; set; }

        public ICommand Navigation { get; set; }

        public bool Visible { get; set; } = true;
    }
}
