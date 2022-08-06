using RogueLibsCore;

namespace Itemized
{
    [ItemCategories(RogueCategories.Usable, RogueCategories.Social)]
    public class JokeBook : CustomItem, IItemUsable
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomItem<JokeBook>()
                     .WithName(new CustomNameInfo
                     {
                         English = "Joke Book",
                     })
                     .WithDescription(new CustomNameInfo
                     {
                         English = "The book is titled \"50 Jokes That Will Make You Follow a Random Person's Orders\". Most of the pages are torn off.",
                     })
                     .WithSprite(Properties.Resources.JokeBook)
                     .WithUnlock(new ItemUnlock
                     {
                         Prerequisites = { VanillaAgents.Comedian },
                     });

        }

        public override void SetupDetails()
        {
            Item.itemType = ItemTypes.Tool;
            Item.itemValue = 5;
            Item.initCount = 10;
            Item.rewardCount = 20;
            Item.stackable = true;
            Item.hasCharges = true;
            Item.goesInToolbar = true;
        }
        public bool UseItem()
        {
            if (Owner!.statusEffects.makingJoke) return false;

            string prev = Owner.specialAbility;
            try
            {
                Owner.specialAbility = VanillaAbilities.Joke;
                Owner.statusEffects.PressedSpecialAbility();
            }
            finally
            {
                Owner.specialAbility = prev;
            }
            Count--;
            return true;
        }

    }
}
