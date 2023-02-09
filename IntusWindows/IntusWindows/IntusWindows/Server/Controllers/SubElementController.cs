using IntusWindows.DAL.DataServices;
using IntusWindows.DAL.DataModels;
using IntusWindows.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace IntusWindows.Server.Controllers
{
    [Route("api/subelements")]
    [ApiController]
    public class SubElementController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IWindowService _windowService;
        private readonly ISubElementService _subElementService;
        public SubElementController(IWindowService windowService,ISubElementService subElementService, IOrderService orderService)
        {
            _orderService = orderService;
            _windowService = windowService;
            _subElementService = subElementService;
        }
        [HttpGet]
        public async Task<ActionResult<List<SubElement>>> GetSubElements()
        {
            try
            {
                var subElements = _subElementService.GetAll("Window").ToList();
                foreach(var e in subElements)
                {
                    e.Window.Order = _orderService.GetById(e.Window.OrderId);
                }

                return Ok(subElements);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SubElement>> GetSubElement(int id)
        {
            try
            {
                var subElement = _subElementService.Get(x => x.Id == id, "Window");
                if (subElement == null)
                {
                    return NotFound("Sub elements not found");
                }
                subElement.Window.Order = _orderService.GetById(subElement.Window.OrderId);
                return Ok(subElement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpGet]
        [Route("filterbyorder/{orderId}")]
        public async Task<ActionResult<List<SubElement>>> GetSubElements(int orderId)
        {
            try
            {
                var subElements = _subElementService.GetAll("Window").Where(x=> orderId==0 || x.Window.OrderId==orderId).ToList();
                foreach (var e in subElements)
                {
                    e.Window.Order = _orderService.GetById(e.Window.OrderId);
                }

                return Ok(subElements);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpGet]
        [Route("filterbywindow/{windowId}")]
        public async Task<ActionResult<List<SubElement>>> GetSubElementsByWindowId(int windowId)
        {
            try
            {
                var subElements = _subElementService.GetMany(x => windowId==0 || x.WindowId == windowId,"Window").ToList();
                foreach (var e in subElements)
                {
                    e.Window.Order = _orderService.GetById(e.Window.OrderId);
                }

                return Ok(subElements);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpPost]
        [Route("new")]
        public async Task<ActionResult> CreateSubElements(SubElement subElement)
        {
            try
            {                
                _subElementService.Create(subElement);
                SetTotalSubelementToWindow(subElement.WindowId, 0);
                return Ok("Successfully create new subElement");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateSubElement(SubElement subElement)
        {
            try
            {
                int prevWindowId = 0, newWindowId = subElement.WindowId;
                var pSubElement = _subElementService.GetById(subElement.Id);
                prevWindowId = pSubElement.WindowId;
                pSubElement.Width = subElement.Width;
                pSubElement.Element = subElement.Element;
                pSubElement.Height = subElement.Height;
                pSubElement.Type = subElement.Type;
                pSubElement.WindowId=subElement.WindowId;                
                _subElementService.Update(pSubElement);
                SetTotalSubelementToWindow(newWindowId, prevWindowId);
                return Ok("Successfully update subElement info");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteSubElement(int id)
        {
            try
            {   var windowId=_subElementService.GetById(id).WindowId;
                _subElementService.Delete(id);
                SetTotalSubelementToWindow(0, windowId);
                return Ok("Sub Element deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        private async void SetTotalSubelementToWindow(int newWindowId,int previousWindowId)
        {
            if (newWindowId != previousWindowId)
            {
                if (newWindowId > 0)
                {
                    var window = _windowService.GetById(newWindowId);
                    window.TotalSubElements += 1;// _subElementService.GetMany(x => x.WindowId == newWindowId).ToList().Count;
                    _windowService.Update(window);
                }

                if (previousWindowId > 0)
                {
                    var prevWindow = _windowService.GetById(previousWindowId);
                    prevWindow.TotalSubElements -= 1;// _subElementService.GetMany(x => x.WindowId == previousWindowId).ToList().Count;
                    _windowService.Update(prevWindow);
                }
            }
        }
    }
}
