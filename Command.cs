using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;

namespace RevitShapeAnalyzer
{
    [Transaction(TransactionMode.ReadOnly)]
    public class Command : IExternalCommand
    {
        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIDocument uidoc =
                commandData.Application.ActiveUIDocument;

            Document doc = uidoc.Document;

            Analyzer analyzer = new Analyzer();

            var results = analyzer.GetObjectCounts(doc);

            UI.MainWindow window =
                new UI.MainWindow(results);

            window.ShowDialog();

            return Result.Succeeded;
        }
    }
}