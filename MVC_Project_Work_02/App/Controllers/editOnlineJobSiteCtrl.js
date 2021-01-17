angular.module("projectApp")
    .controller("editOnlineJobSiteCtrl", ($scope, $routeParams, apiUrl, customUrl, ApiService) => {
        var id = $routeParams.id;
        $scope.companyToEdit = {};
        $scope.newCompanyProduct = {};
        ApiService.get(customUrl + "OnlineJobSites/" + id)
            .then(r => {
                $scope.companyToEdit = r.data;
                $scope.companyToEdit.StartedJourney = new Date($scope.companyToEdit.StartedJourney);
            }, err => {
                console.log(err);
            });
        $scope.editDone = (frm) => {
            ApiService.put(apiUrl + "OnlineJobSites/" + $scope.companyToEdit.OnlineJobSiteId, $scope.companyToEdit, null)
                .then(r => {
                    console.log(r);
                }, err => {
                    console.log(err);
                })
        }
        $scope.deleteCompanyService = (index) => {
            $scope.companyToEdit.RegisterApplicants.splice(index, 1);
            
        }
        $scope.deleteCompanyProduct = (index) => {
            $scope.companyToEdit.JobAdvertisements.splice(index, 1);
        }
        $scope.addCompanyProduct = () => {
            alert('ok')
            //$scope.companyToEdit.Products.push($scope.newCompanyProduct);
            //console.log($scope.companyToEdit);
            //$scope.newCompanyProduct = {};
            ////frm.$setPristine();
            $("#addProductModal").modal('hide');
        }
        $scope.showAddProductModal = () => {
            
            $("#addProductModal").modal('show');
        }
    });