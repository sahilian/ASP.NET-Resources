﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Implementation;
using ModelValidatonExample.Models;

namespace ModelValidatonExample.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        [Route("register")]
        public IActionResult Index([Bind(nameof(Person.PersonName),nameof(Person.Email), nameof(Person.Phone), nameof(Person.Password), nameof(Person.ConfirmedPassword), 
            nameof(Person.Price))]Person person)
        {

            if (!ModelState.IsValid)
            {
                List<string> errorMessages = new List<string>();
                foreach (var item in ModelState)
                {
                    //if (item.Errors.Count > 0)
                    //{

                    //}
                    //foreach (var error in item.Errors)
                    //{
                    //    errorMessages.Add(error.ErrorMessage);
                    //}
                }
                string errorList = string.Join("\n", ModelState.Values
                    .SelectMany(errors => errors.Errors)
                    .Select(errorMessage => errorMessage.ErrorMessage).ToList());

                string fullErrorMessage = string.Join(", ", ModelState.Values
                    .Where(v => v.Errors.Count > 0) // using count of erors
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));


                return BadRequest(errorList);
            }
            return Content($"{person}");
            //return new ContentResult();
        }

        [Route("school")]
        public IActionResult School(SchoolVM schoolVM)
        {
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();
                string errorName = string.Join("\n", ModelState.Values.
                    SelectMany(errors => errors.Errors).
                    Select(errorMessage => errorMessage.ErrorMessage).
                    ToList());

                return BadRequest(errorName);
            }
            else
            {
                return Content($"{schoolVM}");
            }
        }
    }
}