namespace WebMusic.Models
{
    public class SortViewModel
    {
        public SortState SongSort { get; set; } 
        public SortState ExecutorSort { get; set; }  
        
        public SortState Current { get; set; }    
        public bool Up { get; set; }  

        public SortViewModel(SortState sortOrder)
        {
           
            SongSort = SortState.SongAsc;
            ExecutorSort = SortState.ExecutorAsc;
           
            Up = true;

            if (sortOrder == SortState.SongDesc || sortOrder == SortState.ExecutorDesc)
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortState.SongDesc:
                    Current = SongSort = SortState.SongAsc;
                    break;
                case SortState.ExecutorAsc:
                    Current = ExecutorSort = SortState.ExecutorDesc;
                    break;
                case SortState.ExecutorDesc:
                    Current = ExecutorSort = SortState.ExecutorAsc;
                    break;
               
                default:
                    Current = SongSort = SortState.SongDesc;
                    break;
            }
        }
    }
}
