using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.General.OptionsModel
{
    public class JwtConfigsOptions
    {
        public string Secret { get; set; } = string.Empty;
    }
}
