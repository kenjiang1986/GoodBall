using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollection.Entity;
using EnumUtils;
using Helper.Enum;
using Repository;
using Service.Dto;

namespace Service
{
    public class PayAmountService : SingModel<PayAmountService>
    {
         private static readonly PayAmountRepository payAmountRepository = PayAmountRepository.Instance;
         private static readonly ReturnRuleRepository returnRuleRepository = ReturnRuleRepository.Instance;

         private PayAmountService() { }

         public List<PayAmountDto> GetPayAmountListByPage(int size, int index, out int total)
         {
             var query = payAmountRepository.Source;
             query = query.OrderByDescending(x => x.CreateTime);
             return payAmountRepository.FindForPaging(size, index, query, out total).ToList().ToListModel<PayAmount, PayAmountDto>();
         }

         public PayAmountDto GetPayAmount(long id)
         {
             return payAmountRepository.Find(x => x.Id == id).FirstOrDefault().ToModel<PayAmountDto>();
         }

         public void AddPayAmount(PayAmountDto dto)
         {
             var entity = dto.ToModel<PayAmount>();
             payAmountRepository.Insert(entity);
         }

         public void UpdatePayAmount(PayAmountDto dto)
         {
             payAmountRepository.Save(x => x.Id == dto.Id, x => new PayAmount { BaseAmount = dto.BaseAmount, CalType = EnumHelper.Parse<CalTypeEnum>(dto.CalType), GiveAmount = dto.GiveAmount });
         }

         public void DeletePayAmount(long id)
         {
             payAmountRepository.Delete(x => x.Id == id);
         }

         public void AddReturnRule(ReturnRuleDto dto)
         {
             var entity = dto.ToModel<ReturnRule>();
             returnRuleRepository.Insert(entity);
         }

         public void UpdateReturnRule(ReturnRuleDto dto)
         {
            var rule = returnRuleRepository.Source.FirstOrDefault();
            if(rule == null)
            {
                returnRuleRepository.Insert(new ReturnRule() { Type = 1, Numerical = (decimal)dto.Numerical/100, FirstPrice = dto.FirstPrice });
            }
            else
            {
                rule.Numerical = (decimal)dto.Numerical/100;
                rule.FirstPrice = dto.FirstPrice;
                returnRuleRepository.Save(rule);
            }
         }

         public ReturnRuleDto GetReturnRule()
         {
             return returnRuleRepository.Source.FirstOrDefault().ToModel<ReturnRuleDto>();
         }
         
    }
}
