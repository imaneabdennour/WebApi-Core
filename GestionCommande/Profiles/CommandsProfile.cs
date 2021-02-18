using AutoMapper;
using GestionCommande.DTOs;
using GestionCommande.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionCommande.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source -> Target
            CreateMap<Command, CommandReadDTO>();   // from command to commandreaddto

            CreateMap<CommandCreateDTO, Command>();

            CreateMap<CommandUpdateDTO, Command>();
        }
    }
}
