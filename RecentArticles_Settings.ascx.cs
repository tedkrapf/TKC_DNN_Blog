using System;
using DotNetNuke.Data;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using System.Linq;

namespace Techemistry.TKC_DnnBlog
{
    public partial class RecentArticles_Settings : TKC_DnnBlogModuleSettingsBase
    {
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack == false)
                {

                    BlogDataDataContext db = new BlogDataDataContext();
                    var blogs = db.Blog_Blogs.Where(c => c.PortalID == PortalId).Select(c => new { Name = c.Title, ID = c.BlogID }).ToList();
                    ddlBlogID.DataSource = blogs;
                    ddlBlogID.DataTextField = "Name";
                    ddlBlogID.DataValueField = "ID";
                    ddlBlogID.DataBind();

                    if (Settings.Contains("txtArticlesQty"))
                        txtArticlesQty.Text = Settings["txtArticlesQty"].ToString();

                    if (Settings.Contains("txtBlogUrl"))
                        txtBlogUrl.Text = Settings["txtBlogUrl"].ToString();

                    if (Settings.Contains("ddlBlogID"))
                        ddlBlogID.SelectedValue = Settings["ddlBlogID"].ToString();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private class BlogModules
        {
            public int ModuleID { get; set; }
            public string Name { get; set; }
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void UpdateSettings()
        {
            try
            {
                var modules = new ModuleController();

                //the following are two sample Module Settings, using the text boxes that are commented out in the ASCX file.
                //module settings
                modules.UpdateModuleSetting(ModuleId, "txtArticlesQty", txtArticlesQty.Text);
                modules.UpdateModuleSetting(ModuleId, "txtBlogUrl", txtBlogUrl.Text);
                modules.UpdateModuleSetting(ModuleId, "ddlBlogID", ddlBlogID.SelectedValue);

                //tab module settings
                //modules.UpdateTabModuleSetting(TabModuleId, "Setting1",  txtSetting1.Text);
                //modules.UpdateTabModuleSetting(TabModuleId, "Setting2",  txtSetting2.Text);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
    }
}