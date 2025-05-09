using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using Emata.Shared.Shared;
namespace Emata.API.Configuration.EndpointConfiguration
{
    public interface IEndpoints
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }

    public static class EndpointConfiguration
    {
        public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
        {
            ServiceDescriptor[] serviceDescriptors = assembly
                .DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                               type.IsAssignableTo(typeof(IEndpoints)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoints), type))
                .ToArray();

            services.TryAddEnumerable(serviceDescriptors);

            return services;
        }

        public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
        {
            IEnumerable<IEndpoints> endpoints = app.Services
                .GetRequiredService<IEnumerable<IEndpoints>>();

            IEndpointRouteBuilder builder =
                routeGroupBuilder is null ? app : routeGroupBuilder;

            foreach (IEndpoints endpoint in endpoints)
            {
                endpoint.MapEndpoint(builder);
            }

            return app;
        }

        public static IResult Paged<T>(this IResultExtensions resultExtensions, PagedList<T> list)
        {
            ArgumentNullException.ThrowIfNull(list, nameof(list));

            return new PagedResult<T>(list);
        }
    }

    internal class PagedResult<T> : IResult
    {
        private readonly PagedList<T> _response;

        public PagedResult(PagedList<T> response)
        {
            _response = response;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.Headers.Append(Constants.PaginationHeaders.TotalCount, _response.TotalCount.ToString());
            httpContext.Response.Headers.Append(Constants.PaginationHeaders.TotalPages, _response.TotalPages.ToString());
            httpContext.Response.Headers.Append(Constants.PaginationHeaders.CurrentPage, _response.CurrentPage.ToString());
            httpContext.Response.Headers.Append(Constants.PaginationHeaders.PageSize, _response.PageSize.ToString());

            return httpContext.Response.WriteAsJsonAsync(_response);
        }
    }
}
