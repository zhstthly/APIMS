//About ajax
function LoadAjax(replaceID, action, data) {
    $.ajax({
        type: "POST",
        url: action,
        data: data,
        datatype: "json",
        success: function (data) {
            $(replaceID).html(data);
        }
    });
}

//common function
Date.prototype.format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,
        "d+": this.getDate(),
        "h+": this.getHours(),
        "m+": this.getMinutes(),
        "s+": this.getSeconds(),
        "q+": Math.floor((this.getMonth() + 3) / 3),
        "S": this.getMilliseconds()
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) :
                (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
};

ShowMessage = function (message, life) {
    var time = 2000;
    if (life) {
        time = life;
    }
    if ($("#tip_message").text().length > 0) {
        var msg = "<span>" + message + "</span>";
        $("#tip_message").empty().append(msg);
    } else {
        var msg = "<p id='tip_message'><span>" + message + "</span></p>";
        $("body").append(msg);
    }
    $("#tip_message").show();
    setTimeout($("#tip_message").fadeOut(time), time);
};

ShowSuccessMessage = function (message, life) {
    ShowMessage(message, life);
    $("#tip_message span").addClass("success");
}

ShowErrorMessage = function (message, life) {
    ShowMessage(message, life);
    $("#tip_message span").addClass("error");
};

function tableNoneSelected(table,haveHead) {
    var rows = $(table).find('tr');
    var i = 0;
    if (haveHead)
        i = 1;
    for (; i < rows.length; i++) {
        rows.eq(i).removeClass('selected');
    }
}

function tableRowSelected($row) {
    $row.addClass('selected');
}

function tableTrClickNoHead($sender) {
    tableNoneSelected($sender.parent());
    tableRowSelected($sender,false);
};

function tableTrClick($sender) {
    tableNoneSelected($sender.parent());
    tableRowSelected($sender,true);
};

