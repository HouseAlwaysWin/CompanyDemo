ELEMENT.locale(el_zh_TW);

Vue.component('validation-provider', VeeValidate.ValidationProvider);
Vue.component('validation-observer', VeeValidate.ValidationObserver);
VeeValidate.localize({
    zh_TW: {
        names: {
            roleName: '權限名稱'
        }
    }
});
VeeValidate.localize('zh_TW', zh_TW);

var memberVue = new Vue({
    el: "#memberVue",
    data: {
        showArea: {
            memberInfo: true,
            roleInfo: true,
            addRoleInfo: false,
            notInRoleInfo: false,
        },
        memberInfo: {
            list: [],
            itemsPerPage: 10,
            currentPage: 1,
            totalCount: 0,
            modalType: '',
            data: {
                roleID: 0,
                memberID: 0,
                memberName: '',
                email: '',
                phoneNumber: ''
            },
            ownRoleList: [],
            selectSearchType: 'Id',
            selectSearchOptions: [
                { value: 'Id', name: '會員ID' },
                { value: 'MemberName', name: '會員名稱' },
            ],
            searchText: ''
        },
        roleInfo: {
            list: [],
            itemsPerPage: 10,
            currentPage: 1,
            totalCount: 0,
            modalType: '',
            mapMember: {},
            data: {
                roleID: '',
                roleName: '',
            },
            selectSearchType: 'roleID',
            selectSearchOptions: [
                { value: 'roleID', name: '權限ID' },
                { value: 'roleName', name: '權限名稱' },
            ],
            searchText: ''
        },
    },
    created: function () {
        var self = this;
        self.loading = true;
        self.memberFindAllUsers();
        self.roleFindAllRoles();
    },
    methods: {
        /**重設會員資料狀態 */
        memberResetData: function () {
            var self = this;
            self.memberInfo.data = {
                roleID: 0,
                memberID: 0,
                memberName: '',
                email: '',
                phoneNumber: ''
            };
            self.$refs.memberModalRef.reset();
        },

        /**
         * 設定會員表單類型
         * @param {string} type
         * @param {any} data
         */
        memberSetFormType: function (type, data) {
            var self = this;
            self.memberInfo.modalType = type;
            console.log(data);
            if (type === 'update') {
                self.memberInfo.data = {
                    memberID: data.memberID,
                    memberName: data.memberName,
                    memberCode: data.memberCode,
                    phone: data.Phone,
                    taxId: data.TaxID,
                    address: data.Address,
                    webUrl: data.WebsiteURL,
                    owner: data.Owner
                };
            }
        },
        /**根據ID查詢會員資料 */
        memberFindAllUsers: function () {
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

        },


        memberAddRoleToUser: function (data) {
            var self = this;

            var url = "/Admin/AddRoleToUser";

            var postData = {
                userId: self.roleInfo.mapMember.Id,
                roleName: data.Name
            }

            axios.post(url, postData
            ).then(function (result) {
                console.log(result);
                console.log(result.data.data);

                self.loading = false;

                self.roleFindNotInRolesByMemberID();
            }).catch(function (error) {
                console.log(error);
                self.loading = false;
            })
        },

        memberAddRolesToUser: function () {
            var self = this;

            var url = "/Admin/AddRolesToUser";

            var postData = {
                userId: self.roleInfo.mapMember.Id,
                roleIds: self.memberInfo.ownRoleList
            }

            axios.post(url, postData
            ).then(function (result) {
                console.log(result);
                console.log(result.data.data);

                self.loading = false;
                self.roleResetData();

                self.roleFindAllRoles();
            }).catch(function (error) {
                console.log(error);
                self.loading = false;
            })
        },

        memberRemoveRoleFromUser: function (data) {
            var self = this;
            var url = "/Admin/RemoveRoleFromUser";

            var postData = {
                userId: self.roleInfo.mapMember.Id,
                roleId: data.Id
            }

            axios.post(url, postData
            ).then(function (result) {
                console.log(result);
                console.log(result.data.data);

                self.loading = false;
                //self.roleResetData();

                self.roleFindRolesByMemberID();
            }).catch(function (error) {
                console.log(error);
                self.loading = false;
            })
        },

        /** 重設權限資訊 */
        roleResetInfo: function () {
            var self = this;
            self.roleInfo = {
                list: [],
                itemsPerPage: 10,
                currentPage: 1,
                totalCount: 0,
                modalType: '',
                mapMember: {},
                data: {
                    roleID: '',
                    roleName: '',
                },
                selectSearchType: 'roleID',
                selectSearchOptions: [
                    { value: 'roleID', name: '權限ID' },
                    { value: 'roleName', name: '權限名稱' },
                ],
                searchText: ''
            };
            self.$refs.roleModalRef.reset();
        },

        /**重設權限資料狀態 */
        roleResetData: function () {
            var self = this;
            self.roleInfo.data = {
                roleName: '',
            };
            self.$refs.roleModalRef.reset();
        },


        /** 送出權限資料表 */
        roleSubmitForm: function () {
            var self = this;
            var data = self.roleInfo.data;
            self.loading = true;

            var url = '';
            if (self.roleInfo.modalType === 'insert') {
                url = "/Admin/Addrole";
            } else {
                url = "/Admin/Updaterole";
            }

            var postData = {
                roleId: data.roleID,
                roleName: data.roleName,
            };

            axios.post(url, postData
            ).then(function (result) {
                console.log(result);
                console.log(result.data.data);
                $("#roleInsertUpdate").modal("hide");

                self.loading = false;
                self.roleResetData();

                self.roleInfo.selectSearchType = "roleName";
                self.roleInfo.searchText = self.roleInfo.data.roleName;
                self.roleFindAllRoles();
            }).catch(function (error) {
                console.log(error);
                self.loading = false;
            })

        },
        /**
         * 設定權限表單類型
         * @param {string} type
         * @param {any} data
         */
        roleSetFormType: function (type, data) {
            var self = this;
            self.roleInfo.modalType = type;
            console.log(data);
            if (type === 'update') {
                self.roleInfo.data = {
                    roleID: data.Id,
                    roleName: data.Name,
                };
            }
        },
        /**
         * 刪除權限資料
         * @param {any} data
         */
        roleDeleteData: function (data) {
            var self = this;
            Swal.fire({
                title: '確定要刪除?',
                text: "刪除無法復原!!",
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
                    axios.post("/Admin/DeleteRoleByID",
                        {
                            id: postData.Id
                        }).then(function (result) {
                            console.log(result);
                            console.log(result.data.data);

                            self.loading = false;
                            self.roleResetData();
                            Swal.fire(
                                '刪除!',
                                '已刪除',
                                'success'
                            ).then(function (result) {
                                self.roleFindAllRoles();
                            });

                        }).catch(function (error) {
                            console.log(error);
                            self.loading = false;
                        })

                }
            })
        },
        roleFindAllRoles: function () {
            var self = this;

            var url = "/admin/FindAllRoles";

            axios.get(url,
                {
                    params: {
                        currentPage: self.roleInfo.currentPage,
                        itemsPerPage: self.roleInfo.itemsPerPage,
                        isDesc: false,
                    }
                })
                .then(function (result) {
                    console.log(result.data.data);
                    self.roleInfo.list = result.data.data.List;
                    self.roleInfo.totalCount = result.data.data.TotalCount;
                    self.loading = false;

                }).catch(function (error) {
                    console.log(error);
                    self.loading = false;
                });

        },
        roleFindRolesByMemberID: function () {
            var self = this;
            var url = "/Admin/GetRolesByUser";

            axios.get(url,
                {
                    params: {
                        Id: self.roleInfo.mapMember.Id,
                        currentPage: self.roleInfo.currentPage,
                        itemsPerPage: self.roleInfo.itemsPerPage,
                        isDesc: false,
                    }
                })
                .then(function (result) {
                    console.log(result.data.data);
                    self.roleInfo.list = result.data.data.List;
                    self.roleInfo.totalCount = result.data.data.TotalCount;
                    self.loading = false;

                }).catch(function (error) {
                    console.log(error);
                    self.loading = false;
                });

        },
        roleFindNotInRolesByMemberID: function () {
            var self = this;
            var url = "/Admin/GetNotInRolesByUser";

            axios.get(url,
                {
                    params: {
                        Id: self.roleInfo.mapMember.Id,
                        currentPage: self.roleInfo.currentPage,
                        itemsPerPage: self.roleInfo.itemsPerPage,
                        isDesc: false,
                    }
                })
                .then(function (result) {
                    console.log(result.data.data);
                    self.roleInfo.list = result.data.data.List;
                    self.roleInfo.totalCount = result.data.data.TotalCount;
                    self.loading = false;

                }).catch(function (error) {
                    console.log(error);
                    self.loading = false;
                });

        },
        roleShowBtn: function (data) {
            var self = this;
            self.showArea = {
                memberInfo: false,
                roleInfo: false,
                addRoleInfo: true,
                notInRoleInfo: false,
            };
            self.roleInfo.mapMember = data;
            self.roleFindRolesByMemberID();
        },
        roleShowNotInRoleeBtn: function () {
            var self = this;
            self.showArea = {
                memberInfo: false,
                roleInfo: false,
                addRoleInfo: false,
                notInRoleInfo: true,
            };
            self.roleFindNotInRolesByMemberID();
        },

        backMemberInfo: function () {
            var self = this;
            self.showArea = {
                memberInfo: true,
                roleInfo: true,
                addRoleInfo: false,
                notInRoleInfo: false,
            };
            self.roleResetData();
            self.roleFindAllRoles();
            self.memberFindAllUsers();
        },
        backAddRoleInfo: function () {
            var self = this;
            self.showArea = {
                memberInfo: false,
                roleInfo: false,
                addRoleInfo: true,
                notInRoleInfo: false,
            };
            self.roleFindRolesByMemberID();
        }


    },
    computed: {
        /**產品列表總頁數 */
        memberTotalPages: function () {
            var self = this;
            return Math.ceil(self.memberInfo.totalCount / self.memberInfo.itemsPerPage);
        },
        roleTotalPages: function () {
            var self = this;
            return Math.ceil(self.roleInfo.totalCount / self.roleInfo.itemsPerPage);
        },
    },
})