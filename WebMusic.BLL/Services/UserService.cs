using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMusic.BLL.DTO;
using WebMusic.BLL.Interfaces;
using WebMusic.DAL.Entities;
using WebMusic.DAL.Interfaces;
using WebMusic.BLL.Infrastructure;
using AutoMapper;

namespace WebMusic.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }
        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task CreateUser(UserDTO userDTO)
        {
            var user = new Users
            {
               Id = userDTO.Id,
               FirstName = userDTO.FirstName,
               LastName = userDTO.LastName,
               Login = userDTO.Login,
               Email = userDTO.Email,
               Password = userDTO.Password,
               Salt = userDTO.Salt,
               Status = userDTO.Status

            };
            await Database.user.Create(user);
            await Database.Save();
        }
        public async Task UpdateUser(UserDTO userDTO)
        {
            var user = new Users
            {
                Id = userDTO.Id,
                Email = userDTO.Email,
                Status = userDTO.Status

            };
            Database.user.Update(user);
            await Database.Save();
        }
        public async Task DeleteUser(int id)
        {
            await Database.user.Delete(id);
            await Database.Save();
        }
        public async Task<UserDTO> GetUser(int id)
        {
            var user = await Database.user.Get(id);
            if (user == null)
                throw new ValidationException("Wrong user!", "");
            return new UserDTO
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Status= user.Status
                

            };
        }
        public async Task<UserDTO> GetUserByLogin(string login)
        {
            var user = await Database.user.GetByLogin(login);
            if (user == null)
                throw new ValidationException("Wrong user!", "");
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Salt = user.Salt,
                Login = user.Login,
                Email = user.Email,
                Status = user.Status


            };
        }
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Users, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Users>, IEnumerable<UserDTO>>(await Database.user.GetAll());
        }
    }
}
