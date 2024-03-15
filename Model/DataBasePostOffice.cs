using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOffice.Model
{
    class DataBasePostOffice
    {
        public PostOfficeEntites postOfficeEntities { get; set; }

        public DataBasePostOffice(PostOfficeEntites postOfficeEntities)
        {
            this.postOfficeEntities = postOfficeEntities;
        }
    }
}
