

'use strict';
var app = angular.module("CordobaApp", ["ui.router", "LocalStorageModule", "datatables", "ngFileUpload", "ngSanitize", 'ngAnimate', 'ngDragDrop', "textAngular"]);

GetLayoutName();
function GetLayoutName()
{
    $.ajax({
        url: window.location.origin + "/Home/GetLayoutName?HostName=" + window.location.hostname,
        async: false,
        success: function (data) {
            var LayoutName = data;            
            app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
        
                var HomeIndex = {
                    name: 'Home',
                    url: '/Home',
                    templateUrl: 'Templates/' + LayoutName + '/Home/index.cshtml'
                }
                , ShowCountry = {
                    name: 'ShowCountry',
                    url: '/ShowCountry',
                    templateUrl: 'Templates/' + LayoutName + '/Country/Index.html'
                }
                , ManageCountry = {
                    name: 'ManageCountry',
                    url: '/ManageCountry?CountryCd:countryCd',
                    templateUrl: 'Templates/' + LayoutName + '/Country/AddOrUpdateCountry.html'
                }
                , TestPage = {
                    name: 'TestPage',
                    url: '/TestPage',
                    templateUrl: 'Templates/TestPage/Index.html'
                }
                 , ShowCategory = {
                     name: 'ShowCategory',
                     url: '/ShowCategory',
                     templateUrl: 'Templates/' + LayoutName + '/Category/Index.html'
                 }
                , ManageCategory = {
                    name: 'ManageCategory',
                    url: '/ManageCategory?CategoryId:categoryId',
                    templateUrl: 'Templates/' + LayoutName + '/Category/ManageCategory.html'
                }
                 , ShowManufacturer = {
                     name: 'ShowManufacturer',
                     url: '/ShowManufacturer',
                     templateUrl: 'Templates/' + LayoutName + '/Manufacturers/Index.html'
                 }
                  , ManageManufacturer = {
                      name: 'ManageManufacturer',
                      url: '/ManageManufacturer?ManufacturerID:manufacturerID',
                      templateUrl: 'Templates/' + LayoutName + '/Manufacturers/ManageManufacturer.html'
                  }
                  , ShowSupplier = {
                      name: 'ShowSupplier',
                      url: '/ShowSupplier',
                      templateUrl: 'Templates/' + LayoutName + '/Supplier/Index.html'
                  }
                  , ManageSupplier = {
                      name: 'ManageSupplier',
                      url: '/ManageSupplier?SupplierID:supplierID',
                      templateUrl: 'Templates/' + LayoutName + '/Supplier/ManageSupplier.html'
                  }
                 , ShowStore = {
                     name: 'ShowStore',
                     url: '/ShowStore',
                     templateUrl: 'Templates/' + LayoutName + '/Store/Index.html'
                 }
                  , ManageStore = {
                      name: 'ManageStore',
                      url: '/ManageStore?StoreID:storeID',
                      templateUrl: 'Templates/' + LayoutName + '/Store/ManageStore.html'
                  }
                
                ;
                $stateProvider.state(HomeIndex);
                $stateProvider.state(ShowCountry);
                $stateProvider.state(ManageCountry);
                $stateProvider.state(ShowCategory);
                $stateProvider.state(ManageCategory);
                $stateProvider.state(ShowManufacturer);
                $stateProvider.state(ManageManufacturer);
                $stateProvider.state(ShowSupplier);
                $stateProvider.state(ManageSupplier);
                $stateProvider.state(ShowStore);
                $stateProvider.state(ManageStore);
                $stateProvider.state(TestPage);
                //any url that doesn't exist in routes redirect to '/'
                $urlRouterProvider.otherwise('/Home');
                //$locationProvider.html5Mode({
                //    enabled: true,
                //    requireBase: false
                //});

            })
             .run(function ($http, $rootScope, $location, $filter, $state, localStorageService) {
                 
                 
                 //var now1 = new Date();
                 //var GETLocalStorageDateTime = localStorageService.get("CurrentDateTime");
                 //var diff = (now1.getTime() - new Date(GETLocalStorageDateTime).getTime());

                 //if (diff > 10000) {

                 //    localStorageService.clearAll();
                 //    localStorageService.set("CurrentDateTime", now1);
                 //    if (document.location.origin.toLowerCase() != document.location.href.toString().toLowerCase()) {

                 //        location.href = document.location.origin;
                 //    }
                 //    //localStorageService.remove("loggedInUser");
                 //    //localStorageService.remove("ImpersonatedUserDetail");
                 //    //localStorageService.remove("NotificationMesseges");
                 //    //localStorageService.remove("userPermissions");
                 //    //localStorageService.remove("CurrentDateTime");

                 //    //if (localStorageService != undefined) {
                 //    //    for (var i = 0; i < localStorageService.keys(0).length; i++) {

                 //    //        if (localStorageService.keys(0)[i].split('-')[0] == 'pageName') {

                 //    //            localStorageService.remove(localStorageService.keys(0)[i]);
                 //    //        }
                 //    //    }
                 //    //    var now = new Date();
                 //    //    localStorageService.set("CurrentDateTime", now);

                 //    //}


                 //    //if (!document.location.origin &&  document.location.href.toString().toLowerCase().indexOf('home') == -1) {

                 //    //    location.href = document.location.origin;
                 //    //}



                 //}


                 //localStorageService.remove("NotificationMesseges");
                 //localStorageService.remove("userPermissions");
                 $rootScope.$state = $state;

                 $rootScope.GlobalDateFormat = 'MM/dd/yyyy';

                 //function InitGlobalVariables() {

                 //    $rootScope.CurrentUser = function () {
                 //        var user = localStorageService.get("loggedInUser");
                 //        if (user != null) {
                 //            return user;
                 //        }
                 //        return null;
                 //    };

                 //    $rootScope.CurrentUserInternalId = function () {
                 //        var user = $rootScope.CurrentUser();
                 //        if (user != null) {
                 //            return user.InternalId;
                 //        }
                 //        return 0;
                 //    };

                 //    $rootScope.CurrentUserIsAdmin = function () {
                 //        var user = $rootScope.CurrentUser();
                 //        if (user != null) { return user.IsAdmin; }
                 //        return false;
                 //    };

                 //}


                 //InitGlobalVariables();
                 //InitGloablEnums();

                 //$rootScope.isSubModuleAccessibleToUser = function (subModule, func) {
                 //    var permissions = [];
                 //     permissions = localStorageService.get("userPermissions");
                 //    if (subModule == 'default') {
                 //        return true;
                 //    }
                 //    if (permissions == null) {
                 //        var userPermissions = permissionService.GetUserPermissions(user.InternalId)

                 //        userPermissions.success(function (permissions) {
                 //            localStorageService.set("userPermissions", permissions);
                 //            permissions = localStorageService.get("userPermissions");                
                 //            return false;
                 //        });
                 //    }   
                 //    if (permissions != undefined && permissions != null)
                 //            {
                 //           if (permissions.length) {
                 //           return $filter('filter')(permissions, { SubModule: subModule, Function: func }, true).length >= 1;
                 //         }
                 //     }

                 //    else {
                 //        return true;
                 //    }

                 //}



                 $rootScope.$on('$locationChangeStart', function (event, next, current) {

                     var geturlparameters = next.toString().split('?')[1];
                     var isAlreadyDecoded = false;
                     if (geturlparameters != undefined) {
                         if (geturlparameters.indexOf('~') == -1)
                             isAlreadyDecoded = true;
                     }
                     if (isAlreadyDecoded) {
                         window.location.href = 'home/accessdenied';
                     }

                 });
                 // Redirect to login if route requires auth and you're not logged in
                 $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
                     $state.previous = fromState;
                     $state.previousParams = fromParams;
                     if (fromState.name != "") {
                         localStorageService.remove("previous");
                         localStorageService.set("previous", $state.previous);
                         localStorageService.remove("previousParams");
                         localStorageService.set("previousParams", $state.previousParams);
                     }
                     if (!(fromParams == toParams)) {
                         encodeParams(toParams);

                     }
                     // to track previous url
                     $state.previousHref = $state.href(fromState, fromParams)

                     //check for user authentication         


                     // check for authorization
                     //if (localStorageService.get("userPermissions") != null) {

                     //    var hasAccess = $filter('filter')(localStorageService.get("userPermissions"),
                     //        { SubModule: toState.submodule, Function: toState.submoduleFunction }, true).length >= 1;
                     //    if (toState.submodule == 'default') {
                     //        hasAccess = true;
                     //    }
                     //    if (!hasAccess) {
                     //        window.location.href = 'home/accessdenied';
                     //    }
                     //}
                     //else {
                     //    var user = $rootScope.CurrentUser();

                     //    if (user != null) {

                     //        var userPermissions = permissionService.GetUserPermissions(user.InternalId)

                     //        userPermissions.success(function (permissions) {
                     //            localStorageService.set("userPermissions", permissions);

                     //            var hasAccess = $filter('filter')(localStorageService.get("userPermissions"),
                     //        { SubModule: toState.submodule, Function: toState.submoduleFunction }, true).length >= 1;

                     //            if (toState.submodule == 'default') {
                     //                hasAccess = true;
                     //            }
                     //            if (!hasAccess) {
                     //                window.location.href = 'home/accessdenied';
                     //            }

                     //        });
                     //    }
                     //}
                 });
             });


        }
    });
}


app.factory("ShareData", function () {
    return { value: 0 };
});

app.config(function (localStorageServiceProvider) {
    localStorageServiceProvider
      .setStorageType('localStorage');

});
