// <copyright file="UrlQuery.cs" company="PlaceholderCompany">
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
    /// Encapsulates urlQuery.
    /// </summary>
    public class UrlQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlQuery"/> class.
        /// </summary>
        /// <param name="query">Query string.</param>
        public UrlQuery(string query)
        {
            query = this.ClenseQuery(query);
            this.Query = query;
            this.Parameters = this.ExtractParameters(query);
        }

        /// <summary>
        /// Gets QueryParameters.
        /// </summary>
        public UrlQueryParameter[] Parameters { get; private set; }

        /// <summary>
        /// Gets or sets query string.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// override for tostring.
        /// </summary>
        /// <returns>Query as a string.</returns>
        public override string ToString()
        {
            return '?' + this.Query;
        }

        private string ClenseQuery(string query)
        {
            if (query.Contains('?'))
            {
                query = query.Replace(query.Substring(0, query.IndexOf('?') +1), string.Empty);
            }

            return query;
        }

        private UrlQueryParameter[] ExtractParameters(string query)
        {
            List<UrlQueryParameter> list = new List<UrlQueryParameter>();
            foreach (var v in query.Split('&', '\0'))
            {
                list.Add(new UrlQueryParameter(v));
            }

            return list.ToArray();
        }
    }
}
