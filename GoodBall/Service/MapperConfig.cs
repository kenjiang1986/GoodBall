using AutoMapper;
using DataCollection.Entity;
using EnumUtils;
using GoodBall.Dto;
using Helper;
using Helper.Enum;
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

            AutoMapper.Mapper.CreateMap<Match, MatchDto>()
                .ForMember(x => x.MatchState, x => x.MapFrom(src => src.MatchState.ToString()));
            AutoMapper.Mapper.CreateMap<MatchDto, Match>()
                .ForMember(x => x.MatchState, x => x.MapFrom(src => MatchStateEnum.未开始))
                .ForMember(x => x.Operator, x => x.MapFrom(src => UserService.GetCurrentUser().UserName))
                .ForMember(x => x.CreateTime, x => x.MapFrom(src => DateTime.Now));
            
            AutoMapper.Mapper.CreateMap<News, NewsDto>()
                .ForMember(x => x.NewsType, x => x.MapFrom(src => src.NewsType.ToString())); 
            AutoMapper.Mapper.CreateMap<NewsDto, News>()
                .ForMember(x => x.Operator, x => x.MapFrom(src => UserService.GetCurrentUser().UserName))
                .ForMember(x => x.CreateTime, x => x.MapFrom(src => DateTime.Now))
                .ForMember(x => x.NewsType, x => x.MapFrom(src => EnumHelper.Parse<NewsTypeEnum>(src.NewsType)));

            AutoMapper.Mapper.CreateMap<Promote, PromoteDto>()
                 .ForMember(x => x.RaceType, x => x.MapFrom(src => src.RaceType.ToString()))
                  .ForMember(x => x.State, x => x.MapFrom(src => src.State.ToString()))
                  .ForMember(x => x.IsSend, x => x.MapFrom(src => src.IsSend ? "是" : "否"))
                   .ForMember(x => x.SendType, x => x.MapFrom(src => src.SendType.ToString()))
                .ForMember(x => x.MatchName, x => x.MapFrom(src => src.Match.TeamA + "VS" + src.Match.TeamB));
            AutoMapper.Mapper.CreateMap<PromoteDto, Promote>()
                   .ForMember(x => x.Operator, x => x.MapFrom(src => UserService.GetCurrentUser().UserName))
                .ForMember(x => x.CreateTime, x => x.MapFrom(src => DateTime.Now))
                .ForMember(x => x.RaceType, x => x.MapFrom(src => EnumHelper.Parse<RaceTypeEnum>(src.RaceType)))
                  .ForMember(x => x.State, x => x.MapFrom(src => EnumHelper.Parse<PromoteStateEnum>(src.State)))
                 .ForMember(x => x.SendType, x => x.MapFrom(src => EnumHelper.Parse<SendTypeEnum>(src.SendType)));
                



            AutoMapper.Mapper.CreateMap<RechargeRecord, RechargeRecordDto>().ForMember(x => x.RechargeUser, x => x.MapFrom(src => src.RechargeUser.UserName)); 
            AutoMapper.Mapper.CreateMap<RechargeRecordDto, RechargeRecord>();

            AutoMapper.Mapper.CreateMap<Goods, GoodsDto>();
            AutoMapper.Mapper.CreateMap<GoodsDto, Goods>().ForMember(x => x.CreateTime, x => x.MapFrom(src => DateTime.Now));

            AutoMapper.Mapper.CreateMap<Order, OrderDto>()
                 .ForMember(x => x.GoodsName, x => x.MapFrom(src => src.Goods.GoodsName))
                .ForMember(x => x.GoodsImage, x => x.MapFrom(src => src.Goods.GoodsImage));
            AutoMapper.Mapper.CreateMap<OrderDto, Order>()
                .ForMember(x => x.OrderNo, x => x.MapFrom(src => OrderHelper.GetOrderNo()));

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
