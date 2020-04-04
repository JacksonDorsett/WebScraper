// <copyright file="UrlQueryParameter.cs" company="PlaceholderCompany">
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
    /// Url Query Parameter Class.
    /// </summary>
    public class UrlQueryParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlQueryParameter"/> class.
        /// </summary>
        /// <param name="parameter">full Query parameter.</param>
        public UrlQueryParameter(string parameter)
        {
            char[] chars = { '=' };
            var s = parameter.Split(chars, 2);
            if (s.Length < 2)
            {
                throw new UrlParameterException();
            }
            else
            {
                this.Field = s[0];
                this.Value = s[1];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlQueryParameter"/> class.
        /// </summary>
        /// <param name="field">field of parameter.</param>
        /// <param name="value">value of parameter.</param>
        public UrlQueryParameter(string field, string value)
        {
            this.Field = field;
            this.Value = value;
        }

        /// <summary>
        /// Gets Field of Parameter.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets Value of parameter.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Override for tostring for UrlQueryParameter.
        /// </summary>
        /// <returns>returns Parameter as a string.</returns>
        public override string ToString()
        {
            return this.Field + '=' + this.Value;
        }
    }
}
