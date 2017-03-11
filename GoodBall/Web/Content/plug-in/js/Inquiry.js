function SetLocation(x, y) {
    map.clearOverlays();
    var new_point = new BMap.Point(x, y);
    var marker = new BMap.Marker(new_point);  // 创建标注
    map.addOverlay(marker);              // 将标注添加到地图中
    map.panTo(new_point);
}

//禁用页面所有按钮  input select textare（查看用）
function DisableAll() {
    $("input[type='text'],select").each(function () {
        $(this).attr('disabled', true);
        $(this).css("background", "#ebebe5");
    });
    $(".easyui-combobox").each(function () {
        $(this).combobox({ disabled: true });
    });
    $("#BtnSearch,#BtnSave,#BtnLixiang,#tabs,#SearchCustomer,#AddCustomer").each(function () {
        $(this).css("display", "none");
    });
}

//取消下列combobox回车事件
function UnBindEnterCombox() {
    var diableEnter = ["HouseNum", "PropertyType"];
    //write by 徐铮
    //array.forEach()不支持ie8以下的版本
    //Array.forEach implementation for IE support..  
    if (!Array.prototype.forEach) {
        Array.prototype.forEach = function (callback, thisArg) {
            var T, k;
            if (this == null) {
                throw new TypeError(" this is null or not defined");
            }
            var O = Object(this);
            var len = O.length >>> 0; // Hack to convert O.length to a UInt32  
            if ({}.toString.call(callback) != "[object Function]") {
                throw new TypeError(callback + " is not a function");
            }
            if (thisArg) {
                T = thisArg;
            }
            k = 0;
            while (k < len) {
                var kValue;
                if (k in O) {
                    kValue = O[k];
                    callback.call(T, kValue, k, O);
                }
                k++;
            }
        };
    }
    diableEnter.forEach(function (value) {
        var input = $("#" + value).parent().find("input").first();
        input.unbind("keydown");
        input.unbind("keypress");
        input.unbind("keyup");
        input.keyup(AddressChang);
    });
}
//询价总值
var _totalPrice = 0;
var _isLoadPrice = false;
function CountPrice(opeation, isbit) {
    if (_isLoadPrice) {
        _isLoadPrice = false;
        return;
    }
    _isLoadPrice = true;
    var area = $.trim($("#BuildingArea").val());
    if (area == "")
        return;
    if (isNaN(area))
        return;
    area = parseFloat(area);
    var totalPrice = $.trim($("#InquiryResult").val());
    var singlePrice = $.trim($('#InquiryPrice').numberbox('getValue'));
    if (opeation == 'total') {
        if (singlePrice == "")
            return;
        if (isNaN(singlePrice))
            return;
        singlePrice = parseFloat(singlePrice);
        var bit = $("#BitNumber").val().ToInt();
        var result = ((singlePrice * area) / 10000).toFixed(bit);
        _totalPrice = result;
        $("#InquiryResult").textbox("setValue", result);
    }
    if (opeation == 'single') {
        if (totalPrice == "")
            return;
        if (isNaN(totalPrice))
            return;
        totalPrice = parseFloat(totalPrice);
        var result = (totalPrice * 10000 / area).toString().ToInt();
        $('#InquiryPrice').numberbox('setValue', result);
        if (_totalPrice != totalPrice) {
            $('#InquiryPrice').numberbox('setValue', result);
        }
    }
}

//根据面积匹配居室
function GetHouseType(event) {
    if (event.keyCode != "13")
        return;
    if (isNaN(this.value)) {
        alert("面积只能为数字");
        return;
    }
    var address = $("#ResidentialAreaName").val();
    if (address == "") {
        alert("请填写小区名称");
        return;
    }
    loadTips.showLoading();
    var area = parseInt(this.value);
    $.ajax({
        type: "post",
        url: "/House/GetHouseTypeByArea",
        data: { area: area, address: address },
        success: function (data) {
            if (data.length > 0)
                $("#HouseType").combobox('select', data[0].Key);
            loadTips.hideLoadind();
        },
        error: function (error) {
            loadTips.hideLoadind();
        }
    });
}

