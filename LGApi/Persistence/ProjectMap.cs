using MongoDB.Bson.Serialization;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Persistence
{
    public class ProjectMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Project>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id);
                map.MapMember(x => x.Project_Name).SetIsRequired(true);
                map.MapMember(x => x.Risk).SetIsRequired(true);
                map.MapMember(x => x.initDate).SetIsRequired(true);
                map.MapMember(x => x.finalDate).SetIsRequired(true);
                map.MapMember(x => x.Project_Value).SetIsRequired(true);
                map.MapMember(x => x.members).SetIsRequired(true);
            });
        }
    }
}