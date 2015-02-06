(function (angular) {
    angular.module('paginationApp', ['angularPagination'])
    .controller('paginatonCtrl', ['$scope', '$window', 'Pagination', function ($scope, $window, Pagination) {
        $scope.pagination = Pagination.create();
        $scope.init = function (actionUrl, itemsPerPage, itemsCount, maxNumbers, startPage) {
            $scope.url = actionUrl;
            $scope.pagination.itemsPerPage = itemsPerPage;
            $scope.pagination.itemsCount = itemsCount;
            $scope.pagination.maxNumbers = maxNumbers;
            $scope.pagination.setCurrent(startPage);
        };
    }]);
})(window.angular);