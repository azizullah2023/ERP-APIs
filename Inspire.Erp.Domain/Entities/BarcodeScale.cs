using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class BarcodeScale
    {
        public int Id { get; set; }
        public int ScaleNo { get; set; }
        public int? ScaleDigit { get; set; }
        public int? ScaleOne { get; set; }
        public int? ScaleTwo { get; set; }
        public int? ScaleThree { get; set; }
        public int? ScaleFour { get; set; }
    }
}
