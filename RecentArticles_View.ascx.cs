using System;
using System.Web.UI.WebControls;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using System.Linq;
using System.Web;

namespace Techemistry.TKC_DnnBlog
{
    public partial class RecentArticles_View : TKC_DnnBlogModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int artQty = 10;
                if (Settings.Contains("txtArticlesQty"))
                    int.TryParse(Settings["txtArticlesQty"].ToString(), out artQty);

                int blogID = 0;
                if (Settings.Contains("ddlBlogID"))
                    blogID = Convert.ToInt32(Settings["ddlBlogID"].ToString());

                BlogDataDataContext db = new BlogDataDataContext();
                var articlesRaw = db.Blog_Entries.Where(c => c.BlogID == blogID && c.Published == true).OrderByDescending(c => c.AddedDate).Take(artQty).ToList();

                string url = "#";
                if (Settings.Contains("txtBlogUrl"))
                    url = Settings["txtBlogUrl"].ToString();


                var articles = articlesRaw
                    .Select(c => new
                    {
                        ID = c.EntryID,
                        Title = c.Title,
                        Created = c.AddedDate,
                        //ReadMoreHyperLink = "<a href='" + url + "/tabid/" + Request["tabid"] + "/article/" + c.EntryID.ToString() + "'>read more</a>",
                        //RawUrl = url + "/tabid/" + Request["tabid"] + "/article/" + c.EntryID.ToString(),
                        ReadMoreHyperLink = "<a href='" + url + "?article=" + c.EntryID.ToString() + "'>read more</a>",
                        RawUrl = url + "?article=" + c.EntryID.ToString()

                    }).ToList();

                
                    rptRecentArticles.DataSource = articles;
                    rptRecentArticles.DataBind();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }
    }
}