//根据小区名获取楼栋信息&&根据小区名获取特殊因素
function GetLoudong(key) {
    if (key == null || key == "")
        return;
    loadTips.showLoading();
    try {
        $("#HouseNum").combobox('clear');
    }
    catch (e) {
        $("#HouseNum").val("");
    }
    $("#UnitName").combobox('clear');
    $.ajax({
        type: "post",
        data: { address: key, cityname: $("#CityName").combobox('getText') },
        url: "/House/GetLoudong",
        success: function (data) {
            $('#BuildingName').combobox({
                data: data,
                valueField: 'Key',
                textField: 'Value',
                onChange: GetDanyuanById,
                filter: function (matchstr, row) {
                    var ismatch = false;
                    var opts = $(this).combobox('options');
                    var chinese = row[opts.textField];
                    ismatch = chinese.indexOf(matchstr) >= 0;
                    //如果中文匹配不到,匹配拼音
                    if (!ismatch && row.Pingyin != null) {
                        ismatch = row.Pingyin.First(function (item) {
                            return item.toUpperCase().indexOf(matchstr.toUpperCase()) >= 0;
                        }) != null;
                    }
                    return ismatch;
                }
            });
            AddressChang();
            loadTips.hideLoadind();
            //$.ajax({
            //    type: "post",
            //    data: { address: key },
            //    url: "/House/GetSpecialInfo",
            //    success: function (data) {
            //        $('#SpecialInfo').combobox({
            //            data: data,
            //            valueField: 'Key',
            //            textField: 'Key'
            //        });
            //        loadTips.hideLoadind();
            //    }, error: loadTips.hideLoadind
            //});
        },
        error: function () {
            loadTips.hideLoadind();
        }
    });
}
//根据楼栋号获取单元信息
function GetDanyuanById(key) {
    AddressChang();
    if (key == null || key == "" || isNaN(key))
        return;
    loadTips.showLoading();
    $.ajax({
        type: "post",
        data: { buildid: key, cityname: $("#CityName").combobox('getText') },
        url: "/House/GetDanyuanHao",
        success: function (data) {
            $('#UnitName').combobox({
                data: data,
                valueField: 'Key',
                textField: 'Value',
                onChange: GetHus
            });
            loadTips.hideLoadind();
        },
        error: function () {
            loadTips.hideLoadind();
        }
    });
    $("#HouseNum").combobox('clear');
}
//根据单元号获取户信息
function GetHus(key) {
    AddressChang();
    if (key == null || key == "" || isNaN(key))
        return;
    loadTips.showLoading();
    $.ajax({
        type: "post",
        data: { buildid: key, cityname: $("#CityName").combobox('getText') },
        url: "/House/GetHuming",
        success: function (data) {
            $('#HouseNum').combobox({
                data: data,
                valueField: 'Key',
                textField: 'Value',
                onChange: ShowFloor
            });
            loadTips.hideLoadind();
        },
        error: function () {
            loadTips.hideLoadind();
        }
    });
}
//选中单元号，加载所在层及总楼层信息
function ShowFloor(value) {
    AddressChang();
    var jdata = JSON.parse(value);
    var maxfloor = jdata.maxfloor;
    var floor = jdata.floor;
    if (maxfloor != null)
        $('#MaxFloor').numberbox('setValue', maxfloor);
    if (floor != null)
        $('#Floor').numberbox('setValue', floor);
}
//根据城市获取行政区
function RegionChange(value) {
    value = parseInt(value, 10);
    if (value.toString() == "NaN")
        return;
    $.ajax({
        type: "post",
        url: "/House/GetRegionList",
        data: { parentid: value },
        success: function (data) {
            $('#DistrictName').combobox({
                data: data,
                valueField: 'TID',
                textField: 'Name',
                onChange: AddressChang
            });
            AddressChang();
        }
    });

}
//询价查询
function SearchPrice() {
    var selecttitile = $('#tabs').tabs('getSelected').panel('options').title;
    var result = LoadPrice();
    if (result == false)
        return;
    Tabforload(selecttitile);
}
//根据tab选中的项来加载具体某一项的数据
function Tabforload(title, begin, end) {
    if (title == '历史询价记录')
        loadHistory();
    if (title == '价格走势')
        loadCharts();
    if (title == '报盘案例')
        loadershoubaogao(begin, end);
    if (title == '报告案例')
        loadpinggu(begin, end);
    if (title == '成交案例')
        loadershoujiaoyi(begin, end);
    if (title == '老系统询价记录') {
        var community = $("#ResidentialAreaName").val();
        var mianji = $("#BuildingArea").val();
        if (mianji == "") {
            alert("请输入建筑面积！");
            return;
        }
        var urlparams = "community=" + community + "&mianji=" + mianji;
        if ($("#BuildingName").combobox("getText") != "") {
            urlparams += "&buildingname=" + $("#BuildingName").combobox("getText");
        }
        $("#frame_oldInquiry").attr("src", "http://182.92.219.161:8081/EstimateMiddleware-0.0.1/admin/query/appraisal/pepinquiryPrice.htm?" + urlparams);
    }
    $('html, body').animate({
        scrollTop: $("#DgInquiry").offset().top + 300
    }, 50);
}
//加载询价结果内容
function LoadPrice() {
    var ResidentialAreaName = $("#ResidentialAreaName").val();
    if (ResidentialAreaName == '') {
        alert("请填写询价小区");
        return false;
    }
    var BuildingArea = $("#BuildingArea").val();
    if (BuildingArea == "" || isNaN(BuildingArea) || BuildingArea == "0") {
        alert("请填写正确的面积");
        return false;
    }
    if (parseInt(BuildingArea) <= 0) {
        alert("建筑面积必须大于0");
        return false;
    }
    var house_type = $("#PropertyType").combobox("getValue");
    //前端显示的跟接口要的参数不一致的，下面的dictionary是配置
    var dictionary =
    [
        { Key: "成套住宅", Value: "住宅" },
        { Key: "别墅", Value: "别墅" }
    ];
    var linq = new LINQ(dictionary);
    var item = linq.FirstOrDefault(function (item) { return item.Key == house_type });
    if (item == null) {
        alert("暂时不支持(" + house_type + ")类型的询价");
        return false;
    }
    var postdata = {
        house_type: item.Value,
        room_type: $("#HouseType").combobox("getText"),
        BuildingArea: BuildingArea,
        ResidentialAreaName: ResidentialAreaName,
        cityname: $("#CityName").combobox("getText"),
        floor: $("#Floor").numberbox("getText"),
        totalfloor: $("#MaxFloor").numberbox("getText"),
        builted_time: $("#BuildedYear").numberbox("getText"),
        toward: $("#Toword").combobox("getText"),
        specialinfo: $("#SpecialInfo").combobox("getText"),
        renovation: $("#Decoration").combobox("getText"),
    };
    var url = "/Project/GetFindPriceByMoHu";
    $("#PriceInfo").html("");
    $("#areaprice").html("");
    $("#areapricetotal").html("");
    $("#pricemin").html("");
    $("#pricemax").html("");
    $("#totalpricemin").html("");
    $("#totalpricemax").html("");
    $("#xunjiaid").val("");
    loadTips.showLoading();
    $.ajax({
        url: url,
        type: "post",
        data: postdata,
        success: function (data) {
            $("#PriceInfo").html(data.infos);
            $("#areaprice").val(data.price);
            $("#areapricetotal").val(data.totalPrice);
            $("#pricemin").val(data.minPrice);
            $("#pricemax").val(data.maxPrice);
            $("#totalpricemin").val(data.totalMinPrice);
            $("#totalpricemax").val(data.totalMaxPrice);
            $("#xunjiaid").val(data.id);
            loadTips.hideLoadind();
            //设置地图坐标
            if (data.x != 0 && data.y != 0) {
                SetLocation(data.x, data.y);
            }
        },
        error: function (data) {
            loadTips.hideLoadind();
            BaseHelper.ShowException(data);
        }
    });
}
//加载客户信息
function loaddata(data) {
    if (data.CustomerID == 0) {
        loadTips.hideLoadind();
        return;
    }
    $.ajax({
        type: "POST",
        url: "/House/GetWSCustomerById",
        dataType: "json",
        async: true,
        data: { customerid: data.CustomerID },
        success: function (data) {
            loadTips.hideLoadind();
            if (data == null)
                return;
            LoadCustomerData(data);
        }, error: loadTips.hideLoadind
    });
}
//地址联动
function AddressChang(val, e, r) {
    UnBindEnterCombox();
    var address = "";
    //地址拼接规则：城市+行政区+小区地址+（小区名称）+楼栋号+单元号+户名号
    address += $("#CityName").combobox('getText');  //城市
    address += $("#DistrictName").val();  //行政区
    var residentialAddress = $("#ResidentialAddress").val();
    if (residentialAddress)
        address += residentialAddress;  //小区地址
    var xqName = $.trim($('#ResidentialAreaName').val());//（小区名称）
    if (xqName.length > 0) {
        address += "(" + xqName + ")";
    }
    if ($("#BuildingName")[0] != null)
        address += $("#BuildingName").combobox('getText');  //楼栋号
    if ($("#UnitName")[0] != null)
        address += $("#UnitName").combobox('getText');  //单元号
    if ($("#HouseNum")[0] != null)
        address += $("#HouseNum").combobox('getText');  //户名号
    $("#Address").val(address);
}

