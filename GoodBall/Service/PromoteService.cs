﻿using System;
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
            var query = promoteRepository.Source.Where(x => x.PromoteType == cond.PromoteType);
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
            promoteRepository.Transaction(() =>
            {
                promoteRepository.Save(entity);
                if (entity.State.Equals(PromoteStateEnum.中))
                {
                    ReturnPrice(entity.Price, entity.UserList.ToList());
                    
                }
            });
            
        }

        /// <summary>
        /// 退款
        /// </summary>
        public void ReturnPrice(int price, IList<User> userList)
        {
            var rule = ReturnRuleRepository.Instance.Source.FirstOrDefault();
            int updatePrice = 0;
            if (rule != null)
            {
                //按百分比退款
                if (rule.Type == 1)
                {
                    updatePrice = price + (int)(price * rule.Numerical);
                }

                UserRepository.Instance.Save(x => userList.Select(y => y.UserName).Contains(x.UserName), x => new User { Balance = updatePrice });
                foreach (var user in userList)
                {
                        RechargeRecordService.Instance.AddRecharge
                        (
                            new RechargeRecordDto()
                            {
                                Price = updatePrice,
                                Operator = UserService.GetCurrentUser().UserName,
                                RechargeUserId = user.Id,
                                Remark = string.Format("推介不中退款{0}V币", updatePrice)
                            }
                        );
                }
            }
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
