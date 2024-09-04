using OnlineCourses.Entities;

namespace OnlineCourses.Interfaces
{
    public interface IDataFunctions
    {
        Task UsersForCategoryAddAndDeleteTransactionAsync(List<UserCategory> usersSelectedForCategoryToAdd, List<UserCategory> usersSelectedForCategoryToDelete);
    }
}
