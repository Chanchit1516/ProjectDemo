using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectDemo.Application.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetAllError(this ModelStateDictionary modelState)
        {
            var validationErrors = new List<string>();

            foreach (var state in modelState)
            {
                validationErrors.AddRange(state.Value.Errors
                    .Select(error => error.ErrorMessage)
                    .ToList());
            }

            return string.Join(",", validationErrors);
        }
    }
}
