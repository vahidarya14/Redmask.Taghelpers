using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Redmask.Taghelpers.TagHelpers
{
    [HtmlTargetElement("persianDatePicker", Attributes = DescriptionAttributeName, TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PersianDatePickerTagHelper : TagHelper
    {
        private const string DescriptionAttributeName = "asp-for";

        [HtmlAttributeName(DescriptionAttributeName)]
        public ModelExpression Model { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var dt = Model.Model != null ? (DateTime?)Model.Model :null;
            var enabletimepicker = "true";
            output.Content.SetHtmlContent($@"
<div class='input-group mb-3'>
        <input class='form-control' id='{Model.Name}2' value='{(!dt.HasValue?"": ToPersian(dt.Value))}'>
        <input type='hidden' id='{Model.Name}'  name='{Model.Name}'  value='{dt}'  />
        <div class='input-group-prepend'>
            <span class='input-group-text' id='basic-addon1'><i class='icofont-calendar'></i></span>
        </div>
</div>
{ScriptHelper.AddJquery()}
<script> 
   
     $(document).ready(function () {{ 
        
         $('#{Model.Name}2').MdPersianDateTimePicker({{
                targetTextSelector: '#{Model.Name}2',
                targetDateSelector: '#{Model.Name}',
                enableTimePicker: false,
                dateFormat: 'yyyy-MM-dd',
                textFormat: 'yyyy/MM/dd',
         }});
    }}); 
</script> 
");
        }
        public static string ToPersian(DateTime d,bool includeTime=false)
        {
            PersianCalendar pc = new ();
            return !includeTime? $"{pc.GetYear(d)}/{pc.GetMonth(d):00}/{pc.GetDayOfMonth(d):00}":
                $"{pc.GetYear(d)}/{pc.GetMonth(d):00}/{pc.GetDayOfMonth(d):00} {d.Hour:00}:{d.Minute:00}";
        }
    }

}
