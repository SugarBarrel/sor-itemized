using System;
using RogueLibsCore;

namespace Itemized
{
    [ItemCategories(RogueCategories.Social, RogueCategories.Usable)]
    public class CrepeHat : CustomItem, IItemUsable
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomItem<CrepeHat>()
                     .WithName(new CustomNameInfo
                     {
                         English = "Crepe Hat",
                     })
                     .WithDescription(new CustomNameInfo
                     {
                         English = "Join the Crepes today! No screening required, just put on this blue hat and go out into the streets!",
                     })
                     .WithSprite(Properties.Resources.CrepeHat)
                     .WithUnlock(new ItemUnlock
                     {
                         Prerequisites = { VanillaAgents.GangsterCrepe },
                     });

            RoguePatcher patcher = ItemizedPlugin.GetPatcher<CrepeHat>();
            patcher.Postfix(typeof(StatusEffects), nameof(StatusEffects.RemoveAllStatusEffectsNotBetweenLevels));

        }

        public static void StatusEffects_RemoveAllStatusEffectsNotBetweenLevels(StatusEffects __instance)
        {
            HatAlignmentHook? hook = __instance.agent.GetHook<HatAlignmentHook>();
            if (hook is not null)
            {
                hook.HeadPiece = "";
                hook.Set();
                __instance.agent.RemoveHook(hook);
            }
        }

        public override void SetupDetails()
        {
            Item.itemType = ItemTypes.Tool;
            Item.initCount = 1;
            Item.itemValue = 60;
            Item.stackable = false;
            Item.noCountText = true;
            Item.goesInToolbar = true;
        }

        public bool UseItem()
        {
            HatAlignmentHook hook = Owner!.GetOrAddHook<HatAlignmentHook>();
            if (hook.ToCrepes == relStatus.Aligned) return false;
            hook.HeadPiece = "HatBlue";
            hook.ToCrepes = relStatus.Aligned;
            hook.ToBlahds = relStatus.Hostile;
            hook.Set();
            Count--;
            return true;
        }

    }
    public class HatAlignmentHook : HookBase<PlayfieldObject>, IDisposable
    {
        public Agent Agent => (Agent)Instance;
        protected override void Initialize() => prevHeadPiece = Agent.agentHitboxScript.headPieceType;
        public string? HeadPiece;
        public relStatus ToCrepes;
        public relStatus ToBlahds;
        private string? prevHeadPiece;

        public void Dispose()
        {
            Agent.agentHitboxScript.headPieceType = prevHeadPiece;
            Agent.agentHitboxScript.prevDir = "";
            Agent.agentHitboxScript.MustRefresh();
            Agent.agentHitboxScript.SetWBSprites();
            Agent.agentHitboxScript.UpdateAnim();
        }
        public void Set()
        {
            Agent.agentHitboxScript.headPieceType = HeadPiece;
            Agent.agentHitboxScript.prevDir = "";
            Agent.agentHitboxScript.MustRefresh();
            Agent.agentHitboxScript.SetWBSprites();
            Agent.agentHitboxScript.UpdateAnim();

            string crepeRel = ToCrepes.ToString();
            string blahdRel = ToBlahds.ToString();
            foreach (Agent other in GameController.gameController.agentList)
            {
                if (other == Agent) return;
                if (other.agentName is VanillaAgents.GangsterCrepe)
                {
                    other.relationships.SetRel(Agent, crepeRel);
                }
                else if (other.agentName is VanillaAgents.GangsterBlahd)
                {
                    other.relationships.SetRel(Agent, blahdRel);
                }
            }

        }
    }
}
