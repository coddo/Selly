(function () {
    'use strict';

    angular
        .module('selly')
        .factory('OrderService', OrderService);


    function OrderService(API, HelperService) {



        var service = {
            GetAllOrders: loadOrders,
            GetAllOrdersForUser: loadOrdersForUser,
        };

        function initialize() {

        }
        //initialize();

        return service;

        /* IMPLEMENTATION */


        function loadOrders() {
            HelperService.StartLoading('loadOrders');
            return API.getAllOrders(function (success) {
                HelperService.StopLoading('loadOrders');

                if (!success.isSuccess)
                    HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            }, function (error) {
                HelperService.StopLoading('loadOrders');
                HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            }).$promise;
        };

        function loadOrdersForUser(userId) {
            HelperService.StartLoading('loadOrdersForUser');
            return API.getAllOrdersForUser({ userId: userId }, function (success) {
                HelperService.StopLoading('loadOrdersForUser');

                if (!success.isSuccess)
                    HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            }, function (error) {
                HelperService.StopLoading('loadOrdersForUser');
                HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            }).$promise;
        };

    }
})();