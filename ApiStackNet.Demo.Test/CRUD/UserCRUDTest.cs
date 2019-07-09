using System;
using Xunit;
using ApiStackNet.Demo;
using ApiStackNet.Demo.Entities;
using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.BLL.Services;
using ApiStackNet.Demo.DTO;
using System.Collections.Generic;
using System.Linq;
using Calabonga.PagedListLite;
using System.Linq.Expressions;
using System.Web.Http;
using ApiStackNet.BLL.Model;
using ApiStackNet.API.Model;

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
            List<UserDTO> list = UserService.GetUsersList();
            //name, address, email, auditable
            List<User> listToCheck = DbContext.User.Where(x => x.Active == true).ToList();
            //Check on list of entities returned by the method
            Assert.True(list.Count() == listToCheck.Count());
            int i = 0, iterator = list.Count();
            while (i < iterator)
            {
                Assert.True(list[i].Id == listToCheck[i].Id);
                Assert.True(list[i].UserId == listToCheck[i].UserId);
                Assert.True(list[i].Name == listToCheck[i].Name);
                Assert.True(list[i].Address == listToCheck[i].Address);
                Assert.True(list[i].Email == listToCheck[i].Email);
                Assert.True(list[i].CreatedOn == listToCheck[i].CreatedOn);
                Assert.True(list[i].ModifiedOn == listToCheck[i].ModifiedOn);
                Assert.True(list[i].DeletedOn == listToCheck[i].DeletedOn);
                Assert.True(list[i].CreatedBy == listToCheck[i].CreatedBy);
                Assert.True(list[i].ModifiedBy == listToCheck[i].ModifiedBy);
                Assert.True(list[i].DeletedBy == listToCheck[i].DeletedBy);
                i++;
            }

            IList<OrderInfo<User>> orderbyList = new List<OrderInfo<User>>();
            OrderInfo<User> orderInfo = new OrderInfo<User>(x => x.UserId, OrderByDirection.ASC);
            orderbyList.Add(orderInfo);
            OrderInfo<User> orderInfo2 = new OrderInfo<User>(x => x.Id, OrderByDirection.ASC);
            orderbyList.Add(orderInfo2);

            //Restore "Old BO" and save the Entity
            userBO = UserBOCreate();
            userBO.UserId = 31;
            UserDTO = UserService.SaveUser(userBO);

            date = DateTime.Now.Date;

            //Test List(4 args)
            PagedList<UserDTO> pagedList = UserService.List(x => x.CreatedOn < date, 0, 2, orderbyList);
            var queriedList = DbContext.User.Where(x => x.CreatedOn < date && x.Active == true).OrderBy(x => x.Id).OrderBy(x => x.UserId).ToList();
            PagedList<User> pagedListToCheck = new PagedList<User>(queriedList.Take(2).ToList(), 0, 2, queriedList.Count());
            listToCheck = pagedListToCheck.Items.ToList();
            var listFromSw = pagedList.Items.ToList();
            i = 0;
            iterator = pagedListToCheck.Items.Count();
            while (i < iterator)
            {
                Assert.True(listToCheck[i].Id == listFromSw[i].Id);
                Assert.True(listToCheck[i].Name == listFromSw[i].Name);
                Assert.True(listToCheck[i].Address == listFromSw[i].Address);
                Assert.True(listToCheck[i].Email == listFromSw[i].Email);
                Assert.True(listToCheck[i].CreatedOn == listFromSw[i].CreatedOn);
                Assert.True(listToCheck[i].ModifiedOn == listFromSw[i].ModifiedOn);
                Assert.True(listToCheck[i].DeletedOn == listFromSw[i].DeletedOn);
                Assert.True(listToCheck[i].CreatedBy == listFromSw[i].CreatedBy);
                Assert.True(listToCheck[i].ModifiedBy == listFromSw[i].ModifiedBy);
                Assert.True(listToCheck[i].DeletedBy == listFromSw[i].DeletedBy);

                i++;
            }
            Assert.True(pagedList.PageIndex == pagedListToCheck.PageIndex);
            Assert.True(pagedList.PageSize == pagedListToCheck.PageSize);
            Assert.True(pagedList.TotalCount == pagedListToCheck.TotalCount);
            Assert.True(pagedList.TotalPages == pagedListToCheck.TotalPages);
            Assert.True(pagedList.HasPreviousPage == pagedListToCheck.HasPreviousPage);
            Assert.True(pagedList.HasNextPage == pagedListToCheck.HasNextPage);

            //Test List(3 args)
            pagedList = UserService.List(x => x.CreatedOn < date, 0, 2);
            queriedList = DbContext.User.Where(x => x.CreatedOn < date && x.Active == true).OrderBy(x => x.Id).ToList();
            pagedListToCheck = new PagedList<User>(queriedList.Take(2).ToList(), 0, 2, queriedList.Count());
            listToCheck = pagedListToCheck.Items.ToList();
            listFromSw = pagedList.Items.ToList();
            i = 0;
            iterator = pagedListToCheck.Items.Count();
            while (i < iterator)
            {
                Assert.True(listToCheck[i].Id == listFromSw[i].Id);
                Assert.True(listToCheck[i].Name == listFromSw[i].Name);
                Assert.True(listToCheck[i].Address == listFromSw[i].Address);
                Assert.True(listToCheck[i].Email == listFromSw[i].Email);
                Assert.True(listToCheck[i].CreatedOn == listFromSw[i].CreatedOn);
                Assert.True(listToCheck[i].ModifiedOn == listFromSw[i].ModifiedOn);
                Assert.True(listToCheck[i].DeletedOn == listFromSw[i].DeletedOn);
                Assert.True(listToCheck[i].CreatedBy == listFromSw[i].CreatedBy);
                Assert.True(listToCheck[i].ModifiedBy == listFromSw[i].ModifiedBy);
                Assert.True(listToCheck[i].DeletedBy == listFromSw[i].DeletedBy);

                i++;
            }
            Assert.True(pagedList.PageIndex == pagedListToCheck.PageIndex);
            Assert.True(pagedList.PageSize == pagedListToCheck.PageSize);
            Assert.True(pagedList.TotalCount == pagedListToCheck.TotalCount);
            Assert.True(pagedList.TotalPages == pagedListToCheck.TotalPages);
            Assert.True(pagedList.HasPreviousPage == pagedListToCheck.HasPreviousPage);
            Assert.True(pagedList.HasNextPage == pagedListToCheck.HasNextPage);

            //Test List(2 args)
            pagedList = UserService.List(0, 3);
            queriedList = DbContext.User.Where(x => x.Active == true).OrderBy(x => x.Id).ToList();
            pagedListToCheck = new PagedList<User>(queriedList.Take(3).ToList(), 0, 3, queriedList.Count());
            listToCheck = pagedListToCheck.Items.Take(3).ToList();
            listFromSw = pagedList.Items.ToList();
            i = 0;
            iterator = listToCheck.Count();
            while (i < iterator)
            {
                Assert.True(listToCheck[i].Id == listFromSw[i].Id);
                Assert.True(listToCheck[i].Name == listFromSw[i].Name);
                Assert.True(listToCheck[i].Address == listFromSw[i].Address);
                Assert.True(listToCheck[i].Email == listFromSw[i].Email);
                Assert.True(listToCheck[i].CreatedOn == listFromSw[i].CreatedOn);
                Assert.True(listToCheck[i].ModifiedOn == listFromSw[i].ModifiedOn);
                Assert.True(listToCheck[i].DeletedOn == listFromSw[i].DeletedOn);
                Assert.True(listToCheck[i].CreatedBy == listFromSw[i].CreatedBy);
                Assert.True(listToCheck[i].ModifiedBy == listFromSw[i].ModifiedBy);
                Assert.True(listToCheck[i].DeletedBy == listFromSw[i].DeletedBy);

                i++;
            }
            Assert.True(pagedList.PageIndex == pagedListToCheck.PageIndex);
            Assert.True(pagedList.PageSize == pagedListToCheck.PageSize);
            Assert.True(pagedList.TotalCount == pagedListToCheck.TotalCount);
            Assert.True(pagedList.TotalPages == pagedListToCheck.TotalPages);
            Assert.True(pagedList.HasPreviousPage == pagedListToCheck.HasPreviousPage);
            Assert.True(pagedList.HasNextPage == pagedListToCheck.HasNextPage);

            //DELETE by Id
            UserService.Delete(UserDTO.Id);
        }
    }
}