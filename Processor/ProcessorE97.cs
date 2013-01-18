using System;

namespace Processor.Tests
{
    public class ProcessorE97
    {
        private IRam ram;
        private Int16[] registers = new Int16[4];

        public ProcessorE97(IRam ram)
        {
            this.ram = ram;
        }
        public Int16 R0
        {
            set { registers[0] = value; }
            get { return registers[0]; }
        }
        public Int16 R1
        {
            set { registers[1] = value; }
            get { return registers[1]; }
        }
        public Int16 R2
        {
            set { registers[2] = value; }
            get { return registers[2]; }
        }
        public Int16 R3
        {
            set { registers[3] = value; }
            get { return registers[3]; }
        }

        public UInt16 PC { set; get; }
        public UInt16 SP { set; get; }
        public UInt16 PS { set; get; }

        public UInt16 PK { set; get; }
        public UInt16 Op1 { set; get; }
        public UInt16 Op2 { set; get; }
        public Int16 Sum { set; get; }

        public void Step()
        {
            PK = ReadWord();
            ExecuteCommand();
        }

        private UInt16 ReadWord()
        {
            UInt16 word = ram.Read( PC );
            PC += 2;

            return word;
        }

        private void ExecuteCommand()
        {
            byte codeOperation = Convert.ToByte((PK >> 8) & 0x0F);
            byte mode = Convert.ToByte((PK >> 12) & 0x0F);

            byte operator1 = Convert.ToByte((PK >> 4) & 0x0F);
            byte operator2 = Convert.ToByte((PK >> 0) & 0x0F);

            switch (codeOperation)
            {
                case 0x0:
                    {
                        break;
                    }
                case 0x1:
                    {
                        registers[operator2] = registers[operator1];
                        break;
                    }
                case 0x2:
                    {
                        registers[operator2] += registers[operator1];
                        break;
                    }
                case 0x3:
                    {
                        registers[operator2] -= registers[operator1];
                        break;
                    }

                case 0x5:
                    {
                        registers[operator2] *= registers[operator1];
                        break;
                    }

                case 0x6:
                    {
                        registers[operator2] /= registers[operator1];
                        break;
                    }
                case 0xF:
                    {
                        PC = 0;
                        break;
                    }
                default:
                    {
                        new NotImplementedException("Неизвестная команда");
                        break;
                    }
            }
        }

        public void Run()
        {
            do
            {
                Step();
            }
            while (PC != 0);
        }
    }
}
