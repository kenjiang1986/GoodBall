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
            AutoMapper.Mapper.CreateMap<User, UserDto>()
                .ForMember(x => x.PromoteList, x => x.MapFrom(src => src.PromoteList));
            AutoMapper.Mapper.CreateMap<UserDto, User>()
                 .ForMember(x => x.CreateTime, x => x.MapFrom(src => DateTime.Now))
                .ForMember(x => x.Password, x => x.MapFrom(src => MD5Helper.MD5Encrypt64(src.Password)));
            AutoMapper.Mapper.CreateMap<User, UserCookieDto>();

            AutoMapper.Mapper.CreateMap<Match, MatchDto>()
                .ForMember(x => x.MatchState, x => x.MapFrom(src => src.MatchState.ToString()))
                 .ForMember(x => x.MatchName, x => x.MapFrom(src => src.TeamA + "VS" + src.TeamB));
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
                 .ForMember(x => x.BuyState, x => x.MapFrom(src => src.UserList.Any(y => y.Id == UserService.GetCurrentUser().Id)))
                 .ForMember(x => x.RaceType, x => x.MapFrom(src => src.RaceType.ToString()))
                  .ForMember(x => x.State, x => x.MapFrom(src => src.State.ToString()))
                  .ForMember(x => x.IsReturn, x => x.MapFrom(src => src.IsReturn ? "是" : "否"))
                  .ForMember(x => x.IsVip, x => x.MapFrom(src => src.IsVip ? "是" : "否"))
                .ForMember(x => x.MatchTime, x => x.MapFrom(src => src.Match.MatchTime))
                .ForMember(x => x.CreateTimeStr, x => x.MapFrom(src => src.CreateTime.ToString()))
                .ForMember(x => x.Operator, x => x.MapFrom(src => src.Operator.NickName))
                .ForMember(x => x.UserCount, x => x.MapFrom(src => src.UserList.Count))
             .ForMember(x => x.MatchName, x => x.MapFrom(src => src.Match.TeamA + "VS" + src.Match.TeamB));
            AutoMapper.Mapper.CreateMap<PromoteDto, Promote>()
                .ForMember(x => x.CreateTime, x => x.MapFrom(src => DateTime.Now))
                .ForMember(x => x.IsVip, x => x.MapFrom(src => src.IsVip == "是"))
                .ForMember(x => x.RaceType, x => x.MapFrom(src => EnumHelper.Parse<RaceTypeEnum>(src.RaceType)))
                .ForMember(x => x.State, x => x.MapFrom(src => EnumHelper.Parse<PromoteStateEnum>(src.State)));
               
            AutoMapper.Mapper.CreateMap<RechargeRecord, RechargeRecordDto>();
            AutoMapper.Mapper.CreateMap<RechargeRecordDto, RechargeRecord>()
                .ForMember(x => x.CreateTime, x => x.MapFrom(src => DateTime.Now));

            AutoMapper.Mapper.CreateMap<Goods, GoodsDto>()
                .ForMember(x => x.SizeList, x => x.MapFrom(src => src.Size.Split(',').ToList()));

            AutoMapper.Mapper.CreateMap<GoodsDto, Goods>().ForMember(x => x.CreateTime, x => x.MapFrom(src => DateTime.Now));

            AutoMapper.Mapper.CreateMap<Order, OrderDto>()
                 .ForMember(x => x.GoodsName, x => x.MapFrom(src => src.Goods.GoodsName))
                .ForMember(x => x.GoodsImage, x => x.MapFrom(src => src.Goods.GoodsImage));
            AutoMapper.Mapper.CreateMap<OrderDto, Order>()
                .ForMember(x => x.OrderNo, x => x.MapFrom(src => OrderHelper.GetOrderNo()))
                .ForMember(x => x.CreateTime, x => x.MapFrom(src => DateTime.Now));

            AutoMapper.Mapper.CreateMap<Customer, CustomerDto>()
                .ForMember(x => x.AnswerTime, x => x.MapFrom(src => src.AnswerTime));
            AutoMapper.Mapper.CreateMap<CustomerDto, Customer>().ForMember(x => x.CreateTime, x => x.MapFrom(src => DateTime.Now));

            AutoMapper.Mapper.CreateMap<PayAmount, PayAmountDto>();
            AutoMapper.Mapper.CreateMap<PayAmountDto, PayAmount>()
                 .ForMember(x => x.CalType, x => x.MapFrom(src => EnumHelper.Parse<CalTypeEnum>(src.CalType)))
                .ForMember(x => x.CreateTime, x => x.MapFrom(src => DateTime.Now));

            AutoMapper.Mapper.CreateMap<ReturnRule, ReturnRuleDto>().ForMember(x => x.Numerical, x => x.MapFrom(src => src.Numerical * 100));

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
