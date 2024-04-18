

namespace ProductApi.Data
{
    public interface IProductDataSettings
    {
        string Database { get; set; }
        string ConnectionString { get; set; }
        string Collection { get; set; }

    }
}
