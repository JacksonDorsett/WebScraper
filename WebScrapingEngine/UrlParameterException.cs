// <copyright file="UrlParameterException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Url Parameter Exception.
    /// </summary>
    [Serializable]
    internal class UrlParameterException : FormatException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlParameterException"/> class.
        /// </summary>
        public UrlParameterException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlParameterException"/> class.
        /// </summary>
        /// <param name="message">exception message.</param>
        public UrlParameterException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlParameterException"/> class.
        /// </summary>
        /// <param name="message">exception message.</param>
        /// <param name="innerException">inner exception.</param>
        public UrlParameterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlParameterException"/> class.
        /// </summary>
        /// <param name="info">info.</param>
        /// <param name="context">context.</param>
        protected UrlParameterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}