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
    el: "#companyVue",
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
            data: {
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
            mapCompany: null,
            data: {
                productID: 0,
                productName: '',
                productTYpe: '',
                price: 0,
                unit: ''
            },
            selectSearchType: 'ProductID',
            selectSearchOptions: [
                { value: 'ProductID', name: '產品ID' },
                { value: 'ProductName', name: '產品名稱' },
            ],
            searchText: ''
        },
        loading: false,

    },
    created: function () {
        var self = this;
        self.loading = true;
        self.companyFindBy();
    },
    methods: {
        /**重設公司資料狀態 */
        companyResetData: function () {
            var self = this;
            self.companyInfo.data = {
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
        /** 送出公司資料表 */
        companySubmitForm: function () {
            var self = this;
            var data = self.companyInfo.data;
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
                self.companyResetData();

                self.companyInfo.selectSearchType = "CompanyName";
                self.companyInfo.searchText = self.companyInfo.data.CompanyName;
                self.companyFindBy();
            }).catch(function (error) {
                console.log(error);
                self.loading = false;
            })

        },
        /**
         * 設定公司表單類型
         * @param {string} type
         * @param {any} data
         */
        companySetFormType: function (type, data) {
            var self = this;
            self.companyInfo.modalType = type;
            console.log(data);
            if (type === 'update') {
                self.companyInfo.data = {
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
        /**
         * 刪除公司資料
         * @param {any} data
         */
        companyDeleteData: function (data) {
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
                            self.companyResetData();
                            Swal.fire(
                                'Deleted!',
                                'Your file has been deleted.',
                                'success'
                            ).then(function (result) {
                                self.companyFindBy();
                            });

                        }).catch(function (error) {
                            console.log(error);
                            self.loading = false;
                        })

                }
            })
        },
        /**根據ID查詢公司資料 */
        companyFindBy: function () {
            var self = this;

            var url = '';
            if (self.companyInfo.selectSearchType === "CompanyID") {
                url = "/Company/GetCompanyListByID";
            } else {
                url = "/Company/GetCompanyListByName";
            }

            axios.get(url,
                {
                    params: {
                        page: self.companyInfo.currentPage,
                        itemsPerPage: self.companyInfo.itemsPerPage,
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
        /** 重設員工資訊 */
        employeeResetInfo: function () {
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
        /**重設員工表單資訊 */
        employeeResetData: function () {
            var self = this;
            self.employeeInfo.data = {
                employeeID: '',
                employeeName: '',
                email: '',
                birthday: '',
                signInDate: '',
                resignedDate: '',
                isResigned: false,
                salary: 0
            }
            self.$refs.employeeModalRef.reset();
        },
        /**送出員工資料表 */
        employeeSubmitData: function () {
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
                self.employeeFindByCompanyID();
            }).catch(function (error) {
                var response = error.response;
                console.log(response);
                self.loading = false;
            })
        },
        /**
         * 刪除員工資料
         * @param {any} data
         */
        employeeDeleteData: function (data) {
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
                                self.employeeFindByCompanyID();
                            });

                        }).catch(function (error) {
                            console.log(error);
                            self.loading = false;
                        })

                }
            })
        },

        /**
         * 設定員工表單類型
         * @param {any} type
         * @param {any} data
         */
        employeeSetFormType: function (type, data) {
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

        /**
         * 顯示員工列表按鈕
         * @param {any} data
         */
        employeeShowBtn: function (data) {
            var self = this;
            self.showArea = {
                companyInfo: false,
                employeeInfo: true
            };
            self.employeeInfo.mapCompany = data;
            self.employeeFindByCompanyID();
        },

        /**根據公司ID尋找員工列表 */
        employeeFindByCompanyID: function () {
            var self = this;
            axios.get("/Employee/GetListByCompanyID", {
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
        /**---- */
        /** 重設產品資訊 */
        productResetInfo: function () {
            var self = this;
            self.productInfo = {
                list: [],
                itemsPerPage: 10,
                currentPage: 1,
                totalCount: 0,
                modalType: '',
                mapCompany: null,
                data: {
                    productID: 0,
                    productName: '',
                    productTYpe: '',
                    price: 0,
                    unit: ''
                },
                selectSearchType: 'ProductID',
                selectSearchOptions: [
                    { value: 'ProductID', name: '員工ID' },
                    { value: 'ProductName', name: '員工名稱' },
                ],
                searchText: ''
            };
            self.$refs.productModalRef.reset();
        },
        /**重設產品表單資訊 */
        productResetData: function () {
            var self = this;
            self.productInfo.data = {
                productID: 0,
                productName: '',
                productTYpe: '',
                price: 0,
                unit: ''
            }
            self.$refs.productModalRef.reset();
        },
        /**送出產品資料表 */
        productSubmitData: function () {
            var self = this;

            var url = '';
            if (self.productInfo.modalType === 'insert') {
                url = "/Product/AddProduct";
            } else {
                url = "/Product/UpdateProduct";
            }

            var data = this.productInfo.data;

            var postData = {
                CompanyID: self.productInfo.mapCompany.CompanyID,
                ProductID: data.productID,
                ProductName: data.productName,
                ProductType: data.productType,
                Price: data.price,
                Unit: data.unit
            };

            self.loading = true;
            axios.post(url, postData
            ).then(function (result) {
                console.log(result);
                console.log(result.data.data);
                $("#productInsertUpdate").modal("hide");

                self.loading = false;
                self.productFindByCompanyID();
            }).catch(function (error) {
                var response = error.response;
                console.log(response);
                self.loading = false;
            })
        },
        /**
         * 刪除產品資料
         * @param {any} data
         */
        productDeleteData: function (data) {
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
                    axios.post("/Product/DeleteProductByID",
                        {
                            id: postData.ProductID
                        }).then(function (result) {
                            self.loading = false;
                            Swal.fire(
                                '刪除!',
                                '你的資料已刪除',
                                'success'
                            ).then(function (result) {
                                self.productFindByCompanyID();
                            });

                        }).catch(function (error) {
                            console.log(error);
                            self.loading = false;
                        })

                }
            })
        },

        /**
         * 設定產品表單類型
         * @param {any} type
         * @param {any} data
         */
        productSetFormType: function (type, data) {
            var self = this;
            self.productInfo.modalType = type;
            console.log(data);
            if (type === 'update') {
                self.productInfo.data = {
                    productID: data.ProductID,
                    productName: data.ProductName,
                    productType: data.ProductType,
                    price: data.Price,
                    unit: data.Unit
                };
            }
            self.$refs.productModalRef.reset();
        },

        /**
         * 顯示產品列表按鈕
         * @param {any} data
         */
        productShowBtn: function (data) {
            var self = this;
            self.showArea = {
                companyInfo: false,
                productInfo: true
            };
            self.productInfo.mapCompany = data;
            self.productFindByCompanyID();
        },

        /**根據公司ID尋找產品列表 */
        productFindByCompanyID: function () {
            var self = this;
            axios.get("/Product/GetListByCompanyID", {
                params: {
                    id: self.productInfo.mapCompany.CompanyID,
                    currentPage: self.productInfo.currentPage,
                    isDesc: false
                }
            }).then(function (result) {
                console.log(result)
                self.productInfo.list = result.data.data.List;
                self.productInfo.totalCount = result.data.data.TotalCount;
            }).catch(function (error) {
                var response = error.response.data;
                console.log(response);
            });
        },
        /**返回公司資訊 */
        backCompanyInfo: function () {
            var self = this;
            self.showArea = {
                companyInfo: true,
                productInfo: false
            }
            self.productResetInfo();
        },
        /**
         * 設定日期格式
         * @param {any} date
         */
        setDateFormat: function (date) {
            var self = this;
            return moment(date).format('MMMM Do YYYY');
        },


    },
    computed: {
        /**公司列表總頁數 */
        companyTotalPages: function () {
            var self = this;
            return Math.ceil(self.companyInfo.totalCount / self.companyInfo.itemsPerPage);
        },
        /**員工列表總頁數 */
        employeeTotalPages: function () {
            var self = this;
            return Math.ceil(self.employeeInfo.totalCount / self.employeeInfo.itemsPerPage);
        },
        /**產品列表總頁數 */
        productTotalPages: function () {
            var self = this;
            return Math.ceil(self.productInfo.totalCount / self.productInfo.itemsPerPage);
        },
    },
});