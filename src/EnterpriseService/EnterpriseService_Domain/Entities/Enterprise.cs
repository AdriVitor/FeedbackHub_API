using EnterpriseService_Domain.Validations;

namespace EnterpriseService_Domain.Entities
{
    public class Enterprise
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string CNPJ { get; private set; }
        public string Description { get; private set; }

        public Enterprise()
        {
            
        }

        public Enterprise(int id, string name, string cnpj, string description)
        {
            Validations(name, cnpj);
            Id = id;
            Name = name;
            CNPJ = cnpj;
            Description = description;
        }

        private void Validations(string name, string cnpj)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Fill the enterprise name");
            DomainExceptionValidation.When(string.IsNullOrEmpty(cnpj), "Fill the enterprise CNPJ");
        }
    }
}
