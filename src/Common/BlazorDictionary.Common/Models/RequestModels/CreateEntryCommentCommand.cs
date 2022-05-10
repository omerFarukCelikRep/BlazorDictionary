using MediatR;

namespace BlazorDictionary.Common.Models.RequestModels;

public class CreateEntryCommentCommand : IRequest<Guid>
{
    public CreateEntryCommentCommand()
    {

    }
    public CreateEntryCommentCommand(Guid? entryId, string content, Guid? createdById)
    {
        EntryId = entryId;
        Content = content;
        CreatedById = createdById;
    }

    public Guid? EntryId { get; set; }
    public string Content { get; set; }
    public Guid? CreatedById { get; set; }
}
