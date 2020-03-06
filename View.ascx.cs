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
using System.Web.UI.WebControls;
using Techemistry.TKC_DnnBlog.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Techemistry.TKC_DnnBlog
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from TKC_DnnBlogModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : TKC_DnnBlogModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //load blog data

                if (Settings.Contains("ddlBlogID"))
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

                            ReadMoreHyperLink = "<a class='blogReadMore' href='" + url + "?article=" + c.EntryID.ToString() + "'>read more</a>",
                            RawUrl = url + "?article=" + c.EntryID.ToString(),

                            Body = HttpUtility.HtmlDecode(c.Entry),
                            Summary = HtmlStrip_DescriptionOrBody(c.Description ?? "", c.Entry ?? "", 250)


                        }).ToList();



                    rptArticles.Visible = false;
                    rptBlogArticleDetail.Visible = false;
                    btnBackToList.Visible = false;

                    if (Request["article"] == null)
                    {
                        rptArticles.Visible = true;
                        rptArticles.DataSource = articles;
                        rptArticles.DataBind();
                    }
                    else
                    {
                        articles = articles.Where(c => c.ID == Convert.ToInt32(Request["article"])).ToList();
                        rptBlogArticleDetail.Visible = true;
                        rptBlogArticleDetail.DataSource = articles;
                        rptBlogArticleDetail.DataBind();
                        btnBackToList.Visible = true;
                    }
                }
                else
                {
                    lblMsg.Text = "Not setup - please open the module settings and specify a Blog ID #.";
                    return;
                }

                

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private string HtmlStrip_DescriptionOrBody(string description, string body, int shortenLength = 250)
        {
            string html = "";
            bool shorten = false;

            if (description == null || description.Length < 50)
            {
                html = body;
                shorten = true;
            }
            else
                html = description;

            html = HttpUtility.HtmlDecode(html);

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html ?? "");

            string textOnly = doc.DocumentNode.InnerText;

            if (shorten && textOnly.Length > shortenLength)
                textOnly = textOnly.Substring(0, shortenLength);

            return textOnly;
        }

        private void LoadBlogArticlesList(int blogID, string blogMode, int showQty = 100)
        {
            //BlogDataDataContext db = new BlogDataDataContext();
            //var articlesRaw = db.Blog_Entries.Where(c => c.BlogID == blogID && c.Published == true).OrderByDescending(c => c.AddedDate).Take(showQty).ToList();
            //var articles = articlesRaw
            //    .Select(c => new
            //    {
            //        ID = c.EntryID,
            //        Title = c.Title,
            //        Body = HttpUtility.HtmlDecode(c.Entry),
            //        Summary = c.Description == null || c.Description.Length < 50 ? HttpUtility.HtmlDecode(c.Entry).ToCharArray().Take(100).ToString() : HttpUtility.HtmlDecode(c.Description),
            //        Created = c.AddedDate,
            //        ReadMoreHyperLink = "<a href='/tabid/" + Request["tabid"] + "/article/" + c.EntryID.ToString() + "'>read more</a>",
            //        RawUrl = "/tabid/" + Request["tabid"] + "/article/" + c.EntryID.ToString()

            //    }).ToList();

            //rptArticles.Visible = false;
            //rptRecentArticles.Visible = false;
            //rptBlogArticleDetail.Visible = false;

            //if (Request["article"] == null)
            //{
            //    switch (blogMode)
            //    {
            //        case "Recent Articles":
            //            rptRecentArticles.Visible = true;
            //            rptRecentArticles.DataSource = articles;
            //            rptRecentArticles.DataBind();
            //            break;

            //        case "Blog":
            //            rptArticles.Visible = true;
            //            rptArticles.DataSource = articles;
            //            rptArticles.DataBind();
            //            break;
            //    }
            //}
            //else
            //{
            //    rptBlogArticleDetail.Visible = true;
            //    rptBlogArticleDetail.DataSource = articles;
            //    rptBlogArticleDetail.DataBind();
            //}
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

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect(Settings["txtBlogUrl"].ToString());
        }
    }
}