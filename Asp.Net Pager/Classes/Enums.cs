using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechTracking.Classes
{

    public partial class Enums
    {
        public enum DocumentsTypes
        {
            Other = 0,
            DriverLicence = 1,
            SSC = 2,
            PassportImage = 3,
            Resume = 4,

        }
        public enum Pages
        {
            Accounts = 1,
            Customers = 2,
            Positions = 3,
            Shows = 4,
            Technicians = 5,
            Bids = 6,
            Peoples = 7,
            Messages = 8,
            Pages = 9,
            Company = 10
        }
        public enum Ovations
        {
            FaceBook = 0,
            Linkedin = 1,
            Friend = 2,
            Recruiter = 3

        }
        public enum PositionStatus
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2
        }
        public enum Month
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }
    }
}