$(document).ready(function () {
    $(".draggable-item").draggable({
        revert: "invalid",
        cursor: "move",
    });

    var droppedItemsIds = [];

    function updateCarsContainer() {
        $.ajax({
            url: "/Cars/Index",
            type: "POST",
            data: { brandIds: droppedItemsIds },
            success: function (data) {
                if (data && data.cars && Array.isArray(data.cars)) {
                    $(".carscontainer table tbody").empty();
                    $.each(data.cars, function (carIndex, car) {
                        var newRow = $("<tr>");

                        var imgLogo = car.model.brand.logo;
                        $("<td>").append($("<img>").attr("src", imgLogo).addClass("brandlogo")).appendTo(newRow);

                        $("<td>").text(car.model.brand.name).appendTo(newRow);
                        $("<td>").text(car.model.name).appendTo(newRow);
                        $("<td>").text(car.year).appendTo(newRow);
                        $(".carscontainer table tbody").append(newRow);
                    });
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
});
