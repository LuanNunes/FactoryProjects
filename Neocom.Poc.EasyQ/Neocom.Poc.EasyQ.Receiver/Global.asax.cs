﻿using Newtonsoft.Json;
using System;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using Neocom.Poc.EasyQ.Receiver.Bus;

namespace Neocom.Poc.EasyQ.Receiver
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            MessageListener.Start();
        }

        protected void Application_End()
        {
            MessageListener.Stop();
        }
    }
}