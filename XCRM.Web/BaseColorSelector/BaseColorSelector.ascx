<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_BaseColorSelector" Codebehind="BaseColorSelector.ascx.cs" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v18.2, Version=18.2.1.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<a href="javascript:void(0)" class="ThemeButton baseColorLink" title="Current base color: <%= ASPxWebControl.GlobalThemeBaseColor %>" style="position: relative;" id="ColorSelectorButton"><img src="BaseColorSelector/Images/edtDropDown.png" /><span>Theme Settings</span></a>
<dx:ASPxPopupControl ID="ColorSelectorPopup" ClientInstanceName="ColorSelectorPopup" CssClass="ColorSelectorPopup" runat="server"
    ShowHeader="False" ShowShadow="false" PopupAnimationType="None"
    PopupElementID="ColorSelectorButton" PopupHorizontalAlign="RightSides" PopupVerticalAlign="TopSides"
    PopupHorizontalOffset="24" PopupVerticalOffset="-19" PopupAlignCorrection="Disabled"
    PopupAction="LeftMouseClick" Width="309px">
    <contentcollection>
        <dx:PopupControlContentControl runat="server">
            <div class="button-wrapper">
                <div class="color-button-border">
                </div>
            </div>
            <div class="button-wrapper-bottomBorder"></div>
            <b class="Clear"></b>
            <div class="colors-container-wrapper">
                <div class="colors-container">
                    <div class="base-color-container">
                        <dx:ASPxLabel runat="server" ID="lblBaseColor" Text="Base Color" CssClass="caption" EnableViewState="false"></dx:ASPxLabel>
                        <asp:Repeater runat="server" ID="rpColorButtons" EnableViewState="false" EnableTheming="false">
                            <ItemTemplate><!--Fix whitespace issue
                             --><div class="color-item-container">
                                    <div class="color-item-wrapper">
                                        <dx:ASPxButton runat="server" ID="btnColor" GroupName="colors" CssClass="color-item" BackColor="<%# System.Drawing.ColorTranslator.FromHtml((string)Container.DataItem) %>"
                                         UseSubmitBehavior="false" AutoPostBack="false" EnableViewState="false" OnLoad="btnColor_Load">
                                            <CheckedStyle CssClass="selected-item"></CheckedStyle>
                                        </dx:ASPxButton>
                                    </div>
                                </div><!--Fix whitespace issue
                         --></ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <dx:ASPxLabel runat="server" ID="lblThemeFont" EnableViewState="false" Text="Font" CssClass="caption-font"></dx:ASPxLabel>
                    <dx:ASPxRadioButtonList ID="rblFonts" runat="server" ClientInstanceName="rblFonts" RepeatColumns="2" RepeatLayout="Table" EnableViewState="false" Width="100%" Theme="XafTheme">
                        <Border BorderStyle="none" />
                        <Paddings Padding="0" />
                        <ClientSideEvents SelectedIndexChanged="function(){{ DXDemo.SetCurrentFont(rblFonts.GetSelectedItem().value); }}" />
                    </dx:ASPxRadioButtonList>
                </div>
            </div>
        </dx:PopupControlContentControl>
    </contentcollection>
</dx:ASPxPopupControl>
