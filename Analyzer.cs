using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace RevitShapeAnalyzer
{
    public class Analyzer
    {
        public Dictionary<string, int> GetObjectCounts(Document doc)
        {
            Dictionary<string, int> results =
                new Dictionary<string, int>();

            int walls = 0;
            int beams = 0;
            int columns = 0;
            int pipes = 0;
            int cubes = 0;
            int cylinders = 0;

            FilteredElementCollector collector =
                new FilteredElementCollector(doc)
                .WhereElementIsNotElementType();

            Options opt = new Options();

            foreach (Element elem in collector)
            {
                if (elem.Category == null)
                    continue;

                string catName = elem.Category.Name;

                // CATEGORY COUNTS

                if (catName.Contains("Walls"))
                    walls++;

                else if (catName.Contains("Structural Framing"))
                    beams++;

                else if (catName.Contains("Structural Columns"))
                    columns++;

                else if (catName.Contains("Pipes"))
                    pipes++;

                // GEOMETRY COUNTS

                GeometryElement geoElem =
                    elem.get_Geometry(opt);

                if (geoElem == null)
                    continue;

                foreach (GeometryObject geoObj in geoElem)
                {
                    Solid solid = geoObj as Solid;

                    if (solid == null)
                        continue;

                    if (solid.Volume <= 0)
                        continue;

                    int faceCount = solid.Faces.Size;

                    // BOX / CUBE LIKE
                    if (faceCount == 6)
                    {
                        cubes++;
                    }

                    // CYLINDER LIKE
                    else if (faceCount == 3)
                    {
                        cylinders++;
                    }
                }
            }

            // TOTAL OF ALL COUNTS

            int totalObjects =
                walls +
                beams +
                columns +
                pipes +
                cubes +
                cylinders;

            // FINAL RESULTS

            results.Add("Walls", walls);
            results.Add("Beams", beams);
            results.Add("Columns", columns);
            results.Add("Pipes", pipes);
            results.Add("Cube/Box Shapes", cubes);
            results.Add("Cylinder Shapes", cylinders);
            results.Add("Total Objects", totalObjects);

            return results;
        }
    }
}