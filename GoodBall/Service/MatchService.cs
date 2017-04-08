using DataCollection.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Cond;
using Service.Dto;

namespace Service
{
    public class MatchService : SingModel<MatchService>
    {
        private static readonly MatchRepository matchRepository = MatchRepository.Instance;

        private MatchService() { }

        public List<MatchDto> GetMatchListByPage(MatchCond cond, int size, int index, out int total)
        {
            var query = matchRepository.Source;
            if (cond.StartDate != null && cond.EndDate != null)
            {
                query = query.Where(x => x.MatchTime >= cond.StartDate.Value && x.MatchTime <= cond.EndDate.Value);
            }
            if(!string.IsNullOrEmpty(cond.TeamA))
            {
                query = query.Where(x => cond.TeamA.Contains(x.TeamA));
            }
            if (!string.IsNullOrEmpty(cond.TeamB))
            {
                query = query.Where(x => cond.TeamB.Contains(x.TeamB));
            }
            query = query.OrderByDescending(x => x.CreateTime);
            return matchRepository.FindForPaging(size, index, query, out total).ToList().ToListModel<Match, MatchDto>();
        }

        public MatchDto GetMatch(long id)
        {
            return matchRepository.Find(x => x.Id == id).FirstOrDefault().ToModel<MatchDto>();
        }


        public void AddMatch(MatchDto dto)
        {
            matchRepository.Insert(dto.ToModel<Match>());
        }

        public void UpdateMatch(MatchDto dto)
        {
            var entity = matchRepository.Find(x => x.Id == dto.Id).FirstOrDefault();
            entity.TeamA = dto.TeamA;
            entity.TeamB = dto.TeamB;
            entity.MatchTime = Convert.ToDateTime(dto.MatchTime);
            entity.Dish = dto.Dish;
            entity.Venue = dto.Venue;
            matchRepository.Save(entity);
        }

        public void DeleteMatch(long id)
        {
            matchRepository.Delete(x => x.Id == id);
        }
    }
}
