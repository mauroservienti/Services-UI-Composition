/* global angular */
(function () {

    angular.module('app.marketing')
        .directive('productShowcaseDescription', ['$log',
            function($log) {
                $log.debug('productShowcaseDescription directive');
            
                return {
                    restrict: 'E',
                    scope: {
                        product: '=',
                    },
                    templateUrl: '/app/modules/marketing/components/productShowcaseDescription.html'
                };
        }]);

}())