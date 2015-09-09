
var fakejson = {
    "UserId": "U168111",
    "Password": null,
    "IsAdEntId": true,
    "FirstName": "Mehta, Gaurav H",
    "LastName": null,
    "UserEmail": "Gaurav.H.Mehta@tempargo.com",
    "ManagersName": null,
    "ManagersEmail": null,
    "ModulePrivileges": [
       {
           "ModuleId": 0,
           "ModuleName": "Compass  - II",
           "ModuleUrl": "#",
           "ModuleIconUrl": "CompassII.jpg",
           "Hierarchy": "/1/",
           "ParentHierarchy": "/",
           "HierarchyLevel": "1",
           "RoleName": "SuperAdmin",
           "PrivilegeName": null,
           "PrivilegeId": 0,
           "UserId": "U168111"
       },
       { 
           "ModuleId": 0,
           "ModuleName": "Dev Operations",
           "ModuleUrl": "#",
           "ModuleIconUrl": "DevOps.jpg",
           "Hierarchy": "/1/1/",
           "ParentHierarchy": "/1/",
           "HierarchyLevel": "2",
           "RoleName": "SuperAdmin",
           "PrivilegeName": null,
           "PrivilegeId": 0,
           "UserId": "U168111"
       },
       {
           "ModuleId": 0,
           "ModuleName": "Retail Services Exception",
           "ModuleUrl": "RetailServiceExceptions",
           "ModuleIconUrl": "Exceptions.jpg",
           "Hierarchy": "/1/1/1/",
           "ParentHierarchy": "/1/1/",
           "HierarchyLevel": "3",
           "RoleName": "SuperAdmin",
           "PrivilegeName": null,
           "PrivilegeId": 0,
           "UserId": "U168111"
       },
       {
           "ModuleId": 0,
           "ModuleName": "WMG Rate Exception",
           "ModuleUrl": "#",
           "ModuleIconUrl": "RateExceptions.jpg",
           "Hierarchy": "/1/2/",
           "ParentHierarchy": "/1/",
           "HierarchyLevel": "2",
           "RoleName": "SuperAdmin",
           "PrivilegeName": null,
           "PrivilegeId": 0,
           "UserId": "U168111"
       },
       {
           "ModuleId": 0,
           "ModuleName": "WM Rate Exception Decisions",
           "ModuleUrl": "WM Rate Exception Decisions",
           "ModuleIconUrl": "Decisions.jpg",
           "Hierarchy": "/1/2/1/",
           "ParentHierarchy": "/1/2/",
           "HierarchyLevel": "3",
           "RoleName": "SuperAdmin",
           "PrivilegeName": null,
           "PrivilegeId": 0,
           "UserId": "U168111"
       }
    ],
    "UserRoles": null
}

