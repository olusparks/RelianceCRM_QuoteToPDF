using AjaxControlToolkit.HtmlEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumeSoftSparkEntityWCF.App_Code
{

    public class CustomEditor : Editor
    {
        protected override void FillTopToolbar()
        {
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HtmlEditor.ToolbarButtons.Underline());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HtmlEditor.ToolbarButtons.Bold());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HtmlEditor.ToolbarButtons.Italic());
        }

        protected override void FillBottomToolbar()
        {
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HtmlEditor.ToolbarButtons.DesignMode());
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HtmlEditor.ToolbarButtons.PreviewMode());
        }
    }
}