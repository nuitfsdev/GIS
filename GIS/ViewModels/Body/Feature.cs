namespace GIS.ViewModels.Body
{
    public class Geometry
    {
        public string Type { get; set; } = "MultiPolygon";
        public List<List<List<List<double>>>> Coordinates { get; set; }
    }

    public class Properties
    {
        public string BuildingName { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public double Height { get; set; }
        public double Width { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Material { get; set; } = "Xi măng";
        public Guid Id { get; set; }
    }

    public class Feature
    {
        public string Type { get; set; } = "Feature";
        public Properties Properties { get; set; } = new Properties();
        public Geometry Geometry { get; set; } = new Geometry();
    }
}
