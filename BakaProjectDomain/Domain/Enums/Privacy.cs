using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakaProjectDomain.Domain.Enums {
	public enum Privacy {
		[Description("Avalik")]
		PUBLIC = 1,
		[Description("Avalik ainult õpetajatele")]
		PUBLIC_TO_TEACHERS = 2
	}
}
