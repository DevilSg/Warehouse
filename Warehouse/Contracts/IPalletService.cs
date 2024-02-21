using WpfApp1.Warehouse.Data.Models;

namespace WpfApp1.Warehouse.Data.Contracts;

public interface IPalletService : ICrudService<Pallet>
{
    IEnumerable<Pallet> GetAllByGtin(string gtin);
}