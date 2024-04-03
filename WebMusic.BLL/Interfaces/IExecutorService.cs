using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMusic.BLL.DTO;

namespace WebMusic.BLL.Interfaces
{
    public interface IExecutorService
    {
        Task CreateExecutor(ExecutorDTO executorDTO);
        Task UpdateExecutor(ExecutorDTO executorDTO);
        Task DeleteExecutor(int id);
        Task<ExecutorDTO> GetExecutor(string name);
        Task<IEnumerable<ExecutorDTO>> GetExecutors();
    }
}
