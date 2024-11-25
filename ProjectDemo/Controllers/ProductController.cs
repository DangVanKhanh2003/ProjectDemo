using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDemo.Model;
using ProjectDemo.UnitOfWork;

namespace ProjectDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IUnitOfWork _uow;


        public ProductController(IUnitOfWork uow)
        {
            _uow = uow;
            
        }

        /// <summary>
        /// List all products.
        /// </summary>
        /// 
        /// <remarks>
        ///  In default, value of sortBy is null.If you want to sort, you can enter value for "sortBy" parameter.
        /// </remarks>
        /// 
        /// <param name="sortBy">sortBy: price_desc/price_asc/name_desc/null(default)</param>
        [HttpGet]
        public async Task<IActionResult> GetAll(string sortBy = null)
        {
            try
            {
                var data = await _uow.Products.GetAll(sortBy);
               
                return Ok(data); // Return the data in the response
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Get products by id.
        /// </summary>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await _uow.Products.GetById(id);
                
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Count products.
        /// </summary>
        /// 
        [HttpGet("Count")]
        public async Task<IActionResult> CountProducts()
        {
            try
            {
                var count = await _uow.Products.CountProducts();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// List all products by page.
        /// </summary>
        /// <remarks>
        /// Enter pageIndex, pageSize, sortBy.In default, value of sortBy is null.If you want to sort, you can enter value for "sortBy" parameter.
        /// </remarks>
        /// <param name="sortBy">sortBy: price_desc/price_asc/name_desc/null(default)</param>

        [HttpGet("Page")]
        public async Task<IActionResult> GetByPage(int pageIndex = 1, int pageSize = 10, string sortBy = null)
        {
            try
            {
                var data = await _uow.Products.GetByPage(pageIndex, pageSize, sortBy);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        


        /// <summary>
        /// Search product by nameProduct
        /// </summary>
        /// <param name="nameProduct"></param>
        /// <returns></returns>
        [HttpGet("Search")]
        public async Task<IActionResult> SearchProductByName([FromQuery] string nameProduct = null)
        {
            try
            {
                var products = await _uow.Products.SearchByName(nameProduct);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// <summary>
        /// Add new product. 
        /// </summary>
        /// <remarks>mainImg used for show image product in the card product of list product(mainImg is link img)</remarks>
        [HttpPost]
        public async Task<IActionResult> Add(ProductModel model)
        {
            try
            {
                _uow.CreateTransaction();
                
                var result = await _uow.Products.Add(model);

                await _uow.Save();
                _uow.Commit();
                return Ok(result);
            }
            catch(Exception ex)
            {
                _uow.Rollback();
                return BadRequest(ex.Message);

            }
        }
        /// <summary>
        /// Update product
        /// </summary>
        /// 
        /// <remarks>
        /// server find prouduct by id(input) and update the product. mainImg used for show image product in the card product of list product(mainImg is link img)
        /// </remarks>

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ProductModel model, int id)
        {
            try
            {
                _uow.CreateTransaction();

                
                var result = await _uow.Products.Update(model, id);

                
                await _uow.Save();
                _uow.Commit();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _uow.Rollback();
                return BadRequest(ex.Message);

            }
        }

        /// <summary>
        /// Delete product.
        /// </summary>
        /// 
        /// <remarks>
        /// Server find product and delete.
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _uow.CreateTransaction();

                var result = await _uow.Products.Delete(id);
                
                await _uow.Save();
                _uow.Commit();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _uow.Rollback();
                return BadRequest(ex.Message);

            }
        }

    }
}
