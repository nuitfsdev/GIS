using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.ImplementServices;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace GIS.Services.InterfaceServices
{
    public interface ISampleService : ICRUDService<Sample>
    {
        
    }
}