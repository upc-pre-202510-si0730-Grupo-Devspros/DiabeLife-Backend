using System.Net.Mime;
using DevsPros.Diabelife.Platform.API.Community.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Community.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Queries;
using DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;
using DevsPros.Diabelife.Platform.API.Community.Interfaces.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevsPros.Diabelife.Platform.API.Community.Interfaces;

[ApiController]
[Route("api/v1/community-posts/{postId:guid}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Community Comments endpoints")]
public class CommentsController(
    ICommentCommandService commentCommandService,
    ICommentQueryService commentQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all comments for a post",
        Description = "Retrieve all comments associated with a community post",
        OperationId = "GetCommentsByPostId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Comments retrieved", typeof(IEnumerable<CommentResource>))]
    public async Task<IActionResult> GetCommentsByPostId([FromRoute] Guid postId)
    {
        var query = new GetCommentsByPostIdQuery(postId);
        var comments = await commentQueryService.Handle(query);
        var resources = comments.Select(CommentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Add a comment to a post",
        Description = "Creates a new comment for a given post",
        OperationId = "AddCommentToPost")]
    [SwaggerResponse(StatusCodes.Status201Created, "Comment created successfully", typeof(CommentResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid comment request")]
    public async Task<IActionResult> AddComment([FromRoute] Guid postId, [FromBody] CreateCommentResource resource)
    {
        var command = CreateCommentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var comment = await commentCommandService.Handle(command);
        if (comment is null) return BadRequest();

        var commentResource = CommentResourceFromEntityAssembler.ToResourceFromEntity(comment);
        return CreatedAtAction(nameof(GetCommentsByPostId), new { postId }, commentResource);
    }
}