
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzWalk.Data;
using NzWalk.Models;
using NzWalk.Models.DTO;
using NzWalk.Repositories;

namespace NzWalk.controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController: ControllerBase
{
    private readonly NzWalksDbContext dbContext;
    private readonly IRegionRepository regionRepository;

    public RegionsController(NzWalksDbContext dbContext, IRegionRepository regionRepository)
    {
        this.dbContext = dbContext;
        this.regionRepository = regionRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllRegions()
    {
        var regions = await regionRepository.GetAllRegionsAsync();
        
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
    public async Task<IActionResult> GetRegionById(Guid id)
    {
        var region = await dbContext.Regions.FirstOrDefaultAsync(id => id == id);
        
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

    [HttpPost]
    public async Task<IActionResult> CreateRegion(AddRegionDto addRegionDto)
    {
        var regionModel = new Region()
        {
            Name = addRegionDto.Name,
            Code = addRegionDto.Code,
            RegionImageUrl = addRegionDto.RegionImageUrl,
        };
        
        await dbContext.Regions.AddAsync(regionModel);
        await dbContext.SaveChangesAsync();
        
        // Map domain model to DTO
        var regionDto = new RegionDto()
        {
            Id = regionModel.Id,
            Name = regionModel.Name,
            Code = regionModel.Code,
            RegionImageUrl = regionModel.RegionImageUrl
        };
        
        return Ok(regionDto);
    }

    [HttpPut]
    [Route("{id:guid}")]

    public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, UpdateRegionActionDto updateRegionDto)
    {
        var regionModel = await dbContext.Regions.FirstOrDefaultAsync(id => id == id);

        if (regionModel == null)
        {
            return NotFound();
        }
        
        regionModel.Name = updateRegionDto.Name;
        regionModel.RegionImageUrl = updateRegionDto.RegionImageUrl;
        regionModel.Code = updateRegionDto.Code;
        
        await dbContext.SaveChangesAsync();
        
        // Map region model to Dto

        var regionDto = new RegionDto()
        {
            Id = regionModel.Id,
            Name = regionModel.Name,
            Code = regionModel.Code,
            RegionImageUrl = regionModel.RegionImageUrl
        };
        
        return Ok(regionDto);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
    {
        var regionModel = dbContext.Regions.Find(id);

        if (regionModel == null)
        {
            return NotFound();
        }
        
        dbContext.Regions.Remove(regionModel);
        await dbContext.SaveChangesAsync();
        
        // Map Dto model
        var regionDto = new RegionDto()
        {
            Id = regionModel.Id,
            Name = regionModel.Name,
            Code = regionModel.Code,
            RegionImageUrl = regionModel.RegionImageUrl
        };
        
        return Ok(regionDto);
    }
}