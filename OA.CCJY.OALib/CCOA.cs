using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using JumpKick.HttpLib;
using OA.CCJY.OALib.Item.Mail;

namespace OA.CCJY.OALib
{
    public class CCOA
    {
        int flag = 0;

        string CCOADomain;
        string CCOALoginUrl;
        readonly string CCOALoginStr = @"LoginForm%5Busername%5D={0}&LoginForm%5Bpassword%5D={1}";

        string username;
        string password;
        CCOAEvens ccoaLoginEvens;

        public string AttachSavePath{
            get { return Item.Attach.AttachSavePath; }
            set { Item.Attach.AttachSavePath = value; }
        }

        public string AttachTempPath
        {
            get { return Item.Attach.AttachTempPath; }
            set { Item.Attach.AttachTempPath = value; }
        }
        public CCOAMail Mail { get; private set; }
        public CCOADocument Document { get; private set; }

        public CCOA()
        {
            try
            {
                CCOADomain =Setting.GetSystemUrl.domain;
                CCOALoginUrl =CCOADomain + Setting.GetSystemUrl.GetUrl("Login.Post");

                Mail = new CCOAMail();
                Document = new CCOADocument();

                ccoaLoginEvens = new CCOAEvens();
                Subscribe(Mail.GetInboxInfo);
            }
            catch
            {
                flag = -1;
            }
        }

        public void Subscribe(CCOAEvens.CCOAEventHandler handler)
        {
            if(ccoaLoginEvens!=null)
                ccoaLoginEvens.Subscribe(handler);
        }
        //取消订阅事件
        public void UnSubscribe(CCOAEvens.CCOAEventHandler handler)
        {
            if (ccoaLoginEvens != null)
                ccoaLoginEvens.UnSubscribe(handler);
        }

        /// <summary>
        /// 登录OA系统
        /// </summary>
        /// <param name="_username">用户名</param>
        /// <param name="_password">密码</param>
        /// <returns></returns>
        public void Login(string _username, string _password)
        {
            if (flag == -1) ccoaLoginEvens.RaiseEvent(-1, "初始化失败");

            this.username = _username;
            this.password = _password;

            try
            {
                int domainDotPos = CCOADomain.IndexOf(".");
                if (domainDotPos > 0)
                {
                    Cookies.AddCookie("P_username", _username, CCOADomain.Substring(domainDotPos + 1));
                    Http.Get(CCOALoginUrl).OnSuccess(r =>
                    {
                        JumpKick.HttpLib.Builder.RequestBuilder rb = new JumpKick.HttpLib.Builder.RequestBuilder(CCOALoginUrl, HttpVerb.Post).Body(string.Format(CCOALoginStr, _username, _password));
                        rb.AddHeader("Referer", CCOADomain);
                        rb.AddHeader("Content-Type", "application/x-www-form-urlencoded");


                        rb.OnSuccess(r2 =>
                        {
                            ccoaLoginEvens.RaiseEvent(0, r2);
                        }).OnFail(webexception =>
                        {
                            System.Diagnostics.Debug.Write(webexception.Message);
                            ccoaLoginEvens.RaiseEvent(-3, webexception.Message);
                        }).Go();
                    }).OnFail(webexception =>
                    {
                        System.Diagnostics.Debug.Write(webexception.Message);
                        ccoaLoginEvens.RaiseEvent(-2, webexception.Message);
                    }).Go();
                }
                else
                {
                    ccoaLoginEvens.RaiseEvent(-4, "OA域名错误！");
                }
            }
            catch(Exception e)
            {
                ccoaLoginEvens.RaiseEvent(-5, e.Message);
            }
        }

    }
}
