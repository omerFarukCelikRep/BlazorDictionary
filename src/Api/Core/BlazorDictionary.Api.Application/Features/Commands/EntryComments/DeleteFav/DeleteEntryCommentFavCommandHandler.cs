using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.EntryComments;
using BlazorDictionary.Common.Infrastracture;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComments.DeleteFav;

public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExhange(exchangeName: DictionaryConstants.FavExhangeName,
                                            exchangeType: DictionaryConstants.DefaultExchangeType,
                                            queueName: DictionaryConstants.DeleteEntryCommentFavQueueName,
                                            obj: new DeleteEntryCommentFavEvent()
                                            {
                                                EntryCommentId = request.EntryCommentId,
                                                CreatedBy = request.UserId
                                            });

        return await Task.FromResult(true);
    }
}
