using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popup_Dictionairy
{
   internal class Translation
    {
        private string fromLanguage;
        private string toLanguage;
        //private int 

        
        public Translation()
        {
            
        }
        
        public Translation(string fromLanguage, string toLanguage)
        {
            this.fromLanguage = fromLanguage;
            this.toLanguage = toLanguage;
        }
        
        public string FromLanguage
        {
            get { return fromLanguage; }
            set { fromLanguage = value; }
        }

        public string ToLanguage
        {
            get { return toLanguage; }
            set { toLanguage = value;}
        }
 
    }
}
