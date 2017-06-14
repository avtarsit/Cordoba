app.controller('PointsImporterController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion  
    $scope.IsSendEmail = false;
    $scope.store_id="";
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.PageTitle = "Customer Points Importer";

    function GetStoreList() {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreID=0")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.StoreList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


 
    $scope.PointsImporter =function()
    {
        debugger;
        if ($scope.store_id == '' || $scope.files==undefined)
        {
            toastr.error("Select Store & file");
            return;
        }
            
        var fd = new FormData();
        for (var i in $scope.files) {
            fd.append("uploadedFile", $scope.files[i]);
        }
        var xhr = new XMLHttpRequest();
        //xhr.upload.addEventListener("progress", uploadProgress, false);
        xhr.addEventListener("load", uploadComplete, false);
        xhr.addEventListener("error", uploadFailed, false);
        xhr.addEventListener("abort", uploadCanceled, false);

        xhr.open("POST", configurationService.basePath + "api/CustomerApi/PointsImporter?store_id=" + $scope.store_id + "&IsSendEmail=" + $scope.IsSendEmail);

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
        checkfile(element);
        if (!checkfile(element))
        {
            return;
        }
        $scope.$apply(function ($scope) {
            $scope.files = [];
            var validExts = new Array(".xlsx", ".xls");
            for (var i = 0; i < element.files.length; i++) {
                $scope.files.push(element.files[i]);
            }
            $scope.progressVisible = false
        });
    };

    function checkfile(sender) {
        var validExts = new Array(".xlsx", ".xls");
        var fileExt = sender.value;
        fileExt = fileExt.substring(fileExt.lastIndexOf('.'));
        if (validExts.indexOf(fileExt) < 0) {
            toastr.error("Invalid file selected, valid files are of " +
                     validExts.toString() + " types.");
            return false;
        }
        else return true;
    }

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


    GetStoreList();
});