﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;

namespace GMS.WebUI
{
    public class MyConfigHandler : IConfigurationSectionHandler
    {
        public MyConfigHandler()
        {

        }
        public object Create(object parent, object configContext, XmlNode section)
        {
            NameValueCollection configs;
            NameValueSectionHandler baseHandler = new NameValueSectionHandler();
            configs = (NameValueCollection)baseHandler.Create(parent, configContext, section);
            return configs;
        }
    }
}