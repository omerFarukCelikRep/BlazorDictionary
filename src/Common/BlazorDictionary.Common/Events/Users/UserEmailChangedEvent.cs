namespace BlazorDictionary.Common.Events.Users;

public class UserEmailChangedEvent
{
    public string OldEmailAddress { get; set; }
    public string NewEmailAddress { get; set; }
}
