angular
    .module('selly')
    .controller('OrderController', function ($scope, API, HelperService, $routeParams) {


        $scope.order = {};

        function init() {
            loadOrder($routeParams.orderId);
        }
        init();


        function loadOrder(orderId) {
            HelperService.StartLoading('loadOrder');
            API.getOrder({ orderId: orderId }, function (success) {
                $scope.order = success.data;

                $scope.order.total = 0;
                $scope.order.orderItems.forEach(function (item) {
                    $scope.order.total += item.price * item.quantity * (1 + item.product.valueAddedTax.value / 100);
                });

                HelperService.StopLoading('loadOrder');

                if (!success.success)
                    HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            }, function (error) {
                HelperService.StopLoading('loadOrder');
                HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            });
        };


        

    });