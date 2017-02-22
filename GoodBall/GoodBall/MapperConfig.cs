using AutoMapper;
using GoodBall.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodBall
{
    public static class MapperConfig
    {
        static MapperConfig()
        {
            Init();
        }

        private static void Init()
        {
            AutoMapper.Mapper.CreateMap<User, UserDto>();
            AutoMapper.Mapper.CreateMap<UserDto, User>();

        }

        public static TResult ToModel<TResult>(this object entity)
        {
            return Mapper.Map<TResult>(entity);
        }

        public static List<TResult> ToListModel<TResult, TInput>(this IEnumerable<TInput> list)
        {
            return list.Select(x => x.ToModel<TResult>()).ToList();
        }
    }
}
