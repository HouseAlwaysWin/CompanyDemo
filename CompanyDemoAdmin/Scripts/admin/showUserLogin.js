var userLoginsVue = new Vue({
    el: "#userLoginVue",
    data: {
        frontUserInfo: {
            list: [],
            itemsPerPage: 10,
            currentPage: 1,
            totalCount: 0,
            modalType: '',
            data: {
            },
            ownRoleList: [],
            selectSearchType: 'Id',
            selectSearchOptions: [
                { value: 'Id', name: '會員ID' },
                { value: 'MemberName', name: '會員名稱' },
            ],
            searchText: ''
        },
        backUserInfo: {
            list: [],
            itemsPerPage: 10,
            currentPage: 1,
            totalCount: 0,
            modalType: '',
            data: {
            },
            ownRoleList: [],
            selectSearchType: 'Id',
            selectSearchOptions: [
                { value: 'Id', name: '會員ID' },
                { value: 'MemberName', name: '會員名稱' },
            ],
            searchText: ''
        },
    },
    created: function () {

    },
    methods: {
        getUserLoginByFront: function () {
            var self = this;

            var url = "/admin/GetUsersByTypeAndLoginState";

            axios.get(url,
                {
                    params: {
                        currentPage: self.memberInfo.currentPage,
                        itemsPerPage: self.memberInfo.itemsPerPage,
                        isDesc: false,
                    }
                })
                .then(function (result) {
                    console.log(result.data.data);
                    self.memberInfo.list = result.data.data.List;
                    self.memberInfo.totalCount = result.data.data.TotalCount;
                    self.loading = false;

                }).catch(function (error) {
                    console.log(error);
                    self.loading = false;
                });
        },
        getUserLoginByBack: function () {
            var self = this;

            var url = "/admin/FindAllUsers";

            axios.get(url,
                {
                    params: {
                        currentPage: self.memberInfo.currentPage,
                        itemsPerPage: self.memberInfo.itemsPerPage,
                        isDesc: false,
                    }
                })
                .then(function (result) {
                    console.log(result.data.data);
                    self.memberInfo.list = result.data.data.List;
                    self.memberInfo.totalCount = result.data.data.TotalCount;
                    self.loading = false;

                }).catch(function (error) {
                    console.log(error);
                    self.loading = false;
                });
        }
    }
});