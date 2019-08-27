var idTmr;
function getExplorer() {
    var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串
    var isOpera = userAgent.indexOf("Opera") > -1; //判断是否Opera浏览器
    var isIE = userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera; //判断是否IE浏览器
    var isEdge = userAgent.indexOf("Windows NT 6.1; Trident/7.0;") > -1 && !isIE; //判断是否IE的Edge浏览器
    var isFF = userAgent.indexOf("Firefox") > -1; //判断是否Firefox浏览器
    var isSafari = userAgent.indexOf("Safari") > -1 && userAgent.indexOf("Chrome") == -1; //判断是否Safari浏览器
    var isChrome = userAgent.indexOf("Chrome") > -1 && userAgent.indexOf("Safari") > -1; //判断Chrome浏览器

    //ie
    if (isIE || !!window.ActiveXObject || "ActiveXObject" in window) {
        return 'ie';
    }
    //firefox
    else if (isFF) {
        return 'Firefox';
    }
    //Chrome
    else if (isChrome) {
        return 'Chrome';
    }
    //Opera
    else if (isOpera) {
        return 'Opera';
    }
    //Safari
    else if (isSafari) {
        return 'Safari';
    }
}
function export_excel(tableid) {
    if (getExplorer() == 'ie') {
        var curTbl = document.getElementById(tableid);
        var oXL;
        try {
            oXL = new ActiveXObject("Excel.Application"); //创建AX对象excel
        } catch (e) {
            alert("无法启动Excel!\n\n如果您确信您的电脑中已经安装了Excel，" + "那么请调整IE的安全级别。\n\n具体操作：\n\n" + "工具 → Internet选项 → 安全 → 自定义级别 → 对没有标记为安全的ActiveX进行初始化和脚本运行 → 启用");
            return false;
        }

        var oWB = oXL.Workbooks.Add();
        var oSheet = oWB.ActiveSheet;
        var Lenr = curTbl.rows.length;
        for (i = 0; i < Lenr; i++) {
            var Lenc = curTbl.rows(i).cells.length;
            for (j = 0; j < Lenc; j++) {
                oSheet.Cells(i + 1, j + 1).value = curTbl.rows(i).cells(j).innerText;
            }
        }
        oXL.Visible = true;

        //导出剪切板上的数据为excel
        //        var curTbl = document.getElementById(tableid);
        //        var oXL = new ActiveXObject("Excel.Application"); //创建AX对象excel
        //        var oWB = oXL.Workbooks.Add();
        //        var xlsheet = oWB.Worksheets(1);
        //        var sel = document.body.createTextRange();
        //        sel.moveToElementText(curTbl);
        //        sel.collapse(true);
        //        sel.select();
        //        sel.execCommand("Copy");
        //        xlsheet.Paste();
        //        oXL.Visible = true;
        //
        //        try {
        //            var fname = oXL.Application.GetSaveAsFilename("Excel.xls", "Excel Spreadsheets (*.xls), *.xls");
        //        } catch (e) {
        //            console.log("Nested catch caught " + e);
        //        } finally {
        //            oWB.SaveAs(fname);
        //            oWB.Close(savechanges = false);
        //            oXL.Quit();
        //            oXL = null;
        //            idTmr = window.setInterval("Cleanup();", 1);
        //        }
    } else {
        tableToExcel(tableid)
    }
}
function Cleanup() {
    window.clearInterval(idTmr);
    CollectGarbage();
}

var tableToExcel = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,',
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function (s) {
            return window.btoa(unescape(encodeURIComponent(s)))
        },
        // 下面这段函数作用是：将template中的变量替换为页面内容ctx获取到的值
        format = function (s, c) {
            return s.replace(/{(\w+)}/g,
                function (m, p) {
                    return c[p];
                }
            )
        };
    return function (table, name) {
        if (!table.nodeType) {
            table = document.getElementById(table)
        }
        // 获取表单的名字和表单查询的内容
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML };
        // format()函数：通过格式操作使任意类型的数据转换成一个字符串
        // base64()：进行编码
        window.location.href = uri + base64(format(template, ctx))
    }
})()