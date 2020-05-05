if ("alt" in window) {
    $('.button_login').click(function () {
        let name = $('#register_username').val();
        let pass = $('#register_password').val();
        alt.emit('Window:LoginClicked', name, pass);
    });
    $('.button_register').click(function () {
        let name = $('#register_username').val();
        let pass = $('#register_password').val();
        alt.emit('Window:RegisterClicked', name, pass);
    });
}