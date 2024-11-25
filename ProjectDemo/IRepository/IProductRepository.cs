using ProjectDemo.Model;
using ProjectDemo.ViewModel;

namespace ProjectDemo.IRepository
{
    public interface IProductRepository
    {
        Task<List<ProductVM>> GetAll(string sortBy);
        Task<ProductVM> GetById(int id);
        Task<List<ProductVM>> GetByPage(int pageIndex, int pageSize, string sortBy);
        Task<List<ProductVM>> SearchByName(String name);

        Task<int> CountProducts();
        Task<bool> Add(ProductModel model);
        Task<bool> Update(ProductModel model, int id);
        Task<bool> Delete(int id);
    }
}
