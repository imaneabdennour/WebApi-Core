using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionCommande.DTOs
{
    public class CommandCreateDTO
    {
        //public int Id { get; set; } => it is auto generated so doesn't belong to the contract exposed to the client

        public string HowTo { get; set; }

        public string Line { get; set; }

        public string Platform { get; set; }

    }
}
