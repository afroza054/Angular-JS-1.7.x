angular.module("projectApp")
    .controller("createRegisterCtrl", ($scope, $routeParams, $location, apiUrl, ApiService) => {
        $scope.companyInsertMsg = "";
        $scope.activeTab = 0;
        $scope.popup1 = {
            opened: false,
        };
        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };
        ///////////////
        $scope.newService = null;


        $scope.insertService = () => {
            ApiService.post(apiUrl + "RegisterApplicants", $scope.newService, null)
                .then(r => {
                    ////////////
                    $scope.companyInsertMsg = "Data inserted successfully."
                    console.log(r);
                    $scope.$emit('serviceInserted', r.data);

                    // $location.path("/companies");
                }, err => {
                    console.log(err);
                });
        }
    });