using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Bindings.ORiN3.Fields;
using Codeer.LowCode.Bindings.ORiN3.Server;
using Codeer.LowCode.Blazor.DesignLogic;
using Microsoft.AspNetCore.Mvc;
using ORiN3App.Server.Services;

namespace ORiN3App.Server.Controllers
{
    [ApiController]
    [Route("api/orin3")]
    public class ORiN3Controller : ControllerBase
    {
        private static readonly ORiN3IO orin3IO;
        static ORiN3Controller()
        {
            orin3IO = new(SystemConfig.Instance.DesignFileDirectory);
            orin3IO.CallBack = async _ => await Task.CompletedTask;//ORiN3FieldDesign, Values
        }

        [HttpPost("values")]
        public async Task<Dictionary<string, ORiN3IOResult>> GetValuesAsync(List<string> devices)
        {
            Dictionary<string, ORiN3FieldDesign> orin3Dic = [];
            foreach (var mod in DesignerService.GetDesignData().Modules.ToList())
            {
                foreach (var orin3 in mod.Fields.OfType<ORiN3FieldDesign>())
                {
                    orin3Dic[$"{mod.Name}.{orin3.Name}"] = orin3;
                }
            }
            return await orin3IO.GetValuesAsync(orin3Dic, devices);
        }
    }
}
