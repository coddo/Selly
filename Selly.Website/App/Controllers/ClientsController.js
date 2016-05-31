angular
    .module('selly')
    .controller('ClientsController', function ($scope, API, HelperService) {

        function emptyClient() {
            $scope.ui.newClient = {
                firstName: '',
                lastName: '',
                email: '',
                currencyId: 'F9F69F05-B269-4FDB-A920-00659EDA5CE4'
            };
        }


        $scope.clients = [];
        $scope.currencies = [];
        $scope.ui = {
            newClient: {}
        };

        function init() {
            emptyClient();
            loadClients();
            loadCurrencies();
        }
        init();


        function loadClients() {
            HelperService.StartLoading('loadClients');
            API.getAllClients(function (success) {
                $scope.clients = success.data;
                HelperService.StopLoading('loadClients');

                if (!success.isSuccess)
                    HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            }, function (error) {
                HelperService.StopLoading('loadClients');
                HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            });
        };

        function loadCurrencies() {
            HelperService.StartLoading('loadCurrencies');
            API.getAllCurrecies(function (success) {
                $scope.currencies = success.data;
                HelperService.StopLoading('loadCurrencies');

                if (!success.isSuccess)
                    HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            }, function (error) {
                HelperService.StopLoading('loadCurrencies');
                HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            });
        };


        $scope.saveUser = function () {

            if ($scope.ui.newClient.firstName.length < 3) {
                HelperService.ShowMessage('alert-warning', 'The user\'s first name is missing!');
                return;
            }

            if ($scope.ui.newClient.lastName.length < 3) {
                HelperService.ShowMessage('alert-warning', 'The user\'s last name is missing!');
                return;
            }

            if ($scope.ui.newClient.email.length < 3) {
                HelperService.ShowMessage('alert-warning', 'The user\'s email is missing!');
                return;
            }

            HelperService.StartLoading('createUser');
            API.createUser($scope.ui.newClient, function (success) {

                emptyClient();
                loadClients();

                HelperService.StopLoading('createUser');
                if (success.isSuccess)
                    HelperService.ShowMessage('alert-success', 'The user was created successfully!');
                else
                    HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');

            }, function (error) {
                HelperService.StopLoading('createUser');
                HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            });
        }

    });