/*
using RogueLibsCore;

namespace Itemized
{
    public class StickyHand : CustomItem
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomItem<StickyHand>()
                     .WithName(new CustomNameInfo
                     {
                         English = "Sticky Hand",
                     })
                     .WithDescription(new CustomNameInfo
                     {
                         English = "One of those sticky rubber arms to grab stuff. Some elite thieves chop off their arms and replace them with these.",
                     })
                     .WithSprite(Properties.Resources.StickyHand)
                     .WithUnlock(new ItemUnlock
                     {
                         Prerequisites = { VanillaAgents.Thief },
                     });

            RoguePatcher patcher = ItemizedPlugin.GetPatcher<StickyHand>();

        }

        public override void SetupDetails()
        {
            Item.itemType = ItemTypes.WeaponMelee;
            Item.itemValue = 10;
            Item.initCount = 5;
            Item.rewardCount = 10;
            Item.stackable = true;
        }

    }
}
*/