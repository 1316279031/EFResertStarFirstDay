function createTable() {
    let invalidate = function name(value, callback) {
        setTimeout(function () {
            if (/^.+@@.+/.test(value)) {
                callback(true);
            } else {
                callback(false);
            }
        },
            300);
    }
    let container = document.getElementById("handsontable-code");
    let devWidth = $('.views').width();
    console.log(devWidth);
    let count = 0;
    let query = [];
    let hot = new Handsontable(container,
        {
            data: Xz,
            width: "100%",
            height: "540px",
            colWidths: devWidth / 4,
            colHeaders: ["账号", "邮箱", "申请权限", "是否冻结账户"],
            afterChange: function (value, source) {
                console.log(value); //value:修改后与修改前的对比数组
                console.log(source); //是修改还是加载的数据edit(修改)
                if (source == "edit") {
                    query[0] = Xz[value[0][0]];//获取修改的数据行的对象
                    query[0][value[0][1]] = value[0][3]; //改变我们的值
                    Xz[value[0][0]][value[0][1]] = value[0][3]; //改变原有集合数据
                    console.log(query[0]);
                    console.log(Xz[value[0][0]]);
                }
            },
            columns: [
                {
                    data: 'name',
                    readOnly: true
                }, {
                    data: 'age',
                    readOnly: true
                }, {
                    data: 'email',
                    readOnly: true,
                    validator: invalidate,
                    allowInvalid: false
                }, {
                    data: 'free',
                    type: 'checkbox'
                }
            ],
            dropdownMenu:
                ['remove_col', 'make_read_only', 'alignment'],
            className:
                "htCenter htMiddle",

            contextMenu:
            {
                items: {
                    remove_row: {
                        name: "删除"
                    }
                }
            },
            manualRowMove: false,
        });
}
let Xz = [
    {
        name: "黄伟",
        age: 123,
        email: "1316279031@qq.com",
        free: false
    }, {
        name: "黄伟",
        age: 123,
        email: "1316279031@qq.com",
        free: true
    }, {
        name: "黄伟",
        age: 123,
        email: "1316279031@qq.com",
        free: true
    }, {
        name: "黄伟",
        age: 123,
        email: "1316279031@qq.com",
        free: true
    }, {
        name: "黄伟",
        age: 123,
        email: "1316279031@qq.com",
        free: true
    }
];
