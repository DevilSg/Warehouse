using WpfApp1.Warehouse.Data.Models;

namespace WpfApp1.Warehouse.Data.Contracts;

public interface IBoxService : ICrudService<Box>
{
    IEnumerable<Box> GetAllByGtin(string gtin);
}