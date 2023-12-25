﻿using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Body;
using GIS.ViewModels.FaceNode;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyCompController : ControllerBase
    {
        private readonly IBodyCompService _bodyCompService;
        private readonly IFaceNodeService _faceNodeService;
        private readonly IFaceService _faceService;
        private readonly INodeService _nodeService;
        private readonly IBodyMaterialService _bodyMaterialService;
        private readonly IMaterialService _materialService;

        public BodyCompController(IBodyCompService bodyCompService, IFaceNodeService faceNodeService,
            IFaceService faceService, INodeService nodeService, IBodyMaterialService bodyMaterialService,
            IMaterialService materialService)
        {
            _bodyCompService = bodyCompService;
            _faceNodeService = faceNodeService;
            _faceService = faceService;
            _nodeService = nodeService;
            _bodyMaterialService = bodyMaterialService;
            _materialService = materialService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bodyCompService.ReadAllAsync(e => true));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _bodyCompService.ReadByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
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
        public async Task<IActionResult> Delete(Guid id)
        {
            BodyComp? bodyComp = await _bodyCompService.ReadByIdAsync(id);

            if (bodyComp == null)
            {
                return NotFound();
            }
            return Ok(await _bodyCompService.DeleteAsync(id));
        }

        [HttpPut("path")]
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
            IEnumerable<BodyComp> bodyComps = await _bodyCompService.ReadAllAsync(e => true);
            IEnumerable<Face> faces = await _faceService.ReadAllAsync(e => true);
            IEnumerable<FaceNode> faceNodes = await _faceNodeService.ReadAllAsync(e => true);
            IEnumerable<Node> nodes = await _nodeService.ReadAllAsync(e => true);
            IEnumerable<Material> materials = await _materialService.ReadAllAsync(e => true);
            IEnumerable<BodyMaterial> bodyMaterials = await _bodyMaterialService.ReadAllAsync(e => true);

            int lastIndexOfExtension = path.Trim().IndexOf(".");
            Console.WriteLine(lastIndexOfExtension);
            string result = path.Trim().Substring(0, lastIndexOfExtension);
            Console.WriteLine(result);

            var filteredBodyComps = bodyComps.Where(bodyComp =>
                                bodyComp.Path == path
                            ).ToList();

            if (filteredBodyComps.Count() == 0)
            {
                return NotFound("This path is not exist!");
            }

            var bodyMaterial = bodyMaterials.FirstOrDefault(x => x.BodyId == filteredBodyComps[0].Id);
            Material material = new Material();
            if (bodyMaterial != null)
            {
                Console.WriteLine(bodyMaterial.Id);
                material = materials.First(x => x.Id == bodyMaterial.MaterialId);
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
                        BuildingName = filteredBodyComps[0].Name,
                        Path = filteredBodyComps[0].Path,
                        Color = filteredBodyComps[0].Color,
                        Width = filteredBodyComps[0].Width,
                        Material = material.Name,
                        Id = filteredBodyComps[0].Id
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
