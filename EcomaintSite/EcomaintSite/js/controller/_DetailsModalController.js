//Hiển thị thông tin chi tiết dạng form
//Cấu trúc gọi hàm InfoDetails.fn.Show(header, row) header mãng chứa tên cột, row mãng chứa dữ liệu dòng chọn*@
//  $('#tbOrgUnit tbody').on('dblclick', 'tr', function () {
//    $(this).each(function () {
//        if (!this.rowIndex) return;
//        InfoDetails.fn.Show($('#tbOrgUnit thead tr th'), this.cells);
//        return false;
//    });
//});

define([], function () {
    var DefineProp = function (obj, key, value) {
        var config = {
            value: value,
            writable: true,
            enumerable: true,
            configurable: true
        };
        Object.defineProperty(obj, key, config);
    };
    var InfoDetails = {}
    InfoDetails.Init = function () {
        $('#tbDetail').on('click', function (e) {
            e.stopImmediatePropagation();
        });
    }
    InfoDetails.Show = function (header, row, type) {
        var data = Object.create(Object.prototype);
        var maxLength = 0;
        for (var i = 0; i < header.length; i++) {
            if (row[i].textContent == '') continue;
            DefineProp(data, header[i].textContent, row[i].textContent)
            if (maxLength < header[i].textContent.length) maxLength = header[i].textContent.length;
        }
        var div = $('#tbDetail')
        div.empty()
        var html = ''
        for (key in data) {
            if (data[key] == 'true' || data[key] == 'false') {
                div.append('<div class="row"><div class="col-lg-12"><div class="col-lg-4 col-xs-4 col-sm-4"><label>' + key + '</label></div><div class="col-lg-8 col-xs-8 col-sm-8"><label class="btn">' + (data[key] == 'true' ? '<i class="fa fa-check-square-o"></i>' : ' <i class="fa fa-square-o"></i>') + '</label></div></div></div>')
            }
            else {
                if (type == "edit") {
                    div.append('<div class="row"><div class="col-lg-12"><div class="col-lg-4 col-xs-4 col-sm-4"><label>' + key + '</label></div><div class="col-lg-8 col-xs-8 col-sm-8"><input type="text" class="form-control" value="' + data[key] + '</div></div>')
                }
                else {
                    div.append('<div class="row"><div class="col-lg-12"><div class="col-lg-4 col-xs-4 col-sm-4"><label>' + key + '</label></div><div class="col-lg-8 col-xs-8 col-sm-8"><label>' + data[key] + '</label></div></div></div>')
                }
            }
        }
        $('#myModalDetails').modal('show')
    }
    return { fn: InfoDetails };
});