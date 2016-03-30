using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Shared.Html
{
    public class Controls
    {
        public static MvcHtmlString GneFlexList(
            IEnumerable<string> items,
            int breakAtItem = 5
        )
        {
            var rowIndex = 0;
            var flexList = new TagBuilder("ul");
            var ul = new TagBuilder("ul");
            var li = new TagBuilder("li");
            var htmlString = new StringBuilder();

            flexList.AddCssClass("flex-container");
            items = items?.ToList();
            if (items != null && items.Any())
            {
                htmlString.AppendLine(flexList.ToString(TagRenderMode.StartTag));
                htmlString.AppendLine(li.ToString(TagRenderMode.StartTag));
                htmlString.AppendLine(ul.ToString(TagRenderMode.StartTag));
                foreach (var item in items)
                {
                    // Begin new sub-list on the Xth item
                    if (rowIndex != 0 && rowIndex % breakAtItem == 0)
                    {
                        htmlString.AppendLine(ul.ToString(TagRenderMode.EndTag));
                        htmlString.AppendLine(li.ToString(TagRenderMode.EndTag));
                        htmlString.AppendLine(li.ToString(TagRenderMode.StartTag));
                        htmlString.AppendLine(ul.ToString(TagRenderMode.StartTag));
                    }
                    htmlString.AppendLine(li.ToString(TagRenderMode.StartTag));
                    htmlString.Append(item);
                    htmlString.AppendLine(li.ToString(TagRenderMode.EndTag));
                    rowIndex++;
                }
                htmlString.AppendLine(ul.ToString(TagRenderMode.EndTag));
                htmlString.AppendLine(li.ToString(TagRenderMode.EndTag));
                htmlString.AppendLine(ul.ToString(TagRenderMode.EndTag));
            }
            return MvcHtmlString.Create(htmlString.ToString());
        }

        // takes list of Control Items to create checkboxes in style of buttons
        public static MvcHtmlString GneCheckboxButtons(IEnumerable<ControlItem> checboxes)
        {
            var btnGroup = new TagBuilder("div");
            var label = new TagBuilder("label");
            var input = new TagBuilder("input");
            var htmlString = new StringBuilder();

            btnGroup.AddCssClass("btn-group");
            btnGroup.Attributes.Add("data-toggle", "buttons");

            input.Attributes.Add("type", "checkbox");
            input.Attributes.Add("autocomplete", "off");

            checboxes = checboxes?.ToList();
            if (checboxes != null && checboxes.Any())
            {
                htmlString.AppendLine(btnGroup.ToString(TagRenderMode.StartTag));
                foreach (var checkbox in checboxes)
                {
                    label.Attributes.Remove("id");
                    label.Attributes.Add("id", checkbox.Id);

                    label.Attributes.Remove("class");
                    label.AddCssClass("btn btn-default" + checkbox.Classes);
                    if (checkbox.DataAttributes != null)
                    {
                        foreach (var dataAttribute in checkbox.DataAttributes)
                        {
                            var attributeName = "data-" + dataAttribute.Name;
                            label.Attributes.Remove(attributeName);
                            label.Attributes.Add(attributeName, dataAttribute.Value);
                        }
                    }

                    if (checkbox.Enabled)
                    {
                        label.AddCssClass("active");
                    }

                    htmlString.AppendLine(label.ToString(TagRenderMode.StartTag));
                    htmlString.AppendLine(input.ToString(TagRenderMode.SelfClosing));
                    htmlString.Append(checkbox.Text);
                    htmlString.AppendLine(label.ToString(TagRenderMode.EndTag));
                }
                htmlString.AppendLine(btnGroup.ToString(TagRenderMode.EndTag));
            }
            return MvcHtmlString.Create(htmlString.ToString());

        }
    }
}
