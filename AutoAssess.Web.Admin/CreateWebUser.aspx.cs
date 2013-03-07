
using System;
using System.Web;
using System.Web.UI;
using System.Security.Cryptography;
using AutoAssess.Web.Data;
using AutoAssess.Misc;
using NHibernate;

namespace AutoAssess.Web.Admin
{


	public partial class CreateWebUser : System.Web.UI.Page //: AutoAssessPage
	{
		protected void btnCreateUser_Click(object sender, EventArgs e)
		{
			ISession s = this.Session["User?Session"] as ISession;
			
			using (ITransaction t = s.BeginTransaction())
			{
				WebUser user = new WebUser();
				user.Username = txtUsername.Text;
				user.EmailAddress = txtEmailAddress.Text;
				user.UserID = Guid.NewGuid();
				user.IsActive = true;
				//user.CreatedBy = this.CurrentUser.UserID;
				//user.CreatedOn = DateTime.UtcNow;
				
				string hash = Hashing.GetMd5Hash(txtPassword.Text,  "sadf");
				
				user.PasswordHash = hash;
				
				s.Save(user);
				t.Commit();
			}
			
		}
	}
}

