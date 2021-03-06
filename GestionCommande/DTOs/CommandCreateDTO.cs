﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionCommande.DTOs
{
    public class CommandCreateDTO
    {
        //public int Id { get; set; } => it is auto generated so doesn't belong to the contract exposed to the client

        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required]
        public string Line { get; set; }
        [Required]
        public string Platform { get; set; }

    }
}
