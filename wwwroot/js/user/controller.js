define([
    'angular',
    'extension',
    'underscore',
    'pager'
], function () {
    var moduleListCtrl =  angular.module('moduleCtrl', []);
    moduleListCtrl.controller('controller',['$scope', '$window', 'svc', controller]);

    function controller($scope, $window, svc) {
         query_list.uid = common.getQueryString('uid');
         showList();
        $scope.models = [];
        $scope.model = {};
        $scope.saveMode = 'create'; //弹出框保存model模式 : create , edit
        $scope.selectAllStatus = false;
        $scope.selectItems = [];

        function initModel() {
            $scope.model = {
                id : '',
                name : '',
                nick : '',
                email : '',
                portrait : '',
                status : 1,
                createdAt : 0,
                updatedAt : 0,
                selStatus : false
            };
        }

        $scope.$on('$destroy', function() {
            console.log($scope.models.length + '..')
        });

        $scope.$watch('selectItems', function (newValue, oldValue, scope) {
            if(newValue == oldValue) return;
            $scope.selectAllStatus = ($scope.models.length > 0 &&  $scope.selectItems.length === $scope.models.length) ? true : false;

            _.each($scope.models, function(item) {
                item.selStatus= $scope.selectItems.indexOf(item.id) > -1;
            })

        })

        $scope.$on('selectItem', function(evt, args) {
            if(!args) return;

            if(args.val) {
                $scope.selectItems.push(args.id);
                $scope.selectItems = _.uniq($scope.selectItems);
            } else {
                $scope.selectItems = _.without($scope.selectItems, args.id)
            }
        });

        $scope.selectAllItems = function(scope, obj) {
            $scope.selectAllStatus = $(obj).prop('checked');
            if($scope.selectAllStatus) {
                $scope.selectItems = _.pluck($scope.models, 'id');
            } else {
                $scope.selectItems = [];
            }
        }

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
            console.log("showCreateModal")
        };

        $scope.search = function(obj) {
            query_list.month = $('#searchInput').val();
            showList();
        };

        function showList() {
            svc.retrieve()
              .done(function(json) {
                    $scope.models = json.result.entities || [];
                    // console.log(json.result);
                    $('.userList').show();
              }).fail(function() {
                  console.log('数据获取失败');
              })
        }
    }
});
