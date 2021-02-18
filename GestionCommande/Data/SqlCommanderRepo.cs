using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionCommande.Models;

namespace GestionCommande.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        // DI
        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException(nameof(cmd));

            _context.Commands.Add(cmd);

        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(e => e.Id == id);
        }

        public bool SaveChanges()
        {
            return ( _context.SaveChanges() >= 0 );
        }
    }
}
