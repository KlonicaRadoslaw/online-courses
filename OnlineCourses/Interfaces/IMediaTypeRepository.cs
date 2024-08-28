using OnlineCourses.Entities;

namespace OnlineCourses.Interfaces
{
    public interface IMediaTypeRepository
    {
        Task<IEnumerable<MediaType>> GetAll();
        Task<MediaType> GetById(int id);
        bool Add(MediaType mediaType);
        bool Update(MediaType mediaType);
        bool Delete(MediaType mediaType);
        bool Save();
        bool DoesExist(int id);
    }
}
