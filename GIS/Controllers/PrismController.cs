using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Body;
using GIS.ViewModels.BodyComp;
using GIS.ViewModels.FaceNode;
using GIS.ViewModels.Prism;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("listPath")]
        public async Task<IActionResult> Get([FromQuery] string path)
        {
            var lstPrism = await _prismService.ReadAllAsync(e => true);
            var filteredPrism = lstPrism.Where(x => x.Path.Contains(path));
            List<string> paths = new List<string>();
            foreach (var item in filteredPrism)
            {
                paths.Add(item.Path);
            }
            if (paths.Count == 0)
            {
                return NotFound();
            }
            return Ok(paths);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddPrism addPrism)
        {
            var listPrism = await _prismService.ReadAllAsync(e => true);
            var filteredPrism = listPrism.Where(x => x.Path == addPrism.Path);
            if (filteredPrism.Count() > 0)
            { return Ok("Path này đã tồn tại! Tạo mới thất bại!"); }
            Prism prism = new()
            {
                Name = addPrism.Name,
                Path = addPrism.Path,
                Color = addPrism.Color,
                Height = addPrism.Height,
                Material = addPrism.Material
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdatePrism updatePrism)
        {
            Prism? foundPrism = await _prismService.ReadByIdAsync(id);

            if (foundPrism == null)
            {
                return NotFound();
            }
            else
            {
                if (updatePrism.Name != "")
                { foundPrism.Name = updatePrism.Name; }
                if (updatePrism.Path != "")
                { foundPrism.Path = updatePrism.Path; }
                if (updatePrism.Color != "")
                { foundPrism.Color = updatePrism.Color; }
                if (updatePrism.Height != 0)
                { foundPrism.Height = updatePrism.Height; }
                if (updatePrism.Material != "")
                { foundPrism.Material = updatePrism.Material; }

                Console.WriteLine(updatePrism.Height);
            }
            var listPrism = await _prismService.ReadAllAsync(e => true);
            var filteredPrism = listPrism.Where(x => x.Path == updatePrism.Path && x.Id != foundPrism.Id);
            if (filteredPrism.Count() > 0)
            {
                return Ok("Path này đã tồn tại. Cập nhật thất bại");
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
            string result = string.Concat(path.Trim().Substring(0, lastIndexOfExtension),"/");
            Console.WriteLine(result);

            var filteredPrisms = prisms.Where(prism =>
                                prism.Path == path
                            ).ToList();

            if (filteredPrisms.Count() == 0)
            {
                return NotFound("This path is not exist!");
            }

            //var bodyMaterial = bodyMaterials.FirstOrDefault(x => x.BodyId == filteredPrisms[0].Id);
            //Material material = new Material();
            //if (bodyMaterial != null)
            //{
            //    Console.WriteLine(bodyMaterial.Id);
            //    material = materials.First(x => x.Id == bodyMaterial.MaterialId);
            //}             

            var filteredFace = faces.Where(face =>
                                face.Path.StartsWith(result)
                            ).ToList();
            Console.WriteLine("filteredFace.Count()");
            Console.WriteLine(filteredFace.Count());
            foreach (var item in filteredFace)
            {
                Console.WriteLine(item.Path);
            }
            List<FaceListNode> nodesFaceList = new List<FaceListNode>();

            foreach (var face in filteredFace)
            {
                Console.WriteLine("face");
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
                        Console.WriteLine("I'mhere");
                        item.nodeIds.Add(faceNode.NodeId);
                    }
                }
            }

            List<List<List<List<double>>>> coordinates = new List<List<List<List<double>>>>();
            Node? node = new Node();

            for (int i = 0; i < filteredFace.Count(); i++)
            {
                Console.WriteLine(filteredFace.Count());
                coordinates.Add(new List<List<List<double>>>());
                coordinates[i].Add(new List<List<double>>());
                Console.WriteLine(nodesFaceList[i].nodeIds.Count);
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
                        Material = filteredPrisms[0].Material,
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
