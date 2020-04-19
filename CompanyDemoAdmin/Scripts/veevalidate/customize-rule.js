
/* 檢查Email是否有重複 */
var typingTimer = 0;
VeeValidate.extend('isUniqueCompany', {
    message: function (field) { return '公司名稱已重複，請填寫其他的' },
    validate: function (value) {
        return new Promise(function (resolve) {
            clearTimeout(typingTimer);
            typingTimer = setTimeout(function () {
                return axios.get(
                    '/Company/FindComapnyByName',
                    {
                        params: {
                            name: value
                        }
                    })
                    .then(function (response) {
                        console.log(response.data);
                        var status = response.data.status;
                        var data = response.data.data;
                        var result = false;
                        if (status === 'success') {
                            if (!data) {
                                result = true;
                            }
                        }
                        return resolve({
                            valid: result
                        });
                    }).catch(function (errors) {
                        console.log(errors);
                    })
            }, 1000);
        });
    }
});




/* 檢查Email是否有重複 */
var typingTimer = 0;
VeeValidate.extend('isUniqueCompanyCode', {
    message: function (field) { return '公司代碼名稱已重複，請填寫其他的' },
    validate: function (value) {
        return new Promise(function (resolve) {
            clearTimeout(typingTimer);
            typingTimer = setTimeout(function () {
                return axios.get(
                    '/Company/FindComapnyByCompanyCode',
                    {
                        params: {
                            code: value
                        }
                    })
                    .then(function (response) {
                        console.log(response.data);
                        var status = response.data.status;
                        var data = response.data.data;
                        var result = false;
                        if (status === 'success') {
                            if (!data) {
                                result = true;
                            }
                        }
                        return resolve({
                            valid: result
                        });
                    }).catch(function (errors) {
                        console.log(errors);
                    })
            }, 1000);
        });
    }
});



/* 檢查Email是否有重複 */
var typingTimer = 0;
VeeValidate.extend('isUniqueTaxID', {
    message: function (field) { return '統編名稱已重複，請填寫其他的' },
    validate: function (value) {
        return new Promise(function (resolve) {
            clearTimeout(typingTimer);
            typingTimer = setTimeout(function () {
                return axios.get(
                    '/Company/FindComapnyByTaxID',
                    {
                        params: {
                            id: value
                        }
                    })
                    .then(function (response) {
                        console.log(response.data);
                        var status = response.data.status;
                        var data = response.data.data;
                        var result = false;
                        if (status === 'success') {
                            if (!data) {
                                result = true;
                            }
                        }
                        return resolve({
                            valid: result
                        });
                    }).catch(function (errors) {
                        console.log(errors);
                    })
            }, 1000);
        });
    }
});