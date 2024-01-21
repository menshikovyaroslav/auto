var currentIndex = 0;
var cars = null;
var currentcar = null;

$(document).ready(function () {

    var page = 1;

    $(".draggable-item").draggable({
        revert: "invalid",
        cursor: "move",
    });

    var droppedItemsIds = [];

    function updateCarsContainer() {
        $.ajax({
            url: "/Cars/Index",
            type: "POST",
            data: { brandIds: droppedItemsIds, page: page },
            success: function (data) {
                if (data && data.cars && Array.isArray(data.cars)) {
                    $(".carscontainer table tbody tr.cartr").remove();

                    cars = data.cars;

                    $.each(data.cars, function (carIndex, car) {
                        var newRow = $("<tr>").addClass("cartr");

                        var carphoto = car.photos[0].path;
                        var carid = car.id;
                        $("<td>").css("max-width", "100px").append($("<div>").attr("data-car-id", carid).attr("data-image", carphoto).addClass("photo").append($("<img>").attr("src", carphoto).addClass("carphotosmall"))).appendTo(newRow);



                        var brandLogo = car.model.brand.logo;
                        var brandContainer = $("<div>").css({
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "center"
                        });
                        brandContainer.append($("<img>").attr("src", brandLogo).addClass("brandlogo"));
                        brandContainer.append(car.model.brand.name);
                        $("<td>").append(brandContainer).appendTo(newRow);

                        $("<td>").text(car.model.name).appendTo(newRow);
                        $("<td>").text(car.year).appendTo(newRow);
                        $("<td>").text(car.distance).appendTo(newRow);

                        var colorItem = $("<div>").addClass("color-item");
                        var colorCircle = $("<div>").addClass("color-circle").css("background-color", car.color.name);
                        var colorSpan = $("<span>").text(car.color.name);
                        colorItem.append(colorCircle, colorSpan);
                        $("<td>").append(colorItem).appendTo(newRow);

                        $(".carscontainer table tbody").append(newRow);
                    });

                    var paginationBlock = $("<section>").attr("id", "paginationBlock").empty();

                    if (data.pageViewModel.hasPreviousPage) {
                        var previousPageUrl = "@Url.Action('Index', 'Cars', new { page = data.pageViewModel.pageNumber - 1 })";
                        paginationBlock.append($("<a>").attr("href", "Index/").attr("asp-route-page", data.pageViewModel.pageNumber - 1).addClass("pagination-item").text(data.pageViewModel.pageNumber - 1));
                    }

                    paginationBlock.append($("<span>").addClass("pagination-item").text(data.pageViewModel.pageNumber));

                    if (data.pageViewModel.hasNextPage) {
                        var brandIdsString = droppedItemsIds.join(',');

                        var nextPageUrl = carsUrl +
                            '?page=' + (data.pageViewModel.pageNumber + 1) +
                            '&brandIds=' + brandIdsString;
                        paginationBlock.append($("<a>").attr("href", nextPageUrl).addClass("pagination-item").text(data.pageViewModel.pageNumber + 1));
                    }


                    $("#paginationBlock").html(paginationBlock.html());

                } else {
                    console.log("Unexpected data structure received from the server");
                }
            },
            error: function (error) {
                // Handle AJAX request errors
            }
        });
    }

    $(".droppable-area-stakan").droppable({
        accept: ".draggable-item",
        drop: function (event, ui) {
            var droppedItem = ui.helper;
            var droppedItemId = droppedItem.data("id");
            if (droppedItemsIds.indexOf(droppedItemId) === -1) {
                droppedItemsIds.push(droppedItemId);
                updateCarsContainer();
            }
        }
    });

    $(".droppable-area-brands").droppable({
        accept: ".draggable-item",
        drop: function (event, ui) {
            var droppedItem = ui.helper;
            var droppedItemId = droppedItem.data("id");
            var index = droppedItemsIds.indexOf(droppedItemId);
            if (index !== -1) {
                droppedItemsIds.splice(index, 1);
                updateCarsContainer();
            }
        }
    });

    $('#nextButton').on('click', function () {
        currentIndex = (currentIndex + 1) % currentcar.photos.length;

        var imagePath = currentcar.photos[currentIndex].path;
        $('#imageModal img').attr('src', imagePath);
    });

    $('#prevButton').on('click', function () {
        currentIndex = (currentIndex - 1 + currentcar.photos.length) % currentcar.photos.length;

        var imagePath = currentcar.photos[currentIndex].path;
        $('#imageModal img').attr('src', imagePath);
    });

});


$(document).on('click', '.photo', function () {

    var carId = $(this).data('car-id');

    currentcar = cars.find(function (car) {
        return car.id === carId;
    });

    var imagePath = currentcar.photos[currentIndex].path;
    $('#imageModal img').attr('src', imagePath);

    $('#imageModal').modal('show');

});
