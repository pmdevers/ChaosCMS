using ChaosCMS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Converters
{
    /// <summary>
    /// Provides methods to convert from TSource to TDestination
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// /// <typeparam name="TDestination">The destinatin type</typeparam>
    public class PageConverter<TSource, TDestination> : IConverter<TSource, TDestination>
        where TSource : class
        where TDestination : class, new()
    {
        /// <summary>
        /// Constructs a new instance of <see cref="PageConverter{TSource, TDestination}"/>.
        /// </summary>
        /// <param name="sourceManager">The Source Manager</param>
        /// <param name="destinationManager">The destination Manager.</param>
        public PageConverter(PageManager<TSource> sourceManager, PageManager<TDestination> destinationManager)
        {
            SourceManager = sourceManager ?? throw new ArgumentNullException(nameof(sourceManager));
            DestinationManager = destinationManager ?? throw new ArgumentNullException(nameof(destinationManager));
        }

        /// <summary>
        /// 
        /// </summary>
        public PageManager<TSource> SourceManager { get; }
        /// <summary>
        /// 
        /// </summary>
        public PageManager<TDestination> DestinationManager { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public async Task<ConverterResult<TDestination>> Convert(TSource source)
        {
            if(source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            var errors = new List<ChaosError>();

            TDestination destination = await this.GetDestinationAsync(source);

            await this.ConvertNameAsync(source, destination);
            await this.ConvertUrlAsync(source, destination);
            await this.ConvertTemplateAsync(source, destination);
            await this.ConvertStatusCodeAsync(source, destination);
            await this.ConvertPageTypeAsync(source, destination);

            var result = await this.DestinationManager.CreateAsync(destination);

            if(result.Succeeded)
            {
                if(SourceManager.SupportsContents && DestinationManager.SupportsContents)
                {
                    await this.ConvertContentAsync(source, destination);
                }
                // todo sub types
            }

            if (errors.Count > 0)
            {
                return ConverterResult<TDestination>.Failed(destination,  errors.ToArray());
            }

            return ConverterResult<TDestination>.Success(destination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        protected virtual async Task ConvertContentAsync(TSource source, TDestination destination)
        {
            var sourceContent = await this.SourceManager.GetContentAsync(source);
            await this.DestinationManager.SetContentAsync(destination, sourceContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        protected virtual async Task ConvertPageTypeAsync(TSource source, TDestination destination)
        {
            var pageType = await this.SourceManager.GetPageTypeAsync(source);
            await this.DestinationManager.SetPageTypeAsync(destination, pageType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        protected virtual async Task ConvertStatusCodeAsync(TSource source, TDestination destination)
        {
            var code = await this.SourceManager.GetStatusCodeAsync(source);
            await this.DestinationManager.SetStatusCodeAsync(destination, code);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        protected virtual async Task ConvertTemplateAsync(TSource source, TDestination destination)
        {
            var template = await this.SourceManager.GetTemplateAsync(source);
            await this.DestinationManager.SetTemplateAsync(destination, template);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        protected virtual async Task ConvertUrlAsync(TSource source, TDestination destination)
        {
            var url = await this.SourceManager.GetUrlAsync(source);
            await this.DestinationManager.SetUrlAsync(destination, url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        protected virtual async Task ConvertNameAsync(TSource source, TDestination destination)
        {
            var name = await this.SourceManager.GetNameAsync(source);
            await this.DestinationManager.SetNameAsync(destination, name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected virtual async Task<TDestination> GetDestinationAsync(TSource source)
        {
            var externalId = await this.SourceManager.GetIdAsync(source);
            var destination = await this.DestinationManager.FindByExternalIdAsync(externalId);
            if(destination == null)
            {
                return new TDestination();
            }
            return destination;
        }


    }
}
