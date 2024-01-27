using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTiendita.Domain;
using MyTiendita.Persistence.Database;
using MyTiendita.Shared.DTOs;

namespace MyTiendita.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class StocksController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public StocksController(ApplicationDbContext dbContext) 
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(IEnumerable<Stock>))]
        public async Task<ActionResult> GetStocks(int? idProduct)
        {
            var items = this._dbContext.Stocks.AsQueryable()
                .Include(x => x.Product)
                .Select(x => new
                {
                    idProduct = idProduct,
                    
                }).AsAsyncEnumerable();
          
            //if (idProduct is not null) { items = items.Where(x => x.IdProduct == idProduct); }

            //var result = await items.ToListAsync();
            return Ok();
        }



        [HttpPut]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(Stock))]
        public async Task<ActionResult> PutStock(int? idProduct, string? barcode, int Quantity = 1)
        {
            try
            {
                int? Id;

                if (idProduct is not null)
                {
                    var entity = await this._dbContext.Products.FindAsync(idProduct);

                }

                if (barcode is not null)
                {
                    var entity = this._dbContext.Products.Where(i => i.Barcode == barcode).FirstOrDefault();
                    Id = entity.id;

                    var entityStock = this._dbContext.Stocks.Where(i => i.IdProduct == Id).FirstOrDefault();
                    entityStock.Quantity += Quantity;
                    entityStock.Updated = DateTime.Now;

                    this._dbContext.Add((Stock)entityStock);
                    await this._dbContext.SaveChangesAsync();
                }


                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
         
        }
    }
}
