using System;
using Xunit;
using ApiStackNet.Demo;
using ApiStackNet.Demo.Entities;
using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.BLL.Services;
using ApiStackNet.Demo.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ApiStackNet.Demo.Test.CRUD
{
    public class UserCRUDTest : BaseTest
    {
        private UserBO UserBOCreate()
        {
            UserBO user = new UserBO()
            {
                UserId = 23,
                Name = "Riccardo Tinì",
                Address = "Via Fratelli Lumiere, 19, 52100 Arezzo AR",
                Email = "r.tini@sintraconsulting.eu"
            };

            return user;
        }

        [Fact]
        public void CRUDTest()
        {
            //Old BO
            UserBO userBO = UserBOCreate();

            //SAVE
            UserDTO UserDTO = UserService.SaveUser(userBO);

            //New BO
            userBO.Id = UserDTO.Id;
            userBO.Name = "Chiara Bernardini";
            userBO.Email = "c.bernardini@sintraconsulting.eu";
            //EDIT         
            UserService.EditUser(userBO);

            //DELETE by Id
            UserService.Delete(UserDTO.Id);

            //Restore "Old BO" and save the Entity
            userBO = UserBOCreate();
            UserDTO = UserService.SaveUser(userBO);

            //GET BY ID
            UserDTO = UserService.GetById(UserDTO.Id);

            //DELETE by Entity
            UserService.Delete(UserDTO);

            //LIST
            var list = UserService.GetUsersList();
        }
    }
}
