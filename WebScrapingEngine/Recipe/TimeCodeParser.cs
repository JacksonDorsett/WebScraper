// <copyright file="TimeCodeParser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine.Recipe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Parses timecode to minutes in recipe schema.
    /// </summary>
    internal class TimeCodeParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeCodeParser"/> class.
        /// </summary>
        internal TimeCodeParser()
        {
        }

        /// <summary>
        /// converts timecode to minutes.
        /// </summary>
        /// <param name="timeCode">time code input.</param>
        /// <returns>time duration in minutes.</returns>
        public int Parse(string timeCode)
        {
            if (timeCode[0] != 'P' && timeCode[1] != 'T')
            {
                return -1;
            }

            timeCode = timeCode.Replace("PT", string.Empty);
            int minuteTime = 0;
            string t = string.Empty;
            foreach (char c in timeCode)
            {
                if (char.IsNumber(c))
                {
                    t += c;
                }
                else
                {
                    minuteTime += int.Parse(t) * (Convert.ToInt32(c == 'M') + Convert.ToInt32(c == 'H') * 60);
                    t = string.Empty;
                }
            }
            return minuteTime;
        }
    }
}
