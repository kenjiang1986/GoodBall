(function (document, $) {

    $.debug = window.location.hostname == '127.0.0.1';
    /**
     * 基础api生成器
     * @param api 地址
     * @param params 数据
     * @param method
     * @returns {*}
     */
    $.promiseApi = function (api, params, method) {
        var defer = $.Deferred(),
            uri = $.debug ? 'http://127.0.0.1/' : 'http://m.ddjiadian.com/',
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
        //获取推介列表    
        'promote': function (raceType) {
            return $.promiseApi('WechatPromote/GetPromoteList', { raceType: raceType, size:3, index:1 });
        },
        //获取推介用户列表 
        'getusers': function () {
            return $.promiseApi('WechatPromote/GetUserList', {});
        },
        //获取推介用户列表 
        'getUserPromotes': function (userId) {
            return $.promiseApi('WechatPromote/GetUser', { userId: userId });
        },
        //获取推介列表    
        'promoteAll': function (raceType) {
            return $.promiseApi('WechatPromote/GetPromoteList', { raceType: raceType, size: 10000, index: 1 });
        },
        //购买推介    
        'buyPromote': function (promoteId) {
            return $.promiseApi('WechatPromote/BuyPromote', { promoteId: promoteId });
        },
        //获取充值金额列表
        'getPayAmount': function () {
            return $.promiseApi('WechatPay/GetPayAmounts', { });
        },
        

       
    };

})(document, jQuery);
