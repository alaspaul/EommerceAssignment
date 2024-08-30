var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

Dictionary<int, string> countries = new Dictionary<int, string>()
{
 { 1, "United States" },
 { 2, "Canada" },
 { 3, "United Kingdom" },
 { 4, "India" },
 { 5, "Japan" }
};

app.UseEndpoints(endpoints => 
{  
    endpoints.MapGet("/countries", async context =>
    {
        foreach(KeyValuePair<int, string> country in countries)
        {
            await context.Response.WriteAsync($"{country.Key}, {country.Value}\n");
        }
    }); 
});


app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/countries/{countryid:int:range(1,5)}", async context =>
    {
        if(context.Request.RouteValues.ContainsKey("countryid") == false)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("The CountryID should be between 1 and 5");
        }


        int countryid = Convert.ToInt32(context.Request.RouteValues["countryid"]);


        if (countries.ContainsKey(countryid))
        {
            await context.Response.WriteAsync($"{countries[countryid]}");
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync($"[No country]");
        }
    });


    endpoints.MapGet("/countries/{countryid:int:min(6)}", async context =>
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("The CountryID should be between 1 and 5");
    });
});     

app.Run(async context =>
{
    await context.Response.WriteAsync("no response");
});

app.Run();
