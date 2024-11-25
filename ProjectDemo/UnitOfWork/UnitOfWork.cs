using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProjectDemo.Entites;
using ProjectDemo.IRepository;
using ProjectDemo.Repository;
using System.Xml.Linq;

namespace ProjectDemo.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public MyDBContext Context = null;
        private IDbContextTransaction? _objTran = null;
        private IConfiguration _configuration;

        public ProductRepository Products { get; private set; }
        public CategoryRepository Categories { get; private set; }

        public UnitOfWork(MyDBContext context, IMapper mapper, IConfiguration configuration)
        {
            Context = context;
            _configuration = configuration;
            Products = new ProductRepository(Context, mapper);
            Categories = new CategoryRepository(Context, mapper);
            
        }
        public void CreateTransaction()
        {
            _objTran = Context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _objTran?.Commit();
        }
        public void Rollback()
        {
            _objTran?.Rollback();
            _objTran?.Dispose();
        }
        public async Task Save()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
