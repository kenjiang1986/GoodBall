using DataCollection.Entity;
using EnumUtils;
using Helper.Enum;
using Repository;
using Service.Cond;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class NewsService : SingModel<NewsService>
    {
        private static readonly NewsRepository newsRepository = NewsRepository.Instance;
        private NewsService() { }

        public List<NewsDto> GetNewsListByPage(NewsCond cond,int size, int index, out int total)
        {
            var query = newsRepository.Source;
            if (!string.IsNullOrEmpty(cond.NewsType))
            {
                var newsType = (NewsTypeEnum)Convert.ToInt32(cond.NewsType);
                query = query.Where(x => x.NewsType.Equals(newsType));
            }
            if (!string.IsNullOrEmpty(cond.Title))
            {
                query = query.Where(x => x.Title == cond.Title);
            }
            if (cond.StartDate != null && cond.EndDate != null)
            {
                query = query.Where(x => x.CreateTime >= cond.StartDate.Value && x.CreateTime <= cond.StartDate.Value);
            }
            query = query.OrderByDescending(x => x.CreateTime);
            return newsRepository.FindForPaging(size, index, query, out total).ToList().ToListModel<News, NewsDto>();
        }

        public NewsDto GetNews(long id)
        {
            return newsRepository.Find(x => x.Id == id).FirstOrDefault().ToModel<NewsDto>();
        }


        public void AddNews(NewsDto dto)
        {
            NewsRepository.Instance.Insert(dto.ToModel<News>());
        }

        public void UpdateNews(NewsDto dto)
        {
            var entity = newsRepository.Find(x => x.Id == dto.Id).FirstOrDefault();
            entity.Content = dto.Content;
            entity.Title = dto.Title;
            newsRepository.Save(entity);
        }

        public void DeleteNews(long id)
        {
            newsRepository.Delete(x => x.Id == id);
        }
    }
}
