﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProjectDemo.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDemo.Api.Filters
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
        : base(new ValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
