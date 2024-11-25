using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectDemo.Entites;
using ProjectDemo.IRepository;
using ProjectDemo.Model;
using ProjectDemo.ViewModel;

namespace ProjectDemo.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private MyDBContext _context;
        private static MyDBContext s_Context = new MyDBContext();
        private readonly IMapper _mapper;


        public CategoryRepository(MyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        
        public static async Task<bool> checkCategoryExistById(int id)
        {
            var item = await s_Context.Categories.Where(c => c.CategoryId  ==id).FirstOrDefaultAsync();
            if (item != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Add new category item
        /// </summary>
        /// <param name="model"></param>
        /// <returns> if add success true.</returns>
        public async Task<bool> Add(CategoryModel model)
        {
            var checkName = await GetByName(model.CategoryName);

            if (checkName != null)
            {
                throw new Exception("The category name already exists.");
            }
            var item = _mapper.Map<Categories>(model);
            await _context.Categories.AddAsync(item);
            return true;
        }

        /// <summary>
        /// Delete category item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>if delete success true.</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(int id)
        {
            var item = await _context.Categories.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Category don't exist");
            }
            _context.Remove(item);
            return true;
        }

        /// <summary>
        /// Get all item category in DB
        /// </summary>
        /// <returns>List category view model</returns>
        public async Task<List<CategoryVM>> GetAll()
        {
            var items = await _context.Categories.ToListAsync();
            var CategorysVM = _mapper.Map<List<CategoryVM>>(items);
            return CategorysVM;
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>if id exist, return category view model item</returns>
        public async Task<CategoryVM> GetById(int id)
        {
            var item = await _context.Categories.FindAsync(id);
            var CategoryVM = _mapper.Map<CategoryVM>(item);
            return CategoryVM;
        }

        /// <summary>
        /// Update category item by id
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(CategoryModel model, int id)
        {

            var item = await _context.Categories.Where(c => c.CategoryId.Equals(id)).FirstOrDefaultAsync();
            if (item == null)
            {
                throw new Exception("Category don't exist");
            }
            var checkName = await GetByName(model.CategoryName);

            if (checkName != null && id != checkName.CategoryId)
            {
                throw new Exception("The category name already exists.");
            }
            _mapper.Map(model, item);
            _context.Update(item);
            return true;
        }


        /// <summary>
        /// Get category by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>if name exist, return category view model</returns>
        public async Task<CategoryVM> GetByName(string name)
        {
            var item = await _context.Categories.Where(c => c.CategoryName.Equals(name)).FirstOrDefaultAsync();
            var itemVM = _mapper.Map<CategoryVM>(item);
            return itemVM;
        }
    }
}
