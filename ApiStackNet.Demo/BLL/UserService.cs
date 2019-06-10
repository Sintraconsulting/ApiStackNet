using ApiStackNet.BLL.Service;
using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.DTO;
using ApiStackNet.Demo.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BLL.Services
{
    public class UserService : IntDataService<UserDTO, UserBO, User>
    {
        public override IQueryable<User> GetQueriable()
        {
            return base.GetQueriable().Where(x => x.Active == true);
        }

        public UserService(DbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public UserDTO SaveUser(UserBO userBO)
        {
            UserDTO userDTO = base.Save(userBO);

            return userDTO;
        }

        public List<UserDTO> GetUsersList()
        {
            List<UserDTO> usersList = new List<UserDTO>();

            var users = this.GetQueriable().ToList();

            foreach (User user in users)
            {
                UserDTO userDTO = mapper.Map<User, UserDTO>(user);
                usersList.Add(userDTO);
            }

            return usersList;
        }

        public UserDTO EditUser(UserBO userBO)
        {
            UserDTO userDTO = new UserDTO();

            User user = this.GetQueriable().FirstOrDefault(x => x.Id == userBO.Id);

            return userDTO = this.Save(userBO);
        }

    }
}