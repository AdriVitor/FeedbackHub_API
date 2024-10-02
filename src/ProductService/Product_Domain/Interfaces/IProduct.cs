namespace Product_Domain.Interfaces
{
    public interface IProduct : IBase
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int IdEnterprise { get; protected set; }
        public bool IsActive { get; protected set; }
    }
}
