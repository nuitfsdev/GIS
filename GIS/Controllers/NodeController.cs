using GIS.Models;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.Body;
using GIS.ViewModels.Material;
using GIS.ViewModels.Node;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly INodeService _nodeService;
        public NodeController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _nodeService.ReadAllAsync(e => true));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _nodeService.ReadByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Node addNode)
        {
            List<Node> nodeList = (List<Node>)await _nodeService.ReadAllAsync(e => true);
            Node node = null;

            var filteredNodes = nodeList.Where(node =>
                node.X == addNode.X &&
                node.Y == addNode.Y &&
                node.Z == addNode.Z
            ).ToList();
            if(filteredNodes.Count == 0)
            {
                node = new()
                {
                    X = addNode.X,
                    Y = addNode.Y,
                    Z = addNode.Z
                };
            }    
            return Ok(await _nodeService.CreateAsync(node));
        }

        [HttpPost("coordinates")]
        public async Task<IActionResult> PostNodes([FromBody] Coordinates a)
        {
            Console.WriteLine("đã vào 1");

            IEnumerable<Node> nodeList = await _nodeService.ReadAllAsync(e => true);
            Node node = null;
            var filteredNodes = nodeList.Where(node =>
                                node.X == 1 &&
                                node.Y == 2 &&
                                node.Z == 3
                            ).ToList();
            if(a.nodeData != null)
            {
                Console.WriteLine("đã vào 1");
                for (int i = 0; i < a.nodeData.Count; i++)
                {
                    for (int j = 0; j < a.nodeData[i].Count; j++)
                    {
                        for (int k = 0; k < a.nodeData[i][j].Count; k++)
                        {
                            Console.WriteLine("đã vào j = ", a.nodeData[i][j][0]);
                            for (int l = 0; l < a.nodeData[i][j][k].Count; l++)
                            {
                                Console.WriteLine("a = ",a.nodeData[i][j][k][0]);
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
                Console.WriteLine("null thiệt à vl");
            }

            return Ok("Success");
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Node updateNode)
        {
            Node? node = await _nodeService.ReadByIdAsync(id);
            if (node == null)
            {
                return NotFound();
            }
            node.X = updateNode.X;
            node.Y = updateNode.Y;
            node.Z = updateNode.Z;

            return Ok(await _nodeService.UpdateAsync(node));
        }


        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool isDelete = await _nodeService.DeleteAsync(id);
            if (!isDelete)
            {
                return NotFound();
            }

            return Ok(isDelete);
        }

        [HttpDelete("coordinates")]
        public async Task<IActionResult> DeleteByCoordinates([FromBody] Coordinates a)
        {
            List<Node> nodeList = (List<Node>)await _nodeService.ReadAllAsync(e => true);

            var filteredNodes = nodeList.Where(node =>
                                node.X == 1 &&
                                node.Y == 2 &&
                                node.Z == 3
                            ).ToList();

            for (int i = 0; i < a.nodeData.Count; i++)
            {
                for (int j = 0; j < a.nodeData[i].Count; j++)
                {
                    for (int k = 0; k < a.nodeData[i][j].Count; k++)
                    {
                        for (int l = 0; l < a.nodeData[i][j][k].Count; l++)
                        {
                            filteredNodes = nodeList.Where(node =>
                                node.X == a.nodeData[i][j][k][0] &&
                                node.Y == a.nodeData[i][j][k][1] &&
                                node.Z == a.nodeData[i][j][k][2]
                            ).ToList();

                            if (filteredNodes.Count > 0)
                            {
                                for(int m = 0; m < filteredNodes.Count; m++)
                                    await _nodeService.DeleteAsync(filteredNodes[m].Id);
                            }
                        }
                    }
                }
            }
            return Ok("Success");
        }
    }
}
