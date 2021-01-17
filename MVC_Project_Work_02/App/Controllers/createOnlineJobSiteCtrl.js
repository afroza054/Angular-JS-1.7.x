angular.module("projectApp")
    .controller("createOnlineJobSiteCtrl", ($scope, apiUrl, ApiService) => {
        $scope.companyInsertMsg = "";
        $scope.activeTab = 0;
        $scope.popup1 = {
            opened: false,
        };
        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };
        ///////model related
        $scope.newOnline = {};
        $scope.newAdvertise = {};
        $scope.jobAdvertisements = [];
        $scope.newApplicant = {};
        $scope.registerApplicants = [];
        $scope.companyDone = () => {
            $scope.activeTab = 1;
        };
        $scope.productDone = function (frm) {
            //console.log($scope.newProduct);

            $scope.jobAdvertisements.push($scope.newAdvertise);

            $scope.newAdvertise = {};
            frm.$setPristine();

            console.log($scope.jobAdvertisements);
        };
        $scope.productDel = (index) => {
            $scope.jobAdvertisements.splice(index, 1);
        };
        $scope.productPre = () => {
            $scope.activeTab = 0;
        };
        $scope.productNext = () => {
            $scope.activeTab = 2;
        };
        $scope.servicePre = () => {
            $scope.activeTab = 1;
        };
        $scope.serviceNext = (frms) => {
            $scope.newOnline.JobAdvertisements = $scope.jobAdvertisements;
            $scope.newOnline.RegisterApplicants = $scope.registerApplicants;
            ApiService.post(apiUrl + "OnlineJobSites", $scope.newOnline, null)
                .then(r => {
                    $scope.companyInsertMsg = "Data inserted successfully."
                  $scope.$emit('companyInserted', r.data);
                    $scope.newOnline = {};
                    $scope.newAdvertise = {};
                    $scope.jobAdvertisements = [];
                    $scope.newApplicant = {};
                    $scope.registerApplicants = [];
                    $scope.activeTab = 0;
                    frms[0].$setPristine();
                    frms[1].$setPristine();
                    frms[2].$setPristine();
                    console.log(frms);
                }, err => {
                    console.log(r);
                });
        };
        $scope.serviceDel = (index) => {
            $scope.registerApplicants.splice(index, 1);
        }
        $scope.serviceDone = function (frm) {
            //console.log($scope.newProduct);

            $scope.registerApplicants.push($scope.newApplicant);

            $scope.newApplicant = {};
            frm.$setPristine();

            console.log($scope.registerApplicants);
        };
    });