function organiidchange(value, callback) {
    if (value == "") {
        return;
    }
    loadTips.showLoading();
    $("#organichildid").combobox({
        url: "/House/GetWSOrganiListChild?organiid=" + value,
        valueField: 'Key',
        textField: 'Value',
        onSelect: function (item) {
            peoplechange(item.Key);
        },
        onLoadSuccess: function () {
            loadTips.hideLoadind();
            if (callback != null)
                callback();
        }
    });
}

function peoplechange(value, callback) {
    if (value == null || value == "")
        return;
    var CustomerName = $("#customerid").combobox("getValue");
    loadTips.showLoading();
    $.ajax({
        type: "post",
        url: "/House/GetWSCustomerListByOrganiChildId",
        data: { organichildid: value },
        success: function (data) {
            $('#customerid').combobox({
                data: data,
                valueField: 'Key',
                textField: 'Value',
                onSelect: function (item) { loadcustom(item.Key) }
            });
            $("#customerid").combobox("setValue", CustomerName);
            if (callback != null)
                callback();
            loadTips.hideLoadind();
        },
        error: function () {
            loadTips.hideLoadind();
        }
    });
}

//提交
function SubmitForm(isgoon) {
    debugger;
    //自动添加客户
    var customerID = $("#customerid").combobox("getValue").ToInt();
    var customer = {
        TID: customerID,
        organiid: $("#organiid").combobox("getValue"),
        CustomerType: $("#organiid").combobox("getText"),
        OrganiChildId: $("#organichildid").combobox("getValue"),
        CustomerTypeChild: $("#organichildid").combobox("getText"),
        CustomerName: $("#customerid").combobox("getText"),
        MobileNumber: $("#MobileNumber").val(),
        Telephone: $("#Telephone").val(),
        QQ: $("#QQ").val()
    };
    if ($("#PropertyType").combobox("getValue") == "") {
        alert("请选择正确的房屋类型");
        return;
    }
    if ($("#BuildingArea").val() == "" || $("#BuildingArea").val() == 0) {
        alert("请输入正确的建筑面积");
        return;
    }
    //修改2016-6-1
    if (($("#Floor").val() != "" && $("#MaxFloor").val() != "") && (parseInt($("#Floor").val()) > parseInt($("#MaxFloor").val()))) {
        alert("所在楼层不能大于总楼层！");
        return;
    }
    if ($("#Address").val() == "") {
        alert("请输入正确的询价地址");
        return;
    }
    var pattern = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）——|{}【】‘；：”“'。，、？]")
    if ($.trim(customer.MobileNumber) != "" && !/^\d{11}$/.test(customer.MobileNumber)) {
        alert("客户电话号码只能为11位数字");
        return;
    }
    if ($.trim(customer.MobileNumber) != "" && $.trim(customer.CustomerName) == "") {
        alert("客户名称不能为空");
        return;
    }
    if ($.trim(customer.CustomerName) != "" && $.trim(customer.MobileNumber) == "") {
        alert("客户手机不能为空");
        return;
    }
    if (/[\d]+/.test($("#customerid").combobox("textbox")[0].value)) {
        alert("客户姓名不能为数字");
        return;
    }
    if (pattern.test(customer.CustomerName) || customer.CustomerName.length > 25) {
        alert("客户名称非法,客户名称长度不能超过25，且不能有特殊字符");
        return;
    }
    if ($("#Address").val().length > 100) {
        alert("地址过长");
        return;
    }
    if ($("#Telephone").val().length > 0) {
        if (!/^[\d]+$/.test($("#Telephone").val()) && !/^[\d]+-[\d]+$/.test($("#Telephone").val())) {
            alert("固定电话格式不对");
            return;
        }
    }
    if ($("#InquiryPrice").val() == "" || $('#InquiryPrice').numberbox('getValue').ToFloat() == null) {
        alert("请填写正确的询值单价");
        return;
    }
    if ($("#InquiryPriceUp").val() == "" || $('#InquiryPriceUp').numberbox('getValue').ToFloat() == null) {
        alert("请填写正确的询值单价");
        return;
    }
    if ($('#InquiryPrice').numberbox('getValue').ToFloat() > $('#InquiryPriceUp').numberbox('getValue').ToFloat()) {
        alert("请填写正确的询值单价区间");
        return;
    }
    //if ($("#InquiryResult").val() == "" || $("#InquiryResult").val().ToFloat() == null) {
    //    alert("请填写正确的询值总价");
    //    return;
    //}
    //if ($("#InquiryResultUp").val() == "" || $("#InquiryResultUp").val().ToFloat() == null) {
    //    alert("请填写正确的询值总价");
    //    return;
    //}
    if ($('#InquiryResult').numberbox('getValue').ToFloat() > $('#InquiryResultUp').numberbox('getValue').ToFloat()) {
        alert("请填写正确的询值总价区间");
        return;
    }
    var InquiryDetail = {
        TID: $("#InquiryTargetID").val(),
        CityName: $("#CityName").combobox("getText"),
        DistrictName: $("#DistrictName").val(),
        ResidentialAreaName: $("#ResidentialAreaName").val(),
        ResidentialAddress: $("#ResidentialAddress").val(),
        PropertyType: $("#PropertyType").combobox("getText"),
        BuildingArea: $("#BuildingArea").numberbox("getValue"),
        Floor: $("#Floor").numberbox("getValue"),
        MaxFloor: $("#MaxFloor").numberbox("getValue"),
        BuildedYear: $("#BuildedYear").numberbox("getValue"),
        Toword: $("#Toword").combobox("getText"),
        Decoration: $("#Decoration").combobox("getText"),
        Address: $("#Address").val()
    }
    var Inquiry = {
        TID: $("#InTID").val(),
        InquirySource: $("#InquirySource").combobox("getText"),
        InquiryPrice: $("#InquiryPrice").numberbox("getValue"),
        InquiryPriceUp: $("#InquiryPriceUp").numberbox("getValue"),
        InquiryResult: $("#InquiryResult").val(),
        InquiryResultUp: $("#InquiryResultUp").val(),
        Remark: $("#Remark").val(),
        IsLiveSearch: $("#IsLiveSearch").combobox("getText")
    }
    var url = '/Inquiry/CreateInquiryNotHouse';
    loadTips.showLoading();
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        data: {
            inquiry: Inquiry,
            inquirydetail: InquiryDetail,
            customer: customer,
            onlineid: 0
        },
        success: function (data) {
            loadTips.hideLoadind();
            messageAlert(data.message, "info", function () {
                if (data.success) {
                    if (isgoon) {
                        SendtoProject(data.data, customerID);
                    } else {
                        if( parseInt($("#InTID").val())!=0){
                            if (parent && parent.parent && parent.parent.tab.isTabItemExist("询价编辑")) { parent.tab.removeTabItem("询价编辑"); }
                        }
                        else{
                            $("#CityName,#BuildingName,#UnitName,#HouseNum,#PropertyType,#HouseType,#Toword,#Decoration,#SpecialInfo,#InquirySource").each(function () {
                                $(this).combobox("setValue", "");
                            });
                            $("#DistrictName,#ResidentialAreaName,#ResidentialAddress,#Remark,#Address,#areaprice,#areapricetotal,#FeedbackMessage,#pricemin,#pricemax,#totalpricemin,#totalpricemax").each(function () {
                                $(this).val("");
                            });
                            $("#Floor,#MaxFloor,#BuildedYear,#FeedbackPrice,#InquiryPrice,#InquiryPriceUp").each(function () {
                                $(this).numberbox("setValue", "");
                            });
                            $("#InquiryResult,#InquiryResultUp,#BuildingArea").each(function () {
                                $(this).textbox("setValue", "");
                            });
                            $("#PriceInfo").text("");
                        }
                        
                    }
                }
            }); 
        },
        error: function (data) {
            BaseHelper.ShowException(data);
        }
    });
}

function SendtoProject(tid, cid) {
    //alert(cid);
    var temp_totalMaxPrice = $('#totalpricemax').val(); //总价最高(万)
    var temp_totalMinPrice = $('#totalpricemin').val(); //总价最低(万)
    if (parent.tab.isTabItemExist("立项"))
        parent.tab.removeTabItem("立项");
    if (parent.parent.tab.isTabItemExist("create_project_")) { parent.parent.tab.removeTabItem("create_project_"); }
    parent.f_addTab('立项', '立项添加', '/Project/SetProject?cid=' + cid + "&inquiryid=" + tid + "&totalMaxPrice=" + temp_totalMaxPrice + "&totalMinPrice=" + temp_totalMinPrice);
    //调用立项页面的LoadFromInquiry方法，将询价的信息加载进去
    //有2种情况
    //1，询价和立项的界面都开了(2个tab)。可以从A直接调用B里的方法
    //2，只有询价tab。这时转立项会新开一个tab。但无法控制页面是否加载完成，iframe的状态是完成，但还是没内容
    //这时就要重复的去调了。设置了每隔2秒调1次，调用5次后还是找不到LoadFromInquiry方法就return
    var fn = function () {
        var setProjectiframe = BaseHelper.First(parent.frames, function (item) {
            return item.LoadFromInquiry != null;
        });
        if (setProjectiframe != null) {
            //setProjectiframe.totalMaxPrice = temp_totalMaxPrice || "";
            //setProjectiframe.totalMinPrice = temp_totalMinPrice || "";
            return true;
        }
        return false;
    };
    var times = 0;
    var fninner = function () {
        if (fn())
            return;
        times++;
        if (times > 5)
            return;
        setTimeout(fninner, 2000);
    };
    fninner();
}

