angular
    .module('selly')
    .controller('OrderController', function ($scope, API, HelperService, $routeParams) {


        $scope.client = {};

        function init() {
            loadClient($routeParams.clientId);
        }
        init();


        function loadClient(clientId) {
            HelperService.StartLoading('loadClients');
            API.getClient({ clientId: clientId }, function (success) {
                $scope.client = success.data;
                HelperService.StopLoading('loadClients');

                if (!success.isSuccess)
                    HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            }, function (error) {
                HelperService.StopLoading('loadClients');
                HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            });
        };


        

    });