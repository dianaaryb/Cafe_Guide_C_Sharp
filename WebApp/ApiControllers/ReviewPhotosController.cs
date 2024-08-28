using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReviewPhotosController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.ReviewPhoto, App.BLL.DTO.ReviewPhoto> _mapper;
        private readonly IMapper _autoMapper = null!;

        public ReviewPhotosController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.ReviewPhoto, App.BLL.DTO.ReviewPhoto>(autoMapper);

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
        /// Returns all review photos visible to current user
        /// </summary>
        /// <returns>list of review photos</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.ReviewPhoto>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.ReviewPhoto>>> GetReviewPhotos()
        {
            var res = (await _bll.ReviewPhotos.GetAllAsync(UserGuid
                
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }
        
        /// <summary>
        /// Returns all review photos
        /// </summary>
        /// <returns>list of review photos</returns>
        [HttpGet("all")]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.ReviewPhoto>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.ReviewPhoto>>> GetAllReviewPhotos()
        {
            var res = (await _bll.ReviewPhotos.GetAllAsync(
                
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }
        
        /// <summary>
        /// Returns all review photos for specific review
        /// </summary>
        /// <returns>list of review photos</returns>
        [HttpGet("all/{reviewId}")]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.ReviewPhoto>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.ReviewPhoto>>> GetAllReviewPhotosForReview(Guid reviewId)
        {
            var res = (await _bll.ReviewPhotos.GetAllAsync(reviewId
                
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Returns a single review photo by id
        /// </summary>
        /// <param name="id">The ID of the review photo to retrieve</param>
        /// <returns>A review object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.ReviewPhoto>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.ReviewPhoto>> GetReviewPhoto(Guid id)
        {
            var reviewPhoto = await _bll.ReviewPhotos.FirstOrDefaultAsync(id);

            if (reviewPhoto == null)
            {
                return NotFound();
            }
            var mappedReviewPhoto = _mapper.Map(reviewPhoto);
            return Ok(mappedReviewPhoto);
        }
        
        // /// <summary>
        // /// Returns a single review photo by id
        // /// </summary>
        // /// <param name="id">The ID of the review photo to retrieve</param>
        // /// <returns>A review object</returns>
        // [HttpGet("{id}/withoutUser")]
        // [ProducesResponseType<App.BLL.DTO.ReviewPhoto>((int) HttpStatusCode.OK)]
        // [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        // [Produces( "application/json")]
        // [Consumes("application/json")]
        // public async Task<ActionResult<App.DTO.v1_0.ReviewPhoto>> GetReviewPhotoWithoutUser(Guid id)
        // {
        //     var reviewPhoto = await _bll.ReviewPhotos.FirstOrDefaultAsync(id);
        //
        //     if (reviewPhoto == null)
        //     {
        //         return NotFound();
        //     }
        //     var mappedReviewPhoto = _mapper.Map(reviewPhoto);
        //     return Ok(mappedReviewPhoto);
        // }

        /// <summary>
        /// Updates a specific review photo
        /// </summary>
        /// <param name="id">The ID of the review photo to update</param>
        /// <param name="reviewPhoto">The updated review photo data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutReviewPhoto(Guid id, App.DTO.v1_0.ReviewPhoto reviewPhoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            reviewPhoto.Id = id;
            _bll.ReviewPhotos.Update(_mapper.Map(reviewPhoto));

            return NoContent();
            // if (id != reviewPhoto.Id)
            // {
            //     return BadRequest();
            // }
            // try
            // {
            //     var updated = await Task.Run(() => _bll.ReviewPhotos.Update(_mapper.Map(reviewPhoto)));
            //     if (updated == null)
            //     {
            //         return NotFound();
            //     }
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!await _bll.ReviewPhotos.ExistsAsync(id))
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
        /// Adds a new review photo
        /// </summary>
        /// <param name="reviewPhoto">Data transfer object representing the review photo to add</param>
        /// <returns>The created review photo</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.ReviewPhoto>((int) HttpStatusCode.Created)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.ReviewPhoto>> PostReviewPhoto(App.DTO.v1_0.ReviewPhoto reviewPhoto)
        {
            reviewPhoto.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var newReviewPhoto = _bll.ReviewPhotos.Add(_mapper.Map(reviewPhoto)!);
            
            return CreatedAtAction("GetReviewPhoto",
                new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newReviewPhoto.Id }, newReviewPhoto);

            // var addedReviewPhoto = await Task.Run(() => _bll.ReviewPhotos.Add(_mapper.Map(reviewPhoto)));
            // var resultReviewPhoto = _mapper.Map(addedReviewPhoto);
            //
            // return CreatedAtAction("GetReviewPhoto", new
            // {
            //     version = HttpContext.GetRequestedApiVersion()?.ToString(),
            //     id = resultReviewPhoto.Id
            // }, resultReviewPhoto);
        }

        /// <summary>
        /// Deletes a specific review photo
        /// </summary>
        /// <param name="id">The ID of the review photo to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteReviewPhoto(Guid id)
        {
            await _bll.ReviewPhotos.RemoveAsync(id);
            // var reviewPhotoExists = await _bll.ReviewPhotos.ExistsAsync(id);
            // if (!reviewPhotoExists)
            // {
            //     return NotFound();
            // }
            // var rowsAffected = await _bll.ReviewPhotos.RemoveAsync(id);
            // if (rowsAffected == 0)
            // {
            //     return NotFound();
            // }
            return NoContent();
        }
    }
}
