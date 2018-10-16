using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.Web;
using DevExpress.Web.Demos;

public partial class UserControls_BaseColorSelector : System.Web.UI.UserControl {  
    protected void Page_Load(object sender, EventArgs e) {
        rpColorButtons.DataSource = Utils.CustomBaseColors;
        rpColorButtons.DataBind();

        rblFonts.DataSource = Utils.GetFontFamiliesDataSource();
        rblFonts.DataBind();
        rblFonts.SelectedIndex = rblFonts.Items.IndexOfValue(ASPxWebControl.GlobalThemeFont);
    }
    protected void btnColor_Load(object sender, EventArgs e) {
        var button = ((ASPxButton)sender);
        button.ClientSideEvents.Click = string.Format("function(){{ DXDemo.SetCurrentBaseColor('{0}'); }}", ColorTranslator.ToHtml(button.BackColor));
        button.Checked = ColorTranslator.FromHtml(ASPxWebControl.GlobalThemeBaseColor) == button.BackColor;
    }
}
