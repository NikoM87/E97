using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Processor.Tests
{
    [TestClass]
    public class ProcessorE97Tests
    {
        [TestMethod]
        public void CreateProcessor()
        {
            ProcessorE97 processor = new ProcessorE97( new Ram() );

            Assert.AreEqual( 0, processor.PC );
        }

        [TestMethod]
        public void RunSingleWordCommand()
        {
            var ram = new Mock<IRam>();
            ram.Setup( x => x.Read( 0x0000 ) )
                .Returns( 0x0123 );

            ProcessorE97 processor = new ProcessorE97( ram.Object );

            int programCounter = processor.PC;

            processor.Step();

            Assert.AreEqual( 0x0123, processor.PK );
            Assert.AreEqual( programCounter + 2, processor.PC );
        }

        [TestMethod]
        public void RunNopCommand()
        {
            var ram = new Mock<IRam>();
            ram.Setup( x => x.Read( 0x0000 ) )
                .Returns( 0x0000 );

            ProcessorE97 processor = new ProcessorE97( ram.Object );

            processor.Step();
        }

        [TestMethod]
        public void RunMoveCommand()
        {
            var ram = new Mock<IRam>();
            ram.Setup( x => x.Read( 0x0000 ) )
                .Returns( 0x0103 );

            ProcessorE97 processor = new ProcessorE97( ram.Object );

            processor.R0 = 10;
            processor.Step();

            Assert.AreEqual( 10, processor.R0 );
            Assert.AreEqual( processor.R0, processor.R3 );
        }

        [TestMethod]
        public void RunAdditionCommand()
        {
            var ram = new Mock<IRam>();
            ram.Setup( x => x.Read( 0x0000 ) )
                .Returns( 0x0201 );

            ProcessorE97 processor = new ProcessorE97( ram.Object );

            processor.R0 = 10;
            processor.R1 = 2;
            processor.Step();

            Assert.AreEqual( 10, processor.R0 );
            Assert.AreEqual( 12, processor.R1 );
        }

        [TestMethod]
        public void RunSubtractCommand()
        {
            var ram = new Mock<IRam>();
            ram.Setup( x => x.Read( 0x0000 ) )
                .Returns( 0x0301 );

            ProcessorE97 processor = new ProcessorE97( ram.Object );

            processor.R0 = 10;
            processor.R1 = 2;
            processor.Step();

            Assert.AreEqual( 10, processor.R0 );
            Assert.AreEqual( -8, processor.R1 );
        }

        [TestMethod]
        public void RunMultiplyCommand()
        {
            var ram = new Mock<IRam>();
            ram.Setup( x => x.Read( 0x0000 ) )
                .Returns( 0x0501 );

            ProcessorE97 processor = new ProcessorE97( ram.Object );

            processor.R0 = 10;
            processor.R1 = 2;
            processor.Step();

            Assert.AreEqual( 10, processor.R0 );
            Assert.AreEqual( 20, processor.R1 );
        }

        [TestMethod]
        public void RunDivideCommand()
        {
            var ram = new Mock<IRam>();
            ram.Setup( x => x.Read( 0x0000 ) )
                .Returns( 0x0601 );

            ProcessorE97 processor = new ProcessorE97( ram.Object );

            processor.R0 = 2;
            processor.R1 = 10;
            processor.Step();

            Assert.AreEqual( 2, processor.R0 );
            Assert.AreEqual( 5, processor.R1 );
        }

        [TestMethod]
        public void RunStopCommand()
        {
            var ram = new Mock<IRam>();
            ram.Setup( x => x.Read( 0x0000 ) )
                .Returns( 0x0F00 );

            ProcessorE97 processor = new ProcessorE97( ram.Object );

            processor.Step();

            Assert.AreEqual( 0x0000, processor.PC );
        }

        [TestMethod]
        public void RunProgram()
        {
            var ram = new Mock<IRam>();
            ram.Setup( x => x.Read( 0x0000 ) ).Returns( 0x0103 );
            ram.Setup( x => x.Read( 0x0002 ) ).Returns( 0x0213 );
            ram.Setup( x => x.Read( 0x0004 ) ).Returns( 0x0F00 );

            ProcessorE97 processor = new ProcessorE97( ram.Object );

            processor.R0 = -2;
            processor.R1 = 5;
            processor.Run();

            Assert.AreEqual( -2, processor.R0 );
            Assert.AreEqual( 5, processor.R1 );
            Assert.AreEqual( 3, processor.R3 );
        }
    }
}
