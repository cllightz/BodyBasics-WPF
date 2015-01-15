using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Microsoft.Samples.Kinect.BodyBasics
{
	class Hands
	{
		private Hand handL;
		private Hand handR;

		public Hands()
		{
			isDefined = false;
		}

		public Hands( Hand l, Hand r )
		{
			L = l;
			R = r;
			isDefined = true;
		}

		public Hand L
		{
			get { return handL; }
			private set { handL = value; isDefined = true; }
		}

		public Hand R
		{
			get { return handR; }
			private set { handR = value; isDefined = true; }
		}

		private bool isDefined { get; set; }

		public void Update( HandLR LR, Hand hand )
		{
			switch ( LR ) {
				case HandLR.L:
					L = hand;
					break;

				case HandLR.R:
					R = hand;
					break;
			}
		}

		public void Reset()
		{
			isDefined = false;
		}
	}
}
