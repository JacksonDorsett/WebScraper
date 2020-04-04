namespace WebScrapingEngine
{
    using System.Collections.Generic;

    public class PageHistory
    {
        private Dictionary<Url, bool> history;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageHistory"/> class.
        /// </summary>
        public PageHistory()
        {
            this.history = new Dictionary<Url, bool>();
        }

        /// <summary>
        /// checks if url has been visited already.
        /// </summary>
        /// <param name="url">url.</param>
        /// <returns>returns if url has been visited</returns>
        public bool CheckUrl(Url url)
        {
            return false;
        }
        public bool CheckUrl(string url)
        {
            return false;
        }
        /// <summary>
        /// Adds url to history.
        /// </summary>
        /// <param name="url">url.</param>
        public void Add(Url url)
        {

        }

        /// <summary>
        /// Adds string to url History.
        /// </summary>
        /// <param name="url">url</param>
        public void Add(string url)
        {

        }
    }
}