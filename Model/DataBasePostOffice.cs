using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOffice.Model
{
    class DataBasePostOffice
    {
        public PostOfficeEntities postOfficeEntities { get; set; }

        public DataBasePostOffice(PostOfficeEntities postOfficeEntities)
        {
            this.postOfficeEntities = postOfficeEntities;
        }


    }
}
