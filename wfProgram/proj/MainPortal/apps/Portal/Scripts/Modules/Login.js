var webstate = {
    menu: false,
    LoginScreen: false,
    referrerurl: null
}


angular.module('UAP.login', ['ui.router', 'ngStorage'])
    .config(function($httpProvider) {
        $httpProvider.interceptors.push([
            '$injector',
            function($injector) {
                return $injector.get('AuthInterceptor');
            }
        ]);
    })
    .factory('AuthResolver', function ($q, $rootScope, $state) {
    return {
        resolve: function () {
            var deferred = $q.defer();
            var unwatch = $rootScope.$watch('currentUser', function (currentUser) {
                if (angular.isDefined(currentUser)) {
                    if (currentUser) {
                        deferred.resolve(currentUser);
                    } else {
                        deferred.reject();
                        $state.go('Login');
                    }
                    unwatch();
                }
            });
            return deferred.promise;
        }
    };
})

.factory('AuthInterceptor', function ($rootScope, $q, AUTH_EVENTS) {
    return {
        responseError: function (response) {
            $rootScope.$broadcast({
                401: AUTH_EVENTS.notAuthenticated,
                403: AUTH_EVENTS.notAuthorized,
                419: AUTH_EVENTS.sessionTimeout,
                440: AUTH_EVENTS.sessionTimeout
            }[response.status], response);
            return $q.reject(response);
        }
    };
})
.run(function ($rootScope, AUTH_EVENTS, AuthService, $state) {
    $rootScope.$on('$stateChangeStart', function (event, next) {
        var authorizedRoles = next.data.authorizedRoles;
        if (authorizedRoles.indexOf("guest") != -1) {


            $rootScope.$broadcast(AUTH_EVENTS.withoutLogin);
        } 
        else if (!AuthService.isAuthorized(authorizedRoles)) {
           // event.preventDefault();
          if (AuthService.isAuthenticated()) {
                // user is not allowed
                $rootScope.$broadcast(AUTH_EVENTS.loginSuccess);
                //$rootScope.$broadcast(AUTH_EVENTS.notAuthorized);
            } else {
                // user is not logged in
               
                $rootScope.$broadcast(AUTH_EVENTS.notAuthenticated);

            }
        }
    });
})
.directive('formAutofillFix', function ($timeout) {
    return function (scope, element, attrs) {
        element.prop('method', 'post');
        if (attrs.ngSubmit) {
            $timeout(function () {
                element
                  .unbind('submit')
                  .bind('submit', function (event) {
                      event.preventDefault();
                      element
                        .find('input, textarea, select')
                        .trigger('input')
                        .trigger('change')
                        .trigger('keydown');
                      scope.$apply(attrs.ngSubmit);
                  });
            });
        }
    };
})
.directive('loginDialog', function (AUTH_EVENTS, $localStorage, Session, $state) {
    return {
        restrict: 'A',
        template: '<div ng-if="visible" ng-include="\'Views/LoginV2/Login.html\'">',
        link: function (scope,$state) {
            var showDialog = function () {
                webstate.menu = false;
                scope.visible = true;
                webstate.LoginScreen = true;
            };

            scope.visible = false;
            scope.$on(AUTH_EVENTS.notAuthenticated, showDialog);
              scope.$on(AUTH_EVENTS.loginFailed, function() {
                  showDialog();
            });
            scope.$on(AUTH_EVENTS.sessionTimeout, function() {
                showDialog();
            });
            scope.$on(AUTH_EVENTS.withoutLogin, function () {
                scope.visible = false;
                webstate.menu = true;
                webstate.LoginScreen = false;
            });
            scope.$on(AUTH_EVENTS.logoutSuccess, function () {
                var Storage = $localStorage.$default();
                Storage.$reset();
                Session.destroy();
             
                showDialog();
            });
            scope.$on(AUTH_EVENTS.loginSuccess, function () {
                scope.visible = false;
                webstate.menu = true;
                webstate.LoginScreen = false;
            })
        }
    };
})
.service('Session', function () {
    this.create = function (sessionId, userId, userName, userRole, linkAcesss, timeout) {
       
        webstate.menu = true;
    };
    this.destroy = function () {
        webstate.menu = false;
    };
    this.createByObject = function (res) {
        webstate.menu = true;
        this.userRole = res.role;
        UAPConfig.tokenId = res.token;
    };
    this.setSessionTimeOut = function(timeoutminute) {
        var dt = new Date();
        var newtimeout = new Date(dt.getTime() + timeoutminute * 60000);
        return newtimeout;
    };
})

        .factory('AuthService', function ($http, Session, $localStorage) {
            var authService = {};
            authService.templogin = function (credentials) {


            /*    var res = {};
                res.data = {};
                res.data.id = "100012020012";
                res.data.user = {};
                res.data.user.id = "7861";
                res.data.user.role = "admin";
                res.data.user.name = "Nasir Sayed";*/

                //var res = fakejson.users[0]; // admin rights
                //var res = fakejson.users[1]; // editor rights

                /* Temporary logic */
                var res = null;
                var LoginResponse = fakejson;
                res = LoginResponse.users[0];
                if (res.login == credentials.username) {
                    Session.createByObject(res);
                } else {
                    res = LoginResponse.users[1];
                    if (res.login == credentials.username) {
                        Session.createByObject(res);
                    } else {
                        res = LoginResponse.users[2];
                        if (res.login == credentials.username) {
                            Session.createByObject(res);
                        } else {
                            res = LoginResponse.users[3];
                            if (res.login == credentials.username) {
                                Session.createByObject(res);
                            } else {
                                return null;
                            }
                        }
                    }
                }
                
                return res;


            };

            authService.GetAccountModules = function (userinfo,tokenid) {
                var url = UAPConfig.baseurl + UAPConfig.AccountInfo;
                return $http({
                    method: 'POST',
                    url: url,
                    headers:{ Authorization: 'Bearer ' + tokenid, "Content-Type":"application/x-www-form-urlencoded" },
                    transformRequest: function (obj) {
                        var str = [];
                        for (var p in obj)
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                        return str.join("&");
                    },
                    data: userinfo
                }).success(function (res) {

                   
                    return res;
                }).error(function (er, status, code) {
                    if (status == 500) {
                        alert("500: Internal Server error");
                    }
                    SmallBox("UAP - " + er.error, "<b>" + er.error_description + "</b>", "error");

                    return null;
                });

            };
            authService.login = function (credentials) {
                var url = UAPConfig.baseurl + UAPConfig.TOKEN;
                return $http({
                    method: 'POST',
                    url: url,
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    transformRequest: function (obj) {
                        var str = [];
                        for (var p in obj)
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                        return str.join("&");
                    },
                    data: credentials
                }).success(function (res) {

                    
                    /*  var LoginResponse = fakejson;
                    res = LoginResponse.users[0];
                    if (res.login == credentials.username) {
                        Session.createByObject(res);
                    } else {
                        res = LoginResponse.users[1];
                        if (res.login == credentials.username) {
                            Session.createByObject(res);
                        } else {
                            res = LoginResponse.users[2];
                            if (res.login == credentials.username) {
                                Session.createByObject(res);
                            } else {
                                res = LoginResponse.users[3];
                                Session.createByObject(res);
                                
                            }
                        }
                    }*/

                     return res;
                }).error(function(er,status,code) {
                    if (status == 500) {
                        alert("500: Internal Server error");
                    }
                    SmallBox("UAP - " + er.error, "<b>" + er.error_description + "</b>", "error");

                    return null;
                });
          
            };
            authService.isSessionExpire = function (sessiontimeout) {
                var currenttime = new Date();
                 sessiontimeout = new Date(sessiontimeout);
                
                 if (currenttime > sessiontimeout) {
                    return false;

                } else {
                    return true;
                }
               

            };
        authService.isAuthenticated = function () {
                
                var res = $localStorage.user;
               
            if (null != res) {
                    if (authService.isSessionExpire(res.sessiontimeout) == false) {
                        // Session time out.. to do anything (nasir) - may 06 2015
                        return false;
                    }
                    else {
                        //sessio not time out so set menu visibility true and set new timeout watch
                        Session.createByObject(res);
                        res.sessiontimeout = Session.setSessionTimeOut(res.timeout);
                    }
                    return true;
                } else {
                    return false;
                }



                
            };

        authService.isAuthorized = function (authorizedRoles) {

            if (authorizedRoles.indexOf("guest") != -1) {
                return authService;
            }
            if (!angular.isArray(authorizedRoles)) {
                    authorizedRoles = [authorizedRoles];
                }
              
                return (authService.isAuthenticated() &&
                    authorizedRoles.indexOf(Session.userRole) !== -1);
            };

            return authService;
        })

