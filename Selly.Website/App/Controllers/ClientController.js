angular
    .module('selly')
    .controller('ClientController', function ($scope, API, HelperService, $routeParams, OrderService) {


        $scope.client = {};
        $scope.orders = [];

        function init() {
            loadClient($routeParams.clientId);
            loadOrders($routeParams.clientId);
        }
        init();


        function loadClient(clientId) {
            HelperService.StartLoading('loadClients');
            API.getClient({ clientId: clientId }, function (success) {
                $scope.client = success.data;
                HelperService.StopLoading('loadClients');

                if (!success.success)
                    HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            }, function (error) {
                HelperService.StopLoading('loadClients');
                HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            });
        };


        function loadOrders(clientId) {
            OrderService.GetAllOrdersForUser(clientId).then(function (success) {
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