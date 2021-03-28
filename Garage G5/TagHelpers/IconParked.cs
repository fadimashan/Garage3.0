using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Garage_G5.TagHelpers
{

    [HtmlTargetElement("parked")]
    public class IconParked : TagHelper
    {

        public bool InGarage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.AddClass("parked", HtmlEncoder.Default);

            var inGarage = InGarage;

            var commons = "https://www.svgrepo.com/show/";
            var carIn = commons+"32605/car.svg";
            var carOut = commons+"52322/traffic-cone.svg";


            var result = (inGarage == true) ? $"<img src='{carIn}'/>" : $"<img src='{carOut}'/>";

            output.Content.SetHtmlContent(result);

        }
    }
}
