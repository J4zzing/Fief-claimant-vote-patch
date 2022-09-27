// using System;
// using System.Collections.Generic;

// using HarmonyLib;
// using Helpers;
// using TaleWorlds.CampaignSystem;
// using TaleWorlds.CampaignSystem.Settlements;

// namespace FiefVote {
//     // [HarmonyPatch(typeof(FactionHelper))]
//     // [HarmonyPatch(nameof(FactionHelper.GetDistanceToClosestNonAllyFortificationOfFaction))]
//     public class FactionHelperPatch
//     {
//         public static bool Prefix(ref float __result, IFaction faction)
//         {
//             // TODO: check all kinds of faction, kingdom, bandit, clan, rebelclan
//             // foreach (var kingdom in Kingdom.All)
//             // {
//             //     if (kingdom.IsAtWarWith(faction))
//             // }
//             // var atWarWith = new Dictionary<IFaction, Dictionary<IFaction, bool>>();
//             float closestDistance = float.MaxValue;

//             foreach (var fief in Town.AllFiefs)
//             {
//                 // bool IsAtWarWith = atWarWith?[faction]?[fief.MapFaction] ?? (atWarWith?[faction]?[fief.MapFaction] = fief.MapFaction.IsAtWarWith(faction));
//                 // Kingdom kingdom = (faction as Kingdom) ?? (faction as Clan).Kingdom ?? faction;
//                 float distance;
//                 if (fief.MapFaction.IsAtWarWith(faction) 
//                     && Campaign.Current.Models.MapDistanceModel.GetDistance(faction.FactionMidSettlement, fief.Settlement, closestDistance, out distance))
//                 {
//                     closestDistance = distance;
//                 }
//             }
//             __result = closestDistance;
//             return false;
//         }
//     }
// }
