ELEMENT.locale(el_zh_TW);

Vue.component('validation-provider', VeeValidate.ValidationProvider);
Vue.component('validation-observer', VeeValidate.ValidationObserver);
VeeValidate.localize({
    zh_TW: {
        names: {
            'companyName': "公司名稱",
            'companyCode': '代碼',
            'phone': '聯絡電話',
            'taxId': '統編',
            'address': '地址',
            'webUrl': '網址',
            'owner': '負責人'
        }
    }
});
VeeValidate.localize('zh_TW', zh_TW);

var companyVue = new Vue({
    el: "#companyListVue",
    data: {
        showArea: {
            companyInfo: true,
            employeeInfo: false,
            productInfo: false
        },
        companyInfo: {
            companyList: [],
            itemsPerPage: 10,
            currentPage: 1,
            totalCount: 0,
            modalType: '',
            companyData: {
                companyID: '',
                companyName: '',
                companyCode: '',
                phone: '',
                taxId: '',
                address: '',
                webUrl: '',
                owner: ''
            },
            selectSearchType: 'CompanyID',
            selectSearchOptions: [
                { value: 'CompanyID', name: '公司ID' },
                { value: 'CompanyName', name: '公司名稱' },
            ],
            searchText: ''
        },
        employeeInfo: {
            list: [],
            itemsPerPage: 10,
            currentPage: 1,
            totalCount: 0,
            modalType: '',
            mapData: null,
            data: {
                companyID: '',
                employeeID: '',
                employeeName: '',
                fromCompanyName: '',
                email: '',
                birthday: '',
                signInDate: '',
                resignedDate: '',
                isResigned: false,
                salary: 0
            },
            selectSearchType: 'EmployeeID',
            selectSearchOptions: [
                { value: 'EmployeeID', name: '員工ID' },
                { value: 'EmployeeName', name: '員工名稱' },
            ],
            searchText: ''
        },

        productInfo: {
            companyList: [],
            itemsPerPage: 10,
            currentPage: 1,
            totalCount: 0,
            modalType: '',
            companyData: {
                companyID: '',
                companyName: '',
                companyCode: '',
                phone: '',
                taxId: '',
                address: '',
                webUrl: '',
                owner: ''
            },
            selectSearchType: 'CompanyID',
            selectSearchOptions: [
                { value: 'CompanyID', name: '公司ID' },
                { value: 'CompanyName', name: '公司名稱' },
            ],
            searchText: ''
        },

        loading: false,

    },
    created: function () {
        var self = this;
        self.loading = true;
        self.companySearchBy();
    },
    methods: {
        resetStatus: function () {
            var self = this;
            self.companyInfo.companyData = {
                companyID: '',
                companyName: '',
                companyCode: '',
                phone: '',
                taxId: '',
                address: '',
                webUrl: '',
                owner: ''
            };
            self.$refs.observerRef.reset();
        },
        modalSubmit: function () {
            var self = this;
            var data = self.companyInfo.companyData;
            self.loading = true;

            var url = '';
            if (self.companyInfo.modalType === 'insert') {
                url = "/Company/AddCompany";
            } else {
                url = "/Company/UpdateCompany";
            }

            var postData = {
                CompanyID: data.companyID,
                CompanyName: data.companyName,
                CompanyCode: data.companyCode,
                TaxID: data.taxId,
                Phone: data.phone,
                Address: data.address,
                WebsiteURL: data.webUrl,
                Owner: data.owner
            };

            axios.post(url, postData
            ).then(function (result) {
                console.log(result);
                console.log(result.data.data);
                $("#insertUpdate").modal("hide");

                self.loading = false;
                self.resetStatus();

                self.companyInfo.selectSearchType = "CompanyName";
                self.companyInfo.searchText = self.companyInfo.companyData.CompanyName;
                self.searchBy();
            }).catch(function (error) {
                console.log(error);
                self.loading = false;
            })

        },
        employeeModalSubmit: function () {
            var self = this;
            var data = self.employeeInfo.data;
            self.loading = true;

            var url = '';
            if (self.companyInfo.modalType === 'insert') {
                url = "/Employee/AddEmployee";
            } else {
                url = "/Employee/UpdateEmployee";
            }

            var postData = {
                CompanyID: self.companyID,
                EmployeeID: self.employeeID,
                EmployeeName: self.employeeName,
                CompanyName: self.fromCompanyName,
                Email: self.email,
                BirthdayDate: self.birthday,
                SignInDate: self.signInDate,
                ResignedDate: self.resignedDate,
                IsResigned: self.isResigned,
                Salary: self.salary
            };

            axios.post(url, postData
            ).then(function (result) {
                console.log(result);
                console.log(result.data.data);
                $("#insertUpdate").modal("hide");

                self.loading = false;
                self.resetStatus();

                self.searchBy();
            }).catch(function (error) {
                console.log(error);
                self.loading = false;
            })
        },

        deleteSubmit: function (data) {
            var self = this;
            Swal.fire({
                title: '確定要刪除?',
                text: "刪除後會連帶會員一起刪除並且無法復原!!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: '是',
                cancelButtonText: '否'
            }).then((result) => {
                if (result.value) {
                    self.loading = true;
                    var postData = data;
                    axios.post("/Company/DeleteCompanyByID",
                        {
                            id: postData.CompanyID
                        }).then(function (result) {
                            console.log(result);
                            console.log(result.data.data);

                            self.loading = false;
                            self.resetStatus();
                            Swal.fire(
                                'Deleted!',
                                'Your file has been deleted.',
                                'success'
                            ).then(function (result) {
                                self.companySearchBy();
                            });

                        }).catch(function (error) {
                            console.log(error);
                            self.loading = false;
                        })

                }
            })
        },

        deleteEmployeeSubmit: function (data) {

        },
        setCompanyModalType: function (type, data) {
            var self = this;
            self.companyInfo.modalType = type;
            console.log(data);
            if (type === 'update') {
                self.companyData = {
                    companyID: data.CompanyID,
                    companyName: data.CompanyName,
                    companyCode: data.CompanyCode,
                    phone: data.Phone,
                    taxId: data.TaxID,
                    address: data.Address,
                    webUrl: data.WebsiteURL,
                    owner: data.Owner
                };
            }
        },
        setCompanyModalType: function (type, data) {
            var self = this;
            self.employeeInfo.modalType = type;
            console.log(data);
            if (type === 'update') {
                self.companyData = {
                    companyID: data.CompanyID,
                    companyName: data.CompanyName,
                    companyCode: data.CompanyCode,
                    phone: data.Phone,
                    taxId: data.TaxID,
                    address: data.Address,
                    webUrl: data.WebsiteURL,
                    owner: data.Owner
                };
            }
        },
        companySearchBy: function () {
            var self = this;

            var url = '';
            if (self.companyInfo.selectSearchType === "CompanyID") {
                url = "https://localhost:44361/Company/GetCompanyListByID";
            } else {
                url = "https://localhost:44361/Company/GetCompanyListByName";
            }

            axios.get(url,
                {
                    params: {
                        page: self.companyInfo.currentPage,
                        searchText: self.companyInfo.searchText,
                        //isDesc: false,
                    }
                })
                .then(function (result) {
                    console.log(result.data.data);
                    self.companyInfo.companyList = result.data.data;
                    self.loading = false;

                }).catch(function (error) {
                    console.log(error);
                    self.loading = false;
                });

        },
        employeeSearchBy: function () {

        },

        getEmployeesByCompanyID: function (id) {
            var self = this;
            self.showArea = {
                companyInfo: false,
                employeeInfo: true
            };
            self = this.employeeInfo;

            axios.get("https://localhost:44361/Employee/GetListByCompanyID", {
                params: {
                    id: id,
                    currentPage: self.currentPage,
                    isDesc: false
                }
            }).then(function (result) {
                console.log(result)
                self.list = result.data.data.List;
                self.totalCount = result.data.data.TotalCount;
                self.mapData = result.data.data.MapData;
            }).catch(function (error) {

            });
        },
        getProductsByCompanyID: function (id) {

        },
        backCompanyInfo: function () {
            var self = this;
            self.showArea = {
                companyInfo: true,
                employeeInfo: false
            }
        }

    },
    computed: {
        totalPages: function () {
            var self = this;
            return Math.ceil(self.companyInfo.companyList.totalCount / self.companyInfo.companyList.itemsPerPage);
        },
    },
});