using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB_B1
{
	public class PlayingCard:IComparable<PlayingCard>, IPlayingCard
	{
		public PlayingCardColor Color { get; init; }
		public PlayingCardValue Value { get; init; }

		#region IComparable Implementation
		public int CompareTo(PlayingCard other)
        {
			if (this.Value != other.Value)
			{
				return Value.CompareTo(other.Value);
			}
			return Color.CompareTo(other.Color);
        }
		#endregion

        #region ToString() related
		string ShortDescription
		{
			get
			{
				switch (Color)
				{
					case PlayingCardColor.Clubs:
						return $"♣ {Value}";
					case PlayingCardColor.Diamonds:
						return $"♦ {Value}";
                    case PlayingCardColor.Hearts:
                        return $"♥ {Value}";
                    case PlayingCardColor.Spades:
                        return $"♠ {Value}";
					default: //Just in case:
						throw new ArgumentException("Invalid card color", nameof(Color));
                }
			}
		}

		public override string ToString() => ShortDescription;
        #endregion
    }
}