//将立项信息加载出来
function LoadFromProject(data) {
    setTimeout(function () {
        $("#form1").form("load", data);
        var temp = function () {
            $("#form1").form("load", {
                organiid: data.organiid,
                organichildid: data.organichildid,
                customerid: data.customerid,
                MobileNumber: data.MobileNumber,
                Telephone: data.Telephone,
                QQ: data.QQ
            });
            var customer = new LINQ($("#customerid").combobox("getData")).FirstOrDefault(function (item) {
                return item.Value == data.customerid;
            });
            if (customer != null) {
                $("#customerid").combobox("select", customer.Key);
            }
        };
        if (data.organiid != "") {
            var fn = temp;
            if (data.organichildid != "") {
                fn = function () { peoplechange(data.organichildid, temp) };
            }
            organiidchange(data.organiid, fn);
        }
        $("#Address").val(data.ResidentialAreaAddress);
    }, 2500);

    //var fn = function () {
    //    temp();

    //    if (!data.organichildid.IsNullOrEmpty()) {
    //        $('#customerid').combobox({
    //            url: "/House/GetWSCustomerListByOrganiChildId?organichildid=" + data.organichildid,
    //            valueField: 'Key',
    //            textField: 'Value'
    //        });
    //    }

    //    $("#form1").form("load", {
    //        customerid: data.customerid
    //    });
    //};

    //if (data.organiid == 0) {
    //    $("#organiid").combobox("setText", data.CustomerType);
    //    $("#organichildid").combobox("setText", data.CustomerTypeChild);
    //}

    //loadTips.hideLoadind();

    //if (!data.organiid.IsNullOrEmpty()) {
    //    $("#organichildid").combobox({
    //        url: "/House/GetWSOrganiListChild?organiid=" + data.organiid,
    //        valueField: 'Key',
    //        textField: 'Value',
    //        onSelect: function (item) {
    //            peoplechange(item.Key);
    //        },
    //        onLoadSuccess: fn
    //    });
    //}

}


//begin 查询用户信息
function SearchCustomer() {
    if ($.trim($("#MobileNumber").val()) == "") {
        alert("请输入电话号码");
        return;
    }
    loadTips.showLoading();
    $.ajax({
        type: "Get",
        url: "/Project/GetCustomerByMobile",
        dataType: "jsonp",
        jsonp: "callback",
        async: true,
        data: { mobileNumber: $("#MobileNumber").val() },
        success: function (data) {
            loadTips.hideLoadind();
            if (data == null) {
                alert("未查询到对应客户");
                return;
            }
            LoadCustomerData(data);
        }
    });
}

//加载用户信息
function LoadCustomerData(data) {
    if (data == null)
        return;

    if (data.CustomerTypeID != 0) {
        $("#organichildid").combobox({
            url: "/House/GetWSOrganiListChild?organiid=" + data.CustomerTypeID,
            valueField: 'Key',
            textField: 'Value',
            onSelect: function (item) {
                peoplechange(item.Key);  
            },
            onLoadSuccess: function () {
                peoplechange(data.CustomerTypeChildId, function () {
                    $("#customerid").combobox("setText", loaddata.CustomerName);
                    $("#customerid").combobox("setValue", data.TID);
                });
                var loaddata = {
                    organiid: data.CustomerTypeID,
                    organichildid: data.CustomerTypeChildId,
                    CustomerName: data.CustomerName,
                    MobileNumber: data.MobileNumber,
                    Telephone: data.Telephone,
                    QQ: data.QQ
                };
                $("#form1").form("load", loaddata);
            }
        });
    }
    else {
        $("#organiid").combobox("setValue", data.CustomerType);
        $("#organichildid").combobox("setValue", data.CustomerTypeChild);
        $("#MobileNumber").val(data.MobileNumber);
        $("#customerid").combobox("setValue", data.TID);
        $("#customerid").combobox("setText", data.CustomerName);
        $("#QQ").val(data.QQ);
        $("#Telephone").val(data.Telephone);
    }
}

