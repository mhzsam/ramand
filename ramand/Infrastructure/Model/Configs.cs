using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustracture.Models
{
    public class Configs
    {
        public string TokenKey { get; set; }
        public int TokenTimeOut { get; set; }
        public int RefreshTokenTimeOut { get; set; }

    }
}
