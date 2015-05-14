using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakaProjectDomain.Domain {
	public class Resource {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual User User { get; set; }
	}
}
