using SalesWebMvc.Models.Interfaces;
using SalesWebMvc.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller: Imodel
    {
        public int Id { get; set; }

        private string _email;

        [Display(Name = nameof(Name), Prompt = nameof(Name))]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "{0} must have minimum {2} and {1} characters")]
        public string Name { get; set; }

        [Display(Name = nameof(BaseSalary), Prompt = nameof(BaseSalary))]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [MinValue(50)]
        public double? BaseSalary { get; set; }

        [Display(Name=nameof(BirthDate), Prompt = nameof(BirthDate))]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = nameof(Department))]
        public Department Department { get; set; }

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        [Display(Name = nameof(Department))]
        public int DepartmentId { get; set; }

        [Display(Name = nameof(Email), Prompt = nameof(Email))]
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "{0} must be a valid e-mail")]
        public string Email { get => _email; set => _email = value.ToLower(); }

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
            BirthDate = birthDate;
            Department = department;
        }

        public void AddSales(SalesRecord salesRecord)
        {
            Sales.Add(salesRecord);
        }

        public void RemoveSales(SalesRecord salesRecord)
        {
            Sales.Remove(salesRecord);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sale => sale.Date >= initial && sale.Date <= final).Sum(sale => sale.Amount);
        }
    }
}
