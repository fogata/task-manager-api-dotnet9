using System.Net;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Exceptions;

namespace TaskManager.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetStatusCode(ex);

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Error = GetErrorMessage(ex)
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }

    private static HttpStatusCode GetStatusCode(Exception ex) =>
        ex switch
        {
            ValidationException => HttpStatusCode.BadRequest,
            NotFoundException => HttpStatusCode.NotFound,
            DbUpdateConcurrencyException => HttpStatusCode.Conflict,
            _ => HttpStatusCode.InternalServerError
        };

    private static string GetErrorMessage(Exception ex) =>
        ex switch
        {
            ValidationException validation =>
                string.Join(" | ", validation.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")),
            NotFoundException nf => nf.Message,
            DbUpdateConcurrencyException => "Ocorreu um conflito ao atualizar os dados. Tente novamente.",
            _ => "Ocorreu um erro inesperado. Tente novamente mais tarde."
        };
}
