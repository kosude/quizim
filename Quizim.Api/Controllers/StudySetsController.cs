/*
 *   Copyright (c) 2024 Jack Bennett.
 *   All Rights Reserved.
 *
 *   See the LICENCE file for more information.
 */

using Microsoft.AspNetCore.Mvc;

namespace Quizim.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class StudySetsController : ControllerBase
    {
        [HttpGet]
        public string TestGet()
        {
            return "Hello World";
        }
    }
}
