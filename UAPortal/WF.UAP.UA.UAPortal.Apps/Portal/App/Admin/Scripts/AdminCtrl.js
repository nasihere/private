angular.module('App.Admin').controller('AddAppCtrl', function ($scope, $ocLazyLoad) {
    $scope.myVar = "hello world";

});


angular.module('App.Admin').controller('PublishAppCtrl', function ($scope, $ocLazyLoad) {
    $scope.SrcView = "App/Admin/Views/";
    $scope.ShowDropZone = false;
    $scope.dropdown = {
        AppModule:  [
                { key:"Compass", value: "Compass" },
                { key:"MortgageRule", value: "Mortgage Rule" },
                { key:"HighCostMortgage", value: "High Cost Mortgage" },
                { key:"CSI", value: "Credit Service Instrumentation" },
                { key: "FeeEngine", value: "Fee Engine" },
                { key: "DevOperation", value: "Dev Operation" }
        ],
        ApplicationType: [
                { key: "", value: "Appraiser Watch List" },
                { key: "", value: "Benefit To Borrower" },
                { key: "", value: "Compass Doc Platform" },
                { key: "", value: "Consumer Consent Agreement Exception" },
                { key: "", value: "Credit card App Agreement Exception" },
                { key: "", value: "Credit card Table Maintenance" },
                { key: "", value: "Credit Reversal/Exception Request" },
                { key: "", value: "Credit Service Instrumentation" },
                { key: "", value: "Consumer Application Status" }, 
                { key: "", value: "CWS Acaps" },
                { key: "", value: "Dev Operation" },
                { key: "", value: "Direct Auto Admin" },
                { key: "", value: "Education Financial Services Maintenance" },
                { key: "", value: "Equity Application Status" },
                { key: "", value: "Fee Engine" },
                { key: "", value: "High Cost Mortgage" },
                { key: "", value: "High Cost Mortgage Test Harness" },
                { key: "", value: "High Priced Mortgage" },
                { key: "", value: "HEQ Table Maintenance" },
                { key: "", value: "Home Equity Maintenance" },
                { key: "", value: "Home Mortgagee Upload Exception Process" },
                { key: "", value: "Instant Decision Messaging" },
                { key: "", value: "Mortgage Rule Admin" },
                { key: "", value: "Other Income Text Table Maintenance" },
                { key: "", value: "PLL Cross Sell" },
                { key: "", value: "Retail Services Exception" },
                { key: "", value: "PLUS" },
                { key: "", value: "Subordination Service" },
                { key: "", value: "Shared Table Maintenance" },
                { key: "", value: "WFCC Table Maintenance" },


        ],
        AppGroup: [
                {value:"Select", key: "Select"},
                {value:"CBDR", key: "CBDR"},  
                {value:"CC", key: "CC"},
                {value:"BPP", key: "BPP"},
                {value:"Debit Card", key: "Debit Card"},
                {value:"HEQ", key: "HEQ"},
                {value:"Insurance", key: "Insurance"},
                {value:"PCM", key: "PCM"},
                {value:"Retailer", key: "Retailer"},
                {value:"WFAF", key: "WFAF"},
                {value:"TOG", key: "TOG"},
                {value:"WFHM", key: "WFHM"},
                {value:"WMG", key: "WMG"},
                {value:"Systems Mgmt and Consulting", key: "CFS"},
                {value:"EFS", key: "EFS"}
        ]
        
    };

    $scope.Submit = function (param) {
        alert(JSON.stringify(param));
    }


    $scope.$on('ocLazyLoad.moduleLoaded', function (e, params) {
        console.log('event module loaded', params);
    });

    $scope.$on('ocLazyLoad.componentLoaded', function (e, params) {
        console.log('event component loaded', params);
       
    });

    $scope.$on('ocLazyLoad.fileLoaded', function (e, file) {
        console.log('event file loaded', file);
    });

    $scope.loadBootstrap = function () {
        // use events to know when the files are loaded
        var unbind = $scope.$on('ocLazyLoad.fileLoaded', function (e, file) {
            if (file === 'bower_components/bootstrap/dist/css/bootstrap.css') {
                $scope.bootstrapLoaded = true;
                unbind();
            }
        });
        // we could use .then here instead of events
        $ocLazyLoad.load([
         
        ]);
    };
    $scope.aspect_ratio = function () {
        
        // Create variables (in this scope) to hold the API and image size
        var jcrop_api, boundx, boundy,

        // Grab some information about the preview pane
        $preview = $('#preview-pane'),
        $pcnt = $('#preview-pane .preview-container'),
        $pimg = $('#preview-pane .preview-container img'),
        xsize = $pcnt.width(), ysize = $pcnt.height();
        
        $('#target-3').Jcrop({
            onChange: updatePreview,
            onSelect: updatePreview,
            aspectRatio: xsize / ysize
        }, function () {
            // Use the API to get the real image size
            var bounds = this.getBounds();
            boundx = bounds[0];
            boundy = bounds[1];
            // Store the API in the jcrop_api variable
            jcrop_api = this;

            // Move the preview into the jcrop container for css positioning
            $preview.appendTo(jcrop_api.ui.holder);
        });

        function updatePreview(c) {
            if (parseInt(c.w) > 0) {
                var rx = xsize / c.w;
                var ry = ysize / c.h;

                $pimg.css({
                    width: Math.round(rx * boundx) + 'px',
                    height: Math.round(ry * boundy) + 'px',
                    marginLeft: '-' + Math.round(rx * c.x) + 'px',
                    marginTop: '-' + Math.round(ry * c.y) + 'px'
                });
            }
        };

    }
    // end aspect_ratio
    
   
 
   

});

