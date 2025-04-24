using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Bindings.ORiN3.Server;
using Codeer.LowCode.Blazor.DesignLogic;
using Codeer.LowCode.Blazor.Repository;
using Microsoft.AspNetCore.Mvc;
using ORiN3App.Server.Services;

namespace ORiN3App.Server.Controllers
{
    [ApiController]
    [Route("api/orin3")]
    public class ORiN3Controller : ControllerBase
    {
        static ORiN3IO oRiN3IO = new ORiN3IO(SystemConfig.Instance.DesignFileDirectory);
        static private bool a;
        [HttpPost("values")]
        public async Task<Dictionary<string, MultiTypeValue>> GetValuesAsync(List<string> devices)
        {
            ORiN3FieldDesign? orin3 = null;
            foreach (var e in DesignerService.GetDesignData().Modules.ToList())
            {
                orin3 = e.Fields.OfType<ORiN3FieldDesign>().FirstOrDefault();
                if (orin3 != null) break;
            }

            if (!a)
            {
                await oRiN3IO.SetDesignAsync(orin3);
                a = true;
                return await oRiN3IO.GetValuesAsync(devices);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
