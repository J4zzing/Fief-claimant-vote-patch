using HarmonyLib;
using Helpers;
using TaleWorlds.Library;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Election;

namespace FiefVote {
    [HarmonyPatch(typeof(SettlementClaimantDecision))]
    [HarmonyPatch(nameof(SettlementClaimantDecision.CalculateMeritOfOutcome))]
    public class SettlementClaimantDecisionPatch
    {
        public static bool Prefix(ref float __result, ref SettlementClaimantDecision __instance, DecisionOutcome candidateOutcome)
        {
            var clanAsDecisionOutcome = (SettlementClaimantDecision.ClanAsDecisionOutcome)candidateOutcome;
			Clan clan = clanAsDecisionOutcome.Clan;
			float allSettlementValue = 0f;
			int fiefCount = 0;
			foreach (Settlement settlement in clan.Settlements)
			{
				if (settlement.OwnerClan == clanAsDecisionOutcome.Clan && settlement.IsFortification && __instance.Settlement != settlement)
				{
					allSettlementValue += settlement.GetSettlementValueForFaction(clanAsDecisionOutcome.Clan.Kingdom);
                    fiefCount++;
				}
			}
			float targetSettlementValue = __instance.Settlement.GetSettlementValueForFaction(clanAsDecisionOutcome.Clan.Kingdom);
			float clanStrength = clan.TotalStrength;
			if (__instance.Settlement.OwnerClan == clan && __instance.Settlement.Town != null && __instance.Settlement.Town.GarrisonParty != null)
			{
				clanStrength -= __instance.Settlement.Town.GarrisonParty.Party.TotalStrength;
				if (clanStrength < 0f)
				{
					clanStrength = 0f;
				}
			}
			float noFief = (fiefCount == 0) ? 50f : 0f;
			float poverty = (clanAsDecisionOutcome.Clan.Leader.Gold < 30000) ? MathF.Min(30f, 30f - (float)clanAsDecisionOutcome.Clan.Leader.Gold / 1000f) : 0f;
			float captured = (__instance.Settlement.Town != null && __instance.Settlement.Town.LastCapturedBy == clanAsDecisionOutcome.Clan) ? 40f : 0f;
			bool flag = clanAsDecisionOutcome.Clan.Leader == clanAsDecisionOutcome.Clan.Kingdom.Leader;
			float king = flag ? 60f : 0f;
			float player = (clanAsDecisionOutcome.Clan.Leader == Hero.MainHero) ? 30f : 0f;
            __result = ((float)clan.Tier * 30f + clanStrength / 10f + noFief + poverty + captured + king + player) / (allSettlementValue + targetSettlementValue) * 200000f;
            return false;
        }

    }
}
