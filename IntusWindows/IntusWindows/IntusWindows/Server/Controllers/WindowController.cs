using IntusWindows.DAL.DataServices;
using IntusWindows.DAL.DataModels;
using IntusWindows.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace IntusWindows.Server.Controllers
{
    [Route("api/windows")]
    [ApiController]
    public class WindowController : ControllerBase
    {
        private readonly IWindowService _windowService;
        private readonly ISubElementService _subElementService;

        public WindowController(IWindowService windowService, ISubElementService subElementService)
        {
            _windowService = windowService;
            _subElementService = subElementService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Window>>> GetWindows()
        {
            try
            {
                var windows = _windowService.GetAll("Order").ToList();
                return Ok(windows);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpGet]
        [Route("filterbyorder/{orderId}")]
        public async Task<ActionResult<List<Window>>> GetWindowsByOrderId(int orderId)
        {
            try
            {
                var windows = _windowService.GetMany(x => x.OrderId == orderId, "Order").ToList();
                return Ok(windows);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Window>> GetWindow(int id)
        {
            try
            {
                var window = _windowService.Get(x => x.Id == id, "Order");
                if (window == null)
                {
                    return NotFound("Window not found.");
                }
                return Ok(window);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpPost]
        [Route("new")]
        public async Task<ActionResult> CreateWindows(Window window)
        {
            try
            {
                window.Order = null;
                window.TotalSubElements = 0;
                _windowService.Create(window);
                return Ok("Successfully create new window");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateWindow(Window window)
        {
            try
            {
                var pWindow = _windowService.Get(x => x.Id == window.Id, "Order");
                pWindow.QualityOfWindows = window.QualityOfWindows;
                pWindow.Name = window.Name;
                pWindow.OrderId = window.OrderId;
                _windowService.Update(pWindow);
                return Ok("Successfully update window info");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteWindow(int id)
        {
            try
            {
                var subElements = _subElementService.GetMany(x => x.WindowId == id).ToList();
                _subElementService.Delete(x => x.WindowId == id);
                _windowService.Delete(id);
                return Ok("Window deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
    }
}
