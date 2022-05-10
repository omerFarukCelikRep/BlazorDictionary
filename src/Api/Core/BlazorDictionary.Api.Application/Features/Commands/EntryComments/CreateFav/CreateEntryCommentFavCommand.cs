using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComments.CreateFav;

public class CreateEntryCommentFavCommand : IRequest<bool>
{
    public CreateEntryCommentFavCommand(Guid entryCommentId, Guid userId)
    {
        EntryCommentId = entryCommentId;
        UserId = userId;
    }

    public Guid EntryCommentId { get; set; }
    public Guid UserId { get; set; }
}
