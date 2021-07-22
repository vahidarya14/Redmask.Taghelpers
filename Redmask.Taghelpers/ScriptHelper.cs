namespace Redmask.Taghelpers
{
    public class ScriptHelper
    {
        public static string AddJquery() => "<script src='/lib/jquery/jquery.min.js'></script>";
        public static string AddScript(string src) => $"<script src='{src}'></script>";

        public static string AddIfNotExsists(string src) =>
            $@" if(document.querySelector(""script[src='{src}']"")==undefined)
                        {{
                            var s = document.createElement(""script"");
                            s.type = 'text/javascript';
                            s.src = '{src}';
                            document.body.appendChild(s); 
                        }}";
    }
}
