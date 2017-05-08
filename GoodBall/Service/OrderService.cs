using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollection.Entity;
using Helper;
using Repository;
using Service.Cond;
using Service.Dto;

namespace Service
{
    public class OrderService : SingModel<OrderService>
    {
        private static readonly OrderRepository orderRepository = OrderRepository.Instance;

        private OrderService() { }

        public List<OrderDto> GetOrderListByPage(long userId, string orderNo, int size, int index, out int total)
        {
            var query = orderRepository.Source;
            if (userId > 0)
            {
                query = query.Where(x => x.UserId == userId);
            }
            if (!string.IsNullOrEmpty(orderNo))
            {
                query = query.Where(x => x.OrderNo == orderNo);
            }
            query = query.OrderByDescending(x => x.CreateTime);
            return orderRepository.FindForPaging(size, index, query, out total).ToList().ToListModel<Order, OrderDto>();
        }

        public OrderDto GetOrder(long id)
        {
            return orderRepository.Find(x => x.Id == id).FirstOrDefault().ToModel<OrderDto>();
        }

        public void AddOrder(OrderDto dto)
        {
            var goods = GoodsRepository.Instance.Find(x => x.Id == dto.GoodsId).FirstOrDefault();
            if (goods == null)
            {
                throw new ServiceException("不存在此商品");
            }
            if (Convert.ToInt32(dto.Quantity) > goods.Quantity)
            {
                throw new ServiceException("此商品库存不足");
            }
            var entity = dto.ToModel<Order>();
            orderRepository.Insert(entity);
        }

        public void UpdateOrder(OrderDto dto)
        {
            var entity = orderRepository.Find(x => x.Id == dto.Id).FirstOrDefault();
            entity.Receiver = dto.Receiver;
            entity.ContactPhone = dto.ContactPhone;
            entity.Adress = dto.Adress;
            orderRepository.Save(entity);
        }

        public void DeleteOrder(long id)
        {
            orderRepository.Delete(x => x.Id == id);
        }
    }
}
