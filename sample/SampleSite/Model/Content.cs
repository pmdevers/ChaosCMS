using ChaosCMS.Json.Models;
using ChaosCMS.LiteDB.Models;

namespace SampleSite.Model
{
    public class Content : LiteDBContent<Content> // JsonContent // ChaosContent<Content, int>
    {
    }
}