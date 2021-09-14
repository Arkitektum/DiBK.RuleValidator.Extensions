using OSGeo.OGR;
using System.Text.RegularExpressions;

namespace DiBK.RuleValidator.Extensions
{
    public static class GdalExtensions
    {
        public static double[] GetPoint(this Geometry geometry, int index)
        {
            var point = new double[3];
            geometry.GetPoint(index, point);

            return point;
        }

        public static double[][] GetPoints(this Geometry geometry)
        {
            var pointCount = geometry.GetPointCount();
            var points = new double[pointCount][];

            for (int i = 0; i < pointCount; i++)
                points[i] = GetPoint(geometry, i);

            return points;
        }

        public static bool EqualsTopologically(this Geometry geometry, Geometry other)
        {
            return geometry.Within(other) && other.Within(geometry);
        }

        public static string ToWkt(this Geometry geometry, int? decimals)
        {
            geometry.ExportToWkt(out var wkt);

            if (decimals.HasValue && decimals.Value > 0)
                return Regex.Replace(wkt, $@"(\d+\.\d{{{decimals}}})\d+", "$1");
            
            return wkt;
        }
    }
}
