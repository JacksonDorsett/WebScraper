// <copyright file="Pair.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Utility class to pair two different types.
    /// </summary>
    /// <typeparam name="T">First Type.</typeparam>
    /// <typeparam name="U">Second Type.</typeparam>
    internal class Pair<T, U>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pair{T, U}"/> class.
        /// </summary>
        /// <param name="first">First value.</param>
        /// <param name="second">Second Value.</param>
        public Pair(T first = default(T), U second = default(U))
        {
            this.First = first;
            this.Second = second;
        }

        /// <summary>
        /// Gets or sets first value.
        /// </summary>
        public T First { get; set; }

        /// <summary>
        /// Gets or sets Second value.
        /// </summary>
        public U Second { get; set; }
    }
}
