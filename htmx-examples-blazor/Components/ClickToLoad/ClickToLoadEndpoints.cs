using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace htmx_examples_blazor.Components.ClickToLoad;

public class ClickToLoadEndpoints : IEndpoints
{
    public const int PageCount = 5;
    
    public static IEnumerable<Contact> GetPagedResults(int page, int take)
    {
        var start = 10 + (page * take);
        for (int i = start; i < start + take; i++)
        {
            yield return new Contact("Woody", $"me{i}@woody.dev", Guid.NewGuid());
        }
    }
    
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/ClickToLoad/{page:int}", (int page = 0) =>
        {
            var contacts = GetPagedResults(page, PageCount).ToList();
            return new RazorComponentResult<ClickToLoadButton>(new
            {
                Model = contacts,
                CurrentPage = page,
            });
        });
    }
}