using ProjectDemo.Model;
using ProjectDemo.ViewModel;

namespace ProjectDemo.IRepository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryVM>> GetAll();
        Task<CategoryVM> GetById(int id);
        Task<bool> Add(CategoryModel model);
        Task<bool> Update(CategoryModel model, int id);
        Task<bool> Delete(int id);
    }
}
