using Microsoft.EntityFrameworkCore;
using MyTiendita.Data.Repositories;
using MyTiendita.Domain;
using MyTiendita.Shared.DTOs;
using MyTiendita.Shared.DTOs.Mappers;

namespace MyTiendita.Services.BLL;

public class ProductBLL
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductBLL(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }



    public async Task<List<Product>> GetAll(string? barcode = null, string? description = null)
    {
        var query = this._unitOfWork.ProductRepository.GetAll();
        if (barcode is not null) query = query.Where(x => x.Barcode == barcode);
        if (description is not null) query = query.Where(x => x.Description == description);


        return await query.ToListAsync();
    }


    public async Task<ProductDTO> CreateProduct(ProductDTO productDTO)
    {
        try
        {
            if (productDTO is null)
                throw new InvalidOperationException("Error DTO viene nullo");

            if (await _unitOfWork.ProductRepository.GetAll().AnyAsync(x => x.Barcode == productDTO.Barcode))
            {
                throw new InvalidOperationException("El producto ya existe en la base de datos");
            }

            //Initialize transaction in order to can save in Product and Stock tables.
            this._unitOfWork.CreateTransaction();


            //Adding Product to Product Table
            var modelProduct = productDTO.ToModel();
            modelProduct.CreatedById = null;
            modelProduct.Created = DateTime.Now;
            var entityProduct = await this._unitOfWork.ProductRepository.AddAsync(modelProduct);
            await this._unitOfWork.SaveAsync();


            //Creating stock model using 1 unit as default
            var modelStock = new Stock()
            {
                IdProduct = entityProduct.id,
                Quantity = 1,
            };

            //Adding Stock product by product Id in Stock Table
            modelStock.CreatedById = null;
            modelStock.Created = DateTime.Now;
            var entityStock = await this._unitOfWork.StockRepository.AddAsync(modelStock);
            await this._unitOfWork.SaveAsync();


            //Commit all changes to save them.
            this._unitOfWork.Commit();

            //Return DTO to Front-End
            return entityProduct.ToDTO();
        }
        catch (InvalidOperationException ex)
        {
            throw new Exception(ex.Message);
        }
        catch (Exception ex)
        {
            this._unitOfWork.Rollback();
            throw new Exception(ex.Message);
        }
    }



    public async Task<ProductDTO> UpdateProduct(int Id, ProductDTO productDTO)
    {
        if (productDTO is null)
            throw new InvalidOperationException("Error DTO viene nullo");

        var entity = await this._unitOfWork.ProductRepository.GetByIdAsync(Id);

        if (entity is null)
            throw new InvalidOperationException($"El Producto by ID:{Id} no existe en la base de datos");


        entity.Description = productDTO.Description;
        entity.Barcode = productDTO.Barcode;
        entity.Capture = productDTO.Capture;
        entity.UpdatedBy = null;
        entity.Updated = DateTime.Now;


        await this._unitOfWork.SaveAsync();
        return entity.ToDTO();
    }
}