using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakaProjectDomain.Domain.Enums {
	public enum AuthenticationType {
		[Description("Facebook")]
		FACEBOOK = 1,
		[Description("Google")]
		GOOGLE = 2
	}
}
