using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EfeCetirSampleProject.Models;
using Infrastructure.Entity.Models;
using Infrastructure.Service;

namespace EfeCetirSampleProject.Controllers.api
{
    [RoutePrefix("api/Category")]
    public class CategoryApiController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryApiController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [AcceptVerbs("GET")]
        [Route("GetAllCategories")]
        [HttpGet]
        public List<CategoryViewModel> GetAllCategories()
        {
            var categories = _categoryService.GetAll();
            var categoryWMList = categories.Select(category => new CategoryViewModel()
            {
                ActionName = category.ActionName, ControllerName = category.ControllerName, Name = category.Name
            }).ToList();
           return categoryWMList;
        }
        
        [AcceptVerbs("POST")]
        [Route("SendModel")]
        [HttpPost]
        public List<CategoryViewModel> SendModel([FromBody]CategoryViewModel model)
        {
            var _category = new Category()
            {
                ActionName = model.ActionName,
                Name = model.Name,
                ControllerName = model.ControllerName,
            };
            _categoryService.Update(_category);
            var categories = _categoryService.GetAll();
            var categoryWMList = categories.Select(category => new CategoryViewModel()
            {
                ActionName = category.ActionName,
                ControllerName = category.ControllerName,
                Name = category.Name
            }).ToList();
            return categoryWMList;
        }
    }
}
