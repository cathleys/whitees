using Whitees.Data.Enums;

namespace Whitees.Helpers;
public class UserParams : PaginationParams
{
    public ShirtSale? ShirtSale { get; set; }
    public string searchString { get; set; }
}
