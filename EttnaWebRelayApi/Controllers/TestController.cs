using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace EttnaWebRelayApi.Controllers
{
	public class TestController : ApiController
	{
		private string[] m_testArray = new string[] { "This is a test", "This is a string" };

		[HttpGet]
		public string[] Test()
		{
			return m_testArray;
		}

		[HttpGet]
		public string Print(string input)
		{
			return "This is a test controller. Input: " + input;
		}
	}
}
