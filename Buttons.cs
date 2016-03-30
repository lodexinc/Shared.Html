using System;
using System.Text;
using System.Web.Mvc;

namespace Shared.Html
{
    public static class Buttons
    {
        public static MvcHtmlString AddRow(string displayText = "", string buttonId = "")
        {
            return GneButton(
                displayText,
                "fa-plus",
                buttonId,
                "#",
                " addRow "
            );
        }

        public static MvcHtmlString Basic(
                    string displayText = "", 
            string href = "#", 
            string iconClass = "", 
            string cssClasses = "", 
            bool openNewTab = false,
            bool submit = false,
            string tooltipText = "", 
            string buttonId = ""
        )
        {
            return GneButton(
                displayText,
                iconClass,
                buttonId,
                href,
                " btn-primary shadow-z-2 unprintable " + cssClasses,
                openNewTab,
                submit,
                tooltipText
            );
        }

        public static MvcHtmlString Cancel(string href = "#", string cssClasses = "")
        {
            return GneButton(
                "Cancel",
                "fa-power-off",
                string.Empty,
                href,
                " btn-default shadow-z-2 unprintable " + cssClasses
            );
        }

        public static MvcHtmlString Capture(string buttonId = "capture", string cssClasses = "")
        {
            return GneButton(
                "Capture",
                "fa-picture-o",
                buttonId == string.Empty ? "capture" : buttonId,
                "#",
                " btn-default shadow-z-2 capture unprintable " + cssClasses
            );
        }

        public static MvcHtmlString Close(string href = "#", string cssClasses = "", string buttonId = "")
        {
            return GneButton(
                "Close",
                "fa-power-off",
                buttonId,
                href,
                " btn-default shadow-z-2 exit unprintable " + cssClasses
            );
        }

        public static MvcHtmlString Danger(string displayText = "", string iconClass = "", string tooltipText = "", string href = "#", string cssClasses = "", bool submit = false)
        {
            return GneButton(
                displayText,
                iconClass,
                string.Empty,
                href,
                " btn-danger shadow-z-2 submit unprintable " + cssClasses,
                false,
                submit,
                tooltipText
            );
        }

        public static MvcHtmlString Default(string displayText = "", string cssClasses = "", string href = "#", bool submit = false, bool openNewTab = false, string iconClass = "fa-check-square-o", string buttonId = "")
        {
            return GneButton(
                displayText,
                iconClass,
                buttonId,
                href,
                " btn-default shadow-z-2 unprintable " + cssClasses,
                openNewTab,
                submit
            );
        }

        public static MvcHtmlString Delete(string href = "#", string cssClasses = "", string tooltipText = "", string buttonId = "", bool submit = false, string displayText = "Delete", bool buttonElement = false)
        {
            return GneButton(
                displayText,
                "fa-trash-o",
                buttonId,
                href,
                " btn-danger shadow-z-2 unprintable delete " + cssClasses,
                false,
                submit,
                tooltipText,
                string.Empty,
                false,
                string.Empty,
                false,
                buttonElement
            );
        }

        public static MvcHtmlString Edit(string href = "#", bool openNewTab = false, bool submit = false, string cssClasses = "", string displayText = "Edit", string tooltipText = "", string buttonId = "")
        {
            return GneButton(
                displayText,
                "fa-pencil-square-o",
                buttonId,
                href,
                " btn-primary shadow-z-2 unprintable " + cssClasses,
                openNewTab,
                submit,
                tooltipText
            );
        }

        public static MvcHtmlString Email(string displayText = "", string tooltipText = "", string href = "#", string cssClasses = "", bool buttonElement = false)
        {
            return GneButton(
                displayText,
                "fa-envelope-o",
                string.Empty,
                href,
                " btn-primary shadow-z-2 submit unprintable " + cssClasses,
                false,
                true,
                tooltipText,
                string.Empty,
                false,
                string.Empty,
                false,
                buttonElement
            );
        }

        public static MvcHtmlString ExportCsv(string href = "#", string displayText = "Export As CSV", string buttonId = "")
        {
            return GneButton(
                displayText,
                "fa-download",
                buttonId,
                href,
                " btn-primary shadow-z-2 unprintable ",
                true
            );
        }

        public static MvcHtmlString ExportXls(string href = "#", string displayText = "Export As Excel", string buttonId = "")
        {
            return GneButton(
                displayText,
                "fa-file-excel-o",
                buttonId,
                href,
                " btn-primary shadow-z-2 unprintable ",
                true
            );
        }

        public static MvcHtmlString Filter(string displayText = "", string buttonId = "", string href = "#", string cssClasses = "")
        {
            return GneButton(
                displayText,
                "fa-filter",
                buttonId,
                href,
                " btn-info shadow-z-2 unprintable " + cssClasses
            );
        }

