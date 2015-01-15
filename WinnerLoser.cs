using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Microsoft.Samples.Kinect.BodyBasics
{
	class WinnerLoser
	{
		public WinnerLoser( HandState win, HandState lose )
		{
			winner = win;
			loser  = lose;
		}

		public HandState winner { get; set; }
		public HandState loser  { get; set; }
	}
}
