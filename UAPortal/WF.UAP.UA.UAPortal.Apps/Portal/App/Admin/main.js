
angular.module("App.Admin", ['ui.router', 'oc.lazyLoad'])
.config(function ($stateProvider, USER_ROLES, $ocLazyLoadProvider) {
    $stateProvider

           .state("Debug-Search-User", {
               url: "/Debug-Search-User", views: {
                   "": {
                       controller: 'RegistrationCtrl',
                       templateUrl: 'App/Admin/Views/UserRegister/SearchApproverUser.html'
                   }
               }, data: { authorizedRoles: [USER_ROLES.admin, USER_ROLES.editor] }, resolve: {
                   loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                       return $ocLazyLoad.load([

                                     'App/Admin/Scripts/AdminCtrl.js'
                       ])

                   }]
               }
           })

        .state("Debug-UI-TestPage", {
            url: "/Debug-UI-TestPage", views: {
                "": {
                    controller: '',
                    templateUrl: 'App/Admin/Views/UITestPage.html'
                }
            }, data: { authorizedRoles: [USER_ROLES.admin, USER_ROLES.editor, USER_ROLES.guest] }
        })
          .state("Debug-User-URL", {
              url: "/Debug-User-URL", views: {
                  "": {
                      controller: '',
                      templateUrl: 'App/Admin/Views/MenuLink.html'
                  }
              }, data: { authorizedRoles: [USER_ROLES.admin, USER_ROLES.editor, USER_ROLES.guest] }
          })


          .state("Debug-Route-View", {
              url: "/Debug-Route-View", views: {
                  "": {
                      controller: '',
                      templateUrl: 'App/Admin/Views/RouterView.html'
                  }
              }, data: { authorizedRoles: [USER_ROLES.admin, USER_ROLES.editor, USER_ROLES.guest] }
          })


          .state("Debug-Publish-App", {
              url: "/Debug-Publish-App", views: {
                  "": {
                      controller: 'PublishAppCtrl',
                      templateUrl: 'App/Admin/Views/PublishApp.html'
                  }
              }, data: { authorizedRoles: [USER_ROLES.admin, USER_ROLES.editor, USER_ROLES.guest] }
              , resolve: {
                  loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                      return $ocLazyLoad.load([
                                    'Content/js/plugin/dropzone/dropzone.min.js',
                                    'Content/js/plugin/jcrop/jquery.Jcrop.min.js',
                                    'Content/js/plugin/jcrop/jquery.color.min.js',
                                    'App/Admin/Scripts/AdminCtrl.js'
                      ])

                  }]
              }
          })

         
          .state("MenuLink", {
              url: "/MenuLink", views: {
                  "": {
                      controller: '', 
                      templateUrl: 'App/Admin/Views/MenuLink.html'
              }
              },data: {authorizedRoles: [USER_ROLES.admin, USER_ROLES.editor, USER_ROLES.guest]}
          })


         
          .state("Debug-User-Registration", {
              url: "/Debug-User-Registration", views: {
                  "": {
                      controller: 'RegistrationCtrl',
                      templateUrl: 'App/Admin/Views/UserRegister/Registration.html'
                  }
              }, data: { authorizedRoles: [USER_ROLES.guest, USER_ROLES.editor] }, resolve: {
                  loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                      return $ocLazyLoad.load([
                                    
                                    'App/Admin/Scripts/AdminCtrl.js'
                      ])

                  }]
              }
          })

          .state("ViewApp", {
              url: "/ViewApp", views: {
                  "": {
                      controller: '', 
                      templateUrl: 'App/Admin/Views/AppView.html'
                  }
              }, data: { authorizedRoles: [USER_ROLES.admin, USER_ROLES.editor, USER_ROLES.guest] }, resolve: {
                  loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                      return $ocLazyLoad.load('');
                  }]
              }
          })
          .state("Debug", { 
              url: "/Debug", views: {
                  "": {
                      controller: '', 
                      templateUrl: 'App/Admin/Views/Debug.html'
                  }
              }, data: { authorizedRoles: [USER_ROLES.admin, USER_ROLES.editor, USER_ROLES.guest] }, resolve: {
                  loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                      return $ocLazyLoad.load('');
                  }]
              }
          }) 
}
    
);