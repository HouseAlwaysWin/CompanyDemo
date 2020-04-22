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
                selectSearchType: 'CompanyName',
                selectSearchOptions: [
                    { value: 'CompanyName', name: '公司名稱' },
                    { value: 'ProductType', name: '產品類型' },
                    { value: 'ProductPrice', name: '產品價格' },
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
        productFindBy: function (page) {
            var self = this;
            if (page) {
                self.productInfo.currentPage = page;
            }
            var searchType = self.productInfo.selectSearchType;
            var url = '';
            switch (searchType) {
                case "CompanyName":
                    url = "/Product/GetProductListByCompanyName";
                    break;
                case "ProductType":
                    url = "/Product/GetProductListByCompanyProductType";
                    break;
                case "ProductPrice":
                    url = "/Product/GetProductListByProductPrice";
                    break;
            }

            self.loading = true;
            axios.get(url, {
                params: {
                    searchText: self.productInfo.searchText,
                    itemsPerPage: self.productInfo.itemsPerPage,
                    currentPage: self.productInfo.currentPage,
                    isDesc: false
                }
            }).then(function (result) {
                console.log(result)
                self.productInfo.list = result.data.data.List;
                self.productInfo.totalCount = result.data.data.TotalCount;
                self.loading = false;

            }).catch(function (error) {
                var response = error.response.data;
                self.loading = false;

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