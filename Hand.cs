using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Microsoft.Samples.Kinect.BodyBasics
{
	class Hand
	{
		public Hand( HandState hs )
		{
			HS = hs;
		}

		public HandState HS { get; set; }

		public bool isAvailable
		{
			get { return MainWindow.IsHandStateAvailable( HS ); }
		}
	}
}
