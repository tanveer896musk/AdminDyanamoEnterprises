
let materialId = 0;

$(document).on('click', '.edit-material', function (e) {
    debugger
    e.preventDefault();
    let id = $(this).data('id');
    let name = $(this).data('name');

    $('#editMaterialId').val(id);
    $('#editMaterialName').val(name);
    $('#saveButton').text('Update'); // Optional: change button text
});


$(document).on('click', '.delete-material', function () {
    categoryId = $(this).data('id');
    let name = $(this).data('name');
    $('#deleteItemName').text(name);
    $('#deleteModal').modal('show');
});

$('#confirmDeleteBtn').click(function () {
    $.ajax({
        url: '/MaterialType/Delete',
        type: 'POST',
        data: {
            id: materialId,

        },
        success: function (response) {
            if (response.success) {
                $('#deleteModal').modal('hide');
                location.reload(); // or remove the row using JS
            } else {
                alert("Error deleting");
            }
        },
        error: function () {
            alert("Something went wrong.");
        }
    });
});
