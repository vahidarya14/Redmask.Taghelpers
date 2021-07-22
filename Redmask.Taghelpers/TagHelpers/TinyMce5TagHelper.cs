using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Redmask.Taghelpers.TagHelpers
{
    [HtmlTargetElement("TinyMce5", Attributes="id", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TinyMce5TagHelper : TagHelper
    {
        public string Id { get; set; }

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            var random = new Random().Next(1000000);

            var c = (await output.GetChildContentAsync()).GetContent();

            var aa = $@"<textarea name='{Id}' id='{Id}' class='form-control' >{c}</textarea>
{ScriptHelper.AddJquery()}
<script>
     {ScriptHelper.AddIfNotExsists("/lib/tinymce5/tinymce.min.js")}
    $().ready(function () {{

        tinyMCE.init({{
            selector: '#{Id}',
            language: 'fa_IR',
            //mode: 'textareas',
            //theme: 'modern',
            //inline_styles: true,
            menubar: false,
            fontsize_formats: '8pt 9pt 10pt 11pt 12pt 26pt 36pt',
            height: 200,
            width: '100%',
            autoresize_min_height: 200,
            autoresize_max_height: 200,
            menubar: true,
            plugins: [
                ' autolink autoresize directionality lists link image charmap print preview anchor',
                'searchreplace visualblocks code fullscreen textcolor',
                'insertdatetime media table contextmenu paste fullpage  codesample emoticons'
            ],
            directionality: 'rtl',
            codesample_languages: [
                {{ text: 'HTML/XML', value: 'markup' }},
                {{ text: 'JavaScript', value: 'javascript' }},
                {{ text: 'CSS', value: 'css' }},
                {{ text: 'PHP', value: 'php' }},
                {{ text: 'Ruby', value: 'ruby' }},
                {{ text: 'Python', value: 'python' }},
                {{ text: 'Java', value: 'java' }},
                {{ text: 'C', value: 'c' }},
                {{ text: 'C#', value: 'csharp' }},
                {{ text: 'C++', value: 'cpp' }}
            ],
            toolbar1: 'fullscreen | undo redo |forecolor backcolor |ltr rtl | styleselect | fontselect | fontsizeselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link | code codesample image emoticons ',
            codesample_global_prismjs: true,
            file_picker_callback_types: 'file image media',
            file_picker_callback: (callback, value, type)=> {{

                        var roxyFileman = '/fileman/index.html';
                        roxyFileman += (roxyFileman.indexOf('?') < 0 ? '?' : '&') + 'type=' + type.filetype;
                        if (value)
                            roxyFileman += '&value=' + value; // a link to already chosen image if it exists
                        if (tinyMCE.activeEditor.settings.language)
                            roxyFileman += '&langCode=' + tinyMCE.activeEditor.settings.language;
                        const instanceApi = tinyMCE.activeEditor.windowManager.openUrl({{
                            title: 'Roxy Fileman',
                            url: roxyFileman,
                            width: 650,
                            height: 650,
                            plugins: 'media',
                            onMessage: function(dialogApi, details) {{
                                callback(details.content);
                                instanceApi.close();
                            }}
                        }});
                        return false;                   
            }},
            images_upload_handler: (blobInfo, success, failure) => {{
                var xhr, formData;

                xhr = new XMLHttpRequest();
                xhr.withCredentials = false;
                xhr.open('POST', '/UploadFilesAsync');

                xhr.onload = function () {{
                    var json;
                    if (xhr.status !== 200) {{
                        failure('HTTP Error: ' + xhr.status);
                        return;
                    }}
                    json = JSON.parse(xhr.responseText);

                    console.log(json);

                    if (!json || typeof json.location !== 'string') {{
                        failure('Invalid JSON: ' + xhr.responseText);
                        return;
                    }}
                    success(json.location);
                }};

                formData = new FormData();
                formData.append('file', blobInfo.blob(), blobInfo.filename());
                xhr.send(formData);
            }}

        }});


    }});
    </script>";
            output.Content.SetHtmlContent(aa);
        }
    }
}
