using System.ComponentModel.DataAnnotations;

namespace Adapter.MercadoBitcoinAdapter
{
    public class MercadoBitcoinAdapterConfiguration
    {
        [Required]
        public string MercadoBitcoinApiUrlBase { get; set; }
    }
}
