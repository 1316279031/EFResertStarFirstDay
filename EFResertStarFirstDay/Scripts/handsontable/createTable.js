function CreateTable(datas,href) {
    $('#OptionView').remove();
    let currentIndex = 0;
    let data = [];
    datas.forEach(x => {
        data[currentIndex] = {
            Account: datas[currentIndex].AdministratorAccount,
            Email: datas[currentIndex].CreateAdminitratorDetialDatas.Email,
            Authority: datas[currentIndex].CreateAdminitratorDetialDatas.AdministratorAuthority,
            IsFreeze: datas[currentIndex].CreateAdminitratorDetialDatas.IsFreeze
        }
        currentIndex++;
    });
    let invalidate = function name(value, callback) {
        setTimeout(function() {
                if (/^.+@@.+/.test(value)) {
                    callback(true);
                } else {
                    callback(false);
                }
            },
            300);
    }
    let container = document.getElementById("handsontable-code");
    console.log(container);
    let devWidth = $('.views').width();
    let count = 0;
    let query = [];
    let hot = new Handsontable(container,
        {
            data:data,
            width: "100%",
            height: "540px",
            colWidths: devWidth / 4,
            colHeaders: ["账号", "邮箱", "申请权限", "账户状态"],
            afterChange: function (value, source) {
                console.log(value); //value:修改后与修改前的对比数组
                console.log(source); //是修改还是加载的数据edit(修改)
                if (source == "edit") {
                    query[value[0][0]]=datas[value[0][0]];//获取修改的数据行的对象
                    query[0]["CreateAdminitratorDetialDatas"][value[0][1]] = value[0][3]; //改变我们的值
                    datas[value[0][0]]["CreateAdminitratorDetialDatas"][value[0][1]] = value[0][3]; //改变原有集合数据
                    count++;
                    if (count == 5) {
                        console.log(count);
                        AjaxSubmitData(query, href);
                        count = 0;
                    }
                }
            },
            columns: [
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
            ],
            dropdownMenu:
                ['remove_col', 'make_read_only', 'alignment'],
            className:
                "htCenter htMiddle",
            manualRowMove: false
        });
}

function AjaxSubmitData(data,href) {
    if (data == null || href == null) {
        alert("提交数据失败");
    }
    $.ajax({
        type: 'Post',
        url: href,
        data: { adminDatas: data },
        success: function(data, e) {
            alert(data);
        },
        error: function(e) {
            alert("数据保存出现异常");
        }
    });
}