.constant('USER_ROLES', {
    all: '*',
    admin: 'admin',
    editor: 'editor',
    guest: 'guest'
})

.constant('AUTH_EVENTS', {
    loginSuccess: 'auth-login-success',
    loginFailed: 'auth-login-failed',
    logoutSuccess: 'auth-logout-success',
    sessionTimeout: 'auth-session-timeout',
    notAuthenticated: 'auth-not-authenticated',
    notAuthorized: 'auth-not-authorized',
    withoutLogin: 'auth-without-login'
})
.factory('LoginFactory', function () {

    LoginInit();
    var serialize = function (obj) {
        var str = [];
        for (var p in obj)
            if (obj.hasOwnProperty(p)) {
                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
            }
        return str.join("&");
    }
    return {

        validatelogin: function () {
            var response = {};
            response.error = false;
            var username = $.trim($('#un_1').val());
            var password = $.trim($('#pw_1').val());
            if (username === '') {
                response.error = true;
                response.errmsg = "Please enter your username.";
            }
            else if (password === '') {
                response.error = true;
                response.errmsg = "Please enter your password.";
            }
            else {
                response.error = false;
            }

            if (response.error == true) {
                $('#form_1 .fa-user').removeClass('success').addClass('fail');
                $('#form_1').addClass('fail');
            } else {
                $('#form_1 .fa-user').removeClass('fail').addClass('success');
                $('#form_1').removeClass('fail').removeClass('animated');
            }
            return response;
        }
    };


})
.controller('LoginController', function ($scope, $rootScope, AUTH_EVENTS, AuthService, $state, LoginFactory, $localStorage, Session) {
    $scope.Verbiage = "Ad-Ent Login";
    LoginInit();
     
    $scope.credentials = {
        username: '',
        password: '',
        IsAdEntId: true,
        grant_type: "password",

    };
    $scope.LoadingFlag = false;

    $scope.$storage = $localStorage.$default({});
    $scope.login = function (credentials) {
        var resValid= LoginFactory.validatelogin();
        if (resValid.error == true) {
            SmallBox("UA-Portal", "<b>" + resValid.errmsg + "</b>", "error")
            return;
        }
        $scope.LoadingFlag = true;
        credentials.UserId = credentials.username;
        AuthService.login(credentials).then(function (res) {
            if (res == null) {
                $scope.LoadingFlag = false;

                $('#form_1 .fa-user').removeClass('success').addClass('fail');
                $('#form_1').addClass('fail');
               SmallBox("UA-Portal", "<b>Login Response is empty.</b>", "error");
               
                return;
           }
            else if (res != null && res.data.ErrorMessages != undefined) {
                  $scope.LoadingFlag = false;
                $('#form_1 .fa-user').removeClass('success').addClass('fail');
                $('#form_1').addClass('fail');
                SmallBox("", "<b>" + res.data.ErrorMessages.Message + "</b>", "error")

                $rootScope.$broadcast(AUTH_EVENTS.loginFailed);
                return;
            }
            var tokenId = res.data.access_token;
            UAPConfig.tokenId = tokenId;
           AuthService.GetAccountModules(credentials, tokenId).then(function (res) {

                $rootScope.$broadcast(AUTH_EVENTS.loginSuccess);

                /* Fake Menu */


                var user = $scope.fillUpUAPModel(res.data.AccountModel, tokenId);
                Session.createByObject(user);
                /* Fake Menu */


                $scope.setCurrentUser(user);
                $scope.$storage.user = user;
                $scope.$storage.user.sessiontimeout = Session.setSessionTimeOut($scope.$storage.user.timeout);
                if (webstate.referrerurl == '') webstate.referrerurl = "Dashboard";
                $state.go(webstate.referrerurl);
                $scope.LoadingFlag = false;

                SmallBox("", "<b>" + user.profile.displayname + "</b> signed in.", "success")
                // console.log(res);
                return res;
            }, function (err) {
                $scope.LoadingFlag = false;
                $('#form_1 .fa-user').removeClass('success').addClass('fail');
                $('#form_1').addClass('fail');
            
                $rootScope.$broadcast(AUTH_EVENTS.loginFailed);
            });


        }, function (err) {
            $scope.LoadingFlag = false;
            $('#form_1 .fa-user').removeClass('success').addClass('fail');
            $('#form_1').addClass('fail');

            $rootScope.$broadcast(AUTH_EVENTS.loginFailed);
        });

    };
    $scope.fillUpUAPModel = function (acctDetails, token) {
        var user = UAPModal;
        user.profile.FirstName = acctDetails.FirstName;
        user.profile.LastName = acctDetails.LastName;
        user.profile.displayname = acctDetails.FirstName + " " + acctDetails.LastName;
        user.profile.email = acctDetails.UserEmail;
        user.token = token;
        user.role = $scope.UserRoleDetect(acctDetails.UserRoles);
        user.profile.IsAdEntId = acctDetails.IsAdEntId;
        var modPrivilage = $scope.linkAccessSetup(acctDetails.ModulePrivileges);

        user.linkAccess = modPrivilage;
        user = $scope.AdminMenu(user);
        return user;
    };
    $scope.UserRoleDetect = function(role) {
        if (role == null) {
            return "admin";
        } else {
            return role;
        }
    };
    $scope.AdminMenu = function (user) {
        if (user.role == "admin") {
             user.linkAccess.push(UAPAdminMenu);
        }
        return user;
    };
    $scope.linkAccessSetup = function (json) {
        var newjson = [];


        function getAllChild(lookParentHier) {
            var childArr = new Array();
            $.each(json, function (i, j) {
                if (j.ParentHierarchy == lookParentHier) {
                    LoopIndexed.push(j.Hierarchy);
                    childArr.push(j);


                }

            });
            return childArr;
        }
        function traverse(o, func) {
            for (var i in o) {

                func.apply(this, [i, o[i]]);
                if (o[i] !== null && typeof (o[i]) == "object") {
                    //going on step down in the object tree!!
                    traverse(o[i], func);
                }
                if (GlobalParentID == "")
                    break;
            }
        }

        var GlobalParentID = "";
        var GlobalParentObj = [];
        var LoopIndexed = [];
        function process(key, value) {

            if (typeof value == "object" && value != null) {
                if (value.Hierarchy == GlobalParentID) {
                    GlobalParentObj = value;
                    GlobalParentID = "";
                }
            }
        }
        function findParent(parentID) {
            GlobalParentID = parentID
            traverse(newjson, process);

        }
        function unique(a) {
            a.sort();
            for (var i = 1; i < a.length;) {
                if (a[i - 1] == a[i]) {
                    a.splice(i, 1);
                } else {
                    i++;
                }
            }
            return a;
        }
        $.each(json, function (i, j) {
            console.log(json);
            //log(JSON.stringify(j.name));
            if (j.ParentHierarchy == "/") {
                var ParentItem = angular.copy(j);
                var childArr = new Array();
                childArr = getAllChild(j.Hierarchy);
                if (childArr != []) {
                    ParentItem.submenu = angular.copy(childArr);



                }
                newjson.push(ParentItem);

            }
            else {
                if (LoopIndexed.indexOf(j.Hierarchy) == -1) {

                    LoopIndexed.push(angular.copy(j.Hierarchy));
                    var ParentItem = angular.copy(j);
                    var childArr = new Array();
                    childArr = getAllChild(j.Hierarchy);

                    if (childArr.length != 0) {
                        ParentItem.submenu = angular.copy(childArr);



                    }
                    findParent(ParentItem.ParentHierarchy);
                    if (GlobalParentObj.submenu == undefined)
                        GlobalParentObj.submenu = [];
                    GlobalParentObj.submenu.push(ParentItem);

                }

            }
        });

        return newjson;
    }
    
    $scope.templogin = function (credentials) {
        credentials.grant_type = "password";
        credentials.IsAdEntId = true;
        if (LoginFactory.validatelogin()) {
            var user = AuthService.login(credentials);
            if (user == null) {
               
                $('#form_1 .fa-user').removeClass('success').addClass('fail');
                $('#form_1').addClass('fail');
               
                return;
            }
            $rootScope.$broadcast(AUTH_EVENTS.loginSuccess);
            $scope.setCurrentUser(user);
            $scope.$storage.user = user;
            $scope.$storage.user.sessiontimeout = Session.setSessionTimeOut($scope.$storage.user.timeout);
            if (webstate.referrerurl == '') webstate.referrerurl = "Dashboard";
            $state.go(webstate.referrerurl);
        }
    };
})



