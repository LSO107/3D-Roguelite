using Dialogue;
using Extensions;
using Player;
using ScriptingFramework;
using UnityEngine;

namespace UI.DialogueUI
{
    internal sealed class DialogueSetup : MonoBehaviour
    {
        [SerializeField] private FourOptionDialogueSetup m_FourOptions;
        [SerializeField] private ThreeOptionDialogueSetup m_ThreeOptions;
        [SerializeField] private TwoOptionDialogueSetup m_TwoOptions;
        [SerializeField] private ContinueDialogueSetup m_Continue;

        [SerializeField] private CanvasGroup m_FourOptionCanvasGroup;
        [SerializeField] private CanvasGroup m_ThreeOptionCanvasGroup;
        [SerializeField] private CanvasGroup m_TwoOptionCanvasGroup;
        [SerializeField] private CanvasGroup m_ContinueCanvasGroup;

        [SerializeField] private ShopUI.ShopUI m_Blacksmith;
        [SerializeField] private ShopUI.ShopUI m_PotionShop;

        private CanvasGroup m_ActiveCanvasGroup;

        public static DialogueSetup Instance;

        private NpcEngine m_NpcEngine;
    
        private void Awake()
        {
            Instance = this;
            m_NpcEngine = GetComponent<NpcEngine>();
        }

        public void DisplayContinue(string line, int nextState)
        {
            m_Continue.Setup(line, () => m_NpcEngine.GoToNextState(nextState));
            DisplayCanvasGroup(m_ContinueCanvasGroup);
        }

        public void DisplayTwoOptions(DialogueOption optionOne, DialogueOption optionTwo)
        {
            m_TwoOptions.Setup(optionOne, optionTwo, i => m_NpcEngine.GoToNextState(i));
            DisplayCanvasGroup(m_TwoOptionCanvasGroup);
        }

        public void DisplayThreeOptions(DialogueOption optionOne, DialogueOption optionTwo, DialogueOption optionThree)
        {
            m_ThreeOptions.Setup(optionOne, optionTwo, optionThree, i => m_NpcEngine.GoToNextState(i));
            DisplayCanvasGroup(m_ThreeOptionCanvasGroup);
        }

        public void DisplayFourOptions(DialogueOption optionOne, DialogueOption optionTwo, DialogueOption optionThree, DialogueOption optionFour)
        {
            m_FourOptions.Setup(optionOne, optionTwo, optionThree, optionFour, i => m_NpcEngine.GoToNextState(i));
            DisplayCanvasGroup(m_FourOptionCanvasGroup);
        }

        public void OpenBlacksmithShop()
        {
            m_Blacksmith.OpenShop();
        }

        public void OpenPotionShop()
        {
            m_PotionShop.OpenShop();
        }

        public void CloseShops()
        {
            m_Blacksmith.CloseShop();
            m_PotionShop.CloseShop();
        }

        public void EndDialogue()
        {
            DisplayCanvasGroup(null);
            PlayerManager.Instance.EnableInput();
        }

        private void DisplayCanvasGroup(CanvasGroup canvasGroup)
        {
            if (m_ActiveCanvasGroup != null)
                m_ActiveCanvasGroup.ToggleCanvasGroup(false);

            if (canvasGroup != null)
                canvasGroup.ToggleCanvasGroup(true);

            m_ActiveCanvasGroup = canvasGroup;
        }
    }
}