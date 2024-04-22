using WebMusic.BLL.DTO;

namespace WebMusic.Models
{
    public class IndexViewModel
    {
        public IEnumerable<MediaDTO> Songs { get; }
        public PageViewModel PageViewModel { get; }
        public IndexViewModel(IEnumerable<MediaDTO> songs, PageViewModel viewModel)
        {
            Songs = songs;
            PageViewModel = viewModel;
        }
    }
}
