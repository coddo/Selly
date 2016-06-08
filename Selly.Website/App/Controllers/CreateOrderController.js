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

        $scope.products = [
        //    {
        //        id: 1,
        //        name: 'Pita',
        //        price: 23,
        //        valueAddedTax: { id: 12, value: 19 }
        //    },
        //{
        //    id: 2,
        //    name: 'Ulei',
        //    price: 8,
        //    valueAddedTax: { id: 12, value: 9 }
        //},
        //{
        //    id: 3,
        //    name: 'Carne',
        //    price: 199,
        //    valueAddedTax: { id: 12, value: 19 }
        //}
        ];

        function init() {
            loadClients();
            loadProducts();
        }
        init();


        function loadClients() {
            HelperService.StartLoading('loadClients');
            API.getAllClients(function (success) {
                $scope.clients = success.data;
                $scope.ui.selectedClient = $scope.clients[0];

                HelperService.StopLoading('loadClients');

                if (!success.success)
                    HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            }, function (error) {
                HelperService.StopLoading('loadClients');
                HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            });
        };

        function loadProducts() {
            HelperService.StartLoading('loadProducts');
            API.getAllProducts(function (success) {
                $scope.products = success.data;

                HelperService.StopLoading('loadProducts');

                if (!success.success)
                    HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            }, function (error) {
                HelperService.StopLoading('loadProducts');
                HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
            });
        };


        $scope.addToCart = function (product) {
            var index = $scope.ui.selectedProducts.indexOf(product);
            if (index < 0) {
                var p = {
                    productId: product.id,
                    quantity: $scope.ui.saleType == 2 ? -1 : 1,
                    name: product.name,
                    price: product.price * $scope.ui.selectedClient.currency.multiplier,
                    valueAddedTax: product.valueAddedTax,
                };
                $scope.ui.selectedProducts.push(p);
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

        $scope.clearCart = function () {
            $scope.ui.selectedProducts = [];

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

            HelperService.StartLoading('placeOrder');
            API.placeOrder({
                clientId: $scope.ui.selectedClient.id,
                currencyId: $scope.ui.selectedClient.currency.id,
                saleType: $scope.ui.saleType,
                orderItems: $scope.ui.selectedProducts,
                date: new Date()
            },
                function (success) {

                    if (success.success) {
                        $scope.clearCart();
                        HelperService.ShowMessage('alert-success', 'Order placed!');

                    } else
                        HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');

                    HelperService.StopLoading('placeOrder');

                }, function (error) {
                    HelperService.StopLoading('placeOrder');
                    HelperService.ShowMessage('alert-danger', 'An error has occured! Try again!');
                });
        }

    });
