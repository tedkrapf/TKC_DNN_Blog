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
using DotNetNuke.Entities.Users;
using Techemistry.TKC_DnnBlog.Components;
using DotNetNuke.Services.Exceptions;
using System.Linq;
using System.Web;

namespace Techemistry.TKC_DnnBlog
{
    /// -----------------------------------------------------------------------------
    /// <summary>   
    /// The Edit class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from TKC_DnnBlogModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : TKC_DnnBlogModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Implement your edit logic for your module
                if (!Page.IsPostBack)
                {
                    BlogDataDataContext db = new BlogDataDataContext();
                    var articles = db.Blog_Entries.Where(c => c.Blog_Blog.PortalID == PortalId).ToList();

                    ddlArticles.DataSource = articles.Select(c => new { Name = c.Title + " (" + c.AddedDate.ToShortDateString() + ")", ID = c.EntryID, c.AddedDate }).OrderByDescending(c => c.AddedDate).ToList();
                    ddlArticles.DataTextField = "Name";
                    ddlArticles.DataValueField = "ID";
                    ddlArticles.DataBind();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var t = new Item();
            var tc = new ItemController();

            BlogDataDataContext db = new BlogDataDataContext();
            Blog_Entry b = new Blog_Entry();

            if (ddlArticles.SelectedValue != "0")
            {
                //edit
                b = db.Blog_Entries.Where(c => c.EntryID == ArticleID).FirstOrDefault();
                b.Title = txtTitle.Text;
                b.Entry = txtBody.Text;
                b.Description = txtSummary.Text;
                b.Published = chkPublished.Checked;
                db.SubmitChanges();
            }
            else
            {
                //new
                b.Title = txtTitle.Text;
                b.Entry = txtBody.Text;
                b.Description = txtSummary.Text;
                b.Published = chkPublished.Checked;
                b.AddedDate = DateTime.Now;
                b.AllowComments = false;
                b.BlogID = Convert.ToInt32(Settings["BlogIDNum"].ToString());
                b.DisplayCopyright = true;
                b.PermaLink = "n/a";

                db.Blog_Entries.InsertOnSubmit(b);
                db.SubmitChanges();
            }
            
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void ddlArticles_SelectedIndexChanged(object sender, EventArgs e)
        {
            BlogDataDataContext db = new BlogDataDataContext();
            var b = db.Blog_Entries.Where(c => c.EntryID.ToString() == ddlArticles.SelectedValue).FirstOrDefault();
            txtTitle.Text = b.Title;
            txtBody.Text = b.Entry;
            txtSummary.Text = b.Description;
            chkPublished.Checked = b.Published;
        }
    }
}