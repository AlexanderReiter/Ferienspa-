﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Programmer: Kollross Marcel
/// Date: 03.12.2019
/// Verififed by: Reiter Alexander
/// </summary>
namespace Ferienspass
{
    public partial class user_changepassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSavePassword_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text;

            DB db = new DB();
            DataTable dt = db.Query("SELECT * FROM user WHERE email=?", User.Identity.Name);

            if (Password.EncryptPassword(oldPassword, (string)dt.Rows[0]["passwordsalt"]) == (string)dt.Rows[0]["password"])
            {
                if (txtOldPassword.Text == txtNewPassword.Text)
                {
                    litPasswordError.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Altes Passwort und neues Passwort dürfen nicht gleich sein!</div></div></div>";
                }
                else if (txtNewPassword.Text == txtRepeatPassword.Text)
                {
                    string newPassword = txtNewPassword.Text;
                    string salt = Password.GenerateSalt();
                    string encryptPassword = Password.EncryptPassword(newPassword, salt);

                    db.Query("UPDATE user SET password=?, passwordsalt=? WHERE email=?", encryptPassword, salt, User.Identity.Name);

                    Response.Redirect("~/user-settings.aspx");
                }
                else if(string.IsNullOrEmpty(txtNewPassword.Text))
                {
                    litPasswordError.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Neues Passwort eingeben!</div></div></div>";
                }
                else if(string.IsNullOrEmpty(txtRepeatPassword.Text))
                {
                    litPasswordError.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Neues Passwort wiederholen!</div></div></div>";
                }
                else
                {
                    litPasswordError.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Neue Passwörter stimmen nicht überein!</div></div></div>";
                }
            }
            else if(string.IsNullOrEmpty(txtOldPassword.Text))
            {
                litPasswordError.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Altes Passwort eingeben!</div></div></div>";
            }
            else
            {
                litPasswordError.Text = "<div class='row'><div class='col'><div class='alert alert-danger'>Altes Passwort ist falsch!</div></div></div>";
            }
        }

        protected void btnCancelPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/user-settings.aspx");
        }
    }
}