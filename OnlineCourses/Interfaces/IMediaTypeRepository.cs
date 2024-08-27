using OnlineCourses.Entities;

namespace OnlineCourses.Interfaces
{
    public interface IMediaTypeRepository
    {
        Task<IEnumerable<MediaType>> GetAll();
    }
}
