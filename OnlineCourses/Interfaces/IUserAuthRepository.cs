namespace OnlineCourses.Interfaces
{
    public interface IUserAuthRepository
    {
        Task<bool> UserNameExists(string username);
    }
}
