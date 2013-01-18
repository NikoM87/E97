using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
    public interface IRam
    {
        UInt16 Read(UInt16 address);
        void Write(UInt16 address);
    }

    public class Ram : IRam
    {
        public UInt16 Read(UInt16 address)
        {

            return 0;
        }

        public void Write(UInt16 address)
        {
            return;
        }
    }
}
