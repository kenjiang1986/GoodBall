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
            
            AutoMapper.Mapper.CreateMap<Match, MatchDto>();
            AutoMapper.Mapper.CreateMap<MatchDto, Match>()
                .ForMember(x => x.Operator, x => x.MapFrom(src => UserService.GetCurrentUser().UserName))
                .ForMember(x => x.CreateTime, x => x.MapFrom(src => DateTime.Now));
            
            AutoMapper.Mapper.CreateMap<News, NewsDto>()
                .ForMember(x => x.NewsType, x => x.MapFrom(src => src.NewsType.ToString())); 
            AutoMapper.Mapper.CreateMap<NewsDto, News>()
                .ForMember(x => x.Operator, x => x.MapFrom(src => UserService.GetCurrentUser().UserName))
                .ForMember(x => x.CreateTime, x => x.MapFrom(src => DateTime.Now))
                .ForMember(x => x.NewsType, x => x.MapFrom(src => EnumHelper.Parse<NewsTypeEnum>(src.NewsType)));
            
            AutoMapper.Mapper.CreateMap<Promote, PromoteDto>();
            AutoMapper.Mapper.CreateMap<PromoteDto, Promote>();
           
            AutoMapper.Mapper.CreateMap<RechargeRecord, RechargeRecordDto>().ForMember(x => x.RechargeUser, x => x.MapFrom(src => src.RechargeUser.UserName)); 
            AutoMapper.Mapper.CreateMap<RechargeRecordDto, RechargeRecord>();

            AutoMapper.Mapper.CreateMap<Goods, GoodsDto>();
            AutoMapper.Mapper.CreateMap<GoodsDto, Goods>();

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
