(function ($, angular) {
    angular.module('app', ['monospaced.elastic'])
    .directive('myMaxlength', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                var maxlength = Number(attrs.myMaxlength);
                ngModelCtrl.$parsers.push( function (text) {
                    if (text.length > maxlength) {
                        var transformedInput = text.substring(0, maxlength);
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                        return transformedInput;
                    }
                    return text;
                });
            }
        };
    })
    .controller('RateCtrl', ['$scope', '$http', function ($scope, $http) {
        var id = '';
        var url = '';
        $scope.init = function (users, username, isAuthenticated, rating, articleId, rateUrl) {
            $scope.canRate = (isAuthenticated && (-1 == users.indexOf(username)));
            $scope.rating = rating;
            $scope.color = $scope.rating >= 0 ? 'lightgreen' : 'lightcoral';
            id = articleId;
            url = rateUrl;
        };
        $scope.like = function (isLike) {
            $scope.canRate = false;
            $http.post(url, { articleId: id, like: isLike })
            .success(function (date) {
                $scope.rating += (date.like) ? 1 : -1;
                $scope.color = $scope.rating >= 0 ? 'lightgreen' : 'lightcoral';
            })
        };
    }])
    .controller('CommentCtrl', ['$scope', '$http', function ($scope, $http) {
        var url = '';
        var id = '';
        $scope.init = function (canComment, articleId, commentUrl) {
            $scope.canComment = canComment;
            id = articleId;
            url = commentUrl;
        };
        $scope.addComment = function () {
            $scope.pending = true
            $http.post(url, { comment: $scope.comment, id: id })
            .success(function (data) {
                $scope.pending = false;
                $('#comment').after(data);
            });
        }
    }]);
})(window.jQuery, window.angular);

//(function () {
//    $(function () {
//        var _private = {
//            $comment: $('#comment'),
//            commentCount: new CountGroup($('#comment'), 320)
//        };
//        $('.content', _private.$comment).autosize();

//        $('.btn-comment').click(function () {
//            $.ajax({
//                type: 'POST',
//                url: $('.action', _private.$comment).val(),
//                data: {
//                    comment: $('.content', _private.$comment).val(),
//                    id: $('.id', _private.$comment).val()
//                },
//                success: function (data) {
//                    _private.$comment.after(data);
//                }
//            })
//        })
//    });
//})(jQuery);