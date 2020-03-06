<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentArticles_Settings.ascx.cs" Inherits="Techemistry.TKC_DnnBlog.RecentArticles_Settings" %>

<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded">Recent Article Settings</a></h2>
<fieldset>
    <div class="dnnFormItem">
        <asp:Label ID="lblSetting1" runat="server" Text="Number Of Articles To Display: " />
        <asp:TextBox ID="txtArticlesQty" runat="server" />
    </div>
    <div class="dnnFormItem">
        <asp:Label ID="lbl" runat="server" Text="Blog Page URL: " />
        <asp:TextBox ID="txtBlogUrl" runat="server" />
        <p>Should be entered as /[blogpageName]</p>
    </div>

    <div class="dnnFormItem">
        <asp:Label ID="Label1" runat="server" Text="Blog Module ID:" />
        <asp:DropDownList ID="ddlBlogID" runat="server" AppendDataBoundItems="true">
            <asp:ListItem Value="0">...select...</asp:ListItem>
        </asp:DropDownList>
    </div>

</fieldset>