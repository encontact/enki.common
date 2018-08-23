using System.Collections.ObjectModel;

namespace enki.common.core
{
    /// <summary>
    /// A collection of WebMethodInfo objects
    /// </summary>
    public class WebMethodInfoCollection : KeyedCollection<string, WebMethodInfo>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WebMethodInfoCollection() : base() { }

        protected override string GetKeyForItem(WebMethodInfo webMethodInfo)
        {
            return webMethodInfo.Name;
        }
    }
}