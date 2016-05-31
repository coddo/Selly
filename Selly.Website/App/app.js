(function () {
    'use strict';
    var app = angular.module('selly', [ 'ngResource', 'ngRoute', 'ui.bootstrap', 'ui.bootstrap.tooltip']);

    app.config(function ($routeProvider, $locationProvider) {

        $routeProvider
            .when('/', {
                templateUrl: '/Views/Index.html',
                controller: 'IndexController'
            })
            .when('/create', {
                templateUrl: '/Views/CreateOrder.html',
                controller: 'CreateOrderController'
            })
            .when('/clients', {
                templateUrl: '/Views/Clients.html',
                controller: 'ClientsController'
            })
            .when('/clients/:clientId', {
                templateUrl: '/Views/Client.html',
                controller: 'ClientController'
            })

             .when('/orders', {
                 templateUrl: '/Views/Orders.html',
                 controller: 'OrdersController'
             })
             .when('/orders/:orderId', {
                 templateUrl: '/Views/SingleOrder.html',
                 controller: 'OrderController'
             })
            //.when('/scrisoare', {
            //    templateUrl: '/Views/Letter.html',
            //    controller: 'LetterController'
            //})
            .otherwise({
                templateUrl: '/Views/Inexistent.html',
            });
        //.when('/contact', {
        //    templateUrl : 'partials/contact.html',
        //    controller : mainController
        //});


        // use the HTML5 History API
        $locationProvider.html5Mode(true);
    });


})();
