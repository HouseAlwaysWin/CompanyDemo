ELEMENT.locale(el_zh_TW);

var companyList = new Vue({
    el: "#companyListVue",
    data: {
        companyList: [],
        itemsPerPage: 10,
        currentPage: 1,
        totalCount: 0
    },
    created: function () {
        var self = this;
        axios.get("https://localhost:44361/api/Company/GetCompanyList?page=1")
            .then(function (result) {
                console.log(result.data.data);
                self.companyList = result.data.data;
            }).catch(function (error) {
                console.log(error);
            })
    },
    methods: {
        findListByPage: function (page) {
            var self = this;
            axios.get("https://localhost:44361/api/Company/GetCompanyList?page=" + page)
                .then(function (result) {
                    console.log(result.data.data);
                    self.companyList = result.data.data;
                    self.currentPage = page;
                }).catch(function (error) {
                    console.log(error);
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