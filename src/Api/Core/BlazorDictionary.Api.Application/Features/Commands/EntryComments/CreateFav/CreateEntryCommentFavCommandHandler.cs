using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.EntryComments;
using BlazorDictionary.Common.Infrastracture;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComments.CreateFav;

public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
{
    public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExhange(exchangeName: DictionaryConstants.FavExhangeName,
                                            exchangeType: DictionaryConstants.DefaultExchangeType,
                                            queueName: DictionaryConstants.CreateEntryCommentFavQueueName,
                                            obj: new CreateEntryCommentFavEvent()
                                            {
                                                EntryCommentId = request.EntryCommentId,
                                                CreatedBy = request.UserId
                                            });

        return await Task.FromResult(true);
    }
}
