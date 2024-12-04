using Microsoft.AspNetCore.Mvc;

namespace NzWalk.controllers;

[Route("api/[controller]")]
[ApiController]
public class employee : ControllerBase
{
    [HttpGet]
    public IActionResult GetEmployees()
    {
        string[] employeNames = new String[] {"jhon", "adam", "jhonn", "jhonnen"};
        
        return Ok(employeNames);
    }
    
}