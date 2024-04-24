using WebMusic.BLL.DTO;

namespace WebMusic.Models
{
    public class IndexViewModel
    {
        public IEnumerable<MediaDTO> Songs { get; }
        public PageViewModel PageViewModel { get; }
        public SortViewModel SortViewModel { get; set; } = new SortViewModel(SortState.SongAsc);

        public IndexViewModel(IEnumerable<MediaDTO> songs, PageViewModel viewModel, SortViewModel sortViewModel)
        {
            Songs = songs;
            PageViewModel = viewModel;
            SortViewModel = sortViewModel;
        }
    }
}
