using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rewards
{
	public interface IRewardsRepository
	{
		public Task UpdateData(RewardData data);

		public Task<List<RewardData>> ReadAllRewards();
	}
}