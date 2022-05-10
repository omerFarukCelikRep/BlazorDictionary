using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.EntryComments;
using BlazorDictionary.Common.Infrastracture;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComments.CreateVote;

public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
{
    public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExhange(exchangeName: DictionaryConstants.VoteExhangeName,
                                            exchangeType: DictionaryConstants.DefaultExchangeType,
                                            queueName: DictionaryConstants.CreateEntryCommentVoteQueueName,
                                            obj: new CreateEntryCommentVoteEvent()
                                            {
                                                EntryCommentId = request.EntryCommentId,
                                                CreatedBy = request.CreatedBy,
                                                VoteType = request.VoteType
                                            });

        return await Task.FromResult(true);
    }
}
