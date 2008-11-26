﻿using System;
using Ra.Widgets;
using Entities;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected override void OnInit(EventArgs e)
    {
        if (Operator.TryLoginFromCookie())
        {
            // Successfully logged in from cookie...
            Login();
        }
        base.OnInit(e);
    }

    protected void goToProfile_Click(object sender, EventArgs e)
    {
    }

    protected void register_UserRegistered(object sender, EventArgs e)
    {
        infoLabel.Text = "Successfully registered";
        new EffectFadeIn(infoLabel, 1000)
            .ChainThese(new EffectHighlight(infoLabel, 500))
            .Render();
        infoTimer.Enabled = true;
    }

    protected void infoTimer_Tick(object sender, EventArgs e)
    {
        new EffectHighlight(infoLabel, 500)
            .ChainThese(new EffectFadeOut(infoLabel, 1000))
            .Render();
        infoTimer.Enabled = false;
    }

    protected void askQuestion_Click(object sender, EventArgs e)
    {
        ask.ShowAskQuestion();
    }

    protected void ask_QuestionAsked(object sender, EventArgs e)
    {
        IDefault def = (Page as IDefault);
        if (def != null)
            def.QuestionsUpdated();
    }

    protected void loginBtn_Click(object sender, EventArgs e)
    {
        login.ShowLogin();
    }

    protected void registerBtn_Click(object sender, EventArgs e)
    {
        register.ShowRegisterWindow();
    }

    protected void login_LoggedIn(object sender, EventArgs e)
    {
        Login();
        new EffectHighlight(loginPnl, 500).Render();
    }

    private void Login()
    {
        loginBtn.Visible = false;
        registerBtn.Visible = false;
        logouBtn.Visible = true;
        askQuestion.Visible = true;
        goToProfile.Visible = true;
        goToProfile.Text = 
            string.Format("Profile - {1} creds", 
                Operator.Current.Username, 
                Operator.Current.GetCreds());
        logouBtn.Text = "Logout " + Operator.Current.Username;
    }

    protected void logouBtn_Click(object sender, EventArgs e)
    {
        Operator.Logout();
        loginBtn.Visible = true;
        registerBtn.Visible = true;
        goToProfile.Visible = false;
        logouBtn.Visible = false;
        askQuestion.Visible = false;
        new EffectHighlight(loginPnl, 500).Render();
    }
}