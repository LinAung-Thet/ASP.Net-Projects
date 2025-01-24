using DynamicExpression.Database;
using DynamicExpression.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ProductDbContext>(x => x.UseInMemoryDatabase("ProductDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/products", async ([FromBody] ProductSearchCriteria productSearch, ProductDbContext dbContext) =>
{
    await dbContext.Database.EnsureCreatedAsync();
    ParameterExpression parameterExp = Expression.Parameter(typeof(Product), "x");
    Expression predicate = Expression.Constant(true);

    if (productSearch.IsActive.HasValue)
    {
        MemberExpression memberExp = Expression.Property(parameterExp, nameof(productSearch.IsActive));
        ConstantExpression constantExp = Expression.Constant(productSearch.IsActive.Value);
        BinaryExpression binaryExp = Expression.Equal(memberExp, constantExp);
        predicate = Expression.AndAlso(predicate, binaryExp);
    }

    if (productSearch.Categories is not null && productSearch.Categories.Any())
    {
        //x.Category
        MemberExpression memberExp = Expression.Property(parameterExp, nameof(Product.Category));
        Expression orExpresion = Expression.Constant(false);
        foreach (var category in productSearch.Categories)
        {
            var constExp = Expression.Constant(category.Name);
            BinaryExpression binaryExp = Expression.Equal(memberExp, constExp);
            orExpresion = Expression.OrElse(orExpresion, binaryExp);
        }
        predicate = Expression.AndAlso(predicate, orExpresion);
    }

    if (productSearch.Names is not null && productSearch.Names.Any())
    {
        //x.Name
        MemberExpression memberExp = Expression.Property(parameterExp, nameof(Product.Name));
        Expression orExpresion = Expression.Constant(false);
        foreach (var productName in productSearch.Names)
        {
            var constExp = Expression.Constant(productName.Name);
            BinaryExpression binaryExp = Expression.Equal(memberExp, constExp);
            orExpresion = Expression.OrElse(orExpresion, binaryExp);
        }
        predicate = Expression.AndAlso(predicate, orExpresion);
    }

    if (productSearch.Price is not null)
    {
        //x.Price 400
        MemberExpression memberExp = Expression.Property(parameterExp, nameof(Product.Price));
        //x.Price >= 400
        if (productSearch.Price.Min is not null)
        {
            var constExp = Expression.Constant(productSearch.Price.Min);
            var binaryExp = Expression.GreaterThanOrEqual(memberExp, constExp);
            predicate = Expression.AndAlso(predicate, binaryExp);
        }
        //x.Price >= min && x.Price <= max
        if (productSearch.Price.Max is not null)
        {
            var constExp = Expression.Constant(productSearch.Price.Max);
            var binaryExp = Expression.LessThanOrEqual(memberExp, constExp);
            predicate = Expression.AndAlso(predicate, binaryExp);
        }
    }

    var lambda = Expression.Lambda<Func<Product, bool>>(predicate, parameterExp);
    var data = await dbContext.Products.Where(lambda).ToListAsync();
    return Results.Ok(data);
});

app.UseHttpsRedirection();

app.Run();
