using IntusWindows.DAL.DataServices;
using IntusWindows.Shared;
using IntusWindows.DAL.DataModels;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace IntusWindows.Server.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IWindowService _windowService;
        private readonly ISubElementService _subElementService;

        public OrderController(IOrderService orderService, IWindowService windowService, ISubElementService subElementService)
        {
            _orderService = orderService;
            _windowService = windowService;
            _subElementService = subElementService;
        }        

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            try
            {
                var orders = _orderService.GetAll().ToList();
                return Ok(orders);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            try
            {
                var order = _orderService.GetById(id);
                if (order == null)
                {
                    return NotFound("Order not found");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpPost]
        [Route("new")]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            try
            {
                _orderService.Create(order);
                return Ok("Successfully create new order");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateOrder(Order order)
        {
            try
            {
                var pOrder = _orderService.GetById(order.Id);
                pOrder.Name = order.Name;
                pOrder.State = order.State;
                _orderService.Update(pOrder);
                return Ok("Successfully update order info");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            try
            {
                var windows = _windowService.GetMany(x => x.OrderId == id).ToList();
                foreach(var w in windows)
                {
                    _subElementService.Delete(x => x.WindowId == w.Id);
                    _windowService.Delete(w.Id);
                }                
                _orderService.Delete(id);
                return Ok("Order deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetErrorMessage());
            }
        }
    }
}
