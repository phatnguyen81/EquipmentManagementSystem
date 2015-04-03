
require.config({

    baseUrl: "",

    // alias libraries paths
    paths: {
        'application-configuration': 'Scripts/application-configuration',
        'angular': 'Scripts/angular',
        'angular-route': 'Scripts/angular-route',
        'angularAMD': 'Scripts/angularAMD',
        'blockUI': 'Scripts/angular-block-ui',
        'ngload': 'Scripts/ngload',
        'accountsController': '../Views/Shared/AccountsController'
    },

    // Add angular modules that does not support AMD out of the box, put it in a shim
    shim: {
        'angularAMD': ['angular'],
        'angular-route': ['angular'],
        'blockUI': ['angular']
    },

    // kick start application
    deps: ['application-configuration']
});
