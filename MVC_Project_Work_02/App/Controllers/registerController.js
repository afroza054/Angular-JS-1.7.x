angular.module("projectApp")
    .controller("registerController", ($scope, $routeParams, $location, apiUrl, ApiService) => {
        $scope.companyInsertMsg = "";
        $scope.activeTab = 0;
        $scope.popup1 = {
            opened: false,
        };
        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };
        /////////////////////////////////////
        var id = $routeParams.id;
        $scope.currentService = null;
        $scope.newService = null;
        if (id == null) {
            $scope.errorMsg = "Advertisement id not available.";
            $location.path("/onlineJobSite");
        }
        ApiService.get(apiUrl + "RegisterApplicants/" + id, null)
            .then(r => {
                $scope.currentService = r.data;
                console.log(r.data);
            }, err => {
                console.log(err);
            });
        $scope.updateService = () => {
            ApiService.put(apiUrl + "RegisterApplicants/" + id, $scope.currentService, null)
                .then(r => {
                    $scope.companyInsertMsg = "Data update successfully."
                    console.log(r);
                    $scope.$emit('serviceUpdated', $scope.currentService);

                    // $location.path("/companies");
                }, err => {
                    console.log(err);
                });

        }
        $scope.insertService = () => {
            ApiService.post(apiUrl + "RegisterApplicants/", $scope.newService, null)
                .then(r => {
                    $scope.companyInsertMsg = "Data updated successfully."
                    console.log(r);
                    $scope.$emit('serviceInserted', r.data);

                    // $location.path("/companies");
                }, err => {
                    console.log(err);
                });
        }
    });