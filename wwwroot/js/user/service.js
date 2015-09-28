define([
    'angular'
], function () {
    var moduleSvc =  angular.module('moduleSvc', []);
    var apiUrl = '/api/user';

    moduleSvc.factory('svc', ['$http', function($http) {
        return {
            delete: function(ids) {
                var def = $.Deferred();
                var promise = def.promise();

                $http.delete(apiUrl, ids).success(function(json) {
                    if (!json.code || json.code == 'error') return def.reject(json ? json.message : '未知的错误');
                    def.resolve(json.result);
                }).error(function(data, status, headers, config) {
                    def.reject(data);
                });

                return promise;
            },

            update: function(model) {
                var def = $.Deferred();
                var promise = def.promise();
                $http.put(apiUrl, model).success(function(json) {
                    if (!json.code || json.code == 'error') return def.reject(json ? json.message : '未知的错误');
                    def.resolve(json.result);
                }).error(function(data, status, headers, config) {
                    def.reject(data);
                });

                return promise;
            },

            create: function(model) {
                var def = $.Deferred();
                var promise = def.promise();
                $http.post(apiUrl, model).success(function(json) {
                    if (!json.code || json.code == 'error') return def.reject(json ? json.message : '未知的错误');
                    def.resolve(json.result);
                }).error(function(data, status, headers, config) {
                    def.reject(data);
                });

                return promise;
            },

            retrieve:function() {
                var def = $.Deferred();
                var promise = def.promise();
                $http.get(apiUrl, {params : query_list}).success(function(json) {

                    if (!json) return def.reject('未知的错误');
                    if (!json.code || json.code == 'error')  return def.reject(json.message);
                    def.resolve(json);
                }).error(function(data, status, headers, config) {
                    console.log(data);
                    def.reject(data);
                });

                return promise;
            },

            pageUsers:function() {
                var def = $.Deferred();
                var promise = def.promise();

                $http.get(apiUrl, {params : pager.condition}).success(function(json) {
                    if (!json) return def.reject('未知的错误');
                    if (!json.code || json.code === 'error')  return def.reject(json.message);
                    def.resolve(json);
                }).error(function(data, status, headers, config) {
                    def.reject(data);
                });

                return promise;
            }
        };
    }])

});