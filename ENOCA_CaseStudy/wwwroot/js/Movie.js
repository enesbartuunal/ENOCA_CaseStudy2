$(function () {
    $(".delete").click(function () {
        let id = $(this).attr("id");
        Swal.fire({
            title: "Bu öğrenci kaydı silinecek.Enin misiniz?",
            showCancelButton: true,
            confirmButtonText: "Tamam",
            cancelButtonText: "İptal"
        }).then(function (result) {
            if (result.isConfirmed) {
                $.ajax({
                    type: "Post",
                    dataType: "Json",
                    url: "/Movie/Delete",
                    data: { id: id },
                    success: function (response) {
                        Swal.fire({
                            title: "Kayıt Silindi!",
                            confirmButtonText: "Tamam"
                        }).then(function (result) {
                            if (result.isConfirmed)
                                location.reload();
                        });
                    }
                });
            }

        });
    });
});


