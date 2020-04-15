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

var productVue = new Vue({
    el: "#productVue",
    data: {
        showArea: {
            productInfo: true,
            employeeInfo: false,
            productInfo: false
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
            selectSearchType: 'CompanyName',
            selectSearchOptions: [
                { value: 'CompanyName', name: '公司名稱' },
                { value: 'ProductType', name: '產品類型' },
                { value: 'ProductPrice', name: '產品價格' },
            ],
            searchText: ''
        },
        loading: false,

    },
    created: function () {
        var self = this;
        self.loading = true;
        self.productFindBy();
    },
    methods: {
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


        /**根據公司ID尋找產品列表 */
        productFindBy: function () {
            var self = this;
            var searchType = self.productInfo.selectSearchType;
            var url = '';
            switch (searchType) {
                case "All":
                    url = "/Product/FindAll";
                    break;
                case "CompanyName":
                    url = "/Product/GetProductListByCompanyName";
                    break;
                case "ProductType":
                    url = "";
                    break;
                case "ProductPrice":
                    url = "";
                    break;
            }

            axios.get(url, {
                params: {
                    searchText: self.productInfo.searchText,
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
        /**產品列表總頁數 */
        productTotalPages: function () {
            var self = this;
            return Math.ceil(self.productInfo.totalCount / self.productInfo.itemsPerPage);
        },
    },
});