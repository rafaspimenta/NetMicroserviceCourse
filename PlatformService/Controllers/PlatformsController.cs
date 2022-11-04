using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using System;
using System.Collections.Generic;

namespace PlatformService.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/platforms")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Get platforms...");
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(_platformRepo.GetAll()));
        }

        [HttpGet]
        [Route("{id}", Name = nameof(GetPlatform))]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatform(int id)
        {
            Console.WriteLine($"--> Get platform {id}...");
            var platform = _platformRepo.Get(id);
            if (platform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformCreateDto> CreatePlatform([FromBody] PlatformCreateDto platformDto)
        {
            var platform = _mapper.Map<Platform>(platformDto);
            _platformRepo.Create(platform);
            _platformRepo.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platform);

            return CreatedAtRoute(nameof(GetPlatform), new { platformReadDto.Id }, platformReadDto);
        }

    }
}
