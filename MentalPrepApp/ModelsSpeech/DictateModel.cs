using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalPrepApp.ModelsSpeech
{
    public class DictateModel
    {
        private bool CmdModeOn;
        public bool cmdModeOn
        {
            get { return CmdModeOn; }
            set { CmdModeOn = value; }
        }

    }
}
