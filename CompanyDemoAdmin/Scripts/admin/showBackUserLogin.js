var userLoginsVue = new Vue({
    el: "#userLoginVue",
    data: {
        userInfo: {
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
        loading: false
    },
    created: function () {
        var self = this;
        self.getUserLogin();
    },
    methods: {
        getUserLogin: function () {
            var self = this;

            var url = "/admin/GetUsersByTypeAndLoginState";

            axios.get(url,
                {
                    params: {
                        memberType: 2,
                        isLogin: true,
                        currentPage: self.userInfo.currentPage,
                        itemsPerPage: self.userInfo.itemsPerPage,
                        isDesc: false,
                    }
                })
                .then(function (result) {
                    console.log(result.data.data);
                    self.userInfo.list = result.data.data.List;
                    self.userInfo.totalCount = result.data.data.TotalCount;
                    self.loading = false;

                }).catch(function (error) {
                    console.log(error);
                    self.loading = false;
                });
        },
    },
    computed: {
        usersTotalPages: function () {
            var self = this;
            return Math.ceil(self.userInfo.totalCount / self.userInfo.itemsPerPage);
        },
    }
});