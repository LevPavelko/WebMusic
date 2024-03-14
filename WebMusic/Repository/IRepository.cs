using WebMusic.Models;
namespace WebMusic.Repository
{
    public interface IRepository
    {
        Task<List<Media>> GetMediaList();
       
        Task Create(Media item);
        void Edit(Media item);
        Task Delete(Media item);

        //Task CreateUser(Users item);
        //Task EditUser(Users item);
        //Task DeleteUser(Users item);
        Task Save();
    }
}
