define([
    'moment',
    'angular'
], function() {
    var moduleFilter = angular.module('moduleFilter', []);
    moduleFilter.filter('dateFilter', function() {
        return function(input) {
            return moment(parseInt(input) * 1000).format('YYYY-MM-DD HH:mm');
        }
    });

    moduleFilter.filter('searchFilter', function() {
        return function(items, keyword) {
            if (!keyword) return items;
            return _.filter(items, function(item) {
                return item.name.indexOf(keyword) > -1;
            });
        }
    });

});