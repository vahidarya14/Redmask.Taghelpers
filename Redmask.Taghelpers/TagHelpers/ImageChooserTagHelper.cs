using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Redmask.Taghelpers.TagHelpers
{
    [HtmlTargetElement("imageChooser", Attributes = "asp-for", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ImageChooserTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")] public ModelExpression Model { get; set; }

        public string FolderPath { get; set; }
        public int MaxKb { get; set; } = 8000;
        public double MinRatioHeightToWidth { get; set; } = .001;
        public double MaxRatioHeightToWidth { get; set; } = 100;
        public string DefaultAvatar { get; set; } = "/noIimage268.png";
        public string ImgCss { get; set; }
        public string ImgClass { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var src = Model.Model != null ? FolderPath + Model.Model : DefaultAvatar;
            var imgId = Model.Name + Guid.NewGuid().ToString().Split('-')[0];
            var fileId = Model.Name + "file";
            var maxAllowedKillobyte = (MaxKb + 6) * 1000;

            output.Content.SetHtmlContent($@"
<img src='{ src}' id='{imgId}' style='height:100%; cursor:pointer;min-height:50px;border:2px solid red;{ImgCss}' class='{ImgClass}' />
<input type='file' name='{fileId}' id='{fileId}' style='display:none' accept='image/*' />
{ScriptHelper.AddJquery()}
<script>
    $('#{imgId}').click(function () {{ $('#{fileId}').trigger('click'); }});
    $('#{fileId}').on('change', function (evt) {{   
                                                    
             var tgt = evt.target || window.event.srcElement,                                           
                 files = tgt.files;                                                                     
             if (FileReader && files && files.length) {{                                                 
                                                                                                        
                 if (files[0].size > { maxAllowedKillobyte}) {{                                                           
                     alert('حجم عکس باید کمتر از   {MaxKb}  کیلوبایت باشد');                                
                     $('#{fileId}').val('');                                                           
                     return;                                                                            
                 }};                                                                                     
                 var fr = new FileReader();                                                             
                 fr.onload = function () {{                                                              
                                                                                                        
                     var image = new Image();                                                           
                     image.src = fr.result;                                                             
                     image.onload = function () {{                                                       
                         var height = this.height;                                                      
                         var width = this.width;                                                        
                         if (!(height >= { MinRatioHeightToWidth} * width && height <=  {MaxRatioHeightToWidth} * width)) {{                         
                             alert('ارتفاع تصویر باید بین { MinRatioHeightToWidth} تا { MaxRatioHeightToWidth} برابر عرض تصویر باشد');           
                             $('#{ fileId}').val('');                                                   
                             $('#s-btn').css('display', 'none');                                        
                             return false;                                                              
                         }}                                                                              
                                                                                                        
                         document.getElementById('{ imgId }').src = fr.result;                      
                         $('#s-btn').css('display', '');                                                
                         return true;                                                                   
                     }};                                                                                 
                 }};                                                                                     
                 fr.readAsDataURL(files[0]);                                                            
             }}   
             else{{}}
     }});                           
</script>");



        }
    }

}
