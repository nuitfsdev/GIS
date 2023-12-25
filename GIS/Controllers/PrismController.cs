using GIS.Models;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Body;
using GIS.ViewModels.FaceNode;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    public class PrismController : ControllerBase
    {
        private readonly IPrismService _prismService;
        private readonly IFaceNodeService _faceNodeService;
        private readonly IFaceService _faceService;
        private readonly INodeService _nodeService;
        private readonly IBodyMaterialService _bodyMaterialService;
        private readonly IMaterialService _materialService;

        public PrismController(IPrismService prismService,IFaceNodeService faceNodeService, 
            IFaceService faceService, INodeService nodeService, IBodyMaterialService bodyMaterialService, 
            IMaterialService materialService)
        {
            _prismService = prismService;
            _faceNodeService = faceNodeService;
            _faceService = faceService;
            _nodeService = nodeService;
            _bodyMaterialService = bodyMaterialService;
            _materialService = materialService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _prismService.ReadAllAsync(e => true));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _prismService.ReadByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddPrism addPrism)
        {
            Prism prism = new()
            {
                Name = addPrism.Name,
                Path = addPrism.Path,
                Color = addPrism.Color,
                Height = addPrism.Height
            };
            return Ok(await _prismService.CreateAsync(prism));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Prism? prism = await _prismService.ReadByIdAsync(id);

            if (prism == null)
            {
                return NotFound();
            }
            return Ok(await _prismService.DeleteAsync(id));
        }

        [HttpPut("path")]
        public async Task<IActionResult> Put([FromQuery] string path, [FromBody] AddPrism updatePrism)
        {
            IEnumerable<Prism> prisms = await _prismService.ReadAllAsync(e => true);
            Prism? foundPrism = prisms.FirstOrDefault(prism => prism.Path == path);

            if (foundPrism == null)
            {
                return NotFound();
            }
            else
            {
                foundPrism.Name = updatePrism.Name;
                foundPrism.Path = updatePrism.Path;
                foundPrism.Color = updatePrism.Color;
                foundPrism.Height = updatePrism.Height;
            }
            return Ok(await _prismService.UpdateAsync(foundPrism));
        }

        [HttpGet("path")]
        public async Task<IActionResult> GetGeojsonObject([FromQuery] string path)
        {
            IEnumerable<Prism> prisms = await _prismService.ReadAllAsync(e => true);
            IEnumerable<Face> faces = await _faceService.ReadAllAsync(e => true);
            IEnumerable<FaceNode> faceNodes = await _faceNodeService.ReadAllAsync(e => true);
            IEnumerable<Node> nodes = await _nodeService.ReadAllAsync(e => true);
            IEnumerable<Material> materials = await _materialService.ReadAllAsync(e => true);
            IEnumerable<BodyMaterial> bodyMaterials = await _bodyMaterialService.ReadAllAsync(e => true);

            int lastIndexOfExtension = path.Trim().IndexOf(".");
            Console.WriteLine(lastIndexOfExtension);
            string result = path.Trim().Substring(0, lastIndexOfExtension);
            Console.WriteLine(result);

            var filteredPrisms = prisms.Where(prism =>
                                prism.Path == path
                            ).ToList();

            var bodyMaterial = bodyMaterials.FirstOrDefault(x => x.BodyId == filteredPrisms[0].Id);
            Material material = new Material();
            if (bodyMaterial != null)
            {
                material = materials.First(x => x.Id == bodyMaterial.Id);
            }

            if (filteredPrisms.Count() == 0)
            {
                return NotFound("This path is not exist!");
            }    

            var filteredFace = faces.Where(face =>
                                face.Path.StartsWith(result)
                            ).ToList();
            List<FaceListNode> nodesFaceList = new List<FaceListNode>();

            foreach (var face in filteredFace)
            {
                nodesFaceList.Add(new()
                {
                    faceId = face.Id,
                });
            }

            foreach (var faceNode in faceNodes)
            {
                foreach (var item in nodesFaceList)
                {
                    if (faceNode.FaceId == item.faceId)
                    {
                        item.nodeIds.Add(faceNode.NodeId);
                    }
                }
            }

            List<List<List<List<double>>>> coordinates = new List<List<List<List<double>>>>();
            Node? node = new Node();

            for (int i = 0; i < faces.Count(); i++)
            {
                coordinates.Add(new List<List<List<double>>>());
                coordinates[i].Add(new List<List<double>>());
                for (int j = 0; j < nodesFaceList[i].nodeIds.Count; j++)
                {
                    coordinates[i][0].Add(new List<double>());
                    node = await _nodeService.ReadByIdAsync(nodesFaceList[i].nodeIds[j]);
                    if (node != null)
                    {
                        coordinates[i][0][j].Add(node.X);
                        coordinates[i][0][j].Add(node.Y);
                        coordinates[i][0][j].Add(node.Z);
                    }
                }
            }

            Feature feature = new()
            {
                Properties =
                    {
                        BuildingName = filteredPrisms[0].Name,
                        Path = filteredPrisms[0].Path,
                        Color = filteredPrisms[0].Color,
                        Height = filteredPrisms[0].Height,
                        Material = material.Name,
                        Id = filteredPrisms[0].Id
                    },
                Geometry =
                    {
                        Coordinates = coordinates
                    }
            };

            GeoJsonObject geoJsonObject = new GeoJsonObject()
            {
                Timestamp = DateTime.UtcNow.ToString(),
                Features = new List<Feature> { feature }
            };
            return Ok(geoJsonObject);
        }
    }
}
