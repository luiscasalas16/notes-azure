using Bogus;
using Microsoft.AspNetCore.Mvc;

namespace NetApplicationServiceWebMvc.Api
{
    [ApiController, Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            var result = new Faker<UserDto>()
                .RuleFor(o => o.Id, f => f.Random.Int(1, 100))
                .RuleFor(o => o.FistName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Email, f => f.Internet.Email());

            return result.Generate(5);
        }
    }
}