//////////////Custom Components//////////////
//表格初始化
(function () {
    function init(initParams) {
        $(initParams.table).bootstrapTable({
            editable: (initParams.tableEditable ? initParams.tableEditable:false),            //可编辑模式
            url: initParams.getUrl,             //请求后台的URL（*）  
            //queryParams: initParams.selectCondition,        //传递json的数据
            method: 'post',                     //请求方式（*）  
            toolbar: '#toolbar',                //工具按钮用哪个容器  
            striped: false,                      //是否显示行间隔色  
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）  
            pagination: true,                   //是否显示分页（*）  
            sortable: false,                    //是否启用排序  
            sortOrder: "asc",                   //排序方式  
            queryParams: initParams.queryParams,           //传递参数（*），这里应该返回一个object，即形如{param1:val1,param2:val2}  
            sidePagination: "client",           //分页方式：client客户端分页，server服务端分页（*）  
            pageNumber: (initParams.pageNumber ? initParams.pageNumber : 1),         //初始化加载第一页，默认第一页  
            pageSize: (initParams.pageSize ? initParams.pageSize : 10),                 //每页的记录行数（*）  
            pageList: [2, 3, 4, 5,10],             //可供选择的每页的行数（*）  
            search: initParams.haveSearch,        //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大，除非分页方式为client  
            strictSearch: false,                 //设置为 true启用 全匹配搜索，否则为模糊搜索
            showColumns: true,                  //是否显示所有的列  
            showRefresh: true,                  //是否显示刷新按钮  
            minimumCountColumns: 2,             //最少允许的列数  
            clickToSelect: !initParams.tableEditable,                //是否启用点击选中行  
            //height: 500,                      //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度  
            uniqueId: "ID",                     //每一行的唯一标识，一般为主键列  
            showToggle: true,                   //是否显示详细视图和列表视图的切换按钮  
            cardView: false,                    //是否显示详细视图  
            detailView: false,                  //是否显示父子表  
            singleSelect: initParams.singleSelect,         //仅单选
            undefinedText: '',                   //空值
            columns: createCols(initParams),
            onClickRow: initParams.tableClickRow,                         //单击行事件
            onEditableSave: function (field, row, oldValue, $el) {
                $.ajax({
                    type: 'POST',
                    url: initParams.editUrl,
                    data: row,
                    dataType: 'JSON',
                    success: function (data, status) {
                        ShowSuccessMessage(data.Message);
                    },
                    error: function () {
                        ShowErrorMessage('编辑失败!');
                    },
                    complete: function () {

                    }
                });
            }
        });
        formatTable(initParams.table);
    }
    //列初始化
    function createCols(initParams) {
        if (initParams.params.length != initParams.titles.length)
            return null;
        var arr = [];
        if (initParams.hasCheckbox) {
            var objc = {};
            objc.checkbox = true;
            arr.push(objc);
        }
        for (var i = 0; i < initParams.params.length; i++) {
            var obj = {};
            obj.field = initParams.params[i];
            if (initParams.widths[i] > 0)
                obj.width = initParams.widths[i];
            obj.title = initParams.titles[i];
            ColmunFormatter(obj, initParams.formats[i], initParams.editColumns[i], initParams.tableEditable);
            arr.push(obj);
        }
        return arr;
    }
    //列属性初始化
    function ColmunFormatter(obj, formatter, editable, tableEditable) {
        var pro_editable = { emptytext: '空文本' };
        var formatStr = formatter.split(":");
        switch (formatStr[0]) {
            case "date":
                obj.formatter = DateTimeFormatter;
                pro_editable.type = 'date';
                pro_editable.clear = false;
                break;
            case "select":
                if (!tableEditable)
                    obj.formatter = SelectFormatter;
                pro_editable.type = 'select';
                pro_editable.source = formatStr[1];
                pro_editable.sourceError = '无法加载数据源';
                break;
            case "checkbox":
                if (!tableEditable)
                    obj.formatter = CheckboxFormatter;
                pro_editable.type = 'select';
                pro_editable.source = [
                    { value: 1, text: '是' },
                    { value: 0, text: '否' }
                ];
                break;
            default:
                pro_editable.type = 'text';
                pro_editable.clear = false;
        }
        if (editable)
            obj.editable = pro_editable;
    }
    var selectFormatArray = [];
    //select类型格式化
    function SelectFormatter(value, row, index) {
        var field = this.field;
        var formatUrl = initParams.formats[this.fieldIndex - 1].split(":")[1];
        var findArray;
        var s = selectFormatArray;
        for (var i = 0; i < selectFormatArray.length;i++) {
            var f = selectFormatArray[i].field;
            var b = selectFormatArray[i].field == field;
            if (selectFormatArray[i].field == field) {
                findArray = selectFormatArray[i].formatArray;
                break;
            }
        }
        if (findArray) {
            for (var j = 0; j < findArray.length;j++) {
                if (findArray[j].value == value) {
                    return findArray[j].text;
                }
            }
        }
        return value;
    }
    //dateTime类型格式化
    function DateTimeFormatter(value, row, index) {
        var date = eval('new ' + eval(value).source);
        if (!date)
            return value;
        else
            return date.format("yyyy-MM-dd");
    }
    //checkbox类型格式化
    function CheckboxFormatter(value, row, index) {
        var checkStr = '';
        if (value)
            checkStr = 'checked';
        return '<input type="checkbox" onclick="return false;" ' + checkStr + ' />';
    }
    //加载下拉框数据
    function initSelectArray(initParams) {
        for (var i = 0; i < initParams.formats.length; i++) {
            var formatStr = initParams.formats[i].split(":");
            if (formatStr.length > 1) {
                getSelectArray(initParams.params[i], formatStr[1]);
            }
        }
    }
    //获取select下拉菜单的内容
    function getSelectArray(field, formatUrl) {
        $.ajax({
            type: 'GET',
            url: formatUrl,
            dataType: 'JSON',
            success: function (result) {
                selectFormatArray.push({
                    field: field,
                    formatArray: result
                });
            }
        });
    }
    //可发送给服务端的参数：limit->pageSize,offset->pageNumber,search->searchText,sort->sortName(字段),order->sortOrder('asc'或'desc')  
    function queryParams(params) {
        return {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的  
            limit: params.limit,   //页面大小  
            offset: params.offset  //页码  
            //name: $("#txt_name").val()//关键字查询  
        };
    }
    
    //格式化表格样式
    function formatTable(table){
        var head = $(table).children("thead");
        head.css("background-color", "#7FFFD4");
    }

    var initParams = {};
    //创建table
    createBootstrapTable = function (table, getUrl, addUrl, editUrl, deleteUrl, selectCondition,
        params, titles, formats, widths, editColumns, hasCheckbox, singleSelect,haveSearch,
        tableClickRow,tableEditable,
        pageNumber, pageSize) {
        initParams.table = table;
        initParams.getUrl = getUrl;
        initParams.addUrl = addUrl;
        initParams.selectCondition = selectCondition;
        initParams.editUrl = editUrl;
        initParams.deleteUrl = deleteUrl;
        initParams.params = params;
        initParams.titles = titles;
        initParams.formats = formats;
        initParams.widths = widths;
        initParams.editColumns = editColumns;
        initParams.hasCheckbox = hasCheckbox;
        initParams.singleSelect = singleSelect;
        initParams.haveSearch = haveSearch;
        initParams.tableClickRow = tableClickRow;
        initParams.tableEditable = tableEditable;
        initParams.pageNumber = pageNumber;
        initParams.pageSize = pageSize;
        initSelectArray(initParams);
        initToolBar(initParams);
        init(initParams);
    }

    //初始化toolbar
    function initToolBar(initParams) {
        var $toolbar = $('<div style="width:300px" id="toolbar" class="btn-group"></div>');
        $("body").append($toolbar);
        $toolbar.append('<button id="toolbar_add" type="button" class="btn btn-success"><span class="glyphicon glyphicon-plus" aria-hidden="true" ></span>新增</button>');
        $toolbar.append('<button id="toolbar_edit" type="button" class="btn btn-info"><span class="glyphicon glyphicon-pencil" aria-hidden="true" ></span>编辑</button>');
        $toolbar.append('<button id="toolbar_delete" type="button" class="btn btn-danger"><span class="glyphicon glyphicon-remove" aria- hidden="true" ></span>删除</button>');
        $("#toolbar_add").click(function () {
            addItem(initParams.table, initParams.addUrl, initParams.selectCondition);
        });
        $("#toolbar_edit").click(function () {
            editItem(initParams.table);
        });
        $("#toolbar_delete").confirmation({
            animation: true,
            placement: "top",
            title: "确定要删除选择项吗？",
            btnOkLabel: '确定',
            btnCancelLabel: '取消',
            onConfirm: function () {
                deleteItem(initParams);
            }
        });
    }

    //工具栏事件
    addItem = function (table, addUrl, selectCondition) {
        var optRow = $(table).bootstrapTable('getOptions');
        var url = optRow.url;
        $(table).bootstrapTable('refresh', { url: addUrl, silent: true, query: selectCondition });
        optRow.url = url;
    };

    editItem = function (table) {
        var editParams = initParams;
        var optRow = $(table).bootstrapTable('getOptions');
        editParams.tableEditable = !optRow.editable;
        editParams.pageNumber = optRow.pageNumber;
        editParams.pageSize = optRow.pageSize;
        $(table).bootstrapTable('destroy');
        init(editParams);
    }

    deleteItem = function (initParams) {
        var selRow = $(initParams.table).bootstrapTable('getSelections');
        var deleteIds = [];
        for (var i = 0; i < selRow.length; i++) {
            deleteIds.push(selRow[i].ID);
        }
        if (selRow.length > 0) {
            $.ajax({
                type: "POST",
                cache: false,
                async: true,
                dataType: "json",
                url: initParams.deleteUrl,
                data: { ids: deleteIds },
                success: function (data) {
                    if (data.IsSuccess) {
                        $(initParams.table).bootstrapTable('refresh', { url: initParams.getUrl, silent: true, query: initParams.selectCondition });
                    }
                    ShowSuccessMessage(data.Message);

                }
            });
        } else {
            ShowErrorMessage('请选取要删除的数据行！');
        }
    }
})();
//////////////END Custom Components////////////