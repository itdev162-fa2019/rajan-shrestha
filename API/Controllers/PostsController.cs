using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly DataContext context;
        public PostsController(DataContext context)
        {
            this.context = context;
        }

        //GET api/posts
        [HttpGet]
        public ActionResult<List<Post>> Get()
        {
            return this.context.Posts.ToList();
        }

        /// DELETE api/post/[id]
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>True, if successful</returns>
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(Guid id)
        {
            var post = context.Posts.Find(id);

            if (post == null)
            {
                throw new Exception("Could not find post");
            }

            context.Remove(post);

            var success = context.SaveChanges() > 0;

            if (success)
            {
                return true;
            }

            throw new System.Exception("Error deleting post");
        }
    }
}