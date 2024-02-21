using Microsoft.EntityFrameworkCore;
using WpfApp1.Warehouse.Data.Contracts;
using WpfApp1.Warehouse.Data.Models;

namespace WpfApp1.Warehouse.Data.Services;

public class PalletService : BaseCrudService<Pallet>, IPalletService
{
    private readonly IProductService _productService;
    public PalletService(WarehouseDbContext warehouseDbContext, IProductService productService) : base(warehouseDbContext)
    {
        _productService = productService;
        DbSetIncludeAll = DbSet.Include(x => x.Boxes).AsNoTracking();
    }
    public IEnumerable<Pallet> GetAllByGtin(string gtin)
        => _productService.GetAllByGtin(gtin).Select(x=>x.Box.Pallet).DistinctBy(x=>x.Id);
}

