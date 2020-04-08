using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyApiService.Models
{

    public class CompanyModel
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string TaxID { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string WebsiteURL { get; set; }
        public string Owner { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime EditedDate { get; set; }
    }
}