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
    public class GoodsService : SingModel<GoodsService>
    {
        private static readonly GoodsRepository goodsRepository = GoodsRepository.Instance;

        private GoodsService() { }

        public List<GoodsDto> GetGoodsListByPage(string name, int size, int index, out int total)
        {
            var query = goodsRepository.Source;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.GoodsName.Contains(name));
            }
            query = query.OrderByDescending(x => x.CreateTime);
            return goodsRepository.FindForPaging(size, index, query, out total).ToList().ToListModel<Goods, GoodsDto>();
        }

        public GoodsDto GetGoods(long id)
        {
            return goodsRepository.Find(x => x.Id == id).FirstOrDefault().ToModel<GoodsDto>();
        }

        public void AddGoods(GoodsDto dto)
        {
            var entity = dto.ToModel<Goods>();
            goodsRepository.Insert(entity);
        }

        public void UpdateGoods(GoodsDto dto)
        {
            goodsRepository.Save(x => x.Id == dto.Id, x => new Goods
            {
                GoodsName = dto.GoodsName,
                Size= dto.Size,
                Quantity = Convert.ToInt32(dto.Quantity),
                Integral = Convert.ToInt32(dto.Integral)
            });
        }

        public void UpdateGoodsImage(GoodsDto dto)
        {
            goodsRepository.Save(x =>x.Id == dto.Id, x => new Goods{GoodsImage = dto.GoodsImage});
        }

        public void DeleteGoods(long id)
        {
            goodsRepository.Delete(x => x.Id == id);
        }
    }
}
