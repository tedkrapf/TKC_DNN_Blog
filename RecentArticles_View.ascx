<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentArticles_View.ascx.cs" Inherits="Techemistry.TKC_DnnBlog.RecentArticles_View" %>

<asp:Label ID="lblMsg" runat="server" />

<div id="blogRecentArticles">
    <asp:Repeater ID="rptRecentArticles" runat="server">
        <ItemTemplate>
            <div class="blogRecentArticle">
                <a href='<%# Eval("RawUrl") %>' class="blogTitle">
                    <%# Eval("Title") %>
                </a>
                <br />
                <p>Date: <%# Eval("Created") %></p>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>