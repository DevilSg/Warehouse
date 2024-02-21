using Microsoft.EntityFrameworkCore;
using WpfApp1.Warehouse.Data.Contracts;
using WpfApp1.Warehouse.Data.Models;

namespace WpfApp1.Warehouse.Data.Services;

public class BoxService : BaseCrudService<Box>, IBoxService
{
    private readonly IProductService _productService;
    public BoxService(WarehouseDbContext warehouseDbContext,IProductService productService) : base(warehouseDbContext)
    {
        _productService = productService;
        DbSetIncludeAll = DbSet.Include(x => x.Pallet).AsNoTracking();
    }
    public IEnumerable<Box> GetAllByGtin(string gtin)
        => _productService.GetAllByGtin(gtin).Select(x=>x.Box).DistinctBy(x=>x.Id);
    

}