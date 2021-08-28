
1) add @addTagHelper *,Redmask.Taghelpers to _ViewImport.cshtml

2) do not forget to download wwwroot from repository and add to your project . url is https://github.com/vahidarya14/Redmask.Taghelpers/tree/master/Redmask.Taghelpers/wwwroot

3) make these tow folders in side wwwroot "/_files/Temp" and "/_files/uploadimages"

  ## imageChooser
  ```html
   <imageChooser asp-for="Icon" folder-path="@Setting.ContentsFolder" max-kb="1500" img-css="max-height:200px;border:2px solid blue;" ></imageChooser>

```
       
 ## persianDatePicker
 ```html
  <persianDatePicker asp-for="DateTimePublishFrom"></persianDatePicker>
  ```
  
  ## TinyMce5
  ```html
   <TinyMce5For asp-for="FullText"></TinyMce5For>
```

# Themeable
  add Theme folder to your project and set Themes name(which is folder name) into IMemoryCache with key "Theme"
  it loads .cshtml from /Themes/{theme_name}
