using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers.CategoryController
{
    [Authorize]
    public class CategoryController : ApiController
    {
        public IHttpActionResult Get()
        {
            CategoryService categoryService = CreateCategoryService();
            var categorys = categoryService.GetCategories();
            return Ok(categorys);
        }
        public IHttpActionResult Post(CategoryCreate Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = CreateCategoryService();
            if (!service.CreateCategory(Category))
            {
                return InternalServerError();
            }
            return Ok();
        }
        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var CategoryService = new CategoryService(userId);
            return CategoryService;
        }
        public IHttpActionResult Get(int id)
        {
            CategoryService CategoryService = CreateCategoryService();
            var Category = CategoryService.GetCategorybyId(id);
            return Ok(Category);
        }
        public IHttpActionResult Put(CategoryEdit Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = CreateCategoryService();
            if (!service.UpdateCategories(Category))
            {
                return InternalServerError();
            }
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            var service = CreateCategoryService();
            if (!service.DeleteCategory(id))
            {
                return InternalServerError();
            }
            return Ok();
        }
    }
}