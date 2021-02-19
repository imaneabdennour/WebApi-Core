using AutoMapper;
using GestionCommande.Data;
using GestionCommande.DTOs;
using GestionCommande.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionCommande.Controllers
{
    [Route("api/commands/")]     // eq to api/commands
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all the commands
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commandItems));
        }

        /// <summary>
        /// Get a command by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // Get api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]  
        public ActionResult<CommandReadDTO> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            if(commandItem != null)
                return Ok(_mapper.Map<CommandReadDTO>(commandItem));

            return NotFound();
        }
        
        /// <summary>
        /// Create a new command
        /// </summary>
        /// <param name="commandCreateDTO"></param>
        /// <returns></returns>
        // Post api/commands
        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO commandCreateDTO)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDTO);

            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDTO = _mapper.Map<CommandReadDTO>(commandModel);

            // Response : Created
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDTO.Id }, commandReadDTO);

            /*             
             GetCommandId is my method name ( i specified it lfoq ) | name fo route
             I pass the Id for the method | route data
             The content value to format in the entity body
            */
        }

        /// <summary>
        /// Update a command
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<CommandReadDTO> UpdateCommand(int id, CommandUpdateDTO commandUpdateDTO)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);

            if (commandModelFromRepo == null)
                return NotFound();

            _mapper.Map(commandUpdateDTO, commandModelFromRepo);    //updates commandModelFromRepo with content dial 1st param

            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();

            // Response : OK
        }

        /// <summary>
        /// Delete a command
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);

            if (commandModelFromRepo == null)
                return NotFound();

            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
