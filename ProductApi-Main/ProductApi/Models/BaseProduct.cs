using Microsoft.VisualBasic;

namespace ProductApi.Models
{
    public abstract class BaseProduct
    {
        public string Id { get; set; }
        public DateAndTime CreatedAt { get; set; }
    }
}
