﻿$(document).ready(function () { "use strict"; $("#products-datatable11").DataTable({ language: { paginate: { previous: "<i class='mdi mdi-chevron-left'>", next: "<i class='mdi mdi-chevron-right'>" }, info: "", lengthMenu: '显示个数： <select class=\'custom-select custom-select-sm ml-1 mr-1\'><option value="10">10</option><option value="20">20</option><option value="-1">全部</option></select> ' }, pageLength: 10, columns: [{ orderable: !1, render: function (e, o, l, t) { return "display" === o && (e = '<div class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input dt-checkboxes"><label class="custom-control-label">&nbsp;</label></div>'), e }, checkboxes: { selectRow: !0, selectAllRender: '<div class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input dt-checkboxes"><label class="custom-control-label">&nbsp;</label></div>' } }, { orderable: !0 }, { orderable: !0 }, { orderable: !0 }, { orderable: !0 }, { orderable: !0 }, { orderable: !0 }, { orderable: !0 }, { orderable: !0 }, { orderable: !0 }, { orderable: !0 }, { orderable: !0 }, { orderable: !0 }], select: { style: "multi" }, order: [[1, "asc"]], drawCallback: function () { $(".dataTables_paginate > .pagination").addClass("pagination-rounded") } }) });