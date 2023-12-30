namespace GIS.ViewModels.Body
{
    public class GeoJsonObject
    {
        public string Type { get; set; } = "FeatureCollection";
        public string Generator { get; set; } = "NHÓM 4";
        public string Copyright { get; set; } = "Nhóm 4";
        public string Timestamp { get; set; } = "2023-12-25T07:56:51.795Z";
        public List<Feature> Features { get; set; } = new List<Feature>();
    }
}
