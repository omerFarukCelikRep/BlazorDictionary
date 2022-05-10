using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Entries;
using BlazorDictionary.Common.Infrastracture;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Entries.CreateVote;

public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
{
    public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExhange(exchangeName: DictionaryConstants.VoteExhangeName,
                                         exchangeType: DictionaryConstants.DefaultExchangeType,
                                         queueName: DictionaryConstants.CreateEntryVoteQueueName,
                                         obj: new CreateEntryVoteEvent()
                                         {
                                             EntryId = request.EntryId,
                                             CreatedBy = request.CreatedBy,
                                             VoteType = request.VoteType
                                         });

        return await Task.FromResult(true);
    }
}
