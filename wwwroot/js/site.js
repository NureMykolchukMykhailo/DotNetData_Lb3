// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Находим элементы управления ползунками
var minAgeSlider = document.getElementById('minAge');
var maxAgeSlider = document.getElementById('maxAge');

// Находим элементы для отображения текущих значений
var minAgeLabel = document.getElementById('minAgeLabel');
var maxAgeLabel = document.getElementById('maxAgeLabel');

// Отображаем текущие значения при загрузке страницы
minAgeLabel.innerHTML = minAgeSlider.value;
maxAgeLabel.innerHTML = maxAgeSlider.value;

// Обновляем отображаемые значения при изменении ползунков
minAgeSlider.addEventListener('input', function () {
    minAgeLabel.innerHTML = minAgeSlider.value;
});

maxAgeSlider.addEventListener('input', function () {
    maxAgeLabel.innerHTML = maxAgeSlider.value;
});

