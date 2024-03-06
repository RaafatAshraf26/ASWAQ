document.getElementById('myAnchor').addEventListener('click', function (event) {
    event.preventDefault();
    document.getElementById('successMessage').style.display = 'block';

    setTimeout(function () {
        document.getElementById('successMessage').style.display = 'none';
        window.location.href = 'FinalCheckOut';
    }, 2000);
});