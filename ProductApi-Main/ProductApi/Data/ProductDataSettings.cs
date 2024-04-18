

namespace ProductApi.Data
{
    public class ProductDataSettings : IProductDataSettings
    {
        public string Database { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string Collection { get; set; } = string.Empty;
    }
}