        public static MvcHtmlString Info(string displayText = "", string buttonId = "", string href = "#", string iconClass = "", string cssClasses = "", bool openNewTab = false)
        {
            return GneButton(
                displayText,
                iconClass,
                buttonId,
                href,
                " btn-info shadow-z-2 unprintable " + cssClasses,
                openNewTab
            );
        }

        public static MvcHtmlString ModalClose(string displayText = "Close", string iconClass = "fa-power-off", string buttonId = "closeModalBtn", string cssClasses = "")
        {
            return GneButton(
                displayText,
                iconClass,
                buttonId,
                "#",
                " btn-default shadow-z-2 unprintable " + cssClasses,
                false,
                false,
                string.Empty,
                string.Empty,
                false,
                string.Empty,
                true
            );
        }

        public static MvcHtmlString ModalOpen(string displayText = "Save", string modalTarget = "saveModal", string iconClass = "fa-floppy-o", string buttonId = "showModalBtn", string cssClasses = "")
        {
            return GneButton(
                displayText,
                iconClass,
                buttonId,
                "#",
                " btn-primary shadow-z-2 unprintable " + cssClasses,
                false,
                false,
                string.Empty,
                modalTarget
            );
        }

        public static MvcHtmlString New(string displayText = "", string buttonId = "", string href = "#", bool openNewTab = false, bool submit = false, string tooltipText = "", string cssClasses = "")
        {
            return GneButton(
                displayText,
                "fa-plus",
                buttonId,
                href,
                " btn-primary shadow-z-2 unprintable " + cssClasses,
                openNewTab,
                submit,
                tooltipText
            );
        }

        public static MvcHtmlString Print(string displayText = "Print", string href = "#", string cssClasses = "")
        {
            return GneButton(
                displayText,
                "fa-print",
                "printable",
                href,
                " btn-default shadow-z-2 print unprintable " + cssClasses
            );
        }

        public static MvcHtmlString Reload(string href = "#", string cssClasses = "")
        {
            return GneButton(
                "Reload",
                "fa-refresh",
                "reloadWindow",
                href,
                " btn-primary shadow-z-2 reloadWindow unprintable " + cssClasses
            );
        }

        public static MvcHtmlString Save(string href = "#", string cssClasses = "", string displayText = "Save", string iconClass = "fa-floppy-o")
        {
            return GneButton(
                displayText,
                iconClass,
                string.Empty,
                href,
                " btn-primary shadow-z-2 submit unprintable " + cssClasses
            );
        }

        public static MvcHtmlString Submit(string displayText = "Save", string iconClass = "fa-floppy-o", string tooltipText = "", string href = "#", string cssClasses = "", string value = "", bool buttonElement = false, bool openNewTab = false)
        {
            return GneButton(
                displayText,
                iconClass,
                string.Empty,
                href,
                " btn-primary shadow-z-2 submit unprintable " + cssClasses,
                openNewTab,
                true,
                tooltipText,
                string.Empty,
                false,
                value,
                false,
                buttonElement,
                "submit"
            );
        }

        public static MvcHtmlString Success(string displayText = "", string cssClasses = "", string href = "#", bool submit = false, bool openNewTab = false, string iconClass = "fa-check-square-o", string buttonId = "", string tooltipText = "")
        {
            return GneButton(
                displayText,
                iconClass,
                buttonId,
                href,
                " btn-success shadow-z-2 unprintable " + cssClasses,
                openNewTab,
                submit,
                tooltipText
            );
        }
        public static MvcHtmlString TableBasic(string displayText = "", string href = "#", string iconClass = "", string cssClasses = "", bool openNewTab = false, bool submit = false, string tooltipText = "", string buttonId = "")
        {
            return GneButton(
                displayText,
                iconClass,
                buttonId,
                href,
                " btn-primary btn-block " + cssClasses,
                openNewTab,
                submit,
                tooltipText
            );
        }

        public static MvcHtmlString TableDelete(string href = "#", string cssClasses = "", bool readOnlyDisabled = false)
        {
            return GneButton(
                string.Empty,
                "fa-trash-o",
                string.Empty,
                href,
                " btn-danger btn-block deleteRow " + cssClasses,
                false,
                false,
                string.Empty,
                string.Empty,
                readOnlyDisabled
            );
        }

        public static MvcHtmlString TableEdit(string displayText = "", string href = "#", bool openNewTab = false, bool submit = false)
        {
            return GneButton(
                displayText,
                "fa-pencil-square-o",
                string.Empty,
                href,
                " btn-primary btn-block ",
                openNewTab,
                submit
            );
        }

