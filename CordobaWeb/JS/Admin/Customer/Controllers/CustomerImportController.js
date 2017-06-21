app.controller('CustomerImportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions

    decodeParams($stateParams);
    BindToolTip();
    Tab();
    
    
    $scope.LoggedInUserId = 0;

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                       .withOption('bDestroy', true)
                       .withOption("deferRender", true);

    $scope.PageTitle = "Customer Import";

    $scope.store_id = 0;

    $scope.GetStoreList = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreId=" + $scope.store_id + '&LoggedInUserId=' + $scope.LoggedInUserId)
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

    $scope.GetStoreList();


    $scope.GetCustomerGroupList = function () {
        $http.get(configurationService.basePath + "api/CustomerGroupApi/GetCustomerGroupList?StoreId=" + $scope.store_id + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {

              if (response.data.length > 0) {
                  $scope.CustomerGroupList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetCustomerGroupList();




    //Import xls/xlsx file

    //$scope.uploadFile = function (file) {

    //    if (file == null) {
    //        alert("Please Select File");
    //    }

    //    var filename = file.type;
    //    var allowed = ["xls", "xlsx"];
    //    var found = false;
    //    allowed.forEach(function (extension) {
    //        if (filename.match('Document/' + extension)) {
    //            found = true;
    //        }
    //    });

    //    if (!found) {
    //        $scope.docFile = null;
    //        alert('file type should be .xls, .xlsx');
    //        return;
    //    }
    //    else {
    //        file.upload = Upload.upload({
    //            $http.post(configurationService.basePath + "api/CustomerApi/InsertUpdateCustomer?store_id="+$scope.store_id+'&customer_group_id='+ $scope.customer_group_id)
                
    //        });
    //        file.upload.then(function (response) {

    //            if (response.data.IsSuccess == true) {
    //                $scope.docFile = null;
    //            }
    //            //showNotification(response.data.IsSuccess, response.data.Message);
    //        });
    //    }
    //}
    $scope.CustomerImport = function ()
    {
        debugger;
        if (!($scope.store_id >=0) || $scope.files.length ==0)
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

        xhr.open("POST", configurationService.basePath + "api/CustomerApi/CustomerImport?store_id=" + $scope.store_id + "&LoggedInUserId=" + $scope.LoggedInUserId + "&customer_group_id=" + $scope.CustomerImportObj.customer_group_id);

        $scope.progressVisible = true;

        xhr.onreadystatechange = function () {         
            if (xhr.readyState == 4) {
                if (xhr.status == 200) {
                    if (xhr.responseText == -1)
                    {
                        toastr.error("Here your Email msg.");
                    }
                    if (xhr.responseText > 0)
                    {
                        toastr.success("File Successfully Submitted.");
                    }
                   
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




    
});