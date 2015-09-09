app.config(["$stateProvider", "$urlRouterProvider", "$httpProvider", 'USER_ROLES',
function ($stateProvider, $urlRouterProvider, $httpProvider, USER_ROLES) {

    $urlRouterProvider.when("", "Dashboard");


    $stateProvider

        .state("WMGException", {
            url: "/WMGException",
            views: {
                "": { templateUrl: "Views/Dashboard/WMGException.html" },
                "list_menu": { templateUrl: "App/Admin/Views/WMGException.html" }
            },
            data: {
                authorizedRoles: [USER_ROLES.admin, USER_ROLES.admin]
            }
        })


        .state("Dashboard", {
            url: "/Dashboard",
            views: {
                "": { templateUrl: "Views/Dashboard/Dashboard.html" },
                "list_menu": { templateUrl: "App/Admin/Views/AppView.html" }
            },
            data: {
                authorizedRoles: [USER_ROLES.admin, USER_ROLES.editor]
            }
        })
        .state("Profile", {
            url: "/Profile",
            templateUrl: "Views/UAPortal/Profile.html",
            data: {
                authorizedRoles: [USER_ROLES.admin, USER_ROLES.editor, USER_ROLES.guest]
            }
        })
      
        .state("MyWindow", {
            url: "/MyWindow",
            templateUrl: "Views/UAPortal/iFrameWindow.html",
            data: {
                authorizedRoles: [USER_ROLES.admin, USER_ROLES.editor]
            }
            })
    
            
         /*   .state("Login", {
                url: "/Login",
                data: {
                    authorizedRoles: [USER_ROLES.admin, USER_ROLES.editor]
                },
                templateUrl: '<div></div>'
            })*/
           
            .state("Login-Top-Secret", {
                url: "/Login-Top-Secret",
                templateUrl: "Views/LoginV2/Login.html",
                controller: "LoginTopSecretCtrl"

            })
            
            .state("Login-Adent", {
                url: "/Login-Adent",
                templateUrl: "Views/LoginV2/Login.html",
                controller: "LoginTopSecretCtrl"

            })

    }
]);