using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullStackApp.Models
{
   [Serializable]
    public class ComboBoxItem
    {
        public int index { get; set; }
        public string value { get; set; }

        public ComboBoxItem(int index, string value)
        {
            this.index = index;
            this.value = value;
        }
    }
}