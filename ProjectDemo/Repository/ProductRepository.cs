using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectDemo.Entites;
using ProjectDemo.Helper;
using ProjectDemo.IRepository;
using ProjectDemo.Model;
using ProjectDemo.ViewModel;

namespace ProjectDemo.Repository
{
    public class ProductRepository : IProductRepository
    {

        private MyDBContext _context;
        private static MyDBContext s_Context = new MyDBContext();
        private readonly IMapper _mapper;


        public ProductRepository(MyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<ProductVM> GetByName(String name)
        {
            var model = await _context.Products.Where(p => p.NameProduct.Equals(name))
                                               
                                                .FirstOrDefaultAsync();

            if (model != null)
            {
                return _mapper.Map<ProductVM>(GetByName);
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> Add(ProductModel model)
        {
            var checkName = await GetByName(model.NameProduct);
            //if name exist => throw exception
            if (checkName != null)
            {
                throw new Exception("The product name already exists.");
            }

            var checkCatagory = await CategoryRepository.checkCategoryExistById(model.CategoryId);
            if (checkCatagory == false)
            {
                throw new Exception("The category id don't already exists.");
            }
            if (model.Price <= 0)
            {
                throw new Exception("The price invalid.");

            }
            Products p = _mapper.Map<Products>(model);
            await _context.Products.AddAsync(p);
            return true;
        }

        public async Task<int> CountProducts()
        {
            var p = await _context.Products.CountAsync();
            return p;
        }

        public async Task<bool> Delete(int id)
        {
            // get item by id
            var p = await _context.Products.Where(p => p.ProductId.Equals(id)).FirstOrDefaultAsync();
            // if item don't exist
            if (p == null)
            {
                throw new Exception("Product don't exist!");
            }
            else
            {
                _context.Products.Remove(p);
                return true;
            }
            
        }

        public async Task<List<ProductVM>> GetAll(string sortBy)
        {
            // get list product

            var products = await _context.Products
                    .Include(p => p.Categories)
                    .ToListAsync();
            // convert to queryable
            var queryableProduct = products.AsQueryable();
            // sort
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc": products = products.OrderByDescending(p => p.NameProduct).ToList(); break;
                    case "price_asc": products = products.OrderBy(p => p.Price).ToList(); break;
                    case "price_desc": products = products.OrderByDescending(p => p.Price).ToList(); break;
                }
            }

            return _mapper.Map<List<ProductVM>>(products);
        }

        public async Task<ProductVM> GetById(int id)
        {
            //get item by id
            var item = await _context.Products.Include(p => p.Categories) // Include the category navigation property
                                                .Where(p => p.ProductId == id)
                                                .FirstOrDefaultAsync();

            

            // mapper and return
            return _mapper.Map<ProductVM>(item);
        }

        public async Task<List<ProductVM>> SearchByName(string name)
        {
            List<Products> listProduct;
            if (name == null)
            {
                listProduct = await _context.Products.ToListAsync();
            }
            else
            {
                // get list
                listProduct = await _context.Products.Where(i => i.NameProduct.Contains(name)
                                                       || name.Contains(i.NameProduct)).ToListAsync();
            }

            // map to list productVM
            var listProductVM = _mapper.Map<List<ProductVM>>(listProduct);
            return listProductVM;
        }

        public async Task<List<ProductVM>> GetByPage(int pageIndex, int pageSize, string sortBy)
        {
            // get list product
            var products = await _context.Products
                    .Include(p => p.Categories)
                    .ToListAsync();
            
            //sort
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc": products = products.OrderByDescending(p => p.NameProduct).ToList(); break;
                    case "price_asc": products = products.OrderBy(p => p.Price).ToList(); break;
                    case "price_desc": products = products.OrderByDescending(p => p.Price).ToList(); break;
                }
            }
            // map to list product view model and convert to queryable 
            var queryableProduct =_mapper.Map<List<ProductVM>>(products).AsQueryable();

            var items = PaginatedList<ProductVM>.create(queryableProduct, pageIndex, pageSize);

            return items;
        }

      

        public async Task<bool> Update(ProductModel model, int id)
        {

            var checkName = await GetByName(model.NameProduct);
            //if name exist => throw exception
            if (checkName != null && id != checkName.ProductId)
            {
                throw new Exception("The product name already exists.");
            }

            var checkCatagory = await CategoryRepository.checkCategoryExistById(model.CategoryId);
            if (checkCatagory == false)
            {
                throw new Exception("The category id don't already exists.");
            }
            if (model.Price <= 0)
            {
                throw new Exception("The price invalid.");

            }
            // get item by id
            var p = await _context.Products.Where(p => p.ProductId.Equals(id)).FirstOrDefaultAsync();
            
            // if product don't exist
            if (p == null)
            {
                throw new Exception("Product don't exist!");
            }

            // map from model product to product
            _mapper.Map(model, p);

            _context.Products.Update(p);


            return true;
        }


        
    }
}
