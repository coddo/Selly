angular
    .module('selly')
    .controller('OrdersController', function ($scope, API, HelperService, OrderService) {


        $scope.orders = [];

        function init() {
            loadOrders();
        }
        init();


        function loadOrders() {
            OrderService.GetAllOrders().then(function (success) {
                $scope.orders = success.data;

                success.data.forEach(function (order) {
                    order.total = 0;
                    order.orderItems.forEach(function (item) {
                        order.total += item.price * item.quantity;
                    });
                });
            });
        };


    });