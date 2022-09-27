using System.Linq;

using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.GameComponents;

namespace FiefVote {
    [HarmonyPatch(typeof(DefaultSettlementValueModel))]
    [HarmonyPatch("GeographicalAdvantageForFaction")]
    public class DefaultSettlementValueModelPatch
    {
        public static bool Prefix(ref float __result, Settlement settlement, IFaction faction)
        {
            // Make geo advantage scales very slowly, since this advantage is considered little.
            Settlement factionMidSettlement = faction.FactionMidSettlement;
            float distanceToFactionMidSettlement = Campaign.Current.Models.MapDistanceModel.GetDistance(settlement, factionMidSettlement);
            
            // Ideally this should ranged between 0-1.
            float disadvantage = distanceToFactionMidSettlement / Campaign.MapDiagonal;
            // Should range between 0.5-1
            __result = 1f / (1f + disadvantage);
            return false;
        }

        // TODO: inject it to Settlement class and cached it.
        // static float DistanceToClosestNonEnemyFief(Settlement settlement, Kingdom kingdom)
        // {
        //     float closestDistance = float.MaxValue;
        //     foreach (var otherKingdom in Kingdom.All)
        //     {
        //         if (!kingdom.IsAtWarWith(otherKingdom)) continue;

        //         foreach (var fiefAtWar in otherKingdom.Fiefs)
        //         {
        //             float distance;

        //             if (fiefAtWar.Settlement != settlement
        //                 && Campaign.Current.Models.MapDistanceModel.GetDistance(settlement, fiefAtWar.Settlement, closestDistance, out distance))
        //             {
        //                 closestDistance = distance;
        //             }
        //         }
        //     }
        //     return closestDistance;
        // }

        // TODO
        // [HarmonyPatch(nameof(DefaultSettlementValueModel.CalculateSettlementValueForFaction))]
        // static bool Prefix2(ref float __result, Settlement settlement, IFaction faction)
        // {
        //     return false;
        // }
    }

    // [HarmonyPatch(typeof(Settlement))]
    // public class SettlementPatch
    // {
    // }
}