//添加用户
function AddCustorm() {
   
    var postdata = {
        organiid: $("#organiid").combobox("getValue"),
        OrganiChildId: $("#organichildid").combobox("getValue"),
        CustomerName: $("#customerid").combobox("getText"),
        MobileNumber: $("#MobileNumber").val(),
        Telephone: $("#Telephone").val(),
        QQ: $("#QQ").val()
    };
    if ($.trim(postdata.MobileNumber) == "") {
        alert("请输入客户手机号");
        return;
    }
    if (!/^\d{11}$/.test(postdata.MobileNumber)) {
        alert("电话号码只能为11位数字");
        return;
    }
    if ($.trim(postdata.CustomerName) == "") {
        alert("请输入客户姓名");
        return;
    }
    if (pattern.test(postdata.CustomerName))
    {
        alert("客户姓名中存在非法字符");
        return;
    }
    return;
    loadTips.showLoading();
    $.ajax({
        url: "/Project/AddCustomer",
        data: postdata,
        success: function (data) {
            alert(data.message);
            loadTips.hideLoadind();
            if (data.success) {
                var array = $("#customerid").combobox("getData");
                var item = { Key: data.tid, Value: postdata.CustomerName };
                array.push(item);
                $("#customerid").combobox("setValue", array);
                $("#customerid").combobox("select", item.Value);
            }
        }
    });
}
//end 查询用户信息


function loadcustom(value) {
    if (value == "")
        return;

    BaseHelper.loadform("form1", "/House/GetWSCustomerById?customerid=" + value, null, function (data) {
        delete data.Address;
        return data;
    });
}

//加载散点图,走势图
function loadCharts() {
    var address = $("#ResidentialAreaName").val();
    if (address == "")
        return;
    loadTips.showLoading();
    //走势
    $.ajax({
        type: "post",
        url: "/Project/GetZoushitu",
        data: { xiaoqu: $("#ResidentialAreaName").val() },
        success: function (data) {
            chartsLines(data.Keys, data.Datas);
            //散点
            $.ajax({
                type: "post",
                url: "/Project/GetSanDian",
                data: { address: address },
                success: function (data) {
                    loadTips.hideLoadind();
                    chartsPoints(data.data, data.Pointsx);
                },
                error: function () {
                    loadTips.hideLoadind();
                }
            });
        },
        error: function () {
            loadTips.hideLoadind();
        }
    });


}
//加载历史询价记录
function loadHistory() {
    var ResidentialAreaName = encodeURI($("#ResidentialAreaName").val());
    var Address = encodeURI($("#Address").val());
    $("#DgInquiry").datagrid({ url: "/House/GetInquiryList?ResidentialAreaName=" + ResidentialAreaName + "&Address=" + Address + "&istotask=false" });
}

