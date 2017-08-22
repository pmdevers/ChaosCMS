using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosCMS.Models.Pages;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Text.Encodings.Web;

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


            var grid = new JsonGrid();
            grid.CssClass.Add("container");
            var row = new JsonGridRow();
            var col = new JsonGridColumn() { Lg = 12, Md = 12, Xs = 12, Content = "test" };
            row.Columns.Add(col);
            grid.Rows.Add(row);

            content.SetValue(grid);

            return grid.RenderAsync(chaos, content);
        }

        public class JsonGrid : RenderOptions
        {
            public List<JsonGridRow> Rows { get; set; } = new List<JsonGridRow>();

            public async Task<IHtmlContent> RenderAsync(IChaos chaos, Content content)
            {
                TagBuilder builder = new TagBuilder("div");
                var stringBuilder = new StringBuilder();
                var textWriter = new StringWriter(stringBuilder);

                this.CssClass.ForEach(builder.AddCssClass);
                
                foreach (var row in Rows)
                {
                    var rowContent = await row.RenderAsync(chaos, content);
                    rowContent.WriteTo(textWriter, HtmlEncoder.Default);
                    stringBuilder.AppendLine();
                }

                builder.InnerHtml.SetHtmlContent(stringBuilder.ToString());
                builder.TagRenderMode = TagRenderMode.Normal;
                return builder;
            }
        }

        public class JsonGridRow
        {
            public List<JsonGridColumn> Columns { get; set; } = new List<JsonGridColumn>();

            public async Task<IHtmlContent> RenderAsync(IChaos chaos, Content content)
            {
                var builder = new TagBuilder("div");
                var stringBuilder = new StringBuilder();
                var textWriter = new StringWriter(stringBuilder);
                builder.AddCssClass("row");

                foreach (var col in Columns)
                {
                    var colContent = await col.RenderAsync(chaos, content);
                    colContent.WriteTo(textWriter, HtmlEncoder.Default);
                }

                builder.InnerHtml.SetHtmlContent(stringBuilder.ToString());
                return builder;
            }
        }
        public class JsonGridColumn
        {
            public string Content { get; set; }
            public int Lg { get; set; }
            public int Md { get; set; }
            public int Xs { get; set; }
            internal async Task<IHtmlContent> RenderAsync(IChaos chaos, Content content)
            {
                var builder = new TagBuilder("div");
                var stringBuilder = new StringBuilder();
                var textWriter = new StringWriter(stringBuilder);

                builder.AddCssClass($"col-lg-{Lg}");
                builder.AddCssClass($"col-md-{Md}");
                builder.AddCssClass($"col-xs-{Xs}");

                var colContent = content.Children.FirstOrDefault(x => x.Name.Equals(Content));

                builder.InnerHtml.AppendHtml(await chaos.RenderAsync(colContent));

                return builder;
            }
            
        }
    }
}