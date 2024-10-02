using Product_Domain.Interfaces;
using Product_Domain.Validations;

namespace Product_Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int IdEnterprise { get; private set; }
        public bool IsActive { get; private set; }

        public Product(int id, string name, string description, int idEnterprise, bool isActive)
        {
            ValidationProperties(name, idEnterprise);

            Id = id;
            Name = name;
            Description = description;
            IdEnterprise = idEnterprise;
            IsActive = isActive;
        }

        public Product(string name, string description, int idEnterprise)
        {
            ValidationProperties(name, idEnterprise);

            Name = name;
            Description = description;
            IdEnterprise = idEnterprise;
            IsActive = true;
        }

        private void ValidationProperties(string name, int idEnterprise)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Fill in the product name, please");
            DomainExceptionValidation.When(idEnterprise <= 0, "Fill in the product enterprise, please");
        }
    }
}
