
require.config({

    baseUrl: "",

    // alias libraries paths
    paths: {
        'application-configuration': 'Scripts/application-configuration',
        'angular': 'Scripts/angular',
        'angular-route': 'Scripts/angular-route.min',
        'angularAMD': 'Scripts/angularAMD',
        'blockUI': 'Scripts/angular-block-ui',
        'ngload': 'Scripts/ngload',
        'ui-bootstrap': 'Scripts/angular-ui/ui-bootstrap-tpls.min',
        'angular-sanitize': 'Scripts/angular-sanitize.min',
        'nggrid': 'Scripts/ng-grid.min',

        //services
        'ajaxService': 'services/ajaxServices',
        'accountServices': 'services/accountServices',

        'accountsController': 'Views/Shared/AccountsController'
    },

    // Add angular modules that does not support AMD out of the box, put it in a shim
    shim: {
        'angularAMD': ['angular'],
        'angular-route': ['angular'],
        'blockUI': ['angular'],
        'angular-sanitize': ['angular'],
        'ui-bootstrap': ['angular']

    },

    // kick start application
    deps: ['application-configuration']
});
