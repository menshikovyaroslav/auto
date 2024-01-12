$(document).ready(function () {
    $('.photo').click(function () {
        var imageUrl = $(this).data('image');
        $('#imageModal').find('.modal-body img').attr('src', imageUrl);
        $('#imageModal').modal('show');
    });

    $('#brands').change(function () {
        var selectedBrandId = $(this).val();

        $.ajax({
            url: '/Moderator/Catalog/UpdateModels',
            type: 'POST',
            data: { brandId: selectedBrandId },
            success: function (data) {
                $('#models').empty();
                $.each(data, function (index, item) {
                    $('#models').append('<option value="' + item.id + '">' + item.name + '</option>');
                });
            },
            error: function () {
                console.log('Ошибка при выполнении AJAX-запроса');
            }
        });
    });
});