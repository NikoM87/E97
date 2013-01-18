using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processor
{
    public abstract class Command
    {
         public  abstract void Execute();
    }

    public class MoveCommand : Command
    {
        public MoveCommand()
        {

        }

        public override void Execute()
        { 
        
        }

    }
}