var ConsoleAdminMenu = {
    "ModuleId": -1,
    "ModuleName": "Admin",
    "ModuleUrl": "",
    "ModuleIconUrl": "wf-admincard.png",

    "submenu": [
          
        {
            "ModuleId": 0,
            "ModuleName": "User Activiation",
            "ModuleUrl": "Debug-Search-User",
            "ModuleIconUrl": "wf-admincard.png",

        }
    ]
};
var ConsoleModal = {
    token: "100012020013",
    login: "demo2",
    navigate:[],
    linkAccess: [
        {
            "title": "Credit Service",
            "link": "http://:8001/SalesSimulatorDomain/CreditServicingSimulator.aspx?env=CDEV",
            "icon": "fa-leaf",
            "target": "_iframe",
            "short_desc": "AJAX version uses robust scripts to lazyload pages,",
            "full_desc": "This is our favorite out of all versions, mostly because it's easy to build with, customize and keep things into perspective.",
            "app_icon": "wf-cs"

        },
        {
            "title": "Compass",
            "link": "#",
            "icon": "fa-pencil-square-o",
            "target": "#",
            "app_icon": "wf-pencil",
            "submenu": [
                {
                    "title": "Approver",
                    "link": "Compass-Approver",
                    "target": "_main",
                    "short_desc": "AJAX version uses robust scripts to lazyload pages,",
                    "full_desc": "This is our favorite out of all versions, mostly because it's easy to build with, customize and keep things into perspective.",
                    "app_icon": "wf-approver"

                },
                {
                    "title": "Banner",
                    "link": "BannerApp",
                    "target": "_main",
                    "short_desc": "AJAX version uses robust scripts to lazyload pages,",
                    "full_desc": "This is our favorite out of all versions, mostly because it's easy to build with, customize and keep things into perspective.",
                    "app_icon": "wf-banner"

                }
            ]
        },
        {
            "title": "Credit App",
            "link": "#",
            "icon": "fa-bar-chart-o",
            "target": "#",
            "app_icon": "wf-CAS",
            "short_desc": "AJAX version uses robust scripts to lazyload pages,",
            "full_desc": "This is our favorite out of all versions, mostly because it's easy to build with, customize and keep things into perspective.",

            "submenu": [
                {
                    "title": "HEQ",
                    "link": "http://:8001/SalesSimulatorDomain/SCSInitialDataPage.aspx?env=CDEV",
                    "target": "_self",
                    "short_desc": "AJAX version uses robust scripts to lazyload pages,",
                    "full_desc": "This is our favorite out of all versions, mostly because it's easy to build with, customize and keep things into perspective.",
                    "app_icon": "wf-heq"

                }
            ]
        }, {
            "title": "SCS",
            "link": "#",
            "icon": "fa-table",
            "target": "#",
            "app_icon": "wf-scs",
            "short_desc": "AJAX version uses robust scripts to lazyload pages,",
            "full_desc": "This is our favorite out of all versions, mostly because it's easy to build with, customize and keep things into perspective.",

            "submenu": [
                {
                    "title": "WMG",
                    "link": "http://:8001/SalesSimulatorDomain/SCSInitialDataPage.aspx?env=CDEV",
                    "target": "_iframe",
                    "short_desc": "AJAX version uses robust scripts to lazyload pages,",
                    "full_desc": "This is our favorite out of all versions, mostly because it's easy to build with, customize and keep things into perspective.",
                    "app_icon": "wf-wmg"


                }, {
                    "title": "HEQ",
                    "link": "http://:8001/SalesSimulatorDomain/SCSInitialDataPage.aspx?env=CDEV",
                    "target": "_iframe",
                    "short_desc": "AJAX version uses robust scripts to lazyload pages,",
                    "full_desc": "This is our favorite out of all versions, mostly because it's easy to build with, customize and keep things into perspective.",
                    "app_icon": "wf-heq"

                }, {
                    "title": "Auto",
                    "link": "http://:8001/SalesSimulatorDomain/SCSInitialDataPage.aspx?env=CDEV",
                    "target": "_self",
                    "short_desc": "AJAX version uses robust scripts to lazyload pages,",
                    "full_desc": "This is our favorite out of all versions, mostly because it's easy to build with, customize and keep things into perspective.",
                    "app_icon": "wf-auto"

                }
            ],
        }, {
            "title": "CAS",
            "link": "http://:8001/SalesSimulatorDomain/CASInitialPostPage.aspx?env=CDEV",
            "icon": "fa-pencil-square-o",
            "target": "_iframe",
            "short_desc": "AJAX version uses robust scripts to lazyload pages,",
            "full_desc": "This is our favorite out of all versions, mostly because it's easy to build with, customize and keep things into perspective.",
            "app_icon": "wf-cas"

        }
    ],

    role: "editor",
    profile: {
        FirstName: "Teddy",
        LastName: "Teddy",
        displayname: "",
        designation: "Architect",
        areacode: "408",
        extension: "650 - 7845",
        email: "cineas.teddy@tempargo.com",
        lync: "teddy.cineas",
        calendar: "Free after",
        datetime: "5:30PM",
        title: "Project Architects are focused at the project level and working with the developing vendor to design and implement the project.",
        description: "Architecture is the set of decisions that must be made at the enterprise level before specific applications are designed and built in order to provide conceptual integrity and sanity across the enterprise’s systems.  Architecture includes a decomposition of the systems into separate orthogonal viewpoints along with the enforced rules that enable this clean decomposition and isolation of design viewpoints.  This is done so functional (application requirements) and non-functional (system qualities) and other aspects of the application system may be defined and built by independent specialists in their specific field.  An architecture not only divides the system, it also divides the roles and responsibilities of those who work with the system into separate organizational concerns and disciplines that are conceptually tractable and can be effectively managed.",
        adEntId: "u371860",
        wellfargoid: "1444581",
        joiningDt: "Jan 15, 1992",
        avatar: "Content/img/avatars/4.png",
        IsAdEntId: false,
        IsTopSecrectId: false,
        ManagersName: "",
        ManagersEmail:""
        },
        timeout: "20"
    
    

};




