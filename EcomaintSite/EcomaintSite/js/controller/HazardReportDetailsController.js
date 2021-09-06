    var ThemHazardDetails = (function (module, menu, modal) {
        function init() {
            $formThemHazardDetails = $("#formThemHazardDetails");
            anhienNV();
            checkHalf();
            bindFormAction();
        }
        function bindFormAction() {
            $("#btnCapNhatThuKy").on("click",
                function () {
                    if ($formThemHazardDetails.valid()) {
                        showLoadingOverlay();
                        $.ajax({
                            type: $formThemHazardDetails.prop("method"),
                            url: $formThemHazardDetails.prop("action"),
                            data: $formThemHazardDetails.serialize(),
                            success: function (response) {
                                if (!response.IsSuccess) {
                                    $.notify({ message: response.Messages }, { type: "danger" });
                                } else {
                                    $.notify({ message: response.Messages }, { type: "success" });
                                }
                            },
                            complete: function () {
                                $('#modalthuky').modal("hide");
                                //hideLoadingOverlay();
                            }
                        });
                    }
                    return false;
                });
        }
        return {
            init: init
        }
    })();
}