using WebMusic.BLL.DTO;

namespace WebMusic.Models
{
    public class GenresAndExecutorsViewModel
    {
        public IEnumerable<GenreDTO> Genres { get; set; }
        public IEnumerable<ExecutorDTO> Executors { get; set; }
        public GenresAndExecutorsViewModel(IEnumerable<GenreDTO> genres, IEnumerable<ExecutorDTO> executors)
        {
          Genres = genres;
            Executors = executors;
        }
    }
}
