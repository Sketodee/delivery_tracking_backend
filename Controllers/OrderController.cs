using AutoMapper;
using Azure;
using DeliveryTracking.Data;
using DeliveryTracking.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public OrderController(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        [HttpGet("getallorders")]
        public async Task<ActionResult<ServiceResponse<List<Order>>>> getallorders()
        {
            ServiceResponse<List<Order>> response = new();
            try
            {
                var order = await _context.Orders.Include(x=> x.Items).ToListAsync();
                if (order == null)
                {
                    response.Message = "No orders yet";
                    response.Success = true;
                }
                else if (order != null)
                {
                    response.Data = order;
                    response.Message = "Order successfully fetched";
                    response.Success = true;
                }
                else
                {
                    response.Message = "Couldn't get order";
                    response.Success = true;
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = true;
            }

            return Ok(response);
        }

        [HttpPost("createneworder")]
        public async Task<ActionResult<ServiceResponse<List<AddOrderDto>>>> createneworder(AddOrderDto newOrder)
        {
            ServiceResponse<List<AddOrderDto>> response = new();

            try
            {
                Order order = _mapper.Map<Order>(newOrder);
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                response.Message = "Order added successfully";
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message; 
                response.Success = false;
            }

            return Ok(response);
        }


        [HttpPost("updateorderstatus")]
        public async Task<ActionResult<ServiceResponse<AddOrderDto>>> updateorderstatus(string query, ConfirmOrder confirmOrder)
        {
            ServiceResponse<AddOrderDto> response = new();
            try
            {
                var order = await _context.Orders.Where(x => x.OrderId == query || x.TrackingId == query).FirstOrDefaultAsync();
                
                if(order == null)
                {
                    response.Message = "Order doesn't exist";
                    response.Success = true;
                }
                else if (order != null)
                {
                    order.Status = confirmOrder.status;
                    await _context.SaveChangesAsync();

                    response.Message = "Order status successfully updated"; 
                    response.Success = true;
                } else
                {
                    response.Message = "Internal issues";
                    response.Success = true;
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
            }
            return Ok(response);
        }


        [HttpGet("getorderbytrackingid")]
        public async Task<ActionResult<ServiceResponse<Order>>> getorderbytrackingid(string query)
        {
            ServiceResponse<Order> response= new();
            try
            {
                 var order = await _context.Orders.Where(x => x.TrackingId == query).Include(x=> x.Items).FirstOrDefaultAsync();
                if (order == null)
                {
                    response.Message = "No order with such tracking id";
                    response.Success = true;
                } else if (order != null)
                {
                    response.Data= order;   
                    response.Message = "Order successfully fetched";
                    response.Success = true;
                } else
                {
                    response.Message = "Couldn't get order";
                    response.Success = true;
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = true;
            }

            return Ok(response);
        }
    }
}
