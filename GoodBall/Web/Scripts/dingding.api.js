(function (document, $) {

    $.debug = window.location.hostname == '192.168.3.100';
    /**
     * 基础api生成器
     * @param api 地址
     * @param params 数据
     * @param method
     * @returns {*}
     */
    $.promiseApi = function (api, params, method) {
        var defer = $.Deferred(),
            uri = $.debug ? 'http://192.168.3.100:8070/' : 'http://www.hb6388.com.cn/',
            token = $.debug ? '3eb72Q560BYRrvg9ITkrwyjuHX/FjL7hmTn7e9BuWbZ3J/gB1+e3wPsv3CJHQVlp6VHxJzfpAIDMqGcnDJ4i3EywEa9AUQMfy6kb/RpjBEaHup4E' : '';

        $.ajax({
            url: uri + api,
            //data: $.extend(params, {
            //    access_token: token
            //}),
            data: params,
            type: method ? method : "GET",
            dataType: 'JSON',
            cache: false,
            timeout:10000,
            success: function (r) {
                /*($.debug ? console.log : $.noop)(r);*/
                if (r.status == 200) {
                    defer.resolve(r.data);
                } else {
                    if (r.status == 222) {
                        defer.resolve(r.data);
                        return;
                    }
                    defer.reject(r.msg);
                }
            },
            error: function (e) {
                //$.debug ? console.log(e) : $.noop(e);
                defer.reject('服务器出错');
            }
        });
        return defer.promise();
    };


    $.api = {
        /**
         * 自定义primise
         * @returns {*}
         */
        'promise': function () {
            return $.Deferred();
        },
        /**
         * 获取banner
         * @param city int
         * @returns {*}
         */
        //用户登录 
        'login_action': function (loginInfo) {
            return $.promiseApi('WechatLogin/Login', {
                userName: loginInfo.userName,
                password: loginInfo.pwd,
            },'POST');
        },
        //用户修改 
        'user_action': function (profile) {
            return $.promiseApi('WechatUser/UpdateUser', {
                id:profile.Id,
                nickName: profile.NickName,
                phone: profile.Phone,
                iconUrl: profile.IconUrl,
            }, 'POST');
        },
        //用户修改 
        'update_phone': function (profile) {
            return $.promiseApi('WechatUser/UpdateUserPhone', {
                id: profile.Id,
                phone: profile.Phone,
                code: profile.Code,
            }, 'POST');
        },
        
        //用户退出登录  
        'user_logout': function (userId) {
            return $.promiseApi('WechatUser/Logout', {
            }, 'POST');
        },
        
        //注册
        'reg_action': function (regInfo) {
            return $.promiseApi('WechatReg/Register', {
                phone: regInfo.phone,
                password: regInfo.password,
                code: regInfo.code,

            }, 'POST');
        },
        //获取验证码
        'get_code': function (phone) {
            return $.promiseApi('WechatReg/SendCode', { phone: phone }, 'POST');
        },
        //获取新闻列表 
        'get_newslist': function (newsType) {
            return $.promiseApi('WechatNews/GetList', {
                newsType: newsType,
            });
        },
        //获取新闻详细
        'get_newsdetail': function (id) {
            return $.promiseApi('WechatNews/GetNews', {
                id: id,
            });
        },
        /**
         * 产品列表
         * @param page
         * @param category_id
         * @param filter
         * @returns {*}
         */
        'shop_products': function () {
            return $.promiseApi('WechatShop/GetProductList', {
            });
        },
        //获取用户个人信息
        'profile': function () {
            return $.promiseApi('WechatUser/GetUserInfo', {});
        },
        //获取用户个人信息
        'getUserById': function (userId) {
            return $.promiseApi('WechatUser/GetUserById', { userId: userId });
        },
        //获取推介列表    
        'promote': function (raceType,promoteType) {
            return $.promiseApi('WechatPromote/GetPromoteList', { raceType: raceType, promoteType:promoteType, size: 3, index: 1 });
        },
        //获取推介用户列表 
        'getusers': function (page, index, isAdmin) {
            return $.promiseApi('WechatPromote/GetUserList', { page: page, index: index, isAdmin : isAdmin  });
        },
        //获取用户推介列表 
        'getUserPromotes': function (userId, promoteType) {
            return $.promiseApi('WechatPromote/GetUserPromotes', { userId: userId, promoteType: promoteType });
        },
        //获取用户发布推介列表 
        'getUserPublishPromotes': function (userId, promoteType) {
            return $.promiseApi('WechatPromote/GetUserPublishPromotes', { userId: userId, promoteType: promoteType });
        },
        
        //获取推介列表    
        'promoteAll': function (raceType, promoteType) {
            return $.promiseApi('WechatPromote/GetPromoteList', { raceType: raceType, promoteType: promoteType, size: 10000, index: 1 });
        },
        //购买推介    
        'buyPromote': function (promoteId) {
            return $.promiseApi('WechatPromote/BuyPromote', { promoteId: promoteId });
        },
        //获取充值金额列表
        'getPayAmount': function () {
            return $.promiseApi('WechatPay/GetPayAmounts', { });
        },
        //获取用户订单列表 
        'getUserOrders': function (userId) {
            return $.promiseApi('WechatShop/GetUserOrders', { userId: userId });
        },
       //添加订单
        'addOrder': function (qty,contactor, mobile, postcode, doorplate, goodsid,size) {
            return $.promiseApi('WechatShop/AddOrder', {
                qty: qty,
                contactor: contactor,
                mobile: mobile,
                postcode: postcode,
                doorplate: doorplate,
                goodsid: goodsid,
                size: size
            },'POST');
        },
        //添加竞彩
        'addGame': function (match,content , raceType, result, level, price) {
            return $.promiseApi('WechatPromote/AddGame', {
                matchId: match,
                content: content,
                raceType: raceType,
                result: result,
                level: level,
                price: price
            }, 'POST');
        },
        //微信支付
        'wechatPay': function (id) {
            return $.promiseApi('WechatPay/Pay', {
                payAmountId: id,
            });
        },
        //银联支付
        'unionPay': function (id) {
            return $.promiseApi('WechatPay/UnionPay', {
                payAmountId: id,
            });
        },
        //获取货物尺码列表 
        'getGoodsSize': function (id) {
            return $.promiseApi('WechatShop/GetProductSize', {
                id: id,

            }, 'POST');
        },
       
    };

})(document, jQuery);
