using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTiendita.Domain;
using MyTiendita.Persistence.Database;
using MyTiendita.Services.BLL;
using MyTiendita.Shared.DTOs;
using MyTiendita.Shared.DTOs.Mappers;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;

namespace MyTiendita.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductBLL _ProductBLL;
    public ProductsController(ProductBLL productBLL)
    {
        this._ProductBLL = productBLL ?? throw new ArgumentNullException(nameof(productBLL));
    }

    [HttpGet]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(IEnumerable<ProductDTO>))]
    public async Task<ActionResult> GetAll(string? barcode, string? description)
    {
        var products = await this._ProductBLL.GetAll(barcode, description);
        var productsDto = products.ToDTOs();
        return this.Ok(productsDto);
    }


    [HttpPost]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ProductDTO))]
    public async Task<ActionResult> PostProduct([FromBody] ProductDTO dto)
    {
        try
        {
            var responseDto = await this._ProductBLL.CreateProduct(dto);
            return this.Ok(responseDto);
        }
        catch (InvalidOperationException e)
        {
            return this.BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return this.StatusCode(500, e.Message);
        }
    }


    [Route("{Id}")]
    [HttpPut]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ProductDTO))]
    public async Task<ActionResult> PutProduct(int Id, [FromBody] ProductDTO dto)
    {
        try
        {
            var responseDto = await this._ProductBLL.UpdateProduct(Id, dto);
            return this.Ok(responseDto);
        }
        catch (InvalidOperationException e)
        {
            return this.BadRequest(e.Message);

        }
        catch (Exception e)
        {
            return this.StatusCode(500, e.Message);
        }
    }



}
