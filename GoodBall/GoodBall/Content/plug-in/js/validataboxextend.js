/** 
* jQuery EasyUI 1.4.2 --- 功能扩展 
*  
* Copyright (c) 2009-2015 RCM 
* 
* 新增 validatebox 校验规则 author by 徐铮
* 
*/
$.extend($.fn.validatebox.defaults.rules, {
    minLength: { // 判断最小长度
        validator: function (value, param) {
            return value.length >= param[0];
        },
        message: '最少输入 {0} 个字符。'
    },
    maxLength: { // 判断最大长度
        validator: function (value, param) {
            return value.length <= param[0];
        },
        message: '最多输入 {0} 个字符。'
    },
    personname: {//人员姓名
        validator: function (value) {
            return /^[^*@$#~%&*]+$/.test(value);
        },
        message: '请正确输入人员姓名'
    },
    length: {//判断字符长度区间
        validator: function (value, param) {
            var len = $.trim(value).length;
            return len >= param[0] && len <= param[1];
        },
        message: "内容长度介于{0}和{1}之间."
    },
    phone: {//验证有效的手机号码。
        validator: function (value) {
            return /(13[0-9]|14[0-9]|15[0-9]|18[0-9]|16[0-9])\d{8}$/i.test(value);
        },
        message: '请输入正确手机号码'
    },
    Fixedtelephone: {//固定电话号码
        validator: function (value) {
            return /^\d{7,11}|\(?\d{3,4}[) -]?\d{7,8}$/i.test(value);
        },
        message: '请输入正确电话号码'
    },
    idcard: {// 验证身份证
        validator: function (value) {
            return /^\d{15}(\d{2}[A-Za-z0-9])?$/i.test(value);
        },
        message: '请输入正确身份证号码'
    },
    floatOrInt: {// 验证是否为小数或整数
        validator: function (value) {
            return /^(\d{1,3}(,\d\d\d)*(\.\d{1,3}(,\d\d\d)*)?|\d+(\.\d+))?$/i.test(value);
        },
        message: '请输入合理数字'
    },
    qqnumber: {// 验证QQ,6到13位数字
        validator: function (value) {
            return /^[1-9]\d{5,12}$/i.test(value);
        },
        message: '请输入正确的QQ号码'
    },
    integer: {// 验证整数
        validator: function (value) {
            return /^\d*$/i.test(value);
        },
        message: '请输入整数'
    },
    chinese: {// 验证行政区
        validator: function (value) {
            return /^[0-9\u4e00-\u9faf]+$/i.test(value);
        },
        message: '请输入正确的行政区'
    },
    communityname: {//小区名称
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确小区名称'
    },
    address: {//验证小区地址信息不包含特殊字符和空格
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确的地址(不能含特殊字符和空格)'
    },
    area: {//建筑面积，保留至多两位小数，数字
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确建筑面积(至多两位小数)'
    },
    eartharea: {//土地面积，保留至多两位小数，数字
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确土地面积(至多两位小数)'
    },
    currentflonum: {//所在楼层
        validator: function (value) {
            return /^\d{1,3}|(-)?\d{1,2}$/.test(value);
        },
        message: '请输入正确楼数(至多三位数)'
    },
    totalflonum: {//总楼层数
        validator: function (value) {
            return /^\d{1,3}$/.test(value);
        },
        message: '请输入正确楼数(至多三位数)'
    },
    buildyear: {//建成年代
        validator: function (value) {
            return /^\d{1,4}/.test(value);
        },
        message: '请输入正确建成年代'
    },
    attetioninfo: {//备注信息
        validator: function (value) {
            return /^[^@*$#~&*]+$/.test(value);
        },
        message: '请输入正确备注(不能含特殊字符)'
    },
    auditopinion: {//审核意见
        validator: function (value) {
            return /^[^@*$#~&*]+$/.test(value);
        },
        message: '请输入正确审核意见(不能含特殊字符)'
    },
    feedbackinfo: {//其他反馈信息
        validator: function (value) {
            return /^[^@*$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确反馈信息(不能含特殊字符)'
    },
    changeprice: {//修正单价
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确修正单价'
    },
    price: {//询价单价
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确单价'
    },
    totalprice: {//询价总价
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确总价'
    },
    huizongtotalprice: {//汇总询价总价
        validator: function (value) {
            return /^\d+(?:\.\d{1,4})?$/.test(value);
        },
        message: '请输入正确总价'
    },
    priceup: {//询价单价上限
        validator: function (value, param) {
            return /^\d+(?:\.\d{1,2})?$/.test(value) && value.ToFloat() >= $(param[0]).val().ToFloat();
        },
        message: '请输入正确的单价上限'
    },
    minute: {//回价时间
        validator: function (value) {
            return /^\d{1,3}$/.test(value);
        },
        message: '请输入正确的回价时间'
    },
    minuteup: {//回价时间上限
        validator: function (value, param) {
            return /^\d{1,3}$/.test(value) && value > $(param[0]).val();
        },
        message: '请输入正确的回价时间'
    },
    totalpriceup: {//询价总价上限
        validator: function (value, param) {
            return /^\d+(?:\.\d{1,2})?$/.test(value) && value.ToFloat() >= $(param[0]).val().ToFloat();
        },
        message: '请输入正确的总价上限'
    },
    principalvaluation: {//估价委托方
        validator: function (value) {
            return /^[^@$#=+!:"?%&*\s]+$/.test(value);
        },
        message: '请输入正确估价委托方'
    },
    assignee: {//受理人
        validator: function (value) {
            return /^[^*@$#+=!-`;':"?/\~%&*\s]+$/.test(value);
        },
        message: '请输入正确受理人'
    },
    serialnumber: {//流水号
        validator: function (value) {
            return /^\d{0,12}$/.test(value);
        },
        message: '请输入正确流水号'
    },
    reportnumber: {//报告号
        validator: function (value) {
            return /^\d{0,15}$/.test(value);
        },
        message: '请正确输入报告号'
    },
    reportwriter: {//报告撰写人
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确报告撰写人'
    },
    inquiryagency: {//询价机构
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确询价机构'
    },
    customer: {//询价人（客户名称）
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确询价人(客户名称)'
    },
    clientname: {//客户姓名
        validator: function (value) {
            return /^[^*@$#+=!-;':"?/\~%&*<>\s]+$/.test(value) && value.length <= 20;
        },
        message: '请输入正确客户姓名'
    },
    creditagency: {//贷款机构
        validator: function (value) {
            return /^[^*@$#+=!-;':"?/\~%&*<>\s]+$/.test(value);
        },
        message: '请输入正确贷款机构'
    },
    creditbank: {//贷款支行
        validator: function (value) {
            return /^[^*@$#+=!-;':"?/\~%&*<>\s]+$/.test(value);
        },
        message: '请输入正确贷款支行'
    },
    receivable: {//应收金额
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确金额'
    },
    receptman: {//接件人
        validator: function (value) {
            return /^[^*@$#+=!-;':"?/\~%&*<>\s]+$/.test(value);
        },
        message: '请输入正确接件人'
    },
    advancefee: {//预收费用
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确预收费用'
    },
    creditinfo: {//期贷信息
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确期贷信息'
    },
    invoicetitle: {//(普票)发票抬头
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确(普票)发票抬头'
    },
    invoicebank: {//增值税(专票)开户行
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确(专票)开户行'
    },
    invoicename: {//增值税(专票)名称
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确(专票)名称'
    },
    invoiceumber: {//增值税(专票)账号
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确(专票)账号'
    },
    invoiceaddress: {//增值税(专票)地址
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确(专票)地址'
    },
    invoicetaxpayernum: {//(专票)纳税人识别号
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确(专票)纳税人识别号'
    },
    invoicephone: {//增值税(专票)电话
        validator: function (value) {
            return /^\d{7,11}|\(?\d{3,4}[) -]?\d{7,8}$/i.test(value);
        },
        message: '请输入正确(专票)电话'
    },
    linkperson: {//联系人
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确联系人'
    },
    //汇总数据修改
    ownedcompany: {//所属公司
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确公司名称'
    },
    ownedapart: {//所属部门
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确所属部门'
    },
    reportyears: {//报告年度
        validator: function (value) {
            return /^(19|20|21)\d{2}/.test(value);
        },
        message: '请输入正确报告年度'
    },
    reportmouths: {//报告月度
        validator: function (value) {
            return /^\d{1,4}/.test(value);
        },
        message: '请输入正确报告月度'
    },
    reportallname: {//报告编号
        validator: function (value) {
            return /^[^*@$#%&]+$/.test(value);
        },
        message: '请输入正确报告编号'
    },
    reportpurpose: {//估价目的
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确估价目的(不含特殊字符)'
    },
    purposedisc: {//目的描述
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确目的描述(不含特殊字符)'
    },
    appraisalprincipal: {//估价委托人
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确估价委托人'
    },
    reportuser: {//报告使用方
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确报告使用方'
    },
    subbank: {//支行
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确支行'
    },
    province: {//项目所在省
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s\d]+$/.test(value);
        },
        message: '请输入正确项目所在省'
    },
    city: {//项目所在市
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s\d]+$/.test(value);
        },
        message: '请输入正确项目所在市'
    },
    county: {//项目区县
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确项目区县'
    },
    explorationstaff: {//现场勘查员
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确现场勘查员'
    },
    reportallwritter: {//报告撰写人员
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确报告撰写人员'
    },
    appraisers: {//估价师
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确估价师'
    },
    appraisemethod: {//估价方法
        validator: function (value) {
            return /^[^*@$#~&*\s]+$/.test(value);
        },
        message: '请输入正确估价方法'
    },
    pricetype: {//单价类型
        validator: function (value) {
            return /^[^*@$#~&*\s]+$/.test(value);
        },
        message: '请输入正确单价类型'
    },
    senderman: {//请输入汇款人
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确汇款人'
    },

    projectmanager: {//项目负责人
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '请输入正确负责人'
    },
    projectname: {//项目名称
        validator: function (value) {
            return /^[^*@$#~%&*\s]+$/.test(value);
        },
        message: '请输入正确项目名称'
    },
    buildname: {//楼栋号，单元号，户号
        validator: function (value) {
            return /^[^@$#~%&*\s]+$/.test(value);
        },
        message: '请输入正确数据格式(不含特殊字符)'
    },
    type: {//各种类型
        validator: function (value) {
            return /^[^@$#~%&*\s]+$/.test(value);
        },
        message: '请输入正确类型(不含特殊字符)'
    },
    tax: {//各种税额
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确税额'
    },
    summoney: {//各种款项
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确款额'
    },
    firstaccpet: {//优先受偿款
        validator: function (value) {
            return /^[^@#~%&\s]+$/.test(value);
        },
        message: '请输入正确优先受偿款'
    },
    othercause: {
        validator: function (value) {
            return /^[^*@$#+=~%&\s]+$/.test(value);
        },
        message: '请输入正确数据格式(不含特殊字符)'
    },
    projectpopularizename: {//项目推广名
        validator: function (value) {
            return /^[^*@$#~%&\s]+$/.test(value);
        },
        message: '请输入正确项目推广名'
    },
    housetype: {//户型
        validator: function (value) {
            return /^[^*@$#~%&\s]+$/.test(value);
        },
        message: '请输入正确户型,至多10个字符'
    },
    houseowned: {//房屋所有人
        validator: function (value) {
            return /^[^*@$#~%&\s]+$/.test(value);
        },
        message: '请输入正确房屋所有人'
    },
    houseownedright: {//房屋所有权
        validator: function (value) {
            return /^[^*@$#~%&\s]+$/.test(value);
        },
        message: '请输入正确房屋所有权'
    },
    houseowneddiff: {//产别
        validator: function (value) {
            return /^[^@$#~%&*\s]+$/.test(value);
        },
        message: '请输入正确产别'
    },
    decoration: {//装修程度
        validator: function (value) {
            return /^[^@$#~%&*\s]+$/.test(value);
        },
        message: '请输入正确装修程度'
    },
    lastuseryear: {//剩余经济寿命
        validator: function (value) {
            return /^[^@$#+=~%&*\s]+$/.test(value);
        },
        message: '请输入正确剩余经济寿命'
    },
    ownedearthnum: {//国有土地使用号
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确国有土地使用号'
    },
    ownedearthloc: {//土地坐落
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确土地坐落'
    },
    english: {// 验证英语
        validator: function (value) {
            return /^[A-Za-z]+$/i.test(value);
        },
        message: '请输入英文'
    },
    unnormal: {// 验证是否包含空格和非法字符
        validator: function (value) {
            return /.+/i.test(value);
        },
        message: '输入值不能为空和包含其他非法字符'
    },
    faxno: {// 验证传真
        validator: function (value) {
            return /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/i.test(value);
        },
        message: '传真号码不正确'
    },
    zip: {// 验证邮政编码
        validator: function (value) {
            return /^[1-9]\d{5}$/i.test(value);
        },
        message: '请输入正确邮政编码'
    },
    ip: {// 验证IP地址
        validator: function (value) {
            return /d+.d+.d+.d+/i.test(value);
        },
        message: 'IP地址格式不正确'
    },
    name: {// 验证姓名，可以是中文或英文
        validator: function (value) {
            return /^[\u0391-\uFFE5]+$/i.test(value) | /^\w+[\w\s]+\w+$/i.test(value);
        },
        message: '请输入姓名（不能含特殊字符）'
    },
    expressdenum: {//快递单号
        validator: function (value) {
            return /^[^*@$#~%&\s]+$/.test(value);
        },
        message: '请输入正确快递单号'
    },
    email: {
        validator: function (value) {
            return /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(value);
        },
        message: '请输入有效的电子邮件账号(例：abc@126.com)'
    },
    department: {
        validator: function (value) {
            return /^[0-9]*$/.test(value);
        },
        message: '请输入部门排序号(例：1)'
    },
    datatype: {
        validator: function (value) {
            return /^(\d{4})-(\d{2})-(\d{2})$/.text(value);
        },
        message: '请输入正确时间格式'
    },
    dateValid: {
        validator: function (value, param) { //参数value为当前文本框的值，也就是endDate  
            startTime = $(param[0]).datetimebox('getValue');//获取起始时间的值  
            var start = $.fn.datebox.defaults.parser(startTime);
            var end = $.fn.datebox.defaults.parser(value);
            varify = end >= start;
            return varify;
        },
        message: '结束时间要晚于开始时间!'
    },
    dateValidday: {
        validator: function (value, param) { //参数value为当前文本框的值，也就是endDate  
            start = $(param[0]).datetimebox('getValue');//获取起始时间的值  
            var startday = $.fn.datebox.defaults.parser(start);
            var endday = $.fn.datebox.defaults.parser(value);
            varifyday = endday >= startday;
            return varifyday;
        },
        message: '结束日期要晚于起始日期!'
    },
    floor: {
        validator: function (value, param) { //参数value为当前文本框的值，也就是当前楼层
            var _currentflor = $(param[0]).val();
            var varify = parseInt(value) >= parseInt(_currentflor);
            return varify;
        },
        message: '总楼层要大于等于所在楼层'
    },
    totalfloor: {
        validator: function (value, param) { //参数value为当前文本框的值，也就是当前楼层
            var _currentflor = $(param[0]).val();
            var varify = parseInt(value) <= parseInt(_currentflor);
            return varify;
        },
        message: '所在楼层要小于等于所在总楼层'
    },
    billName: {//票据号
        validator: function (value) {
            return /^[^*@$#%&]+$/.test(value);
        },
        message: '请输入正确票据号'
    },
    theNumber: {//现单号
        validator: function (value) {
            return /^[^*@$#%&]+$/.test(value);
        },
        message: '请输入正确现单号'
    },
    refundPerson: {//退款领取人
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/.test(value);
        },
        message: '退款领取人'
    },
    affilia: {//所属机构
        validator: function (value) {
            return (/^[^*@$#%&]+$/.test(value) && !(/^\d+$/.test(value))) && value.length <= 20;
        },
        message: '请输入正确所属机构'
    },
    branch: {//分支机构
        validator: function (value) {
            return (/^[^*@$#%&]+$/.test(value) && !(/^\d+$/.test(value))) && value.length <= 20;
        },
        message: '请输入正确分支机构'
    },
    companyCode: {// 公司编码
        validator: function (value) {
            return /^[0-9A-Za-z]+$/i.test(value);
        },
        message: '请输入正确公司编码'
    },
    companyName: {// 公司名称
        validator: function (value) {
            return /^[^*@$#%&]+$/i.test(value);
        },
        message: '请输入正确公司名称'
    },
    numberName: {// 帐号名称
        validator: function (value) {
            return /^[^*@$#%&]+$/.test(value);
        },
        message: '请输入正确帐号名称,最多50字符'
    },
    areasName: {// 区域位置名称
        validator: function (value) {
            return /^[^*@$#%&]+$/.test(value);
        },
        message: '请输入正确名称,最多50字符'
    },
    fileName: {// 归档盒号'
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/i.test(value);
        },
        message: '请输入正确归档盒号'
    },
    conditionGround: {//地上物状况
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/i.test(value);
        },
        message: '请输入正确地上物状况'
    },
    priorityInfor: {//优先受偿信息
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/i.test(value);
        },
        message: '请输入正确优先受偿信息'
    },
    landOwner: {//土地使用权人
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/i.test(value);
        },
        message: '请输入正确土地使用权人'
    },
    landUse: {//土地用途
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/i.test(value);
        },
        message: '请输入正确土地用途'
    },
    landArea: {//土地使用权面积 
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确土地使用权面积'
    },
    figureNumber: {//图号 
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/i.test(value);
        },
        message: '请输入正确图号'
    },
    landNumber: {//地号 
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/i.test(value);
        },
        message: '请输入正确地号'
    },
    surplusPeriod: {//剩余土地年限 
        validator: function (value) {
            return /^\d{1,4}$/i.test(value);
        },
        message: '请输入正确剩余土地年限'
    },
    transferor: {//出让人
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/i.test(value);
        },
        message: '请输入正确出让人'
    },
    assigne: {//受让人
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/i.test(value);
        },
        message: '请输入正确受让人'
    },
    parcelarea: {//宗地面积
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确宗地面积'
    },
    transferorarea: {//出让面积
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确出让面积'
    },
    transferorprice: {//出让地价款
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确出让地价款'
    },
    governmentalprofits: {//政府土地收益
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确政府土地收益'
    },
    landcompensation: {//土地发开补偿
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确土地发开补偿'
    },
    planplotratio: {//规划容积率
        validator: function (value) {
            if (parseFloat(value) > 0 && parseFloat(value) < 10) {
                return /^\d+(?:\.\d{1,2})?$/.test(value);
            }
            return false;
        },
        message: '请输入正确规划容积率(保留两位小数)'
    },
    plantotalbuiltarea: {//规划总建筑面积
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确规划总建筑面积'
    },
    planlandtopbuiltArea: {//规划地上建筑面积
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确规划地上建筑面积'
    },
    planlanddownbuiltarea: {//规划地下建筑面积
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确规划地下建筑面积'
    },
    sellcontractnumber: {//出让合同编号
        validator: function (value) {
            return /^[^*@$#+=!~%&*\s]+$/i.test(value);
        },
        message: '请输入正确出让合同编号'
    },
    parcellocated: {//宗地坐落
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确宗地坐落'
    },
    BuildLandPlanNo: {//建设用地规划许可证号
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确建设用地规划许可证号'
    },
    LandUnits: {//用地单位
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入用地单位'
    },
    LandUseProjectName: {//用地项目名称
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确用用地项目名称'
    },
    LandUsePosition: {//用地位置
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确用地位置'
    },
    LandUseNature: {//用地性质
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确用地性质'
    },
    LandUseAreaNew: {//用地面积
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确用地面积'
    },
    BuildEngineeringPlanNo: {//建设工程规划许可证号
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确建设工程规划许可证号'
    },
    BuildScale: {//建设单位
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确建设单位'
    },
    BuildProjectName: {//建设项目名称
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入建设项目名称'
    },
    BuildPosition: {//建设位置
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确建设位置'
    },
    ProjectNature: {//项目性质
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确项目性质'
    },
    TotalBuildingArea: {//总建筑面积
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确总建筑面积'
    },
    BuildEngineeringConNo: {//建设工程施工许可证号
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确建设工程施工许可证号'
    },
    ConUnit: {//施工单位
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确施工单位'
    },
    SupervisionUnits: {//监理单位
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确监理单位'
    },
    ProjectPopularizeName: {//项目推广名
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确项目推广名'
    },
    RegisterUse: {//登记用途
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确登记用途'
    },
    PresentUse: {//现状用途
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确现状用途'
    },
    BuildingStructure: {//建筑结构
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确建筑结构'
    },
    HouseOwnershipNo: {// 房屋所有权号  
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确房屋所有权号'
    },
    HouseOwnershipPeople: {// 房屋所有人  
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确房屋所有人'
    },
    SpareEconomicsYear: {//剩余经济寿命
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确剩余经济寿命'
    },
    DealTotal: {//成交总价
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确成交总价'
    },
    DeaPrice: {//成交单价
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确成交单价'
    },
    HandleTaxQuota: {//处置扣税额
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确处置扣税额'
    },
    TaxAfterNet: {//扣税口净值
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确扣税口净值'
    },
    LandVatQuota: {//土地增值税
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确土地增值税'
    },
    TaxAfterVatQuota: {//扣除土地税
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确扣除土地税'
    },
    OtherInfluenceFactor: {//其它影响因素
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确其它影响因素'
    },
    PayLeasing: {//补交出让金/综合地价款
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value);
        },
        message: '请输入正确补交出让金/综合地价款'
    },
    copies: {//份数
        validator: function (value) {
            return /^[1-9]+\d*$/.test(value);
        },
        message: '请输入正确份数 '
    },
    rightnumber: {//数字
        validator: function (value) {
            return /^[0-9]+\d*$/.test(value);
        },
        message: '请输入正确数字'
    },
    ErrorDescription: {//其它影响因素
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确错误描述'
    },
    rightmessage: {//内容
        validator: function (value) {
            return /^[^@$#~&\s]+$/.test(value);
        },
        message: '请输入正确内容（不含特殊字符和空格）'
    },
    selectValueRequired: {//下拉菜单控制select
        validator: function (value, param) {
            if (value == "" || value.indexOf('请选择') >= 0) {
                return false;
            } else {
                return true;
            }
        },
        message: '该下拉框为必选项'
    },
    chineseName: {// 验证中文
        validator: function (value) {
            return /^[\u4e00-\u9faf]+$/i.test(value);
        },
        message: '请输入中文'
    },
    /*
    amountContrast: {//免收金额不能大于应收金额
        validator: function (value, param) { //参数value为当前文本框的值，也就是当前楼层
            var _currentval = $(param[0]).val();
            var varify = parseInt(value) > parseInt(_currentval);
            return varify;
        },
        message: '免收金额不能大于应收金额'
    }*/
    evalTotalValidate: {//评估总价
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value) && value.ToFloat() >0;
        },
        message: '请输入正确评估总价（必须大于0）'
    },
    evalPriceValidate: {//评估单价
        validator: function (value) {
            return /^\d+(?:\.\d{1,2})?$/.test(value) && value.ToFloat() >0;
        },
        message: '请输入正确评估单价（必须大于0）'
    }
});