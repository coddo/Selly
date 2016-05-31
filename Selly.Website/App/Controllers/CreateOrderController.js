angular
    .module('selly')
    .controller('CreateOrderController', function ($routeParams, $scope, API, HelperService, $timeout) {
        $scope.order = {
            title: '',
            message: '',
            email: ''
        };
        $scope.ui = {
            selectedProducts: [],
            moneyPaidByClient: 0,
            moneyPaidByUs: 0,
            moneyPaidByClientVAT: 0,
            moneyPaidByUsVAT: 0,
        };

        $scope.clients = [];

        $scope.saleTypes = [
            { id: 0, name: 'Normal' },
            { id: 1, name: 'Exchange' },
            { id: 2, name: 'Return' },
        ]
        $scope.ui.saleType = $scope.saleTypes[0].id;

        $scope.products = [{
            id: 1,
            name: 'Pita',
            price: 23,
            valueAddedTax: { id: 12, value: 19 }
        },
        {
            id: 2,
            name: 'Ulei',
            price: 8,
            valueAddedTax: { id: 12, value: 9 }
        },
        {
            id: 3,
            name: 'Carne',
            price: 199,
            valueAddedTax: { id: 12, value: 19 }
        }];

        function init() {
            loadClients();
        }
        init();


        function loadClients() {
            HelperService.StartLoading('loadClients');
            API.getAllClients(function (success) {
                $scope.clients = success.data;
                HelperService.StopLoading('loadClients');
            }, function (error) {
                HelperService.StopLoading('loadClients');
                HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            });
        };


        $scope.addToCart = function (product) {
            var index = $scope.ui.selectedProducts.indexOf(product);
            if (index < 0) {
                product.quantity = $scope.ui.saleType == 2 ? -1 : 1;
                product.productId = product.id;
                $scope.ui.selectedProducts.push(product);
            } else {
                if ($scope.ui.saleType == 2)
                    $scope.ui.selectedProducts[index].quantity--;
                else
                    $scope.ui.selectedProducts[index].quantity++;
            }

            updateMoney();
        }

        $scope.changeQuantity = function (product, val) {
            console.log(parseInt(product.quantity));
            product.quantity = parseInt(product.quantity) + val;

            updateMoney();
        }

        $scope.changePrice = function (product) {
            if (product.editing) {
                product.editing = false;

                updateMoney();
            }
            else
                product.editing = true;
        }

        $scope.deleteProduct = function (product) {
            var index = $scope.ui.selectedProducts.indexOf(product);
            if (index >= 0) {
                $scope.ui.selectedProducts.splice(index, 1);
            }

            updateMoney();
        }

        function updateMoney() {
            $scope.ui.moneyPaidByClient = 0;
            $scope.ui.moneyPaidByUs = 0;
            $scope.ui.moneyPaidByClientVAT = 0;
            $scope.ui.moneyPaidByUsVAT = 0;

            $scope.ui.selectedProducts.forEach(function (product) {
                var productPrice = product.quantity * product.price;
                var productPriceVAT = productPrice + productPrice * product.valueAddedTax.value / 100;

                if (product.quantity > 0) {
                    $scope.ui.moneyPaidByClient += productPrice;
                    $scope.ui.moneyPaidByClientVAT += productPriceVAT;
                }
                else {
                    $scope.ui.moneyPaidByUs -= productPrice;
                    $scope.ui.moneyPaidByUsVAT -= productPriceVAT;
                }
            });
        }


        $scope.placeOrder = function () {
            console.log($scope.ui.selectedProducts);
        }

    });
