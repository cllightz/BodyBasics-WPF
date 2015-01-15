using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Microsoft.Samples.Kinect.BodyBasics
{
	class Player
	{
		public Player()
		{
			score = 0;
			hands = new Hands();
		}

		public long score { get; set; }
		public Hands hands { get; private set; }

		public bool isAvailL
		{
			get { return hands.L.isAvailable; }
		}

		public bool isAvailR
		{
			get { return hands.R.isAvailable; }
		}

		public string textL
		{
			get { return MainWindow.HandStateToString( hands.L.HS ); }
		}

		public string textR
		{
			get { return MainWindow.HandStateToString( hands.R.HS ); }
		}
	}
}
