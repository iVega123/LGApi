using Microsoft.AspNetCore.Mvc;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _uow;

        public ProjectController(IProjectRepository projectRepository, IUnitOfWork uow)
        {
            _projectRepository = projectRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> Get()
        {
            var products = await _projectRepository.GetAll();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> Get(Guid id)
        {
            var product = await _projectRepository.GetById(id);

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> Post([FromBody] ProjectViewModel value)
        {

            List<Members> _members = new List<Members>();

            foreach(MembersViewModel member in value.members)
            {
                _members.Add(new Members(member.name)
                {
                    Name = member.name
                });
            }

            var product = new Project(value.Risk, value.ProjectName, value.initDate, value.finalDate, value.ProjectValue, _members);

            _projectRepository.Add(product);

            // it will be null
            var testProduct = await _projectRepository.GetById(product.Id);

            // If everything is ok then:
            await _uow.Commit();

            // The product will be added only after commit
            testProduct = await _projectRepository.GetById(product.Id);

            return Ok(testProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Project>> Put(Guid id, [FromBody] ProjectViewModel value)
        {
            List<Members> _members = new List<Members>();

            foreach (MembersViewModel member in value.members)
            {
                _members.Add(new Members(member.name)
                {
                    Name = member.name
                });
            }

            var product = new Project(value.Risk, value.ProjectName, value.initDate, value.finalDate, value.ProjectValue, _members);

            _projectRepository.Update(product);

            await _uow.Commit();

            return Ok(await _projectRepository.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            _projectRepository.Remove(id);

            // it won't be null
            var testProduct = await _projectRepository.GetById(id);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            testProduct = await _projectRepository.GetById(id);

            return Ok();
        }
    }
}