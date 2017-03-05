using AutoMapper;
using DataCollection.Entity;
using GoodBall.Dto;
using Helper;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class MapperConfig
    {
        public static object Md5Helper { get; private set; }

        static MapperConfig()
        {
            Init();
        }

        private static void Init()
        {
            AutoMapper.Mapper.CreateMap<User, UserDto>();
            AutoMapper.Mapper.CreateMap<UserDto, User>().ForMember(x => x.Password, x => x.MapFrom(src => MD5Helper.MD5Encrypt64(src.Password)));
            AutoMapper.Mapper.CreateMap<Match, MatchDto>();
            AutoMapper.Mapper.CreateMap<MatchDto, Match>();
            AutoMapper.Mapper.CreateMap<News, NewsDto>();
            AutoMapper.Mapper.CreateMap<NewsDto, News>();
            AutoMapper.Mapper.CreateMap<Promote, PromoteDto>();
            AutoMapper.Mapper.CreateMap<PromoteDto, Promote>();
            AutoMapper.Mapper.CreateMap<RechargeRecord, RechargeRecordDto>().ForMember(x => x.RechargeUser, x => x.MapFrom(src => src.RechargeUser.UserName)); 
            AutoMapper.Mapper.CreateMap<RechargeRecordDto, RechargeRecord>();

        }

        public static TResult ToModel<TResult>(this object entity)
        {
            return Mapper.Map<TResult>(entity);
        }

        public static List<TResult> ToListModel<TInput,TResult>(this List<TInput> list)
        {
            return list.Select(x => x.ToModel<TResult>()).ToList();
        }
    }
}
