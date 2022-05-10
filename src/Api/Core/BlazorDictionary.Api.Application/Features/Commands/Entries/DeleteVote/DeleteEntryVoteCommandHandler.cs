using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Entries;
using BlazorDictionary.Common.Infrastracture;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Entries.DeleteVote;

public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExhange(exchangeName: DictionaryConstants.VoteExhangeName,
                                         exchangeType: DictionaryConstants.DefaultExchangeType,
                                         queueName: DictionaryConstants.DeleteEntryVoteQueueName,
                                         obj: new DeleteEntryVoteEvent()
                                         {
                                             EntryId = request.EntryId,
                                             CreatedBy = request.UserId
                                         });

        return await Task.FromResult(true);
    }
}
