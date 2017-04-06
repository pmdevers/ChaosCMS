using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChaosCMS.Extensions;

namespace ChaosCMS.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public class CarouselRenderer<TContent> : IRenderer<TContent>
        where TContent : class
    {
        private readonly ContentManager<TContent> contentManager;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentManager"></param>
        public CarouselRenderer(ContentManager<TContent> contentManager)
        {
            this.contentManager = contentManager;
        }

        /// <inheritdoc />
        public string TypeName => "carousel";

        /// <inheritdoc />
        public async Task<IHtmlContent> RenderAsync(IChaos<TContent> chaos, TContent content)
        {
            var carousel = new TagBuilder("div");
            var name = await this.contentManager.GetNameAsync(content);
            var options = await this.contentManager.GetValueAsync<CarouselOptions, TContent>(content);

            AddCarouselAttributes(carousel, name);

            carousel.InnerHtml.AppendHtmlLine("<!-- Indicators -->");
            carousel.InnerHtml.AppendHtml(await Indicators(options, content));
            carousel.InnerHtml.AppendHtmlLine("<!-- Wrapper for slides -->");
            carousel.InnerHtml.AppendHtml(await Slides(chaos, options, content));
            carousel.InnerHtml.AppendHtmlLine("<!-- Controls -->");
            carousel.InnerHtml.AppendHtml(await Control(options, content, "left", "prev", "Previous"));
            carousel.InnerHtml.AppendHtml(await Control(options, content, "right", "next", "Next"));

            await chaos.AddScript(new HtmlString($"$('#{name}').carousel();"));

            return carousel;
        }

        private async Task<IHtmlContent> Slides(IChaos<TContent> chaos, CarouselOptions options, TContent content)
        {
            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("carousel-inner");
            wrapper.MergeAttribute("role", "listbox");

            var slides = await this.contentManager.GetChildrenAsync(content);

            foreach(var item in slides)
            {
                var slide = new TagBuilder("div");
                var slideOptions = await this.contentManager.GetValueAsync<SlideOptions, TContent>(item);

                
                if (slideOptions.IsActive)
                {
                    slide.AddCssClass("active");
                }

                slide.AddCssClass("item");

                var image = new TagBuilder("img");
                image.MergeAttributes(slideOptions.Attributes);

                slide.InnerHtml.AppendHtml(image);
                
                var children = await this.contentManager.GetChildrenAsync(item);
                if (children.Count > 0)
                {
                    var caption = new TagBuilder("div");
                    caption.AddCssClass("carousel-caption");

                    foreach (var child in children)
                    {
                        caption.InnerHtml.AppendHtml(await chaos.RenderAsync(child));
                    }

                    slide.InnerHtml.AppendHtml(caption);
                }
                               
                wrapper.InnerHtml.AppendHtml(slide);
            }

            return wrapper; 
        }

        private async Task<IHtmlContent> Control(CarouselOptions options, TContent content, string className, string slide, string label)
        {
            var control = new TagBuilder("a");

            var name = await this.contentManager.GetNameAsync(content);

            control.AddCssClass("carousel-control");
            control.AddCssClass(className);
            control.MergeAttribute("href", "#" + name);
            control.MergeAttribute("role", "button");
            control.MergeAttribute("data-slide", slide);

            var spanIcon = new TagBuilder("span");
            spanIcon.AddCssClass("glyphicon-chevron-" + className);
            spanIcon.AddCssClass("glyphicon");
                        spanIcon.MergeAttribute("aria-hidden", "true");

            var spanLabel = new TagBuilder("span");
            spanLabel.AddCssClass("sr-only");
            spanLabel.InnerHtml.Append(label);

            control.InnerHtml.AppendHtml(spanIcon);
            control.InnerHtml.AppendHtml(spanLabel);

            return control;
        }

        private async Task<IHtmlContent> Indicators(CarouselOptions options, TContent content)
        {
            var indicators = new TagBuilder("ol");
            indicators.AddCssClass("carousel-indicators");

            var name = await this.contentManager.GetNameAsync(content);
            var slides = await this.contentManager.GetChildrenAsync(content);

            foreach (var item in slides)
            {
                var indicator = new TagBuilder("li");
                var slideOptions = await contentManager.GetValueAsync<SlideOptions, TContent>(item);
                indicator.MergeAttribute("data-target", "#" + name);
                indicator.MergeAttribute("data-slide-to", slides.IndexOf(item).ToString());
                if (slideOptions.IsActive)
                {
                    indicator.AddCssClass("active");
                }

                indicators.InnerHtml.AppendHtml(indicator);
            }

            return indicators;
        }

        private void AddCarouselAttributes(TagBuilder carousel, string name)
        {
            carousel.GenerateId(name, "");
            carousel.AddCssClass("slide");
            carousel.AddCssClass(TypeName);
            carousel.MergeAttribute("data-ride", "carousel");
        }

        /// <summary>
        /// 
        /// </summary>
        public class CarouselOptions : RenderOptions
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public class SlideOptions : RenderOptions
        {
            /// <summary>
            /// 
            /// </summary>
            public bool IsActive { get; set; }
        }
    }
}
