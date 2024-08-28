
using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReviewsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Review, App.BLL.DTO.Review> _mapper;
        private readonly IMapper _autoMapper = null!;

        public ReviewsController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Review, App.BLL.DTO.Review>(autoMapper);

        }
        
        private Guid UserGuid
        {
            get
            {
                var userId = _userManager.GetUserId(User);
                return userId != null ? Guid.Parse(userId) : Guid.Empty;
            }
        }

        /// <summary>
        /// Returns all reviews visible to current user
        /// </summary>
        /// <returns>list of reviews</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.Review>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.Review>>> GetReviews()
        {
            var reviews = (await _bll.Reviews.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(reviews);
            // var res = (await _bll.Reviews.GetAllAsync(
            //     Guid.Parse(_userManager.GetUserId(User)!)
            // )).Select(e=> _mapper.Map(e));
            // return Ok(res);
        }

        /// <summary>
        /// Returns a single review by id
        /// </summary>
        /// <param name="id">The ID of the review to retrieve</param>
        /// <returns>A review object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.Review>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Review>> GetReview(Guid id)
        {
            var review = _mapper.Map((await _bll.Reviews.FirstOrDefaultAsync(id))!);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
            // var review = await _bll.Reviews.FirstOrDefaultAsync(id);
            //
            // if (review == null)
            // {
            //     return NotFound();
            // }
            // var mappedReview = _mapper.Map(review);
            // return Ok(mappedReview);
        }
        
        /// <summary>
        /// Returns a single review by id without user
        /// </summary>
        /// <param name="id">The ID of the review to retrieve</param>
        /// <returns>A review object</returns>
        [HttpGet("{id}/withoutUser")]
        [ProducesResponseType<App.BLL.DTO.Review>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Review>> GetReviewWithoutUser(Guid id)
        {
            var review = _mapper.Map((await _bll.Reviews.FirstOrDefaultAsync(id))!);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
            // var review = await _bll.Reviews.FirstOrDefaultAsync(id);
            //
            // if (review == null)
            // {
            //     return NotFound();
            // }
            // var mappedReview = _mapper.Map(review);
            // return Ok(mappedReview);
        }

        /// <summary>
        /// Updates a specific review
        /// </summary>
        /// <param name="id">The ID of the review to update</param>
        /// <param name="review">The updated review data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutReview(Guid id, App.DTO.v1_0.Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            review.Id = id;
            _bll.Reviews.Update(_mapper.Map(review));

            return NoContent();
            // if (id != review.Id)
            // {
            //     return BadRequest();
            // }
            // try
            // {
            //     var updated = await Task.Run(() => _bll.Reviews.Update(_mapper.Map(review)));
            //     if (updated == null)
            //     {
            //         return NotFound();
            //     }
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!await _bll.Reviews.ExistsAsync(id))
            //     {
            //         return NotFound();
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }
            // return NoContent();
        }

        /// <summary>
        /// Adds a new review
        /// </summary>
        /// <param name="review">Data transfer object representing the review to add</param>
        /// <returns>The created review</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.Review>((int) HttpStatusCode.Created)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Review>> PostReview(App.DTO.v1_0.Review review)
        {
            review.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var newReview = _bll.Reviews.Add(_mapper.Map(review)!);
            
            return CreatedAtAction("GetReview",
                new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newReview.Id }, newReview);

            // var addedReview = await Task.Run(() => _bll.Reviews.Add(_mapper.Map(review)));
            // var resultReview = _mapper.Map(addedReview);
            //
            // return CreatedAtAction("GetReview", new
            // {
            //     version = HttpContext.GetRequestedApiVersion()?.ToString(),
            //     id = resultReview.Id
            // }, resultReview);
        }

        /// <summary>
        /// Deletes a specific review
        /// </summary>
        /// <param name="id">The ID of the review to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            
            await _bll.Reviews.RemoveAsync(id, UserGuid);
            return NoContent();
            // var reviewExists = await _bll.Reviews.ExistsAsync(id);
            // if (!reviewExists)
            // {
            //     return NotFound();
            // }
            // var rowsAffected = await _bll.Reviews.RemoveAsync(id);
            // if (rowsAffected == 0)
            // {
            //     return NotFound();
            // }
            // return NoContent();
        }
    }
}




// using System.Net;
// using App.Contracts.BLL;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using App.Domain.Identity;
// using Asp.Versioning;
// using AutoMapper;
// using Microsoft.AspNetCore.Identity;
// using WebApp.Helpers;
//
// namespace WebApp.ApiControllers
// {
//     [ApiVersion("1.0")]
//     [ApiController]
//     [Route("api/v{version:apiVersion}/[controller]")]
//     // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//     public class ReviewsController : ControllerBase
//     {
//         private readonly IAppBLL _bll;
//         private readonly UserManager<AppUser> _userManager;
//         private readonly PublicDTOBllMapper<App.DTO.v1_0.Review, App.BLL.DTO.Review> _mapper;
//         private readonly IMapper _autoMapper;
//
//         public ReviewsController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
//         {
//             _bll = bll;
//             _userManager = userManager;
//             _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Review, App.BLL.DTO.Review>(autoMapper);
//
//         }
//         
//         private Guid UserGuid
//         {
//             get
//             {
//                 var userId = _userManager.GetUserId(User);
//                 return userId != null ? Guid.Parse(userId) : Guid.Empty;
//             }
//         }
//
//         /// <summary>
//         /// Returns all reviews visible to current user
//         /// </summary>
//         /// <returns>list of reviews</returns>
//         [HttpGet]
//         [ProducesResponseType<IEnumerable<App.BLL.DTO.Review>>((int) HttpStatusCode.OK)]
//         [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
//         [Produces( "application/json")]
//         [Consumes("application/json")]
//         public async Task<ActionResult<IEnumerable<App.DTO.v1_0.Review>>> GetReviews()
//         {
//             var reviews = (await _bll.Reviews.GetAllAsync(UserGuid))
//                 .Select(e => _mapper.Map(e));
//             return Ok(reviews);
//             // var res = (await _bll.Reviews.GetAllAsync(
//             //     Guid.Parse(_userManager.GetUserId(User)!)
//             // )).Select(e=> _mapper.Map(e));
//             // return Ok(res);
//         }
//
//         /// <summary>
//         /// Returns a single review by id
//         /// </summary>
//         /// <param name="id">The ID of the review to retrieve</param>
//         /// <returns>A review object</returns>
//         [HttpGet("{id}")]
//         [ProducesResponseType<App.BLL.DTO.Review>((int) HttpStatusCode.OK)]
//         [ProducesResponseType((int) HttpStatusCode.NotFound)] 
//         [Produces( "application/json")]
//         [Consumes("application/json")]
//         public async Task<ActionResult<App.DTO.v1_0.Review>> GetReviewWithUser(Guid id)
//         {
//             var review = _mapper.Map((await _bll.Reviews.FirstOrDefaultWithUserAsync(id, UserGuid))!);
//             if (review == null)
//             {
//                 return NotFound();
//             }
//
//             return Ok(review);
//             // var review = await _bll.Reviews.FirstOrDefaultAsync(id);
//             //
//             // if (review == null)
//             // {
//             //     return NotFound();
//             // }
//             // var mappedReview = _mapper.Map(review);
//             // return Ok(mappedReview);
//         }
//         
//         /// <summary>
//         /// Returns a single review by id
//         /// </summary>
//         /// <param name="id">The ID of the review to retrieve</param>
//         /// <returns>A review object</returns>
//         [HttpGet("{id}/withoutUser")]
//         [ProducesResponseType<App.BLL.DTO.Review>((int) HttpStatusCode.OK)]
//         [ProducesResponseType((int) HttpStatusCode.NotFound)] 
//         [Produces( "application/json")]
//         [Consumes("application/json")]
//         public async Task<ActionResult<App.DTO.v1_0.Review>> GetReviewWithoutUser(Guid id)
//         {
//             var review = _mapper.Map((await _bll.Reviews.FirstOrDefaultAsyncWithoutUser(id))!);
//             if (review == null)
//             {
//                 return NotFound();
//             }
//
//             return Ok(review);
//             // var review = await _bll.Reviews.FirstOrDefaultAsync(id);
//             //
//             // if (review == null)
//             // {
//             //     return NotFound();
//             // }
//             // var mappedReview = _mapper.Map(review);
//             // return Ok(mappedReview);
//         }
//
//         /// <summary>
//         /// Updates a specific review
//         /// </summary>
//         /// <param name="id">The ID of the review to update</param>
//         /// <param name="review">The updated review data</param>
//         /// <returns>A response indicating the result of the update operation</returns>
//         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//         [HttpPut("{id}")]
//         [ProducesResponseType((int) HttpStatusCode.NoContent)]
//         [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
//         [ProducesResponseType((int) HttpStatusCode.NotFound)]
//         [Produces( "application/json")]
//         [Consumes("application/json")]
//         public async Task<IActionResult> PutReview(Guid id, App.DTO.v1_0.Review review)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }
//             review.Id = id;
//             _bll.Reviews.Update(_mapper.Map(review));
//
//             return NoContent();
//             // if (id != review.Id)
//             // {
//             //     return BadRequest();
//             // }
//             // try
//             // {
//             //     var updated = await Task.Run(() => _bll.Reviews.Update(_mapper.Map(review)));
//             //     if (updated == null)
//             //     {
//             //         return NotFound();
//             //     }
//             // }
//             // catch (DbUpdateConcurrencyException)
//             // {
//             //     if (!await _bll.Reviews.ExistsAsync(id))
//             //     {
//             //         return NotFound();
//             //     }
//             //     else
//             //     {
//             //         throw;
//             //     }
//             // }
//             // return NoContent();
//         }
//
//         /// <summary>
//         /// Adds a new review
//         /// </summary>
//         /// <param name="review">Data transfer object representing the review to add</param>
//         /// <returns>The created review</returns>
//         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//         [HttpPost]
//         [ProducesResponseType<App.BLL.DTO.Review>((int) HttpStatusCode.Created)]
//         [Produces( "application/json")]
//         [Consumes("application/json")]
//         public async Task<ActionResult<App.DTO.v1_0.Review>> PostReview(App.DTO.v1_0.Review review)
//         {
//             review.Id = Guid.NewGuid();
//             review.AppUserId = UserGuid;
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }
//           
//             var newReview = _bll.Reviews.Add(_mapper.Map(review)!);
//             
//             return CreatedAtAction("GetReviewWithUser",
//                 new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newReview.Id }, newReview);
//
//             // var addedReview = await Task.Run(() => _bll.Reviews.Add(_mapper.Map(review)));
//             // var resultReview = _mapper.Map(addedReview);
//             //
//             // return CreatedAtAction("GetReview", new
//             // {
//             //     version = HttpContext.GetRequestedApiVersion()?.ToString(),
//             //     id = resultReview.Id
//             // }, resultReview);
//         }
//
//         /// <summary>
//         /// Deletes a specific review
//         /// </summary>
//         /// <param name="id">The ID of the review to delete</param>
//         /// <returns>A response indicating the result of the delete operation</returns>
//         [HttpDelete("{id}")]
//         [ProducesResponseType((int) HttpStatusCode.NotFound)]
//         [ProducesResponseType((int) HttpStatusCode.NoContent)]
//         [Produces( "application/json")]
//         [Consumes("application/json")]
//         public async Task<IActionResult> DeleteReview(Guid id)
//         {
//             
//             await _bll.Reviews.RemoveAsync(id, UserGuid);
//             return NoContent();
//             // var reviewExists = await _bll.Reviews.ExistsAsync(id);
//             // if (!reviewExists)
//             // {
//             //     return NotFound();
//             // }
//             // var rowsAffected = await _bll.Reviews.RemoveAsync(id);
//             // if (rowsAffected == 0)
//             // {
//             //     return NotFound();
//             // }
//             // return NoContent();
//         }
//     }
// }
