<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Techemistry.TKC_DnnBlog.View" %>

<asp:Label ID="lblMsg" runat="server" />

<div id="blogArticles">
    <asp:Repeater ID="rptArticles" runat="server" Visible="false">
        <ItemTemplate>
            <div class="blogArticles">
                <a href='<%# Eval("RawUrl") %>' class="blogTitle"><%# Eval("Title") %></a><br />
                <p>Date: <%# Eval("Created") %></p>

                <p><%# Eval("Summary") %></p>
                <p><%# Eval("ReadMoreHyperLink") %></p>
                
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

<div id="blogArticle">
    <asp:Button ID="btnBackToList" runat="server" Text="go back" OnClick="btnBackToList_Click" style="float:right;" />
    <asp:Repeater ID="rptBlogArticleDetail" runat="server" Visible="false">
        <ItemTemplate>
            <div class="blogRecentArticle">
                <h3><%# Eval("Title") %></h3>
                <p>Date: <%# Eval("Created") %></p>
                <%# Eval("Body") %>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>


