// <copyright file="Url.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Encapsulates a Url.
    /// </summary>
    public class Url
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Url"/> class.
        /// </summary>
        /// <param name="url">page url.</param>
        public Url(string url)
        {
            this.FullUrl = url;
        }

        /// <summary>
        /// Gets Domain name of the website.
        /// </summary>
        public string DomainName
        {
            get
            {
                return this.GetDomainName(this.FullUrl);
            }
        }

        /// <summary>
        /// Gets and sets Full url.
        /// </summary>
        public string FullUrl { get; private set; }

        /// <summary>
        /// Gets and sets filePath.
        /// </summary>
        public string[] FilePath
        {
            get
            {
                return this.GetFilePath(this.FullUrl);
            }
        }

        /// <summary>
        /// Gets Query of the url.
        /// </summary>
        public UrlQuery Query
        {
            get
            {
                return this.GetQuery();
            }
        }

        private string GetDomainName(string url)
        {
            url = this.TrimHttps(url);
            if (url.Contains('/'))
            {
                url = url.Substring(0, url.IndexOf('/'));
            }

            return url;
        }

        private string[] GetFilePath(string url)
        {
            url = this.TrimHttps(url);
            url = url.Replace(this.DomainName + '/', string.Empty);
            var v = url.Split('/');
            if (v[v.Length - 1].Contains('?'))
            {
                var temp = v[v.Length - 1];
                temp = temp.Substring(0, temp.IndexOf('?'));
                v[v.Length - 1] = temp;
            }

            return v;
        }

        private UrlQuery GetQuery()
        {
            if (this.FullUrl.Contains('?'))
            {
                return new UrlQuery(this.FullUrl.Substring(this.FullUrl.IndexOf('?')));
            }

            return null;
        }

        private string TrimHttps(string url)
        {
            var trimmedString = url.Replace("https://", string.Empty);
            return trimmedString;
        }
    }
}
