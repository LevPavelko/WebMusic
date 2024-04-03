using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMusic.BLL.DTO;
using WebMusic.BLL.Infrastructure;
using WebMusic.BLL.Interfaces;
using WebMusic.DAL.Interfaces;
using WebMusic.DAL.Entities;
using AutoMapper;

namespace WebMusic.BLL.Services
{
    public class ExecutorService : IExecutorService
    {
        IUnitOfWork Database { get; set; }
        public ExecutorService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task CreateExecutor(ExecutorDTO executorDTO) 
        {
            var author = new Executor
            {
                Id = executorDTO.Id,
                Name = executorDTO.Name
               
            };
            await Database.executor.Create(author);
            await Database.Save();
        }
        public async Task UpdateExecutor(ExecutorDTO executorDTO) 
        {
            var author = new Executor
            {
                Id = executorDTO.Id,
                Name = executorDTO.Name
                
            };
            Database.executor.Update(author);
            await Database.Save();
        }
        public async Task DeleteExecutor(int id)
        {
            await Database.executor.Delete(id);
            await Database.Save();
        }
        public async Task<ExecutorDTO> GetExecutor(string name)
        {
            var author = await Database.executor.Get(name);
            if (author == null)
                throw new ValidationException("Wrong executor!", "");
            return new ExecutorDTO
            {
                Id = author.Id,
                Name = author.Name
                
            };
        }
        public async Task<IEnumerable<ExecutorDTO>> GetExecutors()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Executor, ExecutorDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Executor>, IEnumerable<ExecutorDTO>>(await Database.executor.GetAll());
        }
    }
}
