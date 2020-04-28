// <copyright file="Sitemap.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    class Sitemap
    {
        private XmlDocument doc;

        public Sitemap(Url site)
        {
            this.doc = new XmlDocument();
            this.doc.Load(site.FullUrl);
        }

        public Url[] GetUrls
        {
            get
            {
                List<Url> list = new List<Url>();
                foreach (XmlNode node in doc.LastChild.ChildNodes)
                {
                    list.Add(new Url(node.InnerText));
                }
                return list.ToArray();
            }
        }

        public static Sitemap[] GetSitemapsFromRobots(Url webAddress)
        {
            return null;
        }

    }
}
