using ProjectDemo.Repository;

namespace ProjectDemo.UnitOfWork
{
    public interface IUnitOfWork
    {
        public ProductRepository Products { get; }
        public CategoryRepository Categories { get; }

        public void CreateTransaction();
        public void Commit();
        public void Rollback();
        Task Save();
    }
}
