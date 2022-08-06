using RogueLibsCore;

namespace Itemized
{
    [ItemCategories(RogueCategories.Social, RogueCategories.Usable)]
    public class BlahdHat : CustomItem, IItemUsable
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomItem<BlahdHat>()
                     .WithName(new CustomNameInfo
                     {
                         English = "Blahd Hat",
                     })
                     .WithDescription(new CustomNameInfo
                     {
                         English = "Join the Blahds today! No screening required, just put on this red hat and go out into the streets.",
                     })
                     .WithSprite(Properties.Resources.BlahdHat)
                     .WithUnlock(new ItemUnlock
                     {
                         Prerequisites = { VanillaAgents.GangsterBlahd },
                     });

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
            if (hook.ToBlahds == relStatus.Aligned) return false;
            hook.HeadPiece = "HatRed";
            hook.ToCrepes = relStatus.Hostile;
            hook.ToBlahds = relStatus.Aligned;
            hook.Set();
            Count--;
            return true;
        }

    }
}
