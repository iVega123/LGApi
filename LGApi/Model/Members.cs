using System;

namespace MongoDB.GenericRepository.Model
{
    public class Members
    {
        public Members(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public Members(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; set; }
    }
}