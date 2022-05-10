using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Entries.DeleteFav;

public class DeleteEntryFavCommand : IRequest<bool>
{
    public DeleteEntryFavCommand(Guid entryId, Guid userId)
    {
        EntryId = entryId;
        UserId = userId;
    }

    public Guid EntryId { get; set; }
    public Guid UserId { get; set; }
}
