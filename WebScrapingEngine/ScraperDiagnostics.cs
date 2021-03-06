﻿// <copyright file="ScraperDiagnostics.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Scraper Diagnostics class.
    /// </summary>
    /// <typeparam name="T">Html doc type.</typeparam>
    /// <typeparam name="U">Output Type.</typeparam>
    public class ScraperDiagnostics<T, U>
    {
        private readonly WebScraper<T, U> scraper;
        private List<uint> queueTrendList;
        private Stopwatch clock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScraperDiagnostics{T, U}"/> class.
        /// </summary>
        /// <param name="scraper">scraper which diagnostics are being collected.</param>
        public ScraperDiagnostics(WebScraper<T, U> scraper)
        {
            this.scraper = scraper;
            this.queueTrendList = new List<uint>();
            this.clock = new Stopwatch();
            this.clock.Reset();
        }

        /// <summary>
        /// Gets Pages scraped.
        /// </summary>
        public uint PagesScraped { get => (uint)this.scraper.ScrapedObjects.Count; }

        /// <summary>
        /// Gets pages Visited.
        /// </summary>
        public uint PagesVisited { get => (uint)this.queueTrendList.Count; }

        /// <summary>
        /// Gets time spent scraping.
        /// </summary>
        public int TimeScraping { get => this.clock.Elapsed.Minutes; }

        /// <summary>
        /// Gets the maximum length of the queue.
        /// </summary>
        public int PeakQueueLength
        {
            get
            {
                uint len = 0;
                foreach (uint length in this.queueTrendList)
                {
                    if (length > len)
                    {
                        len = length;
                    }
                }

                return (int)len;
            }
        }

        /// <summary>
        /// Gets the peak queue length index.
        /// </summary>
        public int PeakQueueIndex
        {
            get
            {
                int index = 0;
                for (int i = 0; i < this.queueTrendList.Count; ++i)
                {
                    if (this.queueTrendList[i] > this.queueTrendList[index])
                    {
                        index = i;
                    }
                }

                return index;
            }
        }

        /// <summary>
        /// Gets List of number of links in queue.
        /// </summary>
        public uint[] QueueLengthTrend { get => this.queueTrendList.ToArray(); }

        /// <summary>
        /// Runs Diagnostics.
        /// </summary>
        public void Run()
        {
            this.Start();
            while (this.scraper.IsScrapeable)
            {
                this.scraper.Scrape();
                this.Update();
            }

            this.Stop();
        }

        /// <summary>
        /// Starts scraper timer.
        /// </summary>
        internal void Start()
        {
            this.clock.Start();
        }

        /// <summary>
        /// Stops scraper timer.
        /// </summary>
        internal void Stop()
        {
            this.clock.Stop();
        }

        /// <summary>
        /// updates the diagnostics called by reference scraper.
        /// </summary>
        /// <param name="pageScraped">was page scraped.</param>
        private void Update()
        {
            this.queueTrendList.Add((uint)this.scraper.UrlQueue.Count);
        }
    }
}
