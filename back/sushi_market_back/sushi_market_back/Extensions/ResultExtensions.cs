using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace sushi_market_back.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(
            this ControllerBase controller,
            Result<T> result)
        {
            if (result == null)
            {
                return controller.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "The operation returned a null result.");
            }

            if (result.IsSuccess)
            {
                if (typeof(T) == typeof(Unit))
                {
                    return controller.NoContent();
                }

                return controller.Ok(result.Value);
            }

            if (result.Errors.Count > 0 &&
                !string.IsNullOrWhiteSpace(result.Errors[0].Message))
            {
                return controller.BadRequest(result.Errors[0].Message);
            }

            return controller.BadRequest("Something went wrong");
        }
    }
}