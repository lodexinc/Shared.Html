using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Shared.Html
{
    public static class HtmlPrefixScopeExtensions
    {
        public static IHtmlString ShortLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            if (fieldName.Contains("."))
            {
                fieldName = fieldName.Replace('.', '_');
            }
            var labelText = metadata.ShortDisplayName ?? metadata.DisplayName ?? fieldName;
            var tag = new TagBuilder("label");
            tag.Attributes.Add("for", fieldName);
            tag.SetInnerText(labelText);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString ShortLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression, string cssClasses)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            if (fieldName.Contains("."))
            {
                fieldName = fieldName.Replace('.', '_');
            }
            var labelText = metadata.ShortDisplayName ?? metadata.DisplayName ?? fieldName;
            var tag = new TagBuilder("label");
            tag.Attributes.Add("for", fieldName);
            tag.SetInnerText(labelText);
            tag.AddCssClass(cssClasses);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString GneDisplayFor<TModel, TValue>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> field, 
            string cssClasses = "",
            bool showLabel = true,
            bool disabledEditor = false
            )
        {
            // out of 12 - by default, control consumes full width
            var controlWidth = 12;
            var labelWidth = 3;

            var htmlString = new StringBuilder();            
            var labelString = helper.ShortLabelFor(field, "control-label col-xs-" + labelWidth);
            var displayString = disabledEditor 
                ? helper.EditorFor(field, new { htmlAttributes = new { @disabled = "disabled", @class = cssClasses } }) 
                : helper.DisplayFor(field);

            //<div class="form-group">
            var formGroup = new TagBuilder("div");
            formGroup.AddCssClass("form-group");
            htmlString.AppendLine(formGroup.ToString(TagRenderMode.StartTag));

            //@Html.LabelFor(model => model.field, htmlAttributes: new { @class = "control-label col-xs-3" })
            if (showLabel)
            {
                htmlString.AppendLine(labelString.ToString());
                controlWidth = 9;
            }

            //<div class="col-sm-10">
            var column = new TagBuilder("div");
            column.AddCssClass("col-xs-" + controlWidth);
            htmlString.AppendLine(column.ToString(TagRenderMode.StartTag));

            //<p class="form-control-static"> - Deliberately NOT AppendLine - keep prevent whitespace at start
            var paragraph = new TagBuilder("p");
            paragraph.AddCssClass("form-control-static");
            paragraph.AddCssClass(cssClasses);
            htmlString.Append(paragraph.ToString(TagRenderMode.StartTag));

            //@Html.DisplayFor(model => model.field)
            htmlString.Append(displayString);

            //</p>
            htmlString.Append(paragraph.ToString(TagRenderMode.EndTag));

            //</div>
            htmlString.AppendLine(column.ToString(TagRenderMode.EndTag));

            //</div>
            htmlString.AppendLine(formGroup.ToString(TagRenderMode.EndTag));

            return MvcHtmlString.Create(htmlString.ToString());
        }

        public static MvcHtmlString GneEditorFor<TModel, TValue>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> field,
            object attributes = null,
            bool showLabel = true
            )
        {
            // out of 12 - by default, control consumes full width
            var controlWidth = 12;
            var labelWidth = 3;
            
            var htmlString = new StringBuilder();
            var labelString = helper.ShortLabelFor(field, "control-label col-xs-" + labelWidth);
            var editorString = helper.EditorFor(field, new { htmlAttributes = GetGneAttributes(attributes) });
            var validatorString = helper.ValidationMessageFor(field, "", new { @class = "text-danger" });

            //<div class="form-group">
            var formGroup = new TagBuilder("div");
            formGroup.AddCssClass("form-group");
            htmlString.AppendLine(formGroup.ToString(TagRenderMode.StartTag));

            //@Html.LabelFor(model => model.field, htmlAttributes: new { @class = "control-label col-xs-3" })
            if (showLabel)
            {
                htmlString.AppendLine(labelString.ToString());
                controlWidth = 9;
            }

            //<div class="col-sm-10">
            var column = new TagBuilder("div");
            column.AddCssClass("col-xs-" + controlWidth);
            htmlString.AppendLine(column.ToString(TagRenderMode.StartTag));

            //@Html.EditorFor(model => model.field, new { htmlAttributes = new { @class = "form-control" } })
            htmlString.AppendLine(editorString.ToString());

            //@Html.ValidationMessageFor(model => model.field, "", new { @class = "text-danger" })
            htmlString.AppendLine(validatorString.ToString());

            //</div>
            htmlString.AppendLine(column.ToString(TagRenderMode.EndTag));

            //</div>
            htmlString.AppendLine(formGroup.ToString(TagRenderMode.EndTag));

            return MvcHtmlString.Create(htmlString.ToString());
        }

        public static MvcHtmlString GneDropDownListFor<TModel, TValue>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> field,
            SelectList selectList,
            object attributes = null, 
            bool showLabel = true
            )
        {
            // out of 12 - by default, control consumes full width
            var controlWidth = 12;
            var labelWidth = 3;

            var htmlString = new StringBuilder();
            var labelString = helper.ShortLabelFor(field, "control-label col-xs-" + labelWidth);
            var dropDownListString = helper.DropDownListFor(field, selectList, "-- Select --", GetGneAttributes(attributes) );
            var validatorString = helper.ValidationMessageFor(field, "", new { @class = "text-danger" });

            //<div class="form-group">
            var formGroup = new TagBuilder("div");
            formGroup.AddCssClass("form-group");
            htmlString.AppendLine(formGroup.ToString(TagRenderMode.StartTag));

            //@Html.LabelFor(model => model.field, htmlAttributes: new { @class = "control-label col-xs-3" })
            if (showLabel)
            {
                htmlString.AppendLine(labelString.ToString());
                controlWidth = 9;   
            }

            //<div class="col-sm-10">
            var column = new TagBuilder("div");
            column.AddCssClass("col-xs-" + controlWidth);
            htmlString.AppendLine(column.ToString(TagRenderMode.StartTag));

            //@Html.DropDownListFor(field, selectList, "-- Select --", new { @class = "form-control" })
            htmlString.AppendLine(dropDownListString.ToString());

            //@Html.ValidationMessageFor(model => model.field, "", new { @class = "text-danger" })
            htmlString.AppendLine(validatorString.ToString());

            //</div>
            htmlString.AppendLine(column.ToString(TagRenderMode.EndTag));

            //</div>
            htmlString.AppendLine(formGroup.ToString(TagRenderMode.EndTag));

            return MvcHtmlString.Create(htmlString.ToString());
        }

        /// <summary>
        /// Usage: if Model is itself an IEnumerable: @Html.GneFlexListFor(m => m, "ProjectSupportGroupName", 3)
        /// Usage: if Model contains a child collection of IEnumerable: @Html.GneFlexListFor(m => m.MeetingStandingAttendees, "PersonName")
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="propertyName"></param>
        /// <param name="breakAtItem">optional</param>
        /// <returns></returns>
        public static IHtmlString GneFlexListFor<TModel, TClass>
            (
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, IEnumerable<TClass>>> expression,
            string propertyName,
            int breakAtItem = 5
            ) where TModel : class
        {
            var rowIndex = 0;
            var flexList = new TagBuilder("ul");
            var ul = new TagBuilder("ul");
            var li = new TagBuilder("li");
            var htmlString = new StringBuilder();

            flexList.AddCssClass("flex-container");
            var items = expression.Compile()(html.ViewData.Model);
            items = items?.ToList();
            if (items != null && items.Any())
            {
                htmlString.AppendLine(flexList.ToString(TagRenderMode.StartTag));
                htmlString.AppendLine(li.ToString(TagRenderMode.StartTag));
                htmlString.AppendLine(ul.ToString(TagRenderMode.StartTag));
                foreach (var item in items)
                {
                    var memberExpression = Expression.Property(Expression.Constant(item), propertyName);
                    var singleItemExpression = Expression.Lambda<Func<TModel, object>>(memberExpression, expression.Parameters);

                    // Begin new sub-list on the Xth item
                    if (rowIndex != 0 && rowIndex%breakAtItem == 0)
                    {
                        htmlString.AppendLine(ul.ToString(TagRenderMode.EndTag));
                        htmlString.AppendLine(li.ToString(TagRenderMode.EndTag));
                        htmlString.AppendLine(li.ToString(TagRenderMode.StartTag));
                        htmlString.AppendLine(ul.ToString(TagRenderMode.StartTag));
                    }
                    htmlString.AppendLine(li.ToString(TagRenderMode.StartTag));
                    htmlString.Append(html.DisplayFor(singleItemExpression));
                    htmlString.AppendLine(li.ToString(TagRenderMode.EndTag));
                    rowIndex++;
                }
                htmlString.AppendLine(ul.ToString(TagRenderMode.EndTag));
                htmlString.AppendLine(li.ToString(TagRenderMode.EndTag));
                htmlString.AppendLine(ul.ToString(TagRenderMode.EndTag));
            }
            return MvcHtmlString.Create(htmlString.ToString());
        }

    /*********************************************************/



    private static ExpandoObject GetGneAttributes(object attributes)
        {
            dynamic gneAttributes = new ExpandoObject();
            var gneCss = " form-control";
            // 'attributes' may be an annonymous type and as a result - immutable
            // we must create a dynamic object that mirrors 'attributes' in order to add the necessary additional classes
            if (attributes != null)
            {
                foreach (var property in attributes.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    )
                {
                    ((IDictionary<string, object>)gneAttributes).Add(property.Name.Replace("_", "-"), property.GetValue(attributes, null));
                }
            }
            // in case we don't yet have form-control added - risk adding it again, but no danger
            object value;
            if (((IDictionary<string, object>)gneAttributes).TryGetValue("class", out value))
            {
                ((IDictionary<string, object>)gneAttributes)["class"] += gneCss;
            }
            else
            {
                ((IDictionary<string, object>)gneAttributes).Add("class", gneCss);
            }
            return gneAttributes;
        }

        #region " Collection Items"
        private const string idsToReuseKey = "__htmlPrefixScopeExtensions_IdsToReuse_";

        public static IDisposable BeginCollectionItem(this HtmlHelper html, string collectionName)
        {
            var idsToReuse = GetIdsToReuse(html.ViewContext.HttpContext, collectionName);
            string itemIndex = idsToReuse.Count > 0 ? idsToReuse.Dequeue() : Guid.NewGuid().ToString();

            // autocomplete="off" is needed to work around a very annoying Chrome behaviour whereby it reuses old values after the user clicks "Back", which causes the xyz.index and xyz[...] values to get out of sync.
            html.ViewContext.Writer.WriteLine(string.Format("<input type=\"hidden\" name=\"{0}.index\" autocomplete=\"off\" value=\"{1}\" />", collectionName, html.Encode(itemIndex)));

            return BeginHtmlFieldPrefixScope(html, string.Format("{0}[{1}]", collectionName, itemIndex));
        }

        public static IDisposable BeginCollectionItem(this HtmlHelper html, string collectionName, out string identifier)
        {
            var idsToReuse = GetIdsToReuse(html.ViewContext.HttpContext, collectionName, string.Empty);
            string itemIndex = idsToReuse.Count > 0 ? idsToReuse.Dequeue() : Guid.NewGuid().ToString();

            // autocomplete="off" is needed to work around a very annoying Chrome behaviour whereby it reuses old values after the user clicks "Back", which causes the xyz.index and xyz[...] values to get out of sync.
            html.ViewContext.Writer.WriteLine("<input type=\"hidden\" name=\"{0}.index\" autocomplete=\"off\" value=\"{1}\" />", collectionName, html.Encode(itemIndex));

            identifier = string.Format("{0}[{1}]", collectionName, itemIndex);
            return BeginHtmlFieldPrefixScope(html, identifier);
        }

        public static IDisposable BeginHtmlFieldPrefixScope(this HtmlHelper html, string htmlFieldPrefix)
        {
            return new HtmlFieldPrefixScope(html.ViewData.TemplateInfo, htmlFieldPrefix);
        }

        private static Queue<string> GetIdsToReuse(HttpContextBase httpContext, string collectionName)
        {
            // We need to use the same sequence of IDs following a server-side validation failure,  
            // otherwise the framework won't render the validation error messages next to each item.
            string key = idsToReuseKey + collectionName;
            var queue = (Queue<string>)httpContext.Items[key];
            if (queue == null)
            {
                httpContext.Items[key] = queue = new Queue<string>();
                var previouslyUsedIds = httpContext.Request[collectionName + ".index"];
                if (!string.IsNullOrEmpty(previouslyUsedIds))
                    foreach (string previouslyUsedId in previouslyUsedIds.Split(','))
                        queue.Enqueue(previouslyUsedId);
            }
            return queue;
        }

        private static Queue<string> GetIdsToReuse(HttpContextBase httpContext, string collectionName, string parentIdentifier)
        {
            string key;
            if (!string.IsNullOrEmpty(parentIdentifier))
            {
                key = idsToReuseKey + parentIdentifier + "." + collectionName;
            }
            else
            {
                key = idsToReuseKey + collectionName;
            }

            var queue = (Queue<string>)httpContext.Items[key];
            if (queue == null)
            {
                httpContext.Items[key] = queue = new Queue<string>();

                string previouslyUsedIds;

                // Format of identifier depends on if we're looking at a parent or child collection
                if (!string.IsNullOrEmpty(parentIdentifier))
                {
                    previouslyUsedIds = httpContext.Request[parentIdentifier + "." + collectionName + ".index"];
                }
                else
                {
                    previouslyUsedIds = httpContext.Request[collectionName + ".index"];
                }

                if (!string.IsNullOrEmpty(previouslyUsedIds))
                    foreach (string previouslyUsedId in previouslyUsedIds.Split(','))
                        queue.Enqueue(previouslyUsedId);
            }
            return queue;
        }

        public static IDisposable BeginChildCollectionItem(this HtmlHelper html, string collectionName, string parentIdentifier, out string identifier)
        {
            var idsToReuse = GetIdsToReuse(html.ViewContext.HttpContext, collectionName, parentIdentifier);
            string itemIndex = idsToReuse.Count > 0 ? idsToReuse.Dequeue() : Guid.NewGuid().ToString();

            html.ViewContext.Writer.WriteLine("<input type=\"hidden\" name=\"{0}.{1}.index\" autocomplete=\"off\" value=\"{2}\" />", parentIdentifier, collectionName, html.Encode(itemIndex));

            // NOTE: We also prefix the identifier here with its parent identifier tag
            identifier = string.Format("{0}.{1}[{2}]", parentIdentifier, collectionName, itemIndex);
            return BeginHtmlFieldPrefixScope(html, identifier);
        }

        /// <summary>
        /// Retrieves the value to use for an 'id' tag for an HTML element that is part of a collection
        /// that is being passed back & forth via POSTing a form.
        /// </summary>
        public static string GetHtmlId(this HtmlHelper html, string identifier, string propertyName)
        {
            var idValue = identifier;
            idValue = idValue.Replace("[", "_");
            idValue = idValue.Replace("]", "__" + propertyName);
            return idValue;
        }

        /// <summary>
        /// Retrieves the value to use for a 'name' tag for an HTML element that is part of a collection
        /// that is being passed back & forth via POSTing a form.
        /// </summary>
        public static string GetHtmlName(this HtmlHelper html, string identifier, string propertyName)
        {
            return identifier + "." + propertyName;
        }

        private class HtmlFieldPrefixScope : IDisposable
        {
            private readonly TemplateInfo templateInfo;
            private readonly string previousHtmlFieldPrefix;

            public HtmlFieldPrefixScope(TemplateInfo templateInfo, string htmlFieldPrefix)
            {
                this.templateInfo = templateInfo;

                previousHtmlFieldPrefix = templateInfo.HtmlFieldPrefix;
                templateInfo.HtmlFieldPrefix = htmlFieldPrefix;
            }

            public void Dispose()
            {
                templateInfo.HtmlFieldPrefix = previousHtmlFieldPrefix;
            }
        }

        //see https://stackoverflow.com/questions/19765439/
        public class ExtendedSelectItem : SelectListItem
        {
            public string Class { get; set; }
        }

        //see https://stackoverflow.com/questions/19765439/
        public static MvcHtmlString ExtendedDropdownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<ExtendedSelectItem> list, string optionLabel, object htmlAttributes)
        {
            return ExtendedDropdownList(htmlHelper, ExpressionHelper.GetExpressionText(expression), list, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        //see https://stackoverflow.com/questions/19765439/
        public static MvcHtmlString ExtendedDropdownList(this HtmlHelper htmlHelper, string name, IEnumerable<ExtendedSelectItem> list, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder dropdown = new TagBuilder("select");
            dropdown.Attributes.Add("name", name);
            dropdown.Attributes.Add("id", name);
            StringBuilder options = new StringBuilder();

            // Make optionLabel the first item that gets rendered.
            if (optionLabel != null)
                options = options.Append("<option value='" + String.Empty + "'>" + optionLabel + "</option>");

            foreach (var item in list)
            {
                if (item.Disabled)
                    options = options.Append("<option " + (item.Selected ? "selected " : "") + "value='" + item.Value + "' class='" + item.Class + "' disabled='disabled'>" + item.Text + "</option>");
                else
                    options = options.Append("<option " + (item.Selected ? "selected " : "") + "value='" + item.Value + "' class='" + item.Class + "'>" + item.Text + "</option>");
            }
            dropdown.InnerHtml = options.ToString();
            dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }

        #endregion
    }
}