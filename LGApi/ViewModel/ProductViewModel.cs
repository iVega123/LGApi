using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;

namespace MongoDB.GenericRepository.ViewModel
{
    public class ProjectViewModel
    {
        public string ProjectName { get; set; }
        
        public bool ShouldCommit { get; set; } = true;
       
        public int Risk { get; set; }
        
        public float ProjectValue { get; set; }
        
        public DateTime initDate { get; set; }

        public DateTime finalDate { get; set; }

        public List<MembersViewModel> members { get; set; }
    }
}
