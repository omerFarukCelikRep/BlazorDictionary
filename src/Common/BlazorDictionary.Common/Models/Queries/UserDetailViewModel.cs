namespace BlazorDictionary.Common.Models.Queries;
public class UserDetailViewModel
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
}
