<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="Techemistry.TKC_DnnBlog.Settings" %>


<!-- uncomment the code below to start using the DNN Form pattern to create and update settings -->


<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded">Settings</a></h2>
<fieldset>
    
    <div class="dnnFormItem">
        Module Mode: 
            <asp:DropDownList ID="ddlMode" runat="server">
                <asp:ListItem Value="Recent Articles">Recent Articles</asp:ListItem>
                <asp:ListItem Value="Blog">Blog</asp:ListItem>
            </asp:DropDownList>
    </div>
    <div class="dnnFormItem">
        <asp:Label ID="Label1" runat="server" Text="Blog Module ID:" />
        <asp:DropDownList ID="ddlBlogID" runat="server" AppendDataBoundItems="true">
            <asp:ListItem Value="0">...select...</asp:ListItem>
        </asp:DropDownList>
    </div>

    <div class="dnnFormItem">
        <asp:Label ID="lblSetting1" runat="server" Text="Number Of Articles To Display: " />
        <asp:TextBox ID="txtArticlesQty" runat="server" />
    </div>

    <div class="dnnFormItem">
        <asp:Label ID="lbl" runat="server" Text="Blog Page URL: " />
        <asp:TextBox ID="txtBlogUrl" runat="server" />
        <p>Should be entered as /[blogpageName]</p>
    </div>

</fieldset>


