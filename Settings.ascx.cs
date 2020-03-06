/*
' Copyright (c) 2020  Techemistry.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Linq;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

namespace Techemistry.TKC_DnnBlog
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// 
    /// Typically your settings control would be used to manage settings for your module.
    /// There are two types of settings, ModuleSettings, and TabModuleSettings.
    /// 
    /// ModuleSettings apply to all "copies" of a module on a site, no matter which page the module is on. 
    /// 
    /// TabModuleSettings apply only to the current module on the current page, if you copy that module to
    /// another page the settings are not transferred.
    /// 
    /// If you happen to save both TabModuleSettings and ModuleSettings, TabModuleSettings overrides ModuleSettings.
    /// 
    /// Below we have some examples of how to access these settings but you will need to uncomment to use.
    /// 
    /// Because the control inherits from TKC_DnnBlogSettingsBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : TKC_DnnBlogModuleSettingsBase
    {
        #region Base Method Implementations

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    //Check for existing settings and use those on this page
                    //Settings["SettingName"]

                    

                    BlogDataDataContext db = new BlogDataDataContext();
                    var blogs = db.Blog_Blogs.Where(c => c.PortalID == PortalId).Select(c => new { Name = c.Title, ID = c.BlogID }).ToList();
                    ddlBlogID.DataSource = blogs;
                    ddlBlogID.DataTextField = "Name";
                    ddlBlogID.DataValueField = "ID";
                    ddlBlogID.DataBind();



                    //uncomment to load saved settings in the text boxes
                   
                    if (Settings.Contains("BlogMode"))
                        ddlMode.SelectedValue = Settings["BlogMode"].ToString();

                    if (Settings.Contains("ddlBlogID"))
                        ddlBlogID.SelectedValue = Settings["ddlBlogID"].ToString();

                    if (Settings.Contains("txtArticlesQty"))
                        txtArticlesQty.Text = Settings["txtArticlesQty"].ToString();

                    if (Settings.Contains("txtBlogUrl"))
                        txtBlogUrl.Text = Settings["txtBlogUrl"].ToString();

                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
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
                modules.UpdateModuleSetting(ModuleId, "BlogMode", ddlMode.SelectedValue);
                modules.UpdateModuleSetting(ModuleId, "ddlBlogID", ddlBlogID.SelectedValue);
                modules.UpdateModuleSetting(ModuleId, "txtArticlesQty", txtArticlesQty.Text);
                modules.UpdateModuleSetting(ModuleId, "txtBlogUrl", txtBlogUrl.Text);

                //tab module settings
                //modules.UpdateTabModuleSetting(TabModuleId, "BlogIDNum",  txtBlogIDNum.Text);
                //modules.UpdateTabModuleSetting(TabModuleId, "Setting2",  txtSetting2.Text);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}