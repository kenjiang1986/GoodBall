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
using Helper.Enum;
using EnumUtils;


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

        public List<OrderDto> GetUserOrderList(long userId)
        {
            var list = orderRepository.Source.Where(x => x.UserId == userId).ToList();
            return list.ToListModel<Order, OrderDto>();
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
            var userId = UserService.GetCurrentUser().Id;
            var user = UserService.Instance.GetUser(userId);
            if (user == null)
            {
                throw new ServiceException("当前用户未登录，不能完成下单操作");
            }
            if (user.Balance < goods.Integral)
            {
                throw new ServiceException("对不起，您的帐户余额暂不足，请选择其它商品购买或者充值后进行购买");
            }

            var entity = dto.ToModel<Order>();
            entity.Integral = goods.Integral;
            entity.OrderNo = OrderHelper.GetOrderNo();
            entity.State = OrderStateEnum.未发货;
            entity.UserId = userId;
            //减库存
            goods.Quantity = goods.Quantity - Convert.ToInt32(dto.Quantity);
            orderRepository.Transaction(() =>
            {
                orderRepository.Insert(entity);
                GoodsRepository.Instance.Save(goods);
                UserService.Instance.UpdateUserBalance(user.Id, goods.Integral, string.Format("用户购买商品{0}，扣除{1}V币，订单号为{2}", goods.GoodsName, goods.Integral, entity.OrderNo), BalanceMethod.Subtract);
            });
        }

        public void UpdateOrder(OrderDto dto)
        {
            var entity = orderRepository.Find(x => x.Id == dto.Id).FirstOrDefault();
            entity.Receiver = dto.Receiver;
            entity.ContactPhone = dto.ContactPhone;
            entity.Address = dto.Address;
            orderRepository.Save(entity);
        }

        public void UpdateOrderState(long id, string state)
        {
            orderRepository.Save(x => x.Id == id, x => new Order { State = EnumHelper.Parse<OrderStateEnum>(state) });
        }

        public void DeleteOrder(long id)
        {
            orderRepository.Delete(x => x.Id == id);
        }
    }
}
