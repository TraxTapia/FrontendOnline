document.addEventListener("DOMContentLoaded", () => {
    SearchEligibility();
    //$('#txtElegibilidad').tagsinput({
    //    maxTags: 5,
    //});
})

function SearchEligibility() {
   
    $.ajax({
        //beforeSend: function () {
        //    $.blockUI({
        //        theme: true,
        //        //title: 'Procesando...',
        //        //message: '<div class="row"><div class="col-lg-10"><br /><p><img src="/SAPF/Content/assets/img/loading.gif" style="width: 35px;" /></p><p> Espere un momento por favor...</p><br /></div></div>',
        //        message: '<div class="row"><div class="col-lg-12"><br /><p style="font-size:small; text-align: center;"><img src="/SASE/Content/assets/img/loading.gif" style="width: 35px;" /></p><p style="font-size:small; text-align: center;"> Espere un momento por favor...</p><br /></div></div>',
        //        baseZ: 10000
        //    });
        //},
        url: '/TiendaOnline/GetListClientes',
        type: 'POST',
        data: '{}',
        success: function (data) {
            $.unblockUI();
            if (data != undefined || data != null) {
                let tableData = "";
                $.each(data.data, function (index, value) {
                    tableData += '<tr>';
                    tableData += '<td  style="font-size:small">' + value.id_cliente + '</td>';
                    tableData += '<td  style="font-size:small">' + value.nombre_cliente + '</td>';
                    //tableData += '<td  style="font-size:small"><i class="fa fa-calendar" aria-hidden="true"></i> ' + getDateIfDate(value.Fecha) + '</td>';
                    tableData += '<td  style="font-size:small"><i class="fa fa-user" aria-hidden="true"></i> ' + value.email_cliente + '</td>';
                    tableData += '<td  style="font-size:small"><i class="fa fa-user" aria-hidden="true"></i> ' + value.telefono_cliente + '</td>';
                    tableData += '<td  style="font-size:small"><i class="fa fa-user" aria-hidden="true"></i> ' + value.fecha_registro + '</td>';
                   
                    tableData += '</tr>';
                });
                $("#tbodyClientes").append(tableData);
            }
        },
        error: function (jqXHR, status, error) {
            $.unblockUI();
            swal({
                title: "¡Error!",
                text: 'Surgio un error inesperado. \n' + jqXHR.status + ' ' + jqXHR.statusText,
                icon: "warning",
                dangerMode: true
            });
        }
    });
}