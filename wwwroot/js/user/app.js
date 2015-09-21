require.config({
    baseUrl:'/js',
    paths: {
        'module' : 'user'
    }
});

require(['main'], function() {
    require(['validator','bootstrap', 'module/service', 'module/controller', 'module/filter','module/editCtrl', 'module/directive'], function() {
        validator.bind();
        angular.module('myApp', ['moduleListCtrl', 'moduleDetailCtrl', 'moduleSvc', 'moduleFilter', 'moduleDirect']);
        angular.element(document).ready(function() {
            angular.bootstrap(document, ['myApp']);
        });
    });
})


