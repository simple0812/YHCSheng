require.config({
    baseUrl:'/js',
    paths: {
        'module' : 'user'
    }
});

require(['main'], function() {
    require(['validator', 'bootstrap', 'module/service', 'module/controller', 'commonFilter', 'commonDirect'], function () {
        validator.bind();
        angular.module('myApp', ['moduleCtrl', 'moduleSvc', 'commonFilter', 'commonDirect']);
        angular.element(document).ready(function() {
            angular.bootstrap(document, ['myApp']);
        });
    });
});


