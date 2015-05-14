using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakaProjectDomain.Domain {
	public class Token {

		public Token(string token)
			: this() {
			Value = token;
		}

		public Token() {
			GeneratedTime = DateTime.Now;
		}

		public virtual int Id { get; set; }
		public virtual string Value { get; set; }
		public virtual DateTime GeneratedTime { get; set; }
	}
}
