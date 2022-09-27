using HarmonyLib;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.GameComponents;

namespace FiefVote {
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad() {
            base.OnSubModuleLoad();

            Harmony harmony = new Harmony("j4zzing.FiefVote");
            harmony.Patch(typeof(SettlementClaimantDecision).GetMethod(nameof(SettlementClaimantDecision.CalculateMeritOfOutcome)),
                          new HarmonyMethod(typeof(SettlementClaimantDecisionPatch).GetMethod("Prefix")));
            harmony.Patch(AccessTools.Method(typeof(DefaultSettlementValueModel), "GeographicalAdvantageForFaction"),
                          new HarmonyMethod(typeof(DefaultSettlementValueModelPatch).GetMethod("Prefix")));

            // Somehow PatchAll cant find the private method GeographicalAdvantageForFaction in release build.
            // harmony.PatchAll();
            // Harmony.DEBUG = true;
        }

#if DEBUG
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            InformationManager.DisplayMessage(new InformationMessage(message));
        }

        private string message = "FiefVote v1";
#endif
    }
}
