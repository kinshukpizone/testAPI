using Presentation.DataTransferObject._BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DataTransferObject
{
    public class LocationModel
    {
    }

    public class StateModel : BaseModel<Guid>
    {
        public string StateName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        //public List<CityModel> Cities { get; set; } = new List<CityModel>(); 
    }

    public class CityModel : BaseModel<Guid>
    {
        public string CityName { get; set; } = string.Empty;
        public StateModel State { get; set; }
    }

}
