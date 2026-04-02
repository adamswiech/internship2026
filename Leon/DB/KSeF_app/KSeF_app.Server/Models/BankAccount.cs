using System;
using System.Collections.Generic;
using System.Text;

namespace KSeF_App.Server.Models
{
    public class BankAccount
    {
        public int Id { get; set; }

        public string FullNumber { get; set; }
        public string Swift { get; set; }
        public string BankName { get; set; }
        public string Description { get; set; }

        public int IsBankOwnAccount { get; set; }//bool  <RachunekWlasnyBanku>2</RachunekWlasnyBanku> or <RachunekWlasnyBanku>1</RachunekWlasnyBanku>
    }
}
