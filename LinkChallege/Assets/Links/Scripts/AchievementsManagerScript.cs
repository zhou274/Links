#if EASY_MOBILE_PRO
//using EasyMobile;
#endif

namespace Links.Scripts
{
    public class AchievementsManagerScript : GameTemplate.Scripts.AchievementsManagerScript
    {
        public override void CollectionCompleted(int collectionIndex)
        {
#if EASY_MOBILE_PRO
            switch (collectionIndex)
            {
                //case 0:
                //{
                //    GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Easy_Levels_completed);
                //    break;
                //}
                //case 1:
                //{
                //    GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Medium_Levels_completed);
                //    break;
                //}
                //case 2:
                //{
                //    GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Hard_Levels_completed);
                //    break;
                //}
            }
#endif
        }
    }
}