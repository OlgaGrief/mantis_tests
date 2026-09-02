using System;

namespace mantis_tests
{
    public class ProjectData :
        IEquatable<ProjectData>,
        IComparable<ProjectData>
    {
        public string ProjectName { get; set; }
        public string Id { get; set; }

        public ProjectData(string name)
        {
            ProjectName = name;
        }

        public ProjectData()
        {
        }
        
        public bool Equals(ProjectData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return ProjectName == other.ProjectName;
        }

        public int CompareTo(ProjectData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }

            return ProjectName.CompareTo(other.ProjectName);
        }

        public override int GetHashCode()
        {
            return ProjectName.GetHashCode();
        }
    }
}