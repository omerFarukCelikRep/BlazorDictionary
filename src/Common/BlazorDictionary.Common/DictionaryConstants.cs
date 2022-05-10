namespace BlazorDictionary.Common;

public class DictionaryConstants
{
    public const string RabbitMqHost = "localhost";
    public const string DefaultExchangeType = "direct";

    public const string UserExhangeName = "UserExchange";
    public const string FavExhangeName = "FavExhange";
    public const string VoteExhangeName = "VoteExhange";

    public const string UserEmailChangedQueueName = "UserEmailChangedQueue";

    public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue";
    public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueue";
    public const string CreateEntryFavQueueName = "CreateEntryFavQueue";
    public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue";

    public const string DeleteEntryFavQueueName = "DeleteEntryFavQueue";
    public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueue";
    public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueue";
    public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueue";
}