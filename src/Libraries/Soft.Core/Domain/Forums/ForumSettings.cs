using Soft.Core.Configuration;

namespace Soft.Core.Domain.Forums
{
    public class ForumSettings : ISettings
    {
        /// <summary>
        /// Si el forum esta activo
        /// </summary>
        public bool ForumsEnabled { get; set; }

        /// <summary>
        /// Si la Fecha y hora relativa esta activa con formato
        /// (e.g. 2 hours ago, a month ago)
        /// </summary>
        public bool RelativeDateTimeFormattingEnabled { get; set; }

        /// <summary>
        /// Si los clientes pueden esitar los post que ellos crearon
        /// </summary>
        public bool AllowCustomersToEditPosts { get; set; }

        /// <summary>
        /// Si lo clientes pueden manejar sus suscripciones
        /// </summary>
        public bool AllowCustomersToManageSubscriptions { get; set; }

        /// <summary>
        /// Si los guess pueden crear posts
        /// </summary>
        public bool AllowGuestsToCreatePosts { get; set; }

        /// <summary>
        /// Si los guess pueden crear topicos
        /// </summary>
        public bool AllowGuestsToCreateTopics { get; set; }

        /// <summary>
        /// Si los clientes pueden borrar los post que ellos crearon 
        /// </summary>
        public bool AllowCustomersToDeletePosts { get; set; }

        /// <summary>
        /// Maxima mongitud de un subject de un topico
        /// </summary>
        public int TopicSubjectMaxLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum length for stripped forum topic names
        /// </summary>
        public int StrippedTopicMaxLength { get; set; }

        /// <summary>
        /// Gets or sets maximum length of post
        /// </summary>
        public int PostMaxLength { get; set; }

        /// <summary>
        /// Gets or sets the page size for topics in forums
        /// </summary>
        public int TopicsPageSize { get; set; }

        /// <summary>
        /// Gets or sets the page size for posts in topics
        /// </summary>
        public int PostsPageSize { get; set; }

        /// <summary>
        /// Gets or sets the number of links to display for pagination of posts in topics
        /// </summary>
        public int TopicPostsPageLinkDisplayCount { get; set; }

        /// <summary>
        /// Gets or sets the page size for search result
        /// </summary>
        public int SearchResultsPageSize { get; set; }

        /// <summary>
        /// Gets or sets the page size for latest customer posts
        /// </summary>
        public int LatestCustomerPostsPageSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show customers forum post count
        /// </summary>
        public bool ShowCustomersPostCount { get; set; }

        /// <summary>
        /// Gets or sets a forum editor type
        /// </summary>
        public EditorType ForumEditor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customers are allowed to specify a signature
        /// </summary>
        public bool SignaturesEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether private messages are allowed
        /// </summary>
        public bool AllowPrivateMessages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an alert should be shown for new private messages
        /// </summary>
        public bool ShowAlertForPm { get; set; }

        /// <summary>
        /// Gets or sets the page size for private messages
        /// </summary>
        public int PrivateMessagesPageSize { get; set; }

        /// <summary>
        /// Gets or sets the page size for (My Account) Forum Subscriptions
        /// </summary>
        public int ForumSubscriptionsPageSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a customer should be notified about new private messages
        /// </summary>
        public bool NotifyAboutPrivateMessages { get; set; }

        /// <summary>
        /// Gets or sets maximum length of pm subject
        /// </summary>
        public int PmSubjectMaxLength { get; set; }

        /// <summary>
        /// Gets or sets maximum length of pm message
        /// </summary>
        public int PmTextMaxLength { get; set; }

        /// <summary>
        /// Gets or sets the number of items to display for Active Discussions on forums home page
        /// </summary>
        public int HomePageActiveDiscussionsTopicCount { get; set; }

        /// <summary>
        /// Gets or sets the number of items to display for Active Discussions page
        /// </summary>
        public int ActiveDiscussionsPageTopicCount { get; set; }

        /// <summary>
        /// Gets or sets the number of items to display for Active Discussions RSS Feed
        /// </summary>
        public int ActiveDiscussionsFeedCount { get; set; }

        /// <summary>
        /// Gets or sets the whether the Active Discussions RSS Feed is enabled
        /// </summary>
        public bool ActiveDiscussionsFeedEnabled { get; set; }

        /// <summary>
        /// Gets or sets the whether Forums have an RSS Feed enabled
        /// </summary>
        public bool ForumFeedsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the number of items to display for Forum RSS Feed
        /// </summary>
        public int ForumFeedCount { get; set; }

        /// <summary>
        /// Gets or sets the minimum length for search term
        /// </summary>
        public int ForumSearchTermMinimumLength { get; set; }
    }
}