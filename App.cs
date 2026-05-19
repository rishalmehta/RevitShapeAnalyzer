using Autodesk.Revit.UI;
using System.Reflection;

namespace RevitShapeAnalyzer
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            string tabName = "Shape Analyzer";

            try
            {
                app.CreateRibbonTab(tabName);
            }
            catch { }

            RibbonPanel panel =
                app.CreateRibbonPanel(tabName, "Model Tools");

            string assemblyPath =
                Assembly.GetExecutingAssembly().Location;

            PushButtonData buttonData =
                new PushButtonData(
                    "AnalyzeBtn",
                    "Launch\nAnalyzer",
                    assemblyPath,
                    "RevitShapeAnalyzer.Command"
                );

            PushButton button =
                panel.AddItem(buttonData) as PushButton;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication app)
        {
            return Result.Succeeded;
        }
    }
}