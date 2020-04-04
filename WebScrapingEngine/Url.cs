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
            this.DomainName = this.GetDomainName(url);
            this.FilePath = this.GetFilePath(url);
            this.FullUrl = url;

            if (this.FilePath[this.FilePath.Length - 1].Contains('?'))
            {
                string s = this.FilePath[this.FilePath.Length - 1];
                this.Query = new UrlQuery(s);
                this.FilePath[this.FilePath.Length - 1] = s.Substring(0, s.IndexOf('?'));
            }
        }

        /// <summary>
        /// Gets Domain name of the website.
        /// </summary>
        public string DomainName { get; private set; }

        /// <summary>
        /// Gets and sets Full url.
        /// </summary>
        public string FullUrl { get; private set; }

        /// <summary>
        /// Gets and sets filePath.
        /// </summary>
        public string[] FilePath { get; private set; }

        /// <summary>
        /// Gets Query of the url.
        /// </summary>
        public UrlQuery Query { get; private set; }

        private string GetDomainName(string url)
        {
            url = this.TrimHttps(url);
            url = url.Substring(0, url.IndexOf('/'));
            return url;
        }

        private string[] GetFilePath(string url)
        {
            url = this.TrimHttps(url);
            url = url.Replace(this.DomainName + '/', string.Empty);
            return url.Split('/');
        }

        private string TrimHttps(string url)
        {
            var trimmedString = url.Replace("https://", string.Empty);
            return trimmedString;
        }
    }
}
