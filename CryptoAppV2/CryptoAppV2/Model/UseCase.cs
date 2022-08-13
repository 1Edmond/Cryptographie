using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CryptoAppV2.Model
{
    public class UseCase
    {
        public string Titre { get; set; } = "";
        public bool TitreVisiblity { get; set; } = true;
        public string Numbrer { get; set; } = "";
        public bool NumberVisibility { get; set; } = true;
        public string Description { get; set; } = "";
        public string Alignement { get; set; } = "StartAndExpand";
        public bool HasBoxView { get; set; } = false;
        public Thickness BoxViewMargin { get; set; } = new Thickness();
    }
}