angular.module('App.Admin').controller('RegistrationCtrl', function ($scope, api,
    $rootScope, AUTH_EVENTS) {





    $scope.sh = {
        regcomplete: false
    };
    $scope.RoleList = [];
    api.get(UAPConfig.GETROLES, {}).then(function (res) {
        $scope.bindRegister.UserRoles = res.data;
       
    });
    $scope.bindRegister = {
        FirstName: "",
        LastName: "",
        UserEmail: "",
        UserId:"",
        ManagersName: "",
        ManagersEmail: "",
        isAdEntId: true,
        UserRoles: null
    };
    $scope.Loading = false;
    $scope.CleanUpRoles = function (UserRole) {
        var UnNull = [];
        var item = angular.copy(UserRole);
        angular.forEach(item, function(value) {
            if (value.Value == true) {
                UnNull.push(value);
            }

        });

        return UnNull;
    }
    $scope.SubmitRegister = function () {
        $scope.Loading = true;
        $scope.bindRegister.UserRoles = $scope.CleanUpRoles($scope.bindRegister.UserRoles);
        api.post(UAPConfig.REGISTER, $scope.bindRegister).then(function (res) {
         
            $scope.CallbackSubmit(res);
        });
    }
    $scope.CallbackSubmit = function (res) {
        $scope.Loading = false;
        if (res.data.ErrorMessages != null) {
            alert(res.data.ErrorMessages[0].Message);
            return;
        }
        $scope.sh = {
            regcomplete: true
        };
//        $rootScope.$broadcast(AUTH_EVENTS.logoutSuccess);
    };
    $scope.search = {
        FirstName: "",
        LastName: "",
        UserId: "",
        isAdEntId: true
    };
    $scope.RolesPreSelect = function (item) {

        angular.forEach($scope.bindRegister.UserRoles, function (DBUserRole) {
            DBUserRole.Value = null;
                
        });

        angular.forEach(item, function (UserRole) {
            angular.forEach($scope.bindRegister.UserRoles, function (DBUserRole) {
                if (DBUserRole.Id == UserRole.Id ) {
                    DBUserRole.Value = true;
                
                    
                }
            });

        });
        

    }
    $scope.ngPopupItem = {};
    $scope.tempPopupItem = {};
    $scope.OpenPopup = function (item) {
        $scope.ngPopupItem = item;
        $scope.tempPopupItem = angular.copy(item.UserRoles);
        $scope.RolesPreSelect(item.UserRoles);
        Popup("#myModal");
        
    }
    $scope.ModAccounts = [];

    $scope.ApproverProfile = function() {
             $scope.LoadingFlag = true;
        $scope.ngPopupItem.UserRoles = angular.copy($scope.bindRegister.UserRoles);
        $scope.ngPopupItem.UserRoles = $scope.CleanUpRoles($scope.ngPopupItem.UserRoles);

        $scope.ngPopupItem.IsAdEntId = true; //Need to discuss on this line with sreedhar for iadent id

        delete $scope.ngPopupItem.$$hashKey;
        delete $scope.ngPopupItem.ErrorMessages;
   
        
        api.post(UAPConfig.SaveAccount, $scope.ngPopupItem).then(function (res) {
            $scope.LoadingFlag = false;
            PopupClose("#myModal");
        }, function(err) {
            $scope.LoadingFlag = false;
            alert("Error: " + err.data.error_description);

        });



    }
    $scope.DeinedProfile = function () {
        PopupClose("#myModal");
    }
    $scope.ngUserList = {};
    $scope.GetUserList = function(model) {
        api.post(UAPConfig.UserList, model).then(function (res) {
            $scope.ngUserList = res.data;
        });
    }

});
