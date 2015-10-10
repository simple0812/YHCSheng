define([
    'angular',
    'extension',
    'underscore',
    'pager'
], function() {
    var moduleListCtrl = angular.module('moduleCtrl', []);
    moduleListCtrl.controller('controller', ['$scope', '$window', 'svc', controller]);

    function controller($scope, $window, svc) {
        $scope.models = [];
        $scope.model = {};
        $scope.saveType = ''; //弹出框保存model模式 : create , update
        $scope.selectAllStatus = false;
        $scope.selectItems = [];

        $scope.pageCondition = {
            keyword : '',
            pageSize: 10,
            pageIndex: 1
        };

        showList();

        $scope.$on('$destroy', function() {
            console.log($scope.models.length + '..');
        });

        $scope.$watch('selectItems', function(newValue, oldValue, scope) {
            if (newValue == oldValue) return;
            $scope.selectAllStatus = ($scope.models.length > 0 && $scope.selectItems.length === $scope.models.length) ? true : false;

            _.each($scope.models, function(item) {
                item.selStatus = $scope.selectItems.indexOf(item.id) > -1;
            });

        });

        $scope.$on('selectItem', function(evt, args) {
            if (!args) return;

            if (args.val) {
                $scope.selectItems.push(args.id);
                $scope.selectItems = _.uniq($scope.selectItems);
            } else {
                $scope.selectItems = _.without($scope.selectItems, args.id);
            }
        });

        $scope.selectAllItems = function(scope, obj) {
            $scope.selectAllStatus = $(obj).prop('checked');
            if ($scope.selectAllStatus) {
                $scope.selectItems = _.pluck($scope.models, 'id');
            } else {
                $scope.selectItems = [];
            }
        };
        $scope.remove = function(scope, obj) {
            if (!confirm('确认删除项目吗？')) return;

            svc.delete([scope.model.id]).done(function() {
                showList();
            }).fail(function(msg) {
                common.popBy(obj, msg);
            });
        };

        $scope.removeBatch = function(scope, obj) {
            if ($scope.selectItems.length == 0) return common.popBy(obj, '请选择要删除的项目');
            if (!confirm('确认删除选中的项目吗？')) return '';

            svc.delete($scope.selectItems).done(function() {
                showList();
            }).fail(function(msg) {
                common.popBy(obj, msg);
            });

        };

        $scope.$on('preSave', function(evt, args) {
            args = args || {};
            $scope.model = adapter(args.model);
            $scope.saveType = args.saveType;
        });

        $scope.save = function(scope, obj) {
            var mothodMap = {
                'create': create,
                'update': update
            };
            if (!mothodMap[$scope.saveType]) return console.log('saveType is  invalid');

            mothodMap[$scope.saveType]();
        };
        $scope.search = function() {
            showList();
        };

        function showList() {
            svc.retrieve($scope.pageCondition)
                .done(function(json) {
                    $scope.models = json.result.entities || [];
                    $('.userList').show();
                }).fail(function() {
                    console.log('数据获取失败');
                });
        }

        function create() {
            svc.create($scope.model).done(function(p) {
                $scope.models.push(adapter(p));
                $scope.$broadcast('postSave');
            }).fail(function() {
                $scope.$broadcast('postSave');
            });
        }

        function update() {
            svc.update($scope.model).done(function() {
                var scope = $('#user').scope();
                var model = _.find(scope.models, function(item) { return item.id === $scope.model.id; });

                for (var each in $scope.model)
                    if ($scope.model.hasOwnProperty(each))model[each] = $scope.model[each];

                $scope.$broadcast('postSave');
            }).fail(function() {
                $scope.$broadcast('postSave');
            });
        }

        function adapter(obj) {
            obj = obj || {};
            return {
                id: obj.id || 0,
                name: obj.name || '',
                nick: obj.name || '',
                email: obj.email || '',
                portrait: obj.portrait || '',
                status: obj.status || 1,
                createdAt: obj.createdAt || 0,
                updatedAt: obj.updatedAt || 0,
                selStatus: obj.selStatus || false
            };
        }
    }
});