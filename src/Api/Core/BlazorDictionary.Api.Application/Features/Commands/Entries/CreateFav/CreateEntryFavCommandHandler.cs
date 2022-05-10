using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Entries;
using BlazorDictionary.Common.Infrastracture;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Entries.CreateFav;

public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
{
    public async Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExhange(exchangeName: DictionaryConstants.FavExhangeName,
                                          exchangeType: DictionaryConstants.DefaultExchangeType,
                                          queueName: DictionaryConstants.CreateEntryFavQueueName,
                                          obj: new CreateEntryFavEvent()
                                          {
                                              EntryId = request.EntryId.Value,
                                              CreatedBy = request.UserId.Value
                                          });

        return await Task.FromResult(true);
    }
}
