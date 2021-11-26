using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IMongoContext context) : base(context)
        {
        }
    }
}
