(function () {
    'use strict';

    angular
        .module('selly')
        .factory('API', API);

    API.$inject = ['$resource', '$rootScope'];

    function API($resource, $rootScope) {
        var baseUrl = '/api/';
        $rootScope.baseUrl = baseUrl;

        var res = $resource('/', {}, {
            //Users
            getAllClients: {
                url: baseUrl + 'client/GetAll',
                method: 'GET',
                isArray: false
            },

            createUser: {
                url: baseUrl + 'client/Create',
                method: 'POST',
                isArray: false
            },

            getClient: {
                url: baseUrl + 'client/Get',
                method: 'GET',
                isArray: false
            },



            //Currencies
            getAllCurrecies: {
                url: baseUrl + 'currency/GetAll',
                method: 'GET',
                isArray: false
            },


            //Orders
            placeOrder: {
                url: baseUrl + 'order/Create',
                method: 'POST',
                isArray: false
            },
            getAllOrders: {
                url: baseUrl + 'order/GetAll',
                method: 'GET',
                isArray: false
            },
            getAllOrdersForUser: {
                url: baseUrl + 'order/GetAllForUser',
                method: 'GET',
                isArray: false
            },
            getOrder: {
                url: baseUrl + 'order/Get',
                method: 'GET',
                isArray: false
            },

            //Products
            getAllProducts: {
                url: baseUrl + 'product/GetAll',
                method: 'GET',
                isArray: false
            },

        });

        return res;
    }
})();