//加载二手房报盘案例
function loadershoubaogao(begin, end) {
    var url = "/House/GetOfferForSaleList?residentialareaname=" + encodeURI($("#ResidentialAreaName").val()) + "&residentialAreaId=" + encodeURI($("#ResidentialAreaId").val()) + "&CityName=" + encodeURI($("#CityName").combobox('getText'));
    if (begin != null && end != null) {
        url += "&begin=" + begin + "&end=" + end;
    }
    $("#Dgershoubaogao").datagrid({ url: url });
}
//加载评估报告
function loadpinggu(begin, end) {
    var url = "/House/GetHouseAppraisalCaseList?residentialareaname=" + encodeURI($("#ResidentialAreaName").val()) + "&residentialAreaId=" + encodeURI($("#ResidentialAreaId").val()) + "&CityName=" + encodeURI($("#CityName").combobox('getText')) + "&Address=" + encodeURI($("#Address").val());
    if (begin != null && end != null) {
        url += "&begin=" + begin + "&end=" + end;
    }
    $("#DgPinggu").datagrid({ url: url });
}

//加载二手房交易案例
function loadershoujiaoyi(begin, end) {
    var url = "/House/GetHouseResoldCaseList?residentialareaname=" + encodeURI($("#ResidentialAreaName").val()) + "&residentialAreaId=" + encodeURI($("#ResidentialAreaId").val()) + "&CityName=" + encodeURI($("#CityName").combobox('getText')) + "&Address=" + encodeURI($("#Address").val());
    if (begin != null && end != null) {
        url += "&begin=" + begin + "&end=" + end;
    }
    $("#Dgershoujiaoyi").datagrid({ url: url });
}

//走势图
function chartsLines(keys, datas) {
    if (datas.length == 0 || datas == null)
        return;
    require.config({
        paths: {
            echarts: '/Content/plug-in/echarts-2.2.1/build/dist'
        }
    });
    require(
       [
           'echarts',
           'echarts/chart/line' // 使用柱状图就加载bar模块，按需加载
       ],
       function (ec) {
           // 基于准备好的dom，初始化echarts图表
           var myChart = ec.init(document.getElementById('Lines'));
           option = {
               title: {
                   subtext: '价格走势'
               },
               tooltip: {
                   trigger: 'axis'
               },
               calculable: true,
               xAxis: [
                   {
                       type: 'category',
                       boundaryGap: false,
                       data: keys
                   }
               ],
               yAxis: [
                   {
                       type: 'value',
                       axisLabel: {
                           formatter: '{value}元'
                       }
                   }
               ],
               series: [
                   {
                       name: '价格',
                       type: 'line',
                       data: datas,
                       markPoint: {
                           data: [
                               { type: 'max', name: '最高' },
                               { type: 'min', name: '最低' }
                           ]
                       }
                   }
               ]
           };
           myChart.setOption(option);
       }
   );
}

//散点图
function chartsPoints(data, x) {
    if (data.length == 0 || data == null)
        return;
    require.config({
        paths: {
            echarts: '/Content/plug-in/echarts-2.2.1/build/dist'
        }
    });
    require(
       [
           'echarts',
           'echarts/chart/scatter' // 使用柱状图就加载bar模块，按需加载
       ],
       function (ec) {
           // 基于准备好的dom，初始化echarts图表
           var myChart = ec.init(document.getElementById('Points'));
           option = {
               title: {
                   subtext: '散点图'
               },
               tooltip: {
                   trigger: 'item',
                   showDelay: 0
               },
               xAxis: [
                   {
                       type: 'category',
                       data: x
                   }
               ],
               yAxis: [
                  {
                      type: 'value',
                      data: [0, 500, 1000],
                      axisLabel: {
                          formatter: '{value} (万元)'
                      }
                  }
               ],
               series: [
                   {
                       name: '时间/价格(万)',
                       type: 'scatter',
                       data: data,
                       markPoint: {
                           data: [
                               { type: 'max', name: '最小值' },
                               { type: 'min', name: '最大值' }
                           ]
                       }
                   }
               ]
           };

           myChart.setOption(option);
       }
   );
}