namespace WebScrapingEngine
{
    using System.Collections.Generic;

    /// <summary>
    /// stores page history.
    /// </summary>
    public class PageHistory
    {
        private Dictionary<string, bool> history;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageHistory"/> class.
        /// </summary>
        public PageHistory()
        {
            this.history = new Dictionary<string, bool>();
        }

        /// <summary>
        /// checks if url has been visited already.
        /// </summary>
        /// <param name="url">url.</param>
        /// <returns>returns if url has been visited.</returns>
        public bool CheckUrl(Url url)
        {
            return this.CheckUrl(url.FullUrl);
        }

        /// <summary>
        /// checks if url has been visited already.
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>returns if url has been visited.</returns>
        public bool CheckUrl(string url)
        {
            if (!this.history.ContainsKey(url))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Adds url to history.
        /// </summary>
        /// <param name="url">url.</param>
        public void Add(Url url)
        {
            this.Add(url.FullUrl);
        }

        /// <summary>
        /// Adds string to url History.
        /// </summary>
        /// <param name="url">url</param>
        public void Add(string url)
        {
            this.history[url] = true;
        }
    }
}