using System.Net.Mime;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Queries;
using DevsPros.Diabelife.Platform.API.Community.Domain.Services;
using DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;
using DevsPros.Diabelife.Platform.API.Community.Interfaces.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevsPros.Diabelife.Platform.API.Community.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Community Posts endpoints")]
public class CommunityPostsController(
    ICommunityCommandService communityPostCommandService,
    ICommunityQueryService communityPostQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all community posts",
        Description = "Returns all posts in the community",
        OperationId = "GetAllCommunityPosts")]
    [SwaggerResponse(StatusCodes.Status200OK, "Posts retrieved successfully", typeof(IEnumerable<CommunityPostResource>))]
    public async Task<IActionResult> GetAllPosts()
    {
        var query = new GetAllPostsQuery();
        var posts = await communityPostQueryService.HandleAsync(query);
        var resources = posts.Select(post => CommunityPostResourceFromEntityAssembler.ToResourceFromEntity(post));
        return Ok(resources);
    }

    [HttpGet("{postId:guid}")]
    [SwaggerOperation(
        Summary = "Get a post by ID",
        Description = "Retrieve a single post by its unique identifier",
        OperationId = "GetCommunityPostById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Post found", typeof(CommunityPostResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Post not found")]
    public async Task<IActionResult> GetPostById([FromRoute] Guid postId)
    {
        var query = new GetPostByIdQuery(postId);
        var post = await communityPostQueryService.HandleAsync(query);
        if (post is null) return NotFound();

        var resource = CommunityPostResourceFromEntityAssembler.ToResourceFromEntity(post);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new community post",
        Description = "Creates a new post with author, content and optional image",
        OperationId = "CreateCommunityPost")]
    [SwaggerResponse(StatusCodes.Status201Created, "Post created successfully", typeof(CommunityPostResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    public async Task<IActionResult> CreatePost([FromBody] CreateCommunityPostResource resource)
    {
        var command = CreateCommunityPostCommandFromResourceAssembler.ToCommandFromResource(resource);
        var post = await communityPostCommandService.Handle(command);
        if (post is null) return BadRequest();

        var postResource = CommunityPostResourceFromEntityAssembler.ToResourceFromEntity(post);
        return CreatedAtAction(nameof(GetPostById), new { postId = post.Id.Value }, postResource);
    }

    [HttpPost("{postId:guid}/likes")]
    [SwaggerOperation(
        Summary = "Like a community post",
        Description = "Adds a like to the specified post",
        OperationId = "LikeCommunityPost")]
    [SwaggerResponse(StatusCodes.Status200OK, "Like added successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid operation")]
    public async Task<IActionResult> LikePost([FromRoute] Guid postId, [FromBody] LikePostResource resource)
    {
        var command = LikePostCommandFromResourceAssembler.ToCommandFromResource(postId, resource);
        var post = await communityPostCommandService.Handle(command);
        return post is not null ? Ok() : BadRequest();
    }
    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Delete a post", OperationId = "DeleteCommunityPost")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Post deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Post not found")]
    public async Task<IActionResult> DeletePost([FromRoute] Guid id)
    {
        var deleted = await communityPostCommandService.Handle(new DeletePostCommand(id));
        if (!deleted)
            return NotFound(new { message = "Post no encontrado" });

        return NoContent();
    }
    
    
}
