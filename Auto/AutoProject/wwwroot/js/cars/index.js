$(document).ready(function () {

    $(".draggable-item").draggable({
        revert: "invalid",
        cursor: "move",
    });

    var droppedItemsIds = [];

    $(".droppable-area-stakan").droppable({
        accept: ".draggable-item",
        drop: function (event, ui) {
            var droppedItem = ui.helper;
            var droppedItemId = droppedItem.data("id");
            if (droppedItemsIds.indexOf(droppedItemId) === -1) {
                droppedItemsIds.push(droppedItemId);

                $.ajax({
                    url: "/Cars/Index",
                    type: "POST",
                    data: { brandIds: droppedItemsIds },
                    success: function (data) {
                        if (data && data.cars && Array.isArray(data.cars)) {
                            $(".carscontainer table tbody").empty();
                            $.each(data.cars, function (carIndex, car) {
                                var newRow = $("<tr>");
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

        }
    });

    $(".droppable-area-brands").droppable({
        accept: ".draggable-item",
        drop: function (event, ui) {
            var droppedItem = ui.helper;
            var droppedItemId = droppedItem.data("id");
            var index = droppedItemsIds.indexOf(droppedItemId);

            if (index === -1) {
            } else {
                droppedItemsIds.splice(index, 1);

                $.ajax({
                    url: "/Cars/Index",
                    type: "POST",
                    data: { brandIds: droppedItemsIds },
                    success: function (data) {
                        if (data && data.cars && Array.isArray(data.cars)) {
                            $(".carscontainer table tbody").empty();
                            $.each(data.cars, function (carIndex, car) {
                                var newRow = $("<tr>");
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
        }
    });
});