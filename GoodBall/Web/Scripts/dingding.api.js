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
            data: $.extend(params, {
                access_token: token
            }),
            type: method ? method : "GET",
            dataType: 'JSON',
            cache: false,
            timeout:10000,
            success: function (r) {
                /*($.debug ? console.log : $.noop)(r);*/
                if (r.status == 200) {
                    defer.resolve(r.data);
                } else {
                    if (r.status == 10086) {
                        //授权登录
                        window.location.href = "/gzh/index.php?origin_url=" + encodeURIComponent(window.location.href);
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
        'login_action': function (city) {
            return $.promiseApi('get_weixin_banner', {
                city: city
            });
        },
        //注册
        'reg_action': function (regInfo) {
            return $.promiseApi('WechatReg/Register', {
                dto: regInfo
            });
        },
        //获取验证码
        'get_code': function (phone) {
            return $.promiseApi('WechatReg/SendCode', { phone: phone }, 'POST');
        },
        /**
         * 获取服务产品
         * @param sid int 1 保养， 2 维修， 3 套餐， 4 安装 5 空调
         * @param city int 默认 1
         * @returns {*}
         */
        'get_goods_info': function (sid, city) {
            return $.promiseApi('get_goods_info', {
                sid: sid,
                city: city
            });
        },
        /**
         * 添加到购物车
         * @param goods json字符串 [{goods_id:"goods_id",gps[{gp_id:"",vp_id:2}],num:1}]
         * @returns {*}
         */
        'add_cart': function (goods) {
            return $.promiseApi('add_cart', {
                goods: goods
            }, 'POST');
        },
        /**
         * 购物车使用优惠券
         * @param coupon_id 优惠券id
         * @returns {*}
         */
        'checkout': function (coupon_id) {
            return $.promiseApi('checkout', {
                coupon_id: coupon_id
            });
        },
        /**
         * 获取城市的区
         * @param city 城市id
         * @returns {*}
         */
        'get_city_dists': function (city) {
            return $.promiseApi('get_city_dists', {
                city: city
            });
        },
        /**
         * 添加地址
         * @param contactor 联系人
         * @param mobile 电话
         * @param city 城市ID
         * @param dist 区ID
         * @param detail 详细地址
         * @param doorplate 门牌号
         * @returns {*}
         */
        'add_addr': function (contactor, mobile, city, dist, detail, doorplate) {
            return $.promiseApi('add_addr', {
                contactor: contactor,
                mobile: mobile,
                city: city,
                dist: dist,
                detail: detail,
                doorplate: doorplate
            });
        },
        /**
         * 更新地址
         * @param idx 地址idx
         * @param contactor 联系人
         * @param mobile 电话
         * @param city 城市ID
         * @param dist 区ID
         * @param detail 详细地址
         * @param doorplate 门牌号
         * @returns {*}
         */
        'update_addr': function (idx, contactor, mobile, city, dist, detail, doorplate) {
            return $.promiseApi('update_addr', {
                idx: idx,
                contactor: contactor,
                mobile: mobile,
                city: city,
                dist: dist,
                detail: detail,
                doorplate: doorplate
            });
        },
        /**
         * 获取单个地址
         * @param idx 地址ID
         * @returns {*}
         */
        'get_add_by_idx': function (idx) {
            return $.promiseApi('get_add_by_idx', {
                idx: idx
            });
        },
        /**
         * 删除单个地址
         * @param idx 地址ID
         * @returns {*}
         */
        'del_addr': function (idx) {
            return $.promiseApi('del_addr', {
                idx: idx
            });
        },
        /**
         * 获取用户地址
         * @returns {*}
         */
        'get_user_addrs': function () {
            return $.promiseApi('get_user_addrs', {});
        },
        /**
         * 获取可用时间库存相关
         * @param sid 服务类型
         * @param city 城市ID
         * @param dist 区ID
         * @returns {*}
         */
        'get_avail_daytime': function (sid, city, dist) {
            return $.promiseApi('get_avail_daytime', {
                sid: sid,
                city: city,
                dist: dist
            });
        },
        /**
         * 生成订单
         * @param address_idx 地址索引
         * @param book_day  2016-09-09
         * @param book_period 8:00-10:00
         * @param coupon_id 优惠券ID
         * @returns {*}
         */
        'gen_order': function (address_idx, book_day, book_period, coupon_id) {
            return $.promiseApi('gen_order', {
                address_idx: address_idx,
                book_day: book_day,
                book_period: book_period,
                coupon_id: coupon_id
            });
        },
        /**
         * 订单详情
         * @param oid 订单ID
         * @returns {*}
         */
        'order_detail': function (oid) {
            return $.promiseApi('order_detail', {
                'oid': oid
            });
        },
        /**
         * 订单详情
         * @param status 订单状态
         * @returns {*}
         */
        'orders': function (status) {
            return $.promiseApi('get_my_orders', {
                'status': status
            });
        },
        /**
         * 商城首页
         * @returns {*}
         */
        'shop_categories': function () {
            return $.promiseApi('shop/categories');
        },
        'shop_orders': function (status) {
            return $.promiseApi('shop/orders', {
                'status': status
            });
        },
        /**
         * 订单详情
         * @param oid 订单ID
         * @returns {*}
         */
        'shop_order_detail': function (oid) {
            return $.promiseApi('shop/order_detail', {
                'oid': oid
            });
        },
        /**
         * 产品列表
         * @param page
         * @param category_id
         * @param filter
         * @returns {*}
         */
        'shop_products': function (page, category_id, filter) {
            return $.promiseApi('shop/products', {
                page: page,
                category_id: category_id,
                filter: filter
            });
        },
        /**
         * 产品明细
         * @param goods_id
         * @returns {*}
         */
        'shop_product': function (goods_id) {
            return $.promiseApi('shop/product', {
                goods_id: goods_id
            });
        },
        /**
         * 商城加入购物车
         * @param goods
         * @returns {*}
         */
        'shop_add_cart': function (goods_id, num) {
            return $.promiseApi('shop/add_cart', {
                goods_id: goods_id,
                num: num
            });
        },
        /**
         * 商城提交订单
         * @param address_idx
         * @returns {*}
         */
        'shop_place_order': function (address_idx) {
            return $.promiseApi('shop/place_order', {
                address_idx: address_idx
            });
        },
        /**
         * 显示购物车
         * @returns {*}
         */
        'shop_cart': function () {
            return $.promiseApi('shop/checkout');
        },
        /**
         * 订单详情
         * @param status 订单状态
         * @returns {*}
         */
        'cancel_order': function (oid) {
            return $.promiseApi('cancel_order', {
                'order_id': oid
            });
        },
        'shop_cancel_order': function (oid) {
            return $.promiseApi('shop/cancel_order', {
                'oid': oid
            });
        },
        'wx_pay': function (oid) {
            return $.promiseApi('payment/wechat/pay', {
                'order_ids': oid
            });
        },
        'profile': function () {
            return $.promiseApi('get_my_account', {});
        },
        'comment': function (order_id,sid, star, comment) {
            var url = sid =='5'?  'shop/comment':'add_order_comment';
            return $.promiseApi(url, {
                order_id: order_id,
                star: star,
                comment: comment
            }, 'POST');
        },
        'comments': function (goods_id, page) {
            return $.promiseApi('shop/comments', {
                goods_id: goods_id,
                page: page
            });
        },
        /**
         * 优惠券列表
         * @param sid
         */
        'get_coupon': function (sid) {
            return $.promiseApi('get_user_coupons', {
                sid: sid
            });
        },

    };

})(document, jQuery);
