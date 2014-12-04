/*---------------------------------------------------------------------------------------------------------------------------------
' Prototipo:	GCSoft.Utilities
' Autor:        Ruben.Cobos
' Fecha:        11-Marzo-2013
'
' Descripción:	Compilación de funciones Javascript de uso general
'           
'----------------------------------------------------------------------------------------------------------------------------------*/

// Prototipos
String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, '') }


// Funciones

function focusControl(sIDControl) {
    var oControl = document.getElementById(sIDControl);

    oControl.focus();
    if (oControl.type == 'text' || oControl.type == 'password') { oControl.select(); }

}


function isEmail(stringEvalMail) {
    // Espacios en blanco
    stringEvalMail = stringEvalMail.trim();

    // Campo vacío
    if (stringEvalMail == '') { return false; }

    // Evaluar cadena
    return /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(stringEvalMail);

}

function validateLogin() {
    var txtEmail = document.getElementById('cntLoginBody_txtEmail').value.trim();
    var txtPassword = document.getElementById('cntLoginBody_txtPassword').value.trim();
    var oSpan = document.getElementById('spanMessage');

    var RegExPattern = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;

    // Campos vacíos
    if (txtEmail == '') {
        oSpan.innerHTML = 'Correo requerido';
        oSpan.style.display = "block";
        focusControl('cntLoginBody_txtEmail');
        return false;
    }

    if (txtPassword == '') {
        oSpan.innerHTML = 'Password requerido';
        oSpan.style.display = "block";
        focusControl('cntLoginBody_txtPassword');
        return false;
    }

    // Formato de correo
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(txtEmail) == false) {
        oSpan.innerHTML = 'Formato de correo incorrecto';
        oSpan.style.display = "block";
        focusControl('cntLoginBody_txtEmail');
        return false;
    }

    oSpan.style.display = "none";
    return true;
}

function validateRecoveryPassword() {
    var txtEmail = document.getElementById('cntLoginBody_txtEmail').value.trim();
    var oSpan = document.getElementById('spanMessage');

    // Campo vacío
    if (txtEmail == '') {
        oSpan.innerHTML = 'Correo requerido';
        oSpan.style.display = "block";
        focusControl('cntLoginBody_txtEmail');
        return false;
    }

    // Formato de correo
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(txtEmail) == false) {
        oSpan.innerHTML = 'Formato de correo incorrecto';
        oSpan.style.display = "block";
        focusControl('cntLoginBody_txtEmail');
        return false;
    }

    oSpan.style.display = "none";
    return true;
}
