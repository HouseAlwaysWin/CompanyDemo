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
            list: [],
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
            mapCompany: null,
            data: {
                employeeID: '',
                employeeName: '',
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
            list: [],
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
        //#region 公司 */
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
            self.$refs.companyModalRef.reset();
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
                self.companySearchBy();
            }).catch(function (error) {
                console.log(error);
                self.loading = false;
            })

        },
        setCompanyModalType: function (type, data) {
            var self = this;
            self.companyInfo.modalType = type;
            console.log(data);
            if (type === 'update') {
                self.companyInfo.companyData = {
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
                        isDesc: false,
                    }
                })
                .then(function (result) {
                    console.log(result.data.data);
                    self.companyInfo.list = result.data.data.List;
                    self.companyInfo.totalCount = result.data.data.TotalCount;
                    self.loading = false;

                }).catch(function (error) {
                    console.log(error);
                    self.loading = false;
                });

        },


        resetEmployeeStatus: function () {
            var self = this;
            self.employeeInfo = {
                list: [],
                itemsPerPage: 10,
                currentPage: 1,
                totalCount: 0,
                modalType: '',
                mapCompany: null,
                data: {
                    employeeID: '',
                    employeeName: '',
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
            };
            self.$refs.employeeModalRef.reset();
        },

        employeeModalSubmit: function () {
            var self = this;

            var url = '';
            if (self.employeeInfo.modalType === 'insert') {
                url = "/Employee/AddEmployee";
            } else {
                url = "/Employee/UpdateEmployee";
            }

            var data = this.employeeInfo.data;

            var postData = {
                CompanyID: self.employeeInfo.mapCompany.CompanyID,
                EmployeeID: data.employeeID,
                EmployeeName: data.employeeName,
                CompanyName: data.fromCompanyName,
                Email: data.email,
                BirthdayDate: data.birthday,
                SignInDate: data.signInDate,
                ResignedDate: data.resignedDate,
                IsResigned: data.isResigned,
                Salary: data.salary
            };

            self.loading = true;
            axios.post(url, postData
            ).then(function (result) {
                console.log(result);
                console.log(result.data.data);
                $("#employeeInsertUpdate").modal("hide");

                self.loading = false;
                //self.resetEmployeeStatus();
                self.getEmployeesByCompanyID();
            }).catch(function (error) {
                var response = error.response;
                console.log(response);
                //Swal.fire(
                //    response.message
                //)

                self.loading = false;
            })
        },

        deleteEmployeeSubmit: function (data) {
            var self = this;
            Swal.fire({
                title: '確定要刪除?',
                text: "刪除後無法復原!!",
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
                    axios.post("/Employee/DeleteEmployeeByID",
                        {
                            id: postData.EmployeeID
                        }).then(function (result) {
                            self.loading = false;
                            Swal.fire(
                                '刪除!',
                                '你的資料已刪除',
                                'success'
                            ).then(function (result) {
                                self.getEmployeesByCompanyID();
                            });

                        }).catch(function (error) {
                            console.log(error);
                            self.loading = false;
                        })

                }
            })
        },

        setEmployeeModalType: function (type, data) {
            var self = this;
            self.employeeInfo.modalType = type;
            console.log(data);
            if (type === 'update') {
                self.employeeInfo.data = {
                    employeeID: data.EmployeeID,
                    employeeName: data.EmployeeName,
                    email: data.Email,
                    birthday: data.BirthdayDate,
                    signInDate: data.SignInDate,
                    resignedDate: data.ResignedDate,
                    isResigned: data.IsResigned,
                    salary: data.Salary
                };
            }
            self.$refs.employeeModalRef.reset();
        },
        showEmployeeList: function (data) {
            var self = this;
            self.showArea = {
                companyInfo: false,
                employeeInfo: true
            };
            self.employeeInfo.mapCompany = data;
            self.getEmployeesByCompanyID();
        },
        getEmployeesByCompanyID: function () {
            var self = this;

            axios.get("https://localhost:44361/Employee/GetListByCompanyID", {
                params: {
                    id: self.employeeInfo.mapCompany.CompanyID,
                    currentPage: self.employeeInfo.currentPage,
                    isDesc: false
                }
            }).then(function (result) {
                console.log(result)
                self.employeeInfo.list = result.data.data.List;
                self.employeeInfo.totalCount = result.data.data.TotalCount;
            }).catch(function (error) {
                var response = error.response.data;
                console.log(response);
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
            self.resetEmployeeStatus();
        },
        setDateFormat: function (date) {
            var self = this;
            return moment(date).format('MMMM Do YYYY');
        },


    },
    computed: {
        companyTotalPages: function () {
            var self = this;
            return Math.ceil(self.companyInfo.totalCount / self.companyInfo.itemsPerPage);
        },
        employeeTotalPages: function () {
            var self = this;
            return Math.ceil(self.employeeInfo.totalCount / self.employeeInfo.itemsPerPage);
        },
    },
});