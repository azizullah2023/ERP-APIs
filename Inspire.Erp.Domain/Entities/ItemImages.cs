using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ItemImages
    {
        public long? ItemImagesItemImageId { get; set; }
        public long? ItemImagesItemId { get; set; }
        public byte[] ItemImagesItemImage { get; set; }
        public byte[] ItemImagesItemImage1 { get; set; }
        public byte[] ItemImagesItemCatelogImage { get; set; }
        public bool? ItemImagesItemImageDelStatus { get; set; }

        public virtual ItemMaster ItemImagesItem { get; set; }
    }
}
