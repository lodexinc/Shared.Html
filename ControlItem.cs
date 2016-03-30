using System.Collections.Generic;

namespace Shared.Html
{
    public class ControlItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Enabled { get; set; }
        public string Classes { get; set; }
        public List<HtmlAttribute> DataAttributes { get; set; }
    }
}
