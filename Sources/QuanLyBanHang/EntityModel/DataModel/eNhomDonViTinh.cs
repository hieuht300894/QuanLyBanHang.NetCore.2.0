﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel.DataModel
{
    [Table("eNhomDonViTinh")]
    public class eNhomDonViTinh
    {
        [Key]
        public int KeyID { get; set; }

        public string Ma { get; set; }

        public string Ten { get; set; }

        public bool KichHoat { get; set; }

        public string GhiChu { get; set; }
    }
}
