using Microsoft.AspNetCore.Mvc;
using Custom_Architecture.Entities;
using Custom_Architecture.Interfaces.Presenters;
using Custom_Architecture.Interfaces.Services;
using Custom_Architecture.Requests;
using Custom_Architecture.Responses;
using System.Net;

namespace Custom_Architecture.Controllers;

[ApiController]
[Route("orders")]
public class OrderController : ControllerBase
{
    private readonly IPresenter _presenter;
    private readonly IOrderService _orderService;

    public OrderController(
        IPresenter presenter,
        IOrderService orderService)
    {
        _presenter = presenter;
        _orderService = orderService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(OrderGetByIdResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetOrderAllAsync()
    {
        var data = await _orderService.GetAllAsync();

        return _presenter.GetResult(data, data => data.Select(order => (OrderGetByIdResponse)order).ToArray());
    }


    [HttpGet("{orderId}")]
    [ActionName(nameof(GetOrderByIdAsync))]
    [ProducesResponseType(typeof(OrderGetByIdResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetOrderByIdAsync(int orderId)
    {
        var data = await _orderService.GetByIdAsync(orderId);

        return _presenter.GetResult(data, data => (OrderGetByIdResponse)data);
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderCreateResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateOrderAsync(OrderCreateRequest request)
    {
        var order = (Order)request;

        var data = await _orderService.CreateAsync(order);

        return _presenter.CreateResult(
            data,
            data => (OrderCreateResponse)data, (data) =>
            (nameof(GetOrderByIdAsync), "Order", new { data.OrderId }));
    }
}
