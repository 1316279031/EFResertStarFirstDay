let saveGetDatas = [];//保存临时的更改了状态的数据
let resetDatas = [];//保存重置数据的数据
let query = [];//提交的数据
let container;//表格容器
let devWidth;//设备右边宽度
let colWidths;//每列宽度
let colHeaders;//标题行
let afterChange;//每次修改执行的函数
let columns;//没列的数据的相关配置
//添加事件监听
function ButtonsAddEventList(query, href,createTableFunction) {
    let $saveButton = $('#saveButton');
    let $resetButton = $('#resetButton');
    $saveButton.off('click');
    $resetButton.off('click');
    $saveButton.on('click',
        function() {
            AjaxSubmitData(query, href, createTableFunction);
        });
    $resetButton.on('click',
            function () {
                //就相当于重新加载一次但是由于我们有数据所以我们只需要直接重制
                createTableFunction(resetDatas, href);
            });
}

//将我们XzJSON数据转换为Javascript数据
function DealWithData(datas) {
    let currentIndex = 0;
    let data = [];
    if (datas.forEach == undefined) {
        datas = JSON.parse(datas);
    }
    datas.forEach(x => {
        data[currentIndex] = {
            Account: datas[currentIndex].AdministratorAccount,
            Email: datas[currentIndex].CreateAdminitratorDetialDatas.Email,
            Authority: datas[currentIndex].CreateAdminitratorDetialDatas.AdministratorAuthority,
            IsFreeze: datas[currentIndex].CreateAdminitratorDetialDatas.IsFreeze
        }
        currentIndex++;
    });
    return data;
}

//重新设置以下DOM中的节点
function HtmlReset() {
    let $handsontable = $('#handsontable-code');
    $handsontable.html("");
    $('#hand-buttons').css({
        display: "block"
    });
    $('#OptionView').remove();
}

//保存数据的检测
// data:我们的改变的数据,href:url
function AjaxSubmitData(data, href, createTableFunction) {
    if (data.length === 0 || href == null) {
        alert("您未更改任何数据无需提交");
        return;
    }
    $.ajax({
        type: 'Post',
        url: href,
        data: { adminDatas: data },
        success: function(datas, e) {
            console.log("储存成功");
            resetDatas = saveGetDatas;
            createTableFunction(datas, href);
        },
        error: function (e) {
            console.log(e.status);
            alert("数据保存出现异常");
        }
    });
}

//判断数组中是否已存在我们的对象
function ValidateQuerYArrayHasElement(account,queryArray,propertyName) {
    let index = 0;
    try {
        queryArray.forEach(x => {
            if (x[propertyName] !== account) {
                index++;
            } else {
                throw new Error();
            }
        });
    } catch (e) {
        return index;
    }
    return -1;
}
//创建Xz权限可看的表格
function CreateTableZxTable(datas, href) {
    if (datas.forEach == undefined) {
        datas = JSON.parse(datas);
    }
    HtmlReset();//每次创建表格时我们都必须要进行一次页面的重置
    let data = [];//创建表格的数据
    data = DealWithData(datas);
    let invalidate = function name(value, callback) {
        setTimeout(function() {
                if (/^.+@@.+/.test(value)) {
                    callback(true);
                } else {
                    callback(false);
                }
            },
            300);
    };
    let container = document.getElementById("handsontable-code");
    let devWidth = $('.views').width();
    query = [];
    let copy = JSON.stringify(datas);
    saveGetDatas = JSON.parse(copy);//深拷贝临时数据
    resetDatas = datas;//获取源数据，并引用源数据
    //设备右边宽度/列数，均摊每列
    colWidths = devWidth / 4;
    colHeaders = ["账号", "邮箱", "申请权限", "账户状态"];
    afterChange = function(value, source) {
        if (source == "edit") {
            saveGetDatas[value[0][0]]["CreateAdminitratorDetialDatas"][value[0][1]] = value[0][3]; //改变原有集合数据
            let index = ValidateQuerYArrayHasElement(datas[value[0][0]]["AdministratorAccount"], query,"AdministratorAccount");
            if (index < 0){
                query.push(saveGetDatas[value[0][0]]);
            } else {
                query[index] = saveGetDatas[value[0][0]];
            }
        }
    };
    columns = [
        {
            data: 'Account',
            readOnly: true
        }, {
            data: 'Email',
            validator: invalidate,
            allowInvalid: false,
            readOnly: true
        }, {
            data: 'Authority',
            readOnly: true
        }, {
            data: 'IsFreeze',
            type: 'checkbox'
        }
    ];
    ButtonsAddEventList(query, href, CreateTableZxTable);
    CreateTable(container, data, colWidths, colHeaders, afterChange, columns);
    //删除未激活的提醒事项
    $('#hot-display-license-info').remove();
}
//创建学籍管理者可看的表格
function DealWithDataForStuAdmin(datas) {
    let currentIndex = 0;
    let data = [];

    datas.forEach(x => {
        data[currentIndex] = {
            ID: datas[currentIndex].ID,
            Name: datas[currentIndex].Name,
            Department: datas[currentIndex].Department,
            Class: datas[currentIndex].Class,
            IdCard: datas[currentIndex].StudentDatas.IdCard,
            Address: datas[currentIndex].StudentDatas.Address,
            Telephone: datas[currentIndex].StudentDatas.Telephone,
            PareventTelephone: datas[currentIndex].StudentDatas.PareventTelephone
        }
        currentIndex++;
    });
    return data;
}
function CreateStuStatusAdministratorTable(datas, href) {
    if (datas.forEach == undefined) {
        datas = JSON.parse(datas);
    }
    HtmlReset();//每次创建表格时我们都必须要进行一次页面的重置
    let data = [];//创建表格的数据
    data = DealWithDataForStuAdmin(datas);
    query = [];
    let copy = JSON.stringify(datas);
    saveGetDatas = JSON.parse(copy);//深拷贝临时数据
    resetDatas = datas;//获取源数据，并引用源数据
    ButtonsAddEventList(query, href, CreateStuStatusAdministratorTable);
    container = document.getElementById("handsontable-code");
    devWidth = $('.views').width();
    colWidths = devWidth / 8;
    colHeaders = ['编号',"姓名", "系别", "班级", "身份证", "住址", "手机号", "父母手机号"];
    columns = [
        {
            data: "ID",
            readOnly:true
        },
        {
            data: "Name"
        }, {
            data: "Department"
        }, {
            data: "Class"
        }, {
             data: "IdCard"
        }, {
            data: "Address"
        }, {
            data: "Telephone"
        }, {
            data: "PareventTelephone"
        }
    ];

    afterChange = function(value, source) {
        let count = 0;
        if (source == "edit") {
            let va = {};
            let properityName;
            let modifyValue;
            console.log(value);
            for (let i = 0; i < value.length; i++) {
                properityName = value[i][1];
                modifyValue = value[i][3];
                let index;
                if (properityName in saveGetDatas[value[i][0]]) {
                    //修改数据不在StudentDatas下
                    saveGetDatas[value[i][0]][properityName] = modifyValue;
                }
                else if (properityName in saveGetDatas[value[i][0]]["StudentDatas"]) {
                    //修改数据在StudentDatas下
                    saveGetDatas[value[i][0]]["StudentDatas"][properityName] = modifyValue;
                }
                index = ValidateQuerYArrayHasElement(datas[value[i][0]].ID, query, "ID");
                if (index < 0) {
                    query.push(saveGetDatas[value[i][0]]);
                } else {
                    query[index] = saveGetDatas[value[i][0]];
                }
            }
        } 
    };
    afterdelete = function (index, sc) {
        let count = 0;
        query[0] = resetDatas[index];
        ButtonsAddEventList(query,
            '/AdministartorsViews/StuStatusDeleteAdmin', CreateStuStatusAdministratorTable);
    }
    let contextMenu = {
        callback: function () {
            //任务
            //添加一行dsaveGetDatas数组中我们需要push一个
            console.log(5);
            //然后我们需要修改后端的逻辑，当没有找到相关ID实体是我们在数据中Add一个
        },
        items: {
            "row_below": {
                name: "向下插入一行"
            },
            "remove_row": {
                name: "删除选中行"
            }
        }
    };
    CreateTable(container, data, colWidths, colHeaders,
        afterChange, columns, contextMenu, afterdelete);
}

//创建图书管理者可看的表格
function DealWithDataForStuAdmins(datas) {
    let currentIndex = 0;
    let data = [];
    //如果不支持foreach那么就说明它是json格式，我们将其转换
    
    datas.forEach(x => {
        data[currentIndex] = {
            ID: datas[currentIndex].ID,
            Name: datas[currentIndex].Name,
            Author: datas[currentIndex].Author,
            DataAdded: datas[currentIndex].DataAdded,
            PublishingHouse: datas[currentIndex].PublishingHouse,
        }
        currentIndex++;
    });
    return data;
}
//创建图书管理LibrayManagetnTable
function CreateLibrayManagentTalbe(datas, href) {
    if (datas.forEach == undefined) {
        datas = JSON.parse(datas);
    }
    HtmlReset();//每次创建表格时我们都必须要进行一次页面的重置
    let data = [];//创建表格的数据
    data = DealWithDataForStuAdmins(datas);
    query = [];
    let copy = JSON.stringify(datas);
    saveGetDatas = JSON.parse(copy);//深拷贝临时数据
    resetDatas = datas;//获取源数据，并引用源数据
    ButtonsAddEventList(query, "/AdministartorsViews/LibrayManagent", CreateLibrayManagentTalbe);
    container = document.getElementById("handsontable-code");
    devWidth = $('.views').width();
    colWidths = devWidth / 5;
    colHeaders = ['书号', "书名", "作者", "上架日期", "出版社"];
    columns = [
        {
            data: "ID",
            readOnly: true
        },
        {
            data: "Name"
        }, {
            data: "Author"
        }, {
            data: "DataAdded"
        }, {
            data: "PublishingHouse"
        }
    ];

    afterChange = function (value, source) {
        let count = 0;
        if (source == "edit") {
            let va = {};
            let properityName;
            let modifyValue;
            console.log(value);
            for (let i = 0; i < value.length; i++) {
                properityName = value[i][1];
                modifyValue = value[i][3];
                let index;
                if (properityName in saveGetDatas[value[i][0]]) {
                    saveGetDatas[value[i][0]][properityName] = modifyValue;
                }
                index = ValidateQuerYArrayHasElement(datas[value[i][0]].ID, query, "ID");
                if (index < 0) {
                    query.push(saveGetDatas[value[i][0]]);
                } else {
                    query[index] = saveGetDatas[value[i][0]];
                }
            }
        }
    };
    afterdelete = function (index, sc) {
        let count = 0;
        let i = 0;
        for (let k = index; k < index + sc; k++,i++) {
            query[i] = resetDatas[k]
        }
        ButtonsAddEventList(query, '/AdministartorsViews/LibrayManagentDelete', CreateLibrayManagentTalbe);
    }
    afterAddRow = function (index, sc) {
        let count = 0;
        let que = [];
       
        que[0] = data[index];
        ButtonsAddEventList(que, '/AdministartorsViews/InserLibrayManagent', CreateLibrayManagentTalbe);
    }
    let contextMenu = {
        callback: function () {
            //任务
            //添加一行dsaveGetDatas数组中我们需要push一个
            console.log(5);
            //然后我们需要修改后端的逻辑，当没有找到相关ID实体是我们在数据中Add一个
        },
        items: {
            "row_below": {
                name: "向下插入一行"
            },
            "remove_row": {
                name: "删除选中设备"
            }
        }
    };
    CreateTable(container, data, colWidths, colHeaders,
        afterChange, columns, contextMenu, afterdelete, afterAddRow);
}
//创建表格通过传递一些参数设置表格
function CreateTable(container, data, colWidths,
    colHeaders, afterChange, columns, contextMenu, afterdelete, afterAddRow) {
    let hot = new Handsontable(container,
        {
            data: data,
            width: "100%",
            colWidths: colWidths,
            colHeaders: colHeaders,
            afterChange: afterChange,//修改后
            afterRemoveRow: afterdelete,//删除后
            afterCreateRow: afterAddRow,
            columns:columns,
            className:
                "htCenter htMiddle",
            manualRowMove: false,
            contextMenu:contextMenu
        });
    //删除未激活的提醒事项
    $('#hot-display-license-info').remove();
}