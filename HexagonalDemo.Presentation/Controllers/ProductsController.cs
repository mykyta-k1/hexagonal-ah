using MediatR;
using Microsoft.AspNetCore.Mvc;
using HexagonalDemo.Domain.Entities;
using HexagonalDemo.Application.UseCases.Products.Commands.CreateProduct;
using HexagonalDemo.Application.UseCases.Products.Commands.DeleteProduct;
using HexagonalDemo.Application.UseCases.Products.Queries.GetAllProducts;
using HexagonalDemo.Application.UseCases.Products.Queries.SearchProductsByName;

namespace HexagonalDemo.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Отримати всі продукти.
    /// GET: api/products
    /// </summary>
    [HttpGet]
    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _mediator.Send(new GetAllProductsQuery());
    }

    /// <summary>
    /// Знайти продукти за назвою.
    /// GET: api/products/search?name=phone
    /// </summary>
    [HttpGet("search")]
    public async Task<IEnumerable<Product>> Search([FromQuery] string name)
    {
        return await _mediator.Send(new SearchProductsByNameQuery(name));
    }

    /// <summary>
    /// Додати новий продукт.
    /// POST: api/products
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    /// <summary>
    /// Видалити продукт.
    /// DELETE: api/products/{id}
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}
