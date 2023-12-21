﻿using GIS.Database;
using GIS.Models;
using GIS.Services.CRUDServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services.ImplementServices
{
    public class PrismService : CRUDService<Prism>, IPrismService
    {
        public PrismService(DatabaseContext context) : base(context)
        {
        }
    }
}
