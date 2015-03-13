using System.Collections.Generic;
using Soft.Admin.Models.Localization;
using Soft.Web.Framework.Mvc;

namespace Soft.Admin.Models.Common
{
    public partial class LanguageSelectorModel : BaseSoftModel
    {
        public LanguageSelectorModel()
        {
            AvailableLanguages = new List<LanguageModel>();
        }

        public IList<LanguageModel> AvailableLanguages { get; set; }

        public LanguageModel CurrentLanguage { get; set; }
    }
}