app.controller('ProfileController', function ($timeout,StoreSessionDetail,UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder) {
    //if (!(UserDetail.customer_id > 0)) {
    //    window.location.href = 'home/accessdenied';
    //}

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion


    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.GetCustomerDetails = function () {
        
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/CustomerDetailLayout?CustomerId=" + UserDetail.customer_id + "&StoreId=" + $scope.StoreDetailInSession.store_id)
        //$http.get(configurationService.basePath + "API/LayoutDashboardAPI/CustomerDetailLayout?CustomerId=9&StoreId=4")
        .then(function (response) { 
              $scope.GetCustomerDetailObj = response.data;            
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    $scope.uploadUserImage = function() {
        var data = new FormData();
        var files = $("#Image").get(0).files;
        if (files.length == 0) {
            notificationFactory.customError("Please select atleast one file.");
            return notificationFactory;
        }

        var filename = files[0].name;

        if (files.length > 0) {
            data.append("UploadedFile", files[0]);
            //console.log(data);
        }

        var ajaxRequest = $.ajax({
            type: "POST",
            url: configurationService.basePath + 'api/CustomerApi/UploadUserImage?customerImage_id=0&customer_id=' + $scope.customer_id,
            contentType: false,
            processData: false,
            data: data,
            //data: {
            //    data: data,
            //    banner: $scope.BannerImageObj[index]
            //},
            success: function (response) {
                notificationFactory.customSuccess("Store Image Upload Successfully.");
                $('#ImageUpload').val('');
                GetUserImage();
            },
            error: function (response) {
                notificationFactory.error("Error occur during image upload.");
            }
        });
    }




    $scope.SaveCustomerBasicDetails = function (form)
    {
        //debugger;
        $scope.ProfileForm.$submitted = true;
        if (form.$valid) {
            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/SaveCustomerBasicDetails_Layout?StoreId=" + $scope.StoreDetailInSession.store_id, $scope.GetCustomerDetailObj)
         .then(function (response) {
             if (response.data>0)
             {
                 $(".update-profile").hide();
                 $("#cancelProfile").hide();
                 $("#editProfile").show();
                 $(".edit-profile i").toggleClass("fa-pencil fa-close");
                 toastr.success("Profile Detail successfully updated.");
             }
             else
             {
                 toastr.error("Something wrong! Please try again later.");
             }            
            })
           .catch(function (response) {

           })
           .finally(function () {
 
           });          
        }
    }

    $scope.SaveChangedPassword=function(form)
    {
        //debugger;
        if (form.$valid) {
            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/SaveChangedPassword_Layout?StoreId=" + $scope.StoreDetailInSession.store_id, $scope.GetCustomerDetailObj)
                   .then(function (response) {
                       if (response.data > 0) {
                           toastr.success("Password successfully Changed.");
                       }
                       else {
                           toastr.error("Something wrong! Please try again later.");
                       }
                   })
                     .catch(function (response) {

                     })
                     .finally(function () {

                     });
        }
       
    }

    $scope.GetCustomerDetails();


});