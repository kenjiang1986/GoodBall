using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Service.Dto;
using DataCollection.Entity;

namespace Service
{
    public class RechargeRecordService : SingModel<RechargeRecordService>
    {
        private RechargeRecordService() { }

        private static readonly RechargeRecordRepository rechargeRepository = RechargeRecordRepository.Instance;

        public List<RechargeRecordDto> GetRechargeListByUserId(long userId)
        {
            var query = rechargeRepository.Source.Include(x => x.RechargeUser).Where(x => x.UserId == userId);
            return query.ToList().ToListModel<RechargeRecord, RechargeRecordDto>();
        }

        public void AddRecharge(RechargeRecordDto dto)
        {
            var entity = dto.ToModel<RechargeRecord>();
            rechargeRepository.Insert(entity);
        }
    }
}
