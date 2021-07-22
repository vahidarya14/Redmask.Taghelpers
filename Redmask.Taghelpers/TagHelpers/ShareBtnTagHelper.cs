using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Redmask.Taghelpers.TagHelpers
{
    [HtmlTargetElement("ShareBtn", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ShareBtnTagHelper : TagHelper
    {
        [ViewContext] public ViewContext ViewContext { get; set; }
        HttpRequest Request => ViewContext.HttpContext.Request;


        public string Subject { get; set; }
        public string Url { get; set; }
        public bool UseCurrentUrl { get; set; } = true;

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            var currentUrl = UseCurrentUrl ? $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}" : Url;
            //var aaa = Request.GetDisplayUrl();
            output.Content.SetHtmlContent($@"       
<div class='dropdown'>
    <button class='btn btn-sm btn-danger' type='button' id='dropdownMenuButton' data-toggle='dropdown'>
        <i class='icofont-share  icofont-1x'></i>
    </button>
    <div class='dropdown-menu' aria-labelledby='dropdownMenuButton'>
        <a class='dropdown-item text-left' href='https://www.facebook.com/sharer.php?u={currentUrl}&t={Subject}' target='_blank'> <i class='icofont-facebook'></i> Facebook</a>
        <a class='dropdown-item text-left' href='http://www.linkedin.com/shareArticle?mini=true&title={Subject}&url={currentUrl}' target='_blank'> <i class='icofont-linkedin'></i> Linkedin</a>
        <a class='dropdown-item text-left' href='https://twitter.com/home?status=Reading:{currentUrl}' title='اشتراک گذاری در تویینر' target='_blank'><i class='icofont-twitter'></i> twitter</a>
        <a class='dropdown-item text-left' href='https://telegram.me/share/url?text={Subject}&url={currentUrl}' target='_blank' title='اشتراک گذاری در تلگرام'><i class='icofont-telegram'></i> telegram</a>
        <a class='dropdown-item text-left' href='https://api.whatsapp.com/send?text={currentUrl}' target='_blank' >واتساپ <i class='icofont-whatsapp'></i></a>
    </div>
</div>");
            return base.ProcessAsync(context, output);
        }
    }
}
