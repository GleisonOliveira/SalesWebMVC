using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models.Enums
{
    public enum SaleStatus: int
    {
        [Display(Description = "Pending",Name = "Pending")]
        Pending = 0,

        [Display(Description = "Billed", Name = "Billed")]
        Billed = 1,

        [Display(Description = "Canceled", Name = "Canceled")]
        Canceled = 2,
    }
}
