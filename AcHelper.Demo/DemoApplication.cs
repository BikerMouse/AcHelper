using Autodesk.AutoCAD.Runtime;
using System;

[assembly: ExtensionApplication(typeof(AcHelper.Demo.DemoApplication))]
[assembly: CommandClass(typeof(AcHelper.Demo.Commands))]
[assembly: CLSCompliant(false)]
namespace AcHelper.Demo
{
    public class DemoApplication : IExtensionApplication
    {
        public void Initialize()
        {
            Active.WriteMessage("\n*** AcHelper Demo loaded ***");
        }

        public void Terminate()
        {
        }
    }
}
