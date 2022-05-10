using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.EntryComments;
using BlazorDictionary.Common.Infrastracture;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComments.DeleteVote;

public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExhange(exchangeName: DictionaryConstants.VoteExhangeName,
                                            exchangeType: DictionaryConstants.DefaultExchangeType,
                                            queueName: DictionaryConstants.DeleteEntryCommentVoteQueueName,
                                            obj: new DeleteEntryCommentVoteEvent()
                                            {
                                                EntryCommentId = request.EntryCommentId,
                                                CreatedBy = request.UserId
                                            });

        return await Task.FromResult(true);
    }
}
