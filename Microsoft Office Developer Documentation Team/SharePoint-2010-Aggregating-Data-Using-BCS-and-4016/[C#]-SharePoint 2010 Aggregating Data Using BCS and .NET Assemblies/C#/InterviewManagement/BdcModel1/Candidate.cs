using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewManagement.BdcNetModel
{
    /// <summary>
    /// This class contains the properties for Entity1. The properties keep the data for Entity1.
    /// If you want to rename the class, don't forget to rename the entity in the model xml as well.
    /// </summary>
    public partial class Candidate
    {
        public Int32 CandidateId {get;set;}
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String MiddleName { get; set; }
        public String PrimaryContactNo { get; set; }
        public String SecondaryContactNo { get; set; }
        public String PrimaryEmail { get; set; }
        public String SecondaryEmail { get; set; }
       

        public Candidate()
        {
        }

        public Candidate(Int32 id)
        {
            CandidateId = id;
        }

        

    }
}
