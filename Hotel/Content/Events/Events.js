$(document).ready(function () {
    $('#edit_cliente').click(function () {
        var options = {
            target: '#upload_cliente',
            url: '/Cliente/Edit',
            type: 'post',
            success: $.unblockUI(),
        };
        $('.form_edit_cliente').ajaxForm(options);
    });
});

$(document).ready(function () {
    $('#cedula_titular').numeric();
    $('#Precio_habitacion').numeric();
    $('#Telefono_Personal').numeric();
    $('#Precio').numeric();
    $('#Total').numeric()
    $('#Abonar').numeric()
    $('#Restante').numeric()
    $('#Tel_personal').numeric();
    $('#Edad').numeric();
    $('#Identificacion').numeric();
    $('#Cantidad_items').numeric();
//    $('#link_create_acompaniantes').hide();
});
//***
function setCedula() {
    $cedula = $('#cedula_titular').val();
    $('#Identificacion').attr("value", $cedula.toString());
    $('#cedula_titular').attr("disabled", true);
    $('.verifica_cliente').hide();
};
$(document).ready(function () {
    $('#submit_acompaniante').click(function () {
        var options = {
            target: '#list_Acompaniantes',
            url: '/Acompaniante/Create',
            type: 'post',
            success: hide_form_Acampaniante,
        };
        $('.form-create').ajaxForm(options);
    })
});
// EDITAR Acompaniantes
$(document).ready(function () {
    $('#submit_edit_Acompaniantes').click(function () {
        var options = {
            target: '#list_Acompaniantes',
            url: '/Acompaniante/Edit',
            type: 'post',
            success: hide_edit_Acompaniantes,
        };
        $('.edit_Acompaniantes').ajaxForm(options);
    })
});
// oculto edit Acompaniantes
function hide_edit_Acompaniantes(){
    $('.edit_Acompaniantes').hide();
    $.unblockUI();
}
// oculto el form para crear Acompaniantes
function hide_form_Acampaniante() {
    //$('form[@action*="Cliente/Create"]').hide();
    $('.form-create').hide();
    $.unblockUI();
};

$(document).ready(function () {
    $('#create_cliente').click(function () {
        var option = {
            target: '#upload_cliente',
            //url: '/Cliente/Create',
            url: '/Cliente/Create',
            type: 'post',
            success: end_block_submit
        };
        $('.form-create-cliente').ajaxForm(option);
    });
});
function end_block_submit(){
    $.unblockUI();
}
// verif cedula
function verific_cedula() {
    alert("el valor de field es => " + $('#cedula_titular').val());
    return $('#cedula_titular').val();
}
$(document).ready(function () {
    $('#verificar_titular').click(function () {
        var option = {
            target: '#upload_cliente',
            url: '/Verificar_titular/verificar',
            type: 'post',
            success: hide_verificar_titular,
        };
        $('#link_create_acompaniantes').fadeIn('slow');
        $('.class_verifica_cedula').ajaxForm(option);
    });
});
function hide_verificar_titular() {
    $('#Identificacion').attr("value", $('#cedula_titular').val());
    $.unblockUI();
}
$(document).ready(function () {
    $("form").on("submit", function () {
        if ($(this).valid()) {
            $.blockUI({message: "ESTELA DEL MAR..!"});
        }
    });
});
$(document).ready(function(){
   $('#hide_Details').click(function(){
        $('.Details_cliente').fadeOut('slow');
   }) 
   $('#hide_Edit').click(function(){
        $('.class_editar').fadeOut('slow');
   }) 
});
function hide_view_EditCliente(){
    $('.Details_cliente').fadeOut('slow');
}
$(document).ready(function(){
    $('#Edit_cliente_details_reserva').click(function(){
        var options = {
            target: '#upload_cliente',
            url: '/Cliente/Edit',
            type: 'post',
            success: stop_blockSubmit,
        }
        $('.Edit_cliente').ajaxForm(options);
    });
});
function stop_blockSubmit(){
    $('.form_edit_cliente').fadeOut('slow');
    $.unblockUI();
}
function hide_verificar_titular() {
    $('#Identificacion').attr("value", $('#cedula_titular').val());
    $('#upload_cedula_titular').hide();
    $.unblockUI();
}
function hide_Details_Cliente(){
    $('#Details_cliente').fadeOut('slow');
}
function hide_index(){
    $('.insite_index').hide();
}
$(document).ready(function(){
    $('#show_disponibles').click(function(){
        var opt = {
            /*target: '#ListHabitacines',*/
            target: '#list_hab_disponibles',
            type: 'post',
            url: '/Habitacion/show_hab_disponibles',
            success: $.unblockUI,
        }     
//        alert("yap..!");
        $('.hab_disponibles').ajaxForm(opt);
    });
});
// *** GALERIA
function slider(){
    if ($('#slider img:visible').length == 0 || $('#slider img:last').is(':visible')){
        $('#slider img').fadeOut().first().fadeIn();
    } else {
        $('#slider img:visible').fadeOut(3000).next('img').fadeIn(3000);
    }
}
$(document).ready(function(){
    slider();
    setInterval(slider, 7000);
});
$(document).ready(function(){
    $('#create_articulo').click(function(){
        var options = {
            target: '#all_articulos',
            url: '/Articulo/Create',
            type: 'post',
        };
        $('.all_articulos').ajaxForm(options);
    });
});
$(document).ready(function(){
    $('#hide_all_create_art').click(function(){
        $('#all_create_art').fadeOut('slow');
    });
});
$(document).ready(function(){
    $('#hide_all_edit_art').click(function(){
    alert("aja..!");
        $('#all_edit_art').fadeOut('slow');
    });
});
$(document).ready(function(){
    $('#submit_query_cliente').click(function(){
        var opt = {
            target: '#load_details_cliente',
            url: '/Cliente/getDataCliente',
            type: 'post',
            success: end_block_submit,
        };
    $('.query_cliente').ajaxForm(opt);
    });    
});


//$(function(){
//$('#campo').keypress(function(e){
//        var code= (e.keyCode ? e.keyCode : e.which);
//        if (code == 13) alert('Enter key was pressed.');
//        
//    };
//    var option = {
//        target: "#load_details_cliente",
//        url: "/Cliente/getDataCliente",
//        type: "get",
//        $('.query_cliente').ajaxForm(option);
//    });
//});



