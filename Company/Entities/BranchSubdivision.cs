using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Entities
{
    public class BranchSubdivision
    {
        private int id;
        private Branch branch;
        private Subdivision subdivision;

        public BranchSubdivision(int id, Branch branch, Subdivision subdivision)
        {
            this.id = id;
            this.branch = branch;
            this.subdivision = subdivision;
        }

        public int Id { get => id; set => id = value; }
        internal Branch Branch { get => branch; set => branch = value; }
        internal Subdivision Subdivision { get => subdivision; set => subdivision = value; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return branch.ToString() + " " + subdivision.ToString();
        }
    }
}
