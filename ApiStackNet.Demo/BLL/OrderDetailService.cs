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
    public class OrderDetailService : IntDataService<OrderDetailDTO, OrderDetailBO, OrderDetail>
    {
        public override IQueryable<OrderDetail> GetQueriable()
        {
            return base.GetQueriable().Where(x => x.Active == true);
        }

        public OrderDetailService(DbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public OrderDetailDTO SaveOrderDetail(OrderDetailBO orderDetailBO)
        {
            OrderDetailDTO orderDetailDTO = base.Save(orderDetailBO);

            return orderDetailDTO;
        }

        public List<OrderDetailDTO> GetProductsList()
        {
            List<OrderDetailDTO> ordersDetailList = new List<OrderDetailDTO>();

            var ordersDetail = this.GetQueriable().ToList();

            foreach (OrderDetail orderDetail in ordersDetail)
            {
                OrderDetailDTO orderDetailDTO = mapper.Map<OrderDetail, OrderDetailDTO>(orderDetail);
                ordersDetailList.Add(orderDetailDTO);
            }

            return ordersDetailList;
        }

        public OrderDetailDTO EditProduct(OrderDetailBO orderDetailBO)
        {
            OrderDetailDTO orderDetailDTO = new OrderDetailDTO();

            OrderDetail orderDetail = this.GetQueriable().FirstOrDefault(x => x.Id == orderDetailBO.Id);

            return orderDetailDTO = this.Save(orderDetailBO);
        }
    }
}