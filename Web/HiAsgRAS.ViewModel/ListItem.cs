using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class ListItem
    {
        
            // Summary:
            //     Gets or sets a value that indicates whether this System.Web.Mvc.SelectListItem
            //     is selected.
            //
            // Returns:
            //     true if the item is selected; otherwise, false.
            public bool Selected { get; set; }
            //
            // Summary:
            //     Gets or sets the text of the selected item.
            //
            // Returns:
            //     The text.
            public string Text { get; set; }
            //
            // Summary:
            //     Gets or sets the value of the selected item.
            //
            // Returns:
            //     The value.
            public string Value { get; set; }
            public long ValueId { get; set; }
        
    }
}
