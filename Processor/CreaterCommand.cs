using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
    class CreaterCommand
    {
        static public Command CreateCommand( UInt16 command )
        {
            return new MoveCommand();
        }
    }


}
