using System;
using System.Collections.Generic;

namespace MongoDB.GenericRepository.Model
{
    public class Project
    {
        public Project(int risk, string project_name, DateTime InitDate, DateTime FinalDate, float project_value, List<Members> listMember)
        {
            Project_Name = project_name;
            Risk = risk;
            initDate = InitDate;
            finalDate = FinalDate;
            Project_Value = project_value;
            members = listMember;
            Id = Guid.NewGuid();

        }

        public Project(Guid id, int risk, string project_name, DateTime InitDate, DateTime FinalDate, float project_value, List<Members> listMember)
        {
            Id = id;
            Project_Name = project_name;
            Risk = risk;
            initDate = InitDate;
            finalDate = FinalDate;
            Project_Value = project_value;
            members = listMember;
        }

        public Guid Id { get; private set; }
        public int Risk { get; private set; }
        public string Project_Name { get; private set; }
        public DateTime initDate { get; private set; }
        public DateTime finalDate { get; private set; }
        public float Project_Value { get; private set; }
        public List<Members> members { get; private set;  }
    }
}