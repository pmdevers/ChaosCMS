using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosCMS.Models.Pages;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace ChaosCMS.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    public class GridRenderer : IRenderer
    {
        /// <summary>
        /// 
        /// </summary>
        public string TypeName => "jsongrid";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chaos"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Task<IHtmlContent> RenderAsync(IChaos chaos, Content content)
        {
            var jsonGrid = content.GetValue<JsonGrid>();
            return jsonGrid.RenderAsync(chaos, content);
        }

        private class JsonGrid : RenderOptions
        {
            private List<JsonGridRow> Rows { get; set; }

            public async Task<IHtmlContent> RenderAsync(IChaos chaos, Content content)
            {
                TagBuilder builder = new TagBuilder("div");

                var stringBuilder = new StringBuilder();
                var textWriter = new StringWriter(stringBuilder);
                foreach (var row in Rows)
                {
                    var rowContent = await row.RenderAsync(chaos, content)
                    rowContent.WriteTo(textWriter, )
                    stringBuilder.AppendLine();
                }

                builder.InnerHtml.SetHtmlContent()
                builder.TagRenderMode = TagRenderMode.Normal;
                return builder;
            }
        }

        private class JsonGridRow
        {
            public List<JsonGridColumn> Columns { get; set; }

            public async Task<IHtmlContent> RenderAsync(IChaos chaos, Content content)
            {
            }

        private class JsonGridColumn
        {

        }
    }
}
