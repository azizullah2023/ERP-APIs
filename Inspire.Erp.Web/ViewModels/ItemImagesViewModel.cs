using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class ItemImagesViewModel
    {
        public long ItemImagesItemImageId { get; set; }
        public long? ItemImagesItemId { get; set; }
        public byte[] ItemImagesItemImage { get; set; }
        public byte[] ItemImagesItemImage1 { get; set; }
        public byte[] ItemImagesItemCatelogImage { get; set; }
        public bool? ItemImagesItemImageDelStatus { get; set; }

    }
}
