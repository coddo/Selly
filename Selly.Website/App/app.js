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
