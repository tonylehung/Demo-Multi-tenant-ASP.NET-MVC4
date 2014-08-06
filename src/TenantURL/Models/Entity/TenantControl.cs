using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TenantURL.Models.Entity
{
    public class TenantControl
    {
        [Key]
        public int Id { get; set; }
        public string TenantUser { get; set; }
        public string Theme { get; set; }
        public string TenantSchema { get; set; }
        public string TenantConnection { get; set; }
    }
}