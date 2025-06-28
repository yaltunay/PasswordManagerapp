// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Şifre üretici fonksiyonu
function generatePassword(length = 12) {
    const charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
    const charsetLength = charset.length;
    let password = Array.from({ length }, () =>
        charset[Math.floor(Math.random() * charsetLength)]
    ).join('');

    $('#passwordInput').val(password); // jQuery ile doğrudan set et
}

// Sayfa yüklendiğinde
$(function () {
    $('.toast').toast('show');
});