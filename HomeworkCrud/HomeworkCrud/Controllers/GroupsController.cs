using HomeworkCrud.Data;
using HomeworkCrud.Data.Entities;
using HomeworkCrud.Dtos.GroupDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public GroupsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet("")]
        public ActionResult<List<GroupGetDto>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var groups = _appDbContext.Groups
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new GroupGetDto
                {
                    Id = x.Id,
                    No = x.No,
                    Limit = x.Limit
                })
                .ToList();

            return StatusCode(200, groups);
        }

        [HttpGet("{id}")]

        public ActionResult<GroupGetDto> GetById(int id)
        {
            var data = _appDbContext.Groups.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                return StatusCode(404);
            }

            GroupGetDto dto = new GroupGetDto
            {
                Id = data.Id,
                No = data.No,
                Limit = data.Limit
            };
            return StatusCode(200, dto);
        }
        [HttpPost("")]
        public ActionResult Create(GroupPostDto groupPostDto)
        {
            Group group = new()
            {
                No = groupPostDto.No,
                Limit = groupPostDto.Limit,
                CreatedAt = DateTime.Now,
            };
            _appDbContext.Groups.Add(group);
            _appDbContext.SaveChanges();
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, GroupPutDto groupPut)
        {
            var data = _appDbContext.Groups.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                return StatusCode(404);
            }

            data.No = groupPut.No;
            data.Limit = groupPut.Limit;
            data.ModifiedAt = DateTime.Now;
            _appDbContext.SaveChanges();
            return StatusCode(201);
        }



        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var data = _appDbContext.Groups.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                return StatusCode(404);
            }
            _appDbContext.Groups.Remove(data);
            _appDbContext.SaveChanges();
            return StatusCode(201);
        }


    }
}
