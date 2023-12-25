using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Body;
using GIS.ViewModels.FaceNode;
using GIS.ViewModels.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyController : ControllerBase
    {
        private readonly IPrismService _prismService;
        private readonly IBodyCompService _bodyCompService;
        private readonly IFaceNodeService _faceNodeService;
        private readonly IFaceService _faceService;
        private readonly INodeService _nodeService;

        public BodyController(IPrismService prismService, IBodyCompService bodyCompService,
            IFaceNodeService faceNodeService, IFaceService faceService, INodeService nodeService)
        {
            _prismService = prismService;
            _bodyCompService = bodyCompService;
            _faceNodeService = faceNodeService;
            _faceService = faceService;
            _nodeService = nodeService;
        }       

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddBody addBody)
        {
            BodyComp? bodyComp = null;
            Prism? prism = null;
            if (addBody.Width == null)
            {
                prism = new()
                {
                    Name = addBody.Name,
                    Path = addBody.Path,
                    Color = addBody.Color,
                    Height = addBody.Height
                };
            }
            else
            {
                bodyComp = new()
                {
                    Name = addBody.Name,
                    Path = addBody.Path,
                    Color = addBody.Color,
                    Width = addBody.Width
                };
            }
            if (prism != null)
                return Ok(await _prismService.CreateAsync(prism));
            return bodyComp != null ? Ok(await _bodyCompService.CreateAsync(bodyComp))
                : Ok(null);
        }       

        [HttpPost("body-comp")]
        public async Task<IActionResult> Post([FromBody] AddBodyComp addBodyComp)
        {
            BodyComp bodyComp = new()
            {
                Name = addBodyComp.Name,
                Path = addBodyComp.Path,
                Color = addBodyComp.Color,
                Width = addBodyComp.Width
            };
            return Ok(await _bodyCompService.CreateAsync(bodyComp));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrism(Guid id)
        {
            Prism? prism = await _prismService.ReadByIdAsync(id);
            BodyComp? bodyComp = await _bodyCompService.ReadByIdAsync(id);

            if (prism == null && bodyComp == null)
            {
                return NotFound();
            }
            return prism == null ? Ok(await _bodyCompService.DeleteAsync(id))
                : Ok(await _prismService.DeleteAsync(id));
        }

        [HttpDelete("body-comp/{id}")]
        public async Task<IActionResult> DeleteBodyComp(Guid id)
        {
            BodyComp? bodyComp = await _bodyCompService.ReadByIdAsync(id);
            if (bodyComp == null)
            {
                return NotFound();
            }
            return Ok(await _bodyCompService.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AddBody updateBody)
        {

            BodyComp? bodyComp = await _bodyCompService.ReadByIdAsync(id);
            Prism? prism = await _prismService.ReadByIdAsync(id);
            if (bodyComp == null && prism == null)
            {
                return NotFound();
            }
            else if (bodyComp != null)
            { 
                bodyComp.Name = updateBody.Name;
                bodyComp.Path = updateBody.Path;
                bodyComp.Color = updateBody.Color;
                bodyComp.Width = updateBody.Width;
            } 
            else if (prism != null) 
            {
                prism.Name = updateBody.Name;
                prism.Path = updateBody.Path;
                prism.Color = updateBody.Color;
                prism.Height = updateBody.Height;
            }
 

            return prism == null ? Ok(await _bodyCompService.UpdateAsync(bodyComp))
                : Ok(await _prismService.UpdateAsync(prism));
        }

        [HttpPut("body-comp/path")]
        public async Task<IActionResult> Put([FromQuery] string path, [FromBody] AddBodyComp updateBodyComp)
        {
            IEnumerable<BodyComp> bodyComps = await _bodyCompService.ReadAllAsync(e => true);
            BodyComp? foundBodyComp = bodyComps.FirstOrDefault(bodyComp => bodyComp.Path == path);

            if (foundBodyComp == null)
            {
                return NotFound();
            }
            else
            {
                foundBodyComp.Name = updateBodyComp.Name;
                foundBodyComp.Path = updateBodyComp.Path;
                foundBodyComp.Color = updateBodyComp.Color;
                foundBodyComp.Width = updateBodyComp.Width;
            }
            return Ok(await _bodyCompService.UpdateAsync(foundBodyComp));
        }

        [HttpGet("path")]
        public async Task<IActionResult> GetGeojsonObject([FromQuery] string path)
        {
            IEnumerable<Prism> prisms = await _prismService.ReadAllAsync(e => true);
            IEnumerable<BodyComp> bodyComps = await _bodyCompService.ReadAllAsync(e => true);
            IEnumerable<Face> faces = await _faceService.ReadAllAsync(e => true);
            IEnumerable<FaceNode> faceNodes = await _faceNodeService.ReadAllAsync(e => true);
            IEnumerable<Node> nodes = await _nodeService.ReadAllAsync(e => true);

            string extension = ".geojson";

            int lastIndexOfExtension = path.Trim().IndexOf(".");
            Console.WriteLine(lastIndexOfExtension);
            string result = path.Trim().Substring(0, lastIndexOfExtension);
            Console.WriteLine(result);

            var filteredPrisms = prisms.Where(prism =>
                                prism.Path == path
                            ).ToList();

            Console.WriteLine(filteredPrisms[0].Path);
            Console.WriteLine(filteredPrisms[0].Height);

            var filteredBodyComps = bodyComps.Where(bodyComp => bodyComp.Path == path).ToList();

            var filteredFace = faces.Where(face =>
                                face.Path.StartsWith(result)
                            ).ToList();
            List<FaceListNode> nodesFaceList = new List<FaceListNode>();
            Node? node = new Node();

            {
                Console.WriteLine("face null");
            }          

            foreach (var face in filteredFace)
            { 
                Console.WriteLine("face id",face.Id);
                nodesFaceList.Add(new()
                {
                    faceId = face.Id,
                });
            }

            foreach (var faceNode in faceNodes)
            {
                Console.WriteLine("faceNode");
                foreach (var item in nodesFaceList)
                {
                    if(faceNode.FaceId == item.faceId)
                    {
                        item.nodeIds.Add(faceNode.NodeId);
                    }
                }                
            }

            List<List<List<List<double>>>> coordinates = new List<List<List<List<double>>>>();

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

            if (filteredPrisms.Count() != 0 && coordinates != null)
            {
                Feature feature = new()
                {
                    Properties =
                    {
                        BuildingName = filteredPrisms[0].Name,
                        Path = filteredPrisms[0].Path,
                        Color = filteredPrisms[0].Color,
                        Height = filteredPrisms[0].Height,
                        Id = filteredPrisms[0].Id
                    },
                    Geometry =
                    {
                        Coordinates = coordinates
                    }
                };
                return Ok(feature);
            }
            else if (filteredBodyComps.Count() != 0 && coordinates != null)
            {
                Feature feature = new()
                {
                    Properties =
                    {
                        BuildingName = filteredBodyComps[0].Name,
                        Path = filteredBodyComps[0].Path,
                        Color = filteredBodyComps[0].Color,
                        Width = filteredBodyComps[0].Width,
                        Id = filteredBodyComps[0].Id
                    },
                    Geometry =
                    {
                        Coordinates = coordinates
                    }
                };
                return Ok(feature);
            }
            return Ok("Not success");
        }
    }
}
