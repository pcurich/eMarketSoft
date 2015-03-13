using FluentValidation;
using Soft.Admin.Models.Stores;
using Soft.Services.Localization;
using Soft.Web.Framework.Validators;

namespace Soft.Admin.Validators.Stores
{
    public class StoreValidator : BaseSoftValidator<StoreModel>
    {
        public StoreValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Configuration.Stores.Fields.Name.Required"));
            RuleFor(x => x.Url).NotEmpty().WithMessage(localizationService.GetResource("Admin.Configuration.Stores.Fields.Url.Required"));
        }
    }
}