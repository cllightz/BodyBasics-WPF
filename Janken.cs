using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Microsoft.Samples.Kinect.BodyBasics
{
	class Janken
	{
		private List<ulong> IDs;
		private List<ulong> CurrentPlayers;
		private SortedDictionary<ulong, Player> players;

		public Janken()
		{
			IDs = new List<ulong>();
			CurrentPlayers = new List<ulong>();
			players = new SortedDictionary<ulong, Player>();
		}

		public void Reset()
		{
			foreach ( var id in IDs ) {
				players[id].hands.Reset();
			}

			CurrentPlayers.Clear();
		}

		public void Update( Body body )
		{
			CurrentPlayers.Add( body.TrackingId );

			if ( ! players.ContainsKey( body.TrackingId ) ) {
				players.Add( body.TrackingId, new Player() );
			}

			players[body.TrackingId].hands.Update( HandLR.L, new Hand( body.HandLeftState ) );
			players[body.TrackingId].hands.Update( HandLR.R, new Hand( body.HandRightState ) );
		}

		public string JudgeL()
		{
			bool c = false;
			bool l = false;
			bool o = false;
			string res = "左手\n";
			
			foreach ( var id in CurrentPlayers ) {
				switch ( players[id].hands.L.HS ) {
					case HandState.Closed:
						c = true;
						break;

					case HandState.Lasso:
						l = true;
						break;

					case HandState.Open:
						o = true;
						break;
				}
			}

			if ( ExistWinner( c, l, o ) ) {
				var wl = GetWL( c, l, o );
				var winner = wl.winner;
				var loser  = wl.loser;

				ulong i = 1;

				foreach ( var id in CurrentPlayers ) {
					var player = players[id];

					if ( player.isAvailL ) {
						if ( player.hands.L.HS == winner ) {
							player.score += +20;
							res += String.Format( "プレイヤー{0,-2} ○:{1}\t", i, player.textL );
						} else {
							player.score += -10;
							res += String.Format( "プレイヤー{0,-2} ×:{1}\t", i, player.textL );
						}
					} else {
						res += String.Format( "プレイヤー{0}\t無効\n", i );
					}

					++i;
				}
			} else {
				ulong i = 1;

				foreach ( var id in CurrentPlayers ) {
					var player = players[id];

					if ( player.isAvailL ) {
						res += String.Format( "プレイヤー{0,-2} △:{1}\t", i, player.textL );
					} else {
						res += String.Format( "プレイヤー{0,-2} 無効\t", i );
						player.score += -1;
					}

					++i;
				}
			}

			res += "\n";
			return res;
		}

		public string JudgeR()
		{
			bool c = false;
			bool l = false;
			bool o = false;
			string res = "右手\n";

			foreach ( var id in CurrentPlayers ) {
				switch ( players[id].hands.R.HS ) {
					case HandState.Closed:
						c = true;
						break;

					case HandState.Lasso:
						l = true;
						break;

					case HandState.Open:
						o = true;
						break;
				}
			}

			if ( ExistWinner( c, l, o ) ) {
				var wl = GetWL( c, l, o );
				var winner = wl.winner;
				var loser  = wl.loser;

				ulong i = 1;

				foreach ( var id in CurrentPlayers ) {
					var player = players[id];

					if ( player.isAvailR ) {
						if ( player.hands.R.HS == winner ) {
							player.score += +20;
							res += String.Format( "プレイヤー{0,-2} ○:{1}\t", i, player.textR );
						} else {
							player.score += -10;
							res += String.Format( "プレイヤー{0,-2} ×:{1}\t", i, player.textR );
						}
					} else {
						res += String.Format( "プレイヤー{0,-2} 無効\t\t", i );
						player.score += -1;
					}

					++i;
				}
			} else {
				ulong i = 1;

				foreach ( var id in CurrentPlayers ) {
					var player = players[id];

					if ( player.isAvailR ) {
						res += String.Format( "プレイヤー{0,-2} △:{1}\t", i, player.textR );
					} else {
						res += String.Format( "プレイヤー{0,-2} 無効\t\t", i );
						player.score += -1;
					}

					++i;
				}
			}

			res += "\n";
			return res;
		}

		private bool ExistWinner( bool c, bool l, bool o )
		{
			return (c?1:0) + (l?1:0) + (o?1:0) == 2;
		}

		private WinnerLoser GetWL( bool c, bool l, bool o )
		{
			if ( c && l ) {
				return new WinnerLoser( HandState.Closed, HandState.Lasso );
			} else if ( l && o ) {
				return new WinnerLoser( HandState.Lasso, HandState.Open );
			} else {
				return new WinnerLoser( HandState.Open, HandState.Closed );
			}
		}

		public string Scores()
		{
			string res = "スコア\n";

			ulong i = 1;

			foreach ( var id in CurrentPlayers ) {
				res += String.Format( "プレイヤー{0} {1,8}点\t", i, players[id].score );
				++i;
			}

			return res;
		}
	}
}
