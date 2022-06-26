﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solarLab.Infrastructure.Features
{
    /// <summary>
    /// Базовый контроллер
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}
