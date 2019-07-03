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
                Name = "Mario Rossi",
                Address = "Via Arezzo, 32",
                Email = "m.rossi@email.com"
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
            
            User UserToCheck = DbContext.User.Where(x => x.Id == UserDTO.Id && x.Active == true).FirstOrDefault();
            var date = DateTime.Now;
            //Check object's fields on SAVE
            Assert.True(UserToCheck.Id == UserDTO.Id);
            Assert.True(UserToCheck.UserId == UserDTO.UserId);
            Assert.True(UserToCheck.Name == UserDTO.Name);
            Assert.True(UserToCheck.Address == UserDTO.Address);
            Assert.True(UserToCheck.Email == UserDTO.Email);         
            Assert.True(UserToCheck.CreatedOn.Date == date.Date);
            Assert.True(UserToCheck.ModifiedOn.Date == date.Date);

            //New BO
            userBO.Id = UserDTO.Id;
            userBO.Name = "Tizio Caio";
            userBO.Email = "t.caio@email.com";
            //EDIT         
            UserService.EditUser(userBO);

            UserToCheck = DbContext.User.Where(x => x.Id == userBO.Id && x.Active == true).FirstOrDefault();
            //Check object's fields on EDIT
            Assert.True(UserToCheck.Id == userBO.Id);
            Assert.True(UserToCheck.UserId == userBO.UserId);
            Assert.True(UserToCheck.Name == userBO.Name);
            Assert.True(UserToCheck.Address == userBO.Address);
            Assert.True(UserToCheck.Email == userBO.Email);
            Assert.True(UserToCheck.CreatedOn == UserDTO.CreatedOn);
            Assert.True(UserToCheck.ModifiedOn > UserDTO.ModifiedOn);
            Assert.True(UserToCheck.DeletedOn == UserDTO.DeletedOn);
            Assert.True(UserToCheck.CreatedBy == UserDTO.CreatedBy);
            //Assert.True(UserToCheck.ModifiedBy == UserDTO.ModifiedBy); who modifies the entity is not always the same who created it
            Assert.True(UserToCheck.DeletedBy == UserDTO.DeletedBy);

            //DELETE by Id
            UserService.Delete(UserDTO.Id);

            UserToCheck = DbContext.User.Where(x => x.Id == UserDTO.Id && x.Active == false).FirstOrDefault();
            //Check if entity is not null
            Assert.True(UserToCheck != null);
            //Check if the deleted date is modified on DELETE
            Assert.True(UserToCheck.DeletedOn > UserDTO.DeletedOn);

            //Restore "Old BO" and save the Entity
            userBO = UserBOCreate();
            UserDTO = UserService.SaveUser(userBO);

            //GET BY ID
            UserDTO = UserService.GetById(UserDTO.Id);

            UserToCheck = DbContext.User.Where(x => x.Id == UserDTO.Id && x.Active == true).FirstOrDefault();
            //Check object's fields on GET BY ID
            Assert.True(UserDTO.Id == UserToCheck.Id);
            Assert.True(UserDTO.UserId == UserToCheck.UserId);
            Assert.True(UserDTO.Name == UserToCheck.Name);
            Assert.True(UserDTO.Address == UserToCheck.Address);
            Assert.True(UserDTO.Email == UserToCheck.Email);
            Assert.True(UserDTO.CreatedOn == UserToCheck.CreatedOn);
            Assert.True(UserDTO.ModifiedOn == UserToCheck.ModifiedOn);
            Assert.True(UserDTO.DeletedOn == UserToCheck.DeletedOn);
            Assert.True(UserDTO.CreatedBy == UserToCheck.CreatedBy);
            Assert.True(UserDTO.ModifiedBy == UserToCheck.ModifiedBy);
            Assert.True(UserDTO.DeletedBy == UserToCheck.DeletedBy);

            //DELETE by Entity
            UserService.Delete(UserDTO);

            UserToCheck = DbContext.User.Where(x => x.Id == UserDTO.Id && x.Active == false).FirstOrDefault();
            //Check if entity is not null
            Assert.True(UserToCheck != null);
            //Check if the deleted date is modified on DELETE
            Assert.True(UserToCheck.DeletedOn > UserDTO.DeletedOn);

            //LIST
            var list = UserService.GetUsersList();

            var listToCheck = DbContext.User.Where(x => x.Active == true);
            //Check on list of entities returned by the method
            Assert.True(list.Count() == listToCheck.Count());
        }
    }
}
