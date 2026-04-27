using ai_agent_demo.Agent;
using Microsoft.AspNetCore.Mvc;

namespace BoFrontend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignController : ControllerBase
    {
        private readonly InventoryOrchestrator _orchestrator;

        public CampaignController(InventoryOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        /// Demo olduğu için GET kullandım, gerçek bir uygulamada bu işlemi başlatmak için POST veya başka bir method daha uygun olabilir.
        [HttpGet("trigger",Name = "TriggerCampaign")] 
        public async Task<IActionResult> StartProcess()
        {
            var result = await _orchestrator.RunCampaignProcessAsync();
            return Ok(new { message = "Ajan işlemi tamamladı", detail = result });
        }
    }
}

