using AcHelper.Commands;
using NUnit.Framework;
using Telerik.JustMock;

namespace BuerTech.AcHelper.Tests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void IAcadCommand_Execute_MustBeCalled()
        {
            // Arrange
            var command = Mock.Create<IAcadCommand>();
            Mock.Arrange(() => command.Execute()).OccursAtLeast(3);

            // Act
            for (int i = 0; i < 3; i++)
            {
                command.Execute();
            }
            // command.Execute();

            // Assert
            Mock.Assert(command);
        }
    }
}
