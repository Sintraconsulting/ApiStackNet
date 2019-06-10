using ApiStackNet.API.Controllers;
using ApiStackNet.API.Model;
using ApiStackNet.Demo.BLL.Services;
using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.DTO;
using ApiStackNet.Demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiStackNet.Demo.Controllers.Api
{
    public class UserController : DataController<UserService, UserDTO, UserBO, User, int>
    {
        private UserService UserService;

        public UserController(UserService service) : base(service)
        {
            this.UserService = service;
        }

        [HttpGet]
        [Route("{id}")]
        public WrappedResponse<UserDTO> GetById([FromUri] int id)
        {
            return WrappedOK(this.UserService.GetById(id));
        }

        [HttpGet]
        [Route("user-list")]
        public WrappedResponse<List<UserDTO>> GetUsersList()
        {
            return WrappedOK(this.UserService.GetUsersList());
        }

        [HttpPost]
        [Route("save")]
        public WrappedResponse<UserDTO> SaveUser(UserBO userBO)
        {
            return WrappedOK(this.UserService.SaveUser(userBO));
        }
        
        [HttpDelete]
        [Route("delete")]
        public override WrappedResponse<bool> Delete(int Id)
        {
            return base.Delete(Id);
        }

        [HttpDelete]
        [Route("delete")]
        public WrappedResponse<bool> DeleteUserByEntity(User user)
        {
            return base.Delete(user.Id);
        }

        [HttpPut]
        [Route("{id}")]
        public WrappedResponse<UserDTO> EditUser(UserBO user)
        {
            return WrappedOK(this.UserService.EditUser(user));
        }
    }
}