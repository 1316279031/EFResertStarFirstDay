//jQuery选择返回的节点对象都是引用类型(对象)
$(function () {
    function SetError(el, message) {
        $(el).data('errorMessage', message);
    }
    function ShowError(el) {
        let $el = $(el);
        //为了避免重复添加，故我们需要检查是否拥有同级error
        let $errorColleactions = $el.siblings('.error');
        let error = $(el).data('errorMessage');
        //0=false 取反位true 1=true 取反位false 只要平级元素==1则不会提娜佳span而是修改错误消息
        if (!$errorColleactions.length) {
            let $errorEl = $('<span class="error"></span>'); //div
            $errorEl.text(error);
            $errorColleactions = $errorEl.insertAfter($el); //append
        } else {
           $errorColleactions.text(error);
        }
    }
    //验证是否必须输入值且是否有值
    //true:代表不是必须的或者是必须的但值以填入
    function Validaterequired(el) {
        let $el = $(el);
        //console.log(el.required);
        if (el.required) {
            if ($el.val() == "" || $el.val() == null) {
                SetError(el,"您必须输入值");
                return false;
            }
        }
        return true;
    }

    function RemoveError(el) {
        let $el = $(el);
        let errorColleactions=$el.siblings('.error');
        if (errorColleactions.length) {
            errorColleactions.remove();
        }
    }
    //自定义验证对象；validate；
    let validate = {};//用于存放进行验证的一些对象
    let isValite;
    validate = {
        Name: function(el) {
            let valide = /^[\u4e00-\u9fa5]{2,5}$/.test(el.value);
            if (!valide) {
                SetError(el, "请输入您的姓名");
            }
            //为true代表通过。。。。
            return valide;
        },
        Class: function(el) {
            //
            let valide = /^[\d]{2}[\u4e00-\u9fa5]{2,8}[\d]{1,2}[\u4e00-\u9fa5]{1}$/.test(el.value);
            if (!valide) {
                SetError(el, "您的班级名称不符合规范");
            }
            //true通过
            return valide;
        },
        IdCard: function(el) {
            let valide = /^[0-9X]{18}$/.test(el.value);
            if (!valide) {
                SetError(el, "请输入正确的省份证号码");
            }
            return valide;
        },
        Telephone: function(el) {
            let valide = /^[0-9]{11}$/.test(el.value);
            if (!valide) {
                SetError(el, "请输入正确的手机号码");
            }
            this.CustomerTelephone = el.value;
            return valide;
        },
        PareventTelephone: function(el) {
            let valide = /^[0-9]{11}$/.test(el.value);
            if (!valide) {
                SetError(el, "请输入正确的手机号码且不能为本人号码");
                return false;
            } else if (this.CustomerTelephone == el.value) {
                SetError(el, "您输入的号码相同请修改");
                return false;
            }
            return true;
        },
        CustomerTelephone: 0
}
    //表单
    let form = document.forms.studentDetials;
    form.noValidate = true;
    //以上指示表单再提交时不进行默认的HTML5的一个验证
    let $form = $(form);
    //1.完成一个表单的验证js验证
    let elements = form.elements;
    form.reset();
    for (var i = 1; i < elements.length-1; i++) {
        $(elements[i]).on('blur', function () {
            let $this = $(this);
            if (Validaterequired(this) == false) {
                ShowError(this);
                isValite = false;
            } else if(this.name in validate){
                //自定义验证
                console.log(validate[this.name]);
                if (validate[this.name](this)==false) {
                    ShowError(this);
                    isValite = false;
                } else {
                    RemoveError(this);
                    isValite = true; 
                }
            } else {
                RemoveError(this);
                isValite = true;
            }
            validate[this.id] = isValite;
        });
    }
    let Modelvalidate = {}
    $form.on('submit',
        function(e) {
            for (id in validate) {
                if (!validate[id]) {
                    e.preventDefault();
                    this.reset();
                }
            }
        });
});