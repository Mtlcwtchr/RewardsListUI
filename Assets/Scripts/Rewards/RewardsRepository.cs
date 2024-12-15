using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Rewards
{
	public class RewardsRepository : IRewardsRepository
	{
		private List<RewardData> _rewards;

		private string _srcFileName;

		public RewardsRepository(string srcFileName)
		{
			_srcFileName = srcFileName;
		}
		
		public async Task UpdateData(RewardData data)
		{
			var reward = _rewards.Find(r => r.id == data.id);
			reward.Copy(data);
			await Flush();
		}

		public async Task<List<RewardData>> ReadAllRewards()
		{
			return Copy((_rewards ??= await ReadRewards()));
		}

		private List<RewardData> Copy(List<RewardData> src)
		{
			var newRewards = new List<RewardData>(src.Count);
			for (var i = 0; i < _rewards.Count; i++)
			{
				newRewards.Add(_rewards[i].Clone());
			}
			return newRewards;
		}

		private async Task<List<RewardData>> ReadRewards()
		{
			StreamReader reader = null;
			try
			{
				reader = new StreamReader(_srcFileName);
				var json = await reader.ReadToEndAsync();
				var rewardsData = JsonConvert.DeserializeObject<List<RewardData>>(json);
				return rewardsData;
			}
			catch (Exception e)
			{
				Debug.LogError(e);
				throw;
			}
			finally
			{
				reader?.Close();
			}
		}

		private async Task Flush()
		{
			await Task.Delay(500);
			StreamWriter writer = null;
			try
			{
				writer = new StreamWriter(_srcFileName, false);
				var json = JsonConvert.SerializeObject(_rewards);
				await writer.WriteAsync(json);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
				throw;
			}
			finally
			{
				writer?.Close();
			}
		}
	}
}