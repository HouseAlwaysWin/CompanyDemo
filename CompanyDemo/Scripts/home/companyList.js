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
        companyList: [],
        itemsPerPage: 10,
        currentPage: 1,
        totalCount: 0,
        loading: false,
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
    created: function () {
        var self = this;
        self.loading = true;
        self.searchBy();
    },
    methods: {
        resetStatus: function () {
            var self = this;
            self.companyData = {
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
            var data = self.companyData;
            self.loading = true;

            var url = '';
            if (self.modalType === 'insert') {
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

                self.selectSearchType = "CompanyName";
                self.searchText = self.companyData.CompanyName;
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
                                self.searchBy();
                            });

                        }).catch(function (error) {
                            console.log(error);
                            self.loading = false;
                        })

                }
            })
        },
        setModalType: function (type, data) {
            var self = this;
            self.modalType = type;
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
        searchBy: function () {
            var self = this;

            var url = '';
            if (self.selectSearchType === "CompanyID") {
                url = "https://localhost:44361/Company/GetCompanyListByID";
            } else {
                url = "https://localhost:44361/Company/GetCompanyListByName";
            }

            axios.get(url,
                {
                    params: {
                        page: self.currentPage,
                        searchText: self.searchText,
                        //isDesc: false,
                    }
                })
                .then(function (result) {
                    console.log(result.data.data);
                    self.companyList = result.data.data;
                    self.loading = false;

                }).catch(function (error) {
                    console.log(error);
                    self.loading = false;
                });

        },

    },
    computed: {
        totalPages: function () {
            var self = this;
            return Math.ceil(self.companyList.totalCount / self.companyList.itemsPerPage);
        },
    },
});