(function(angular) {
    angular.module('maxLength', [])
        .directive('myMaxlength', function () {
            return {
                require: 'ngModel',
                link: function (scope, element, attrs, ngModelCtrl) {
                    var maxlength = Number(attrs.myMaxlength);
                    ngModelCtrl.$parsers.push(function (text) {
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
})(window.angular);