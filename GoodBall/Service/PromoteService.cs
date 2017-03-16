using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollection.Entity;
using Repository;
using Service.Cond;
using Service.Dto;

namespace Service
{
    public class PromoteService : SingModel<PromoteService>
    {
        private static readonly PromoteRepository promoteRepository = PromoteRepository.Instance;
        
        private PromoteService() { }

        public List<PromoteDto> GetNewsListByPage(PromoteCond cond, int size, int index, out int total)
        {
            var query = promoteRepository.Source;
            if (cond.StartDate != null && cond.EndDate != null)
            {
                query = query.Where(x => x.CreateTime >= cond.StartDate.Value && x.CreateTime <= cond.StartDate.Value);
            }
            query = query.OrderByDescending(x => x.CreateTime);
            return promoteRepository.FindForPaging(size, index, query, out total).ToList().ToListModel<Promote, PromoteDto>();
        }

        public void AddPromote(PromoteDto dto)
        {
            var entity = dto.ToModel<Promote>();
            promoteRepository.Insert(entity);
        }

        public void UpdatePromote(PromoteDto dto)
        {
            var entity = promoteRepository.Find(x => x.Id == dto.Id).FirstOrDefault();
            entity.RaceType = dto.RaceType;
            entity.Content = dto.Content;
            entity.level = dto.level;
            entity.Integral = dto.Integral;
            entity.Price = dto.Price;
            entity.State = dto.State;
            entity.Operator = dto.Operator;
            entity.SendType = dto.SendType;
            promoteRepository.Save(entity);
        }

        public void DeletePromote(long id)
        {
            promoteRepository.Delete(x => x.Id == id);
        }
    }
}
