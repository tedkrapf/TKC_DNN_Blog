<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="Techemistry.TKC_DnnBlog.Edit" %>
<%@ Register TagPrefix="dnn" TagName="texteditor" Src="~/controls/texteditor.ascx" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>

<style>
    .titleCol {
        width: 100px;
        font-weight: bold;
        padding-right: 10px;
    }
</style>

<div class="dnnForm dnnEditBasicSettings" id="dnnEditBasicSettings" style="width: 100%;">
    <div class="dnnFormExpandContent dnnRight "><a href="">Expand</a></div>

    <h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead dnnClear">
        <a href="" class="dnnSectionExpanded">Blog Article Editor</a>
    </h2>

    <table style="width: 100%;">
        <tr>
            <td class="titleCol">Previous Articles: </td>
            <td>
                <div class="dnnFormItem">

                    <asp:DropDownList ID="ddlArticles" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlArticles_SelectedIndexChanged" Style="width: 95%; max-width: 100%;">
                        <asp:ListItem Value="0">..select, or create new below</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
        <tr>
            <td class="titleCol">Title: </td>
            <td>
                <div class="dnnFormItem">

                    <asp:TextBox ID="txtTitle" runat="server" Style="width: 95%; max-width: 100%;" />
                </div>
            </td>
        </tr>
        <tr>
            <td class="titleCol">Is Published: </td>
            <td>
                <div class="dnnFormItem">

                    <asp:CheckBox ID="chkPublished" runat="server" Text="Is Published (Show on the site)" />
                </div>
            </td>
        </tr>
        <tr>

            <td colspan="2">

                <div class="dnnFormItem">
                    <h4>Summary: </h4>
                    <asp:TextBox ID="txtSummary" runat="server" TextMode="MultiLine" Style="height: 200px; width: 95%; max-width: 100%;" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="dnnFormItem">
                    <h4>Body: </h4>
                    <dnn:textEditor id="txtBody" runat="server" height="600px" width="100%" ChooseMode="false"></dnn:textEditor>
                </div>
            </td>
        </tr>
    </table>















</div>
<asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" resourcekey="btnSubmit" CssClass="dnnPrimaryAction" />
<asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" resourcekey="btnCancel" CssClass="dnnSecondaryAction" />

<script type="text/javascript">
    /*globals jQuery, window, Sys */
    (function ($, Sys) {
        function dnnEditBasicSettings() {
            $('#dnnEditBasicSettings').dnnPanels();
            $('#dnnEditBasicSettings .dnnFormExpandContent a').dnnExpandAll({ expandText: 'Expand', collapseText: 'Collapse', targetArea: '#dnnEditBasicSettings' });
        }

        $(document).ready(function () {
            dnnEditBasicSettings();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                dnnEditBasicSettings();
            });
        });

    }(jQuery, window.Sys));
</script>
