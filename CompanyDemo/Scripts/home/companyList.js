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

var companyList = new Vue({
    el: "#companyListVue",
    data: {
        companyList: [],
        itemsPerPage: 10,
        currentPage: 1,
        totalCount: 0,
        loading: false,
        modalType: '',
        companyData: {
            companyName: '',
            companyCode: '',
            phone: '',
            taxId: '',
            address: '',
            webUrl: '',
            owner: ''
        }
    },
    created: function () {
        var self = this;
        self.loading = true;
        axios.get("https://localhost:44361/Company/GetCompanyList?page=1")
            .then(function (result) {
                console.log(result.data.data);
                self.companyList = result.data.data;
                self.loading = false;
            }).catch(function (error) {
                console.log(error);
            })
    },
    methods: {
        resetCompanyModel: function () {
            var self = this;
            self.companyData = {
                companyName: '',
                companyCode: '',
                phone: '',
                taxId: '',
                address: '',
                webUrl: '',
                owner: ''
            };
        },
        modalSubmit: function () {
            var self = this;
            var data = self.companyData;
            self.loading = true;

            var url = '';
            if (self.modalType === 'insert') {
                url = "https://localhost:44361/Company/AddCompany";
            } else {
                url = "https://localhost:44361/Company/UpdateCompany";
            }

            axios.post(url, {
                CompanyID: data.companyName,
                CompanyName: data.companyName,
                CompanyCode: data.companyCode,
                TaxID: data.taxId,
                Phone: data.phone,
                Address: data.address,
                WebsiteURL: data.webUrl,
                Owner: data.owner
            }).then(function (result) {
                console.log(result);
                console.log(result.data.data);
                self.loading = false;
                self.resetCompanyModel();
            }).catch(function (error) {
                console.log(error);
                self.loading = false;
            })

        },
        //addNewCompany: function () {
        //    var self = this;
        //    var data = self.companyData;
        //    self.loading = true;

        //    axios.post("https://localhost:44361/Company/AddCompany", {
        //        CompanyID: data.companyName,
        //        CompanyName: data.companyName,
        //        CompanyCode: data.companyCode,
        //        TaxID: data.taxId,
        //        Phone: data.phone,
        //        Address: data.address,
        //        WebsiteURL: data.webUrl,
        //        Owner: data.owner
        //    }).then(function (result) {
        //        console.log(result);
        //        console.log(result.data.data);
        //        self.loading = false;
        //    }).catch(function (error) {
        //        console.log(error);
        //        self.loading = false;
        //    })
        //},
        //updateCompany: function () {
        //    var self = this;

        //    var data = self.companyData;
        //    self.loading = true;

        //    axios.post("https://localhost:44361/Company/UpdateCompany", {
        //        CompanyID: data.companyName,
        //        CompanyCode: data.companyCode,
        //        TaxID: data.taxId,
        //        Phone: data.phone,
        //        Address: data.address,
        //        WebsiteURL: data.webUrl,
        //        Owner: data.owner
        //    }).then(function (result) {
        //        console.log(result.data.data);
        //        self.loading = false;
        //    }).catch(function (error) {
        //        console.log(error);
        //        self.loading = false;
        //    })

        //},
        setModalType: function (type) {
            var self = this;
            self.modalType = type;
        },
        findListByPage: function (page) {
            var self = this;
            self.loading = true;

            axios.get("https://localhost:44361/Company/GetCompanyList?page=" + page)
                .then(function (result) {
                    console.log(result.data.data);
                    self.companyList = result.data.data;
                    self.currentPage = page;
                    self.loading = false;

                }).catch(function (error) {
                    console.log(error);
                    self.loading = false;

                })
        },
    },
    computed: {
        totalPages: function () {
            var self = this;
            return Math.ceil(self.companyList.totalCount / self.companyList.itemsPerPage);
        },
    },
});