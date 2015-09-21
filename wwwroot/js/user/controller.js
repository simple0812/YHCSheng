define([
    'angular',
    'extension',
    'underscore',
    'pager'
], function () {
    var moduleListCtrl =  angular.module('moduleListCtrl', []);
    moduleListCtrl.controller('modelsCtrl',['$scope', '$window', 'svc', modelsCtrl]);

    function modelsCtrl($scope, $window, svc) {
        query_list.uid = common.getQueryString('uid');
        showList();
        $scope.models = [];

        $scope.$on('$destroy', function() {
            console.log($scope.models.length + '..')
        });

        $scope.remove = function(scope, obj) {
            if(confirm('确认删除项目吗？')) {
                svc.delete([scope.model.id])
                    .done(function() {
                        showList();
                    }).fail(function(msg) {
                        common.popBy(obj, msg);
                    });
            }
        };

        $scope.navToEdit = function(scope, obj) {
            var uid = scope.model.attributes.uid;
            var url = (typeof uid == 'array') ? "/user/v/" + uid[0] : '/user/v/' + uid;
            location.href = url;
        };

        $scope.removeModels = function(scope, obj) {
            if($('.chkItem:checked').length == 0) return common.popBy(obj, '请选择要删除的项目');
            var ids = [];
            $('.chkItem:checked').each(function(i, o) {
                ids.push($(o).val());
            });

            if(confirm('确认删除选中的项目吗？')) {
                svc.delete(ids).done(function() {
                    showList();

                }).fail(function(msg) {
                    common.popBy(obj, msg);
                });
            }
        };

        $scope.showEditModal = function(scope, obj) {
            var editScope = $('#createUserModal').scope();

            $('#btnSave').data('save-type', 'edit');
            for(var each in scope.model)
                editScope.model[each] = scope.model[each]
            $('#logoImg').attr('src', scope.model.logo).show();
            $('#createUserModal').modal('show');
        };

        $scope.showCreateModal = function() {
            $('#btnSave').data('save-type', 'create');
            $('#createUserModal').modal('show');
        };

        $scope.search = function(obj) {
            query_list.month = $('#searchInput').val();
            showList();
        };

        function showList() {
            svc.retrieve()
              .done(function(json) {
                    $scope.models = json.result.entities || [];
                    console.log(json.result);
                    $('.userList').show();
              }).fail(function() {
                  console.log('数据获取失败');
              })
        }
    }
});
