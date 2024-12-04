
using Microsoft.AspNetCore.Mvc;

using NzWalk.Data;
using NzWalk.Models.DTO;

namespace NzWalk.controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController: ControllerBase
{
    private readonly NzWalksDbContext dbContext;

    public RegionsController(NzWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult GetAllRegions()
    {
        var regions = dbContext.Regions.ToList();
        
        // Map Domain model to DTO
        var regionDtos = new List<RegionDto>();
        foreach (var region in regions)
        {
            regionDtos.Add(new RegionDto()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            });
        }
        
        // return Dto model to client side
        return Ok(regionDtos);
        
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetRegionById(Guid id)
    {
        var region = dbContext.Regions.Find(id);
        
        var regionDto = new RegionDto()
        {
            Id = region.Id,
            Name = region.Name,
            Code = region.Code,
            RegionImageUrl = region.RegionImageUrl,
        };
        
        if (regionDto == null)
        {
            return NotFound();
        }
        
        return Ok(regionDto);
    }
}