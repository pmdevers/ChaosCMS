using System.IO;
using System.Threading.Tasks;

namespace ChaosCMS.Razor.Internal
{
	public delegate Task RenderAsyncDelegate(TextWriter writer);
}
