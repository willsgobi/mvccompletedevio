using DevIO.App.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;

namespace DevIO.App.Controllers
{
    [HtmlTargetElement("*", Attributes = "suppress-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "suppress-by-claim-value")]
    public class ApagaElementoByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ApagaElementoByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("suppress-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("suppress-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentException(nameof(context));
            if (output == null)
                throw new ArgumentException(nameof(output));

            var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (temAcesso)
                return;

            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("a", Attributes = "disabled-by-claim-name")]
    [HtmlTargetElement("a", Attributes = "disabled-by-claim-value")]
    public class DesabilitaLinkByClaimHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public DesabilitaLinkByClaimHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("disabled-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("disabled-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentException(nameof(context));
            if (output == null)
                throw new ArgumentException(nameof(output));

            var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (temAcesso)
                return;

            output.Attributes.RemoveAll("href");
            output.Attributes.Add(new TagHelperAttribute("style", "cursor: not-allowed"));
            output.Attributes.Add(new TagHelperAttribute("title", "Você não tem permissão."));
        }
    }

    [HtmlTargetElement("*", Attributes = "suppress-by-action")]
    public class ApagaElementoByActionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ApagaElementoByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("suppress-by-action")]
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentException(nameof(context));
            if (output == null)
                throw new ArgumentException(nameof(output));

            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

            if (ActionName.Contains(action))
                return;

            output.SuppressOutput();
        }
    }
}