        public static MvcHtmlString TableNew(string displayText = "", string href = "#", bool openNewTab = false, bool submit = false)
        {
            return GneButton(
                displayText,
                "fa-plus",
                string.Empty,
                href,
                " btn-primary btn-block ",
                openNewTab,
                submit
            );
        }

        public static MvcHtmlString TableSuccess(string displayText = "", string href = "#", string cssClasses = "", bool readOnlyDisabled = false, string iconClass = "fa-check-square-o")
        {
            return GneButton(
                displayText,
                iconClass,
                string.Empty,
                href,
                " btn-success btn-block " + cssClasses,
                false,
                false,
                string.Empty,
                string.Empty,
                readOnlyDisabled
            );
        }

        public static MvcHtmlString TableView(string displayText = "", string href = "#", bool openNewTab = false, bool submit = false)
        {
            return GneButton(
                displayText,
                "fa-eye",
                "",
                href,
                " btn-primary btn-block ",
                openNewTab,
                submit
            );
        }

        public static MvcHtmlString Toggle(string displayText = "", string buttonId = "", string href = "#")
        {
            return GneButton(
                displayText,
                "fa-info-circle",
                buttonId,
                href,
                " btn-success shadow-z-2 unprintable "
            );
        }

        public static MvcHtmlString Undo(string buttonId = "", string href = "#", string cssClasses = "")
        {
            return GneButton(
                "Undo",
                "fa-undo",
                buttonId,
                href,
                " btn-warning shadow-z-2 unprintable " + cssClasses
            );
        }

        public static MvcHtmlString View(string href = "#", string displayText = "View", bool openNewTab = false, bool submit = false, string cssClasses = "")
        {
            return GneButton(
                displayText,
                "fa-eye",
                string.Empty,
                href,
                " btn-primary shadow-z-2 unprintable " + cssClasses,
                openNewTab,
                submit
            );
        }

        public static MvcHtmlString Warning(string displayText = "", string href = "#", string iconClass = "", string tooltipText = "", string cssClasses = "", bool openNewTab = false, bool submit = false, string buttonId = "")
        {
            return GneButton(
                displayText,
                iconClass,
                buttonId,
                href,
                " btn-warning shadow-z-2 unprintable " + cssClasses,
                openNewTab,
                submit,
                tooltipText
            );
        }
        private static MvcHtmlString GneButton(
            string displayText = "",
            string iconClass = "",
            string buttonId = "",
            string href = "#",
            string cssClasses = "",
            bool openNewTab = false,
            bool submit = false,
            string tooltipText = "",
            string modalTarget = "",
            bool readOnlyDisabled = false,
            string value = "",
            bool modalClose = false,
            bool buttonElement = false,
            string buttonName = ""
        )
        {
            var htmlString = new StringBuilder();
            var anchor = new TagBuilder("a");

            if (buttonElement)
            {
                anchor = new TagBuilder("button");
            }

            anchor.AddCssClass("btn " + cssClasses);
            anchor.Attributes.Add("href", href);
            anchor.Attributes.Add("id", buttonId);
            anchor.Attributes.Add("name", buttonName);

            if (openNewTab)
            {
                anchor.Attributes.Add("target", "_blank");
            }

            if (submit)
            {
                anchor.Attributes.Add("type", "submit");
            }

            if (tooltipText != string.Empty)
            {
                anchor.Attributes.Add("title", tooltipText);
                anchor.Attributes.Add("data-toggle", "tooltip");
                anchor.Attributes.Add("data-placement", "bottom");
            }

            if (modalTarget != string.Empty)
            {
                anchor.Attributes.Add("data-target", "#" + modalTarget);
                anchor.Attributes.Add("data-toggle", "modal");
            }

            if (readOnlyDisabled)
            {
                anchor.Attributes.Add("readonly", "true");
                anchor.Attributes.Add("disabled", "true");
            }

            if (value != string.Empty)
            {
                anchor.Attributes.Add("value", value);
            }

            if (modalClose)
            {
                anchor.Attributes.Add("data-dismiss", "modal");
            }

            htmlString.Append(anchor.ToString(TagRenderMode.StartTag));

            if (iconClass != string.Empty)
            {
                var icon = new TagBuilder("i");
                icon.AddCssClass("fa " + iconClass);
                htmlString.Append(icon.ToString(TagRenderMode.StartTag));
                htmlString.Append(icon.ToString(TagRenderMode.EndTag));
                htmlString.Append("&nbsp;");
            }

            htmlString.Append(displayText);
            htmlString.Append(anchor.ToString(TagRenderMode.EndTag));

            return MvcHtmlString.Create(htmlString.ToString());
        }
    }
}