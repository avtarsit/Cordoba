app.controller('ImportCatalogueController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = 0;
    $scope.LoggedInUserId = 0;

    $scope.CatalogueList = [];
    //#endregion  

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.PageTitle = "Import Catalogue";




 
    $scope.ImportCatalogue =function()
    {
        return;
        var fd = new FormData();
        for (var i in $scope.files) {
            fd.append("uploadedFile", $scope.files[i]);
        }

        var xhr = new XMLHttpRequest();
        //xhr.upload.addEventListener("progress", uploadProgress, false);
        xhr.addEventListener("load", uploadComplete, false);
        xhr.addEventListener("error", uploadFailed, false);
        xhr.addEventListener("abort", uploadCanceled, false);

       
        xhr.open("POST", configurationService.basePath + "api/ProductCatalogueApi/ImportCatalogue?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId);

        $scope.progressVisible = true;

        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4) {
                if (xhr.status == 200) {
                    toastr.success("File Successfully Submitted.");
                } else {
                    $scope.$apply(function () {
                        $scope.progress = "Improper data in file";
                    })
                    toastr.error("There is improper data in .xlsx  OR .xls file.");
                }
            }
        };
        xhr.onerror = function () {
            $scope.$apply(function () {
                $scope.progress = "Improper data in file";
            })

            toastr.error("There is improper data in .xlsx  OR .xls file.");

        };
        xhr.send(fd);
    }

    $scope.setFiles = function (element) {
        $scope.$apply(function ($scope) {
            $scope.files = [];
            for (var i = 0; i < element.files.length; i++) {
                $scope.files.push(element.files[i]);
            }
            $scope.progressVisible = false
        });
    };

    function uploadComplete(evt) {
        /* This event is raised when the server send back a response */
        //alert(evt.target.responseText);

    }

    function uploadFailed(evt) {
        alert("There was an error attempting to upload the file.");
    }

    function uploadCanceled(evt) {
        $scope.$apply(function () {
            $scope.progressVisible = false;
        })
        alert("The upload has been canceled by the user or the browser dropped the connection.");
    }

});