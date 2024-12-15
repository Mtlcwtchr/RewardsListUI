namespace Rewards
{
	public class RewardData
	{
		public int id;
		public string avatarLink;
		public string title;
		public string message;
		public int amount;
		public bool claimed;

		public RewardData(int id, string avatarLink, string title, string message, int amount, bool claimed)
		{
			this.id = id;
			this.avatarLink = avatarLink;
			this.title = title;
			this.message = message;
			this.amount = amount;
			this.claimed = claimed;
		}

		public RewardData Clone()
		{
			return new RewardData(id, avatarLink, title, message, amount, claimed);
		}

		public void Copy(RewardData src)
		{
			id = src.id;
			avatarLink = src.avatarLink;
			title = src.title;
			message = src.message;
			amount = src.amount;
			claimed = src.claimed;
		}
	}
}