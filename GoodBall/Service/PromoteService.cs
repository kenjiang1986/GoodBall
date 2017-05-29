using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollection.Entity;
using Repository;
using Service.Cond;
using Service.Dto;
using Helper.Enum;
using EnumUtils;
using Helper;

namespace Service
{
    public class PromoteService : SingModel<PromoteService>
    {
        private static readonly PromoteRepository promoteRepository = PromoteRepository.Instance;
        
        private PromoteService() { }

        public List<PromoteDto> GetPromoteListByPage(PromoteCond cond, int size, int index, out int total)
        {
            var query = promoteRepository.Source;
            if (cond.StartDate != null && cond.EndDate != null)
            {
                query = query.Where(x => x.CreateTime >= cond.StartDate.Value && x.CreateTime <= cond.StartDate.Value);
            }
            if (!string.IsNullOrEmpty(cond.RaceType))
            {
                query = query.Where(x => x.RaceType.ToString() == cond.RaceType);
            }
            query = query.OrderByDescending(x => x.CreateTime);
            return promoteRepository.FindForPaging(size, index, query, out total).ToList().ToListModel<Promote, PromoteDto>();
        }

        public PromoteDto GetPromote(long id)
        {
            return promoteRepository.Find(x => x.Id == id).FirstOrDefault().ToModel<PromoteDto>();
        }

        public void AddPromote(PromoteDto dto)
        {
            var entity = dto.ToModel<Promote>();
            entity.IsSend = false;
            promoteRepository.Insert(entity);
        }

        public void UpdatePromote(PromoteDto dto)
        {
            var entity = promoteRepository.Find(x => x.Id == dto.Id).FirstOrDefault();
            entity.RaceType =  EnumHelper.Parse<RaceTypeEnum>(dto.RaceType);
            entity.Content = dto.Content;
            entity.level = dto.Level;
            entity.Integral = dto.Integral;
            entity.Price = dto.Price;
            entity.State = EnumHelper.Parse<PromoteStateEnum>(dto.State);
            //entity.Operator = dto.Operator;
            entity.SendType = EnumHelper.Parse<SendTypeEnum>(dto.SendType);
            promoteRepository.Save(entity);
        }

        public bool BuyPromote(int id)
        {
            var promote = promoteRepository.Find(x => x.Id == id).FirstOrDefault();
            var userId = UserService.GetCurrentUser().Id;
            var user = UserRepository.Instance.Source.FirstOrDefault(x => x.Id == userId);
            if(user.Balance - promote.Price < 0)
            {
                throw new ServiceException("您的余额不足，请充值后再进行购买");
            }
            promote.UserList.Add(user);
            user.Balance = user.Balance - promote.Price;

            promoteRepository.Transaction(() =>
            {
                promoteRepository.Save(promote);
                UserRepository.Instance.Save(user);
            });
            return promoteRepository.Save(promote);
        }

        public void DeletePromote(long id)
        {
            promoteRepository.Delete(x => x.Id == id);
        }

        public void SendPromote(long id)
        {
            var promote = promoteRepository.Find(x => x.Id == id).FirstOrDefault();

            if(promote.SendType.Equals(SendTypeEnum.短信))
            {
                foreach (var user in promote.UserList)
                {
                    SmsService.SendSms(user.Phone, promote.Content);
                }
            }
            else
            {
                foreach (var user in promote.UserList)
                {
                    SmsService.SendSms(user.Phone, promote.Content);
                }
            }
            promoteRepository.Save(x => x.Id == id, x => new Promote { IsSend = true });
        }
    }
}
