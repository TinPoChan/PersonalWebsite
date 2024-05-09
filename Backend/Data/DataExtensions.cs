using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public static class DataExtensions
{
    public static async Task MigrateDBAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
