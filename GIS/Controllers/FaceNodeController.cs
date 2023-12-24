using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.FaceNode;
using GIS.ViewModels.Node;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaceNodeController : ControllerBase
    {
        private readonly IFaceNodeService _faceNodeService;
        private readonly IFaceService _faceService;
        private readonly INodeService _nodeService;

        public FaceNodeController(IFaceNodeService faceNodeService, IFaceService faceService, INodeService nodeService)
        {
            _faceNodeService = faceNodeService;
            _faceService = faceService;
            _nodeService = nodeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _faceNodeService.ReadAllAsync(e => true));
        }

        // api thêm cả face và node vào cùng một lúc
        [HttpPost("face/node")]
        public async Task<IActionResult> PostNodes([FromBody] AddFaceAndNode a)
        {

            IEnumerable<Node> nodeList = await _nodeService.ReadAllAsync(e => true);
            Node node = null;
            Face face = null;
            string pathTail = "";
            var filteredNodes = nodeList.Where(node =>
                                node.X == 1 &&
                                node.Y == 2 &&
                                node.Z == 3
                            ).ToList();
            if (a.nodeData != null)
            {
                Console.WriteLine("đã vào 1");
                for (int i = 0; i < a.nodeData.Count; i++)
                {
                    pathTail = $"/{i}";
                    if(a.GeneralPath != null)
                    {
                        face = new()
                        {
                             Path = string.Concat(a.GeneralPath, pathTail)
                        };
                        await _faceService.CreateAsync(face);
                    }    
                    
                    for (int j = 0; j < a.nodeData[i].Count; j++)
                    {
                        for (int k = 0; k < a.nodeData[i][j].Count; k++)
                        {
                            Console.WriteLine("đã vào j = ", a.nodeData[i][j][0]);
                            for (int l = 0; l < a.nodeData[i][j][k].Count; l++)
                            {
                                Console.WriteLine("a = ", a.nodeData[i][j][k][0]);
                                filteredNodes = nodeList.Where(node =>
                                     node.X == a.nodeData[i][j][k][0] &&
                                     node.Y == a.nodeData[i][j][k][1] &&
                                     node.Z == a.nodeData[i][j][k][2]
                                 ).ToList();

                                if (filteredNodes.Count == 0)
                                {
                                    node = new()
                                    {
                                        X = a.nodeData[i][j][k][0],
                                        Y = a.nodeData[i][j][k][1],
                                        Z = a.nodeData[i][j][k][2]
                                    };

                                    Console.WriteLine("đã vào 2");

                                    await _nodeService.CreateAsync(node);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("null");
            }

            return Ok("Success");
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddFaceAndNode a)
        {

            IEnumerable<Node> nodeList = await _nodeService.ReadAllAsync(e => true);
            IEnumerable<Face> faceList = await _faceService.ReadAllAsync(e => true);

            Node node = new Node();
            Face face = new Face();
            string pathTail = "";
            var filteredNodes = nodeList.Where(node =>
                                node.X == 1 &&
                                node.Y == 2 &&
                                node.Z == 3
                            ).ToList();
            var filteredFaces = faceList.Where(face =>
                                face.Path == ""
                            ).ToList();
            if (a.nodeData != null)
            {
                for (int i = 0; i < a.nodeData.Count; i++)
                {
                    pathTail = $"/{i}";
                    if (a.GeneralPath != null)
                    {
                        face = new()
                        {
                            Path = string.Concat(a.GeneralPath, pathTail)
                        };
                        await _faceService.CreateAsync(face);
                    }

                    for (int j = 0; j < a.nodeData[i].Count; j++)
                    {
                        for (int k = 0; k < a.nodeData[i][j].Count; k++)
                        {
                            Console.WriteLine("đã vào j = ", a.nodeData[i][j][0]);
                            for (int l = 0; l < a.nodeData[i][j][k].Count; l++)
                            {
                                Console.WriteLine("a = ", a.nodeData[i][j][k][0]);
                                filteredNodes = nodeList.Where(node =>
                                     node.X == a.nodeData[i][j][k][0] &&
                                     node.Y == a.nodeData[i][j][k][1] &&
                                     node.Z == a.nodeData[i][j][k][2]
                                 ).ToList();

                                if (filteredNodes.Count == 0)
                                {
                                    node = new()
                                    {
                                        X = a.nodeData[i][j][k][0],
                                        Y = a.nodeData[i][j][k][1],
                                        Z = a.nodeData[i][j][k][2]
                                    };

                                    Console.WriteLine("đã vào 2");

                                    await _nodeService.CreateAsync(node);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("null");
            }

            return Ok("Success");
        }
    }
}
