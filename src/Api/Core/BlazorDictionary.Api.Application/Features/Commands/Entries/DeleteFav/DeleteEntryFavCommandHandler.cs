using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Entries;
using BlazorDictionary.Common.Infrastracture;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Entries.DeleteFav;

public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExhange(exchangeName: DictionaryConstants.VoteExhangeName,
                                         exchangeType: DictionaryConstants.DefaultExchangeType,
                                         queueName: DictionaryConstants.DeleteEntryFavQueueName,
                                         obj: new DeleteEntryFavEvent()
                                         {
                                             EntryId = request.EntryId,
                                             CreatedBy = request.UserId
                                         });

        return await Task.FromResult(true);
    }
}
