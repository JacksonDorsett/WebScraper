// <copyright file="Sitemap.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    public class Sitemap
    {
        private XmlDocument doc;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sitemap"/> class.
        /// </summary>
        /// <param name="site">site url.</param>
        public Sitemap(Url site)
        {
            this.doc = new XmlDocument();
            if (site.FullUrl.Contains(".gz"))
            {
                HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(site.FullUrl);

                //Create a response for this request
                HttpWebResponse fileResp = (HttpWebResponse)fileReq.GetResponse();

                if (fileReq.ContentLength > 0)
                    fileResp.ContentLength = fileReq.ContentLength;

                //Get the Stream returned from the response
                Stream stream = fileResp.GetResponseStream();

                using (GZipStream gstream = new GZipStream(stream, CompressionMode.Decompress))
                {
                    using (StreamReader sr = new StreamReader(gstream))
                    {
                        this.doc.LoadXml(sr.ReadToEnd());
                    }
                }
                
            }
            else
            {
                this.doc.Load(site.FullUrl);
            }

        }

        /// <summary>
        /// Gets urls in sitemap page.
        /// </summary>
        public Url[] Urls
        {
            get
            {
                List<Url> list = new List<Url>();
                foreach (XmlNode node in doc.LastChild.ChildNodes)
                {
                    list.Add(new Url(node.FirstChild.InnerText));
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
