using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Valve.VR.InteractionSystem
{
    public class SceneController : MonoBehaviour
    {
        public SteamVR_Action_Boolean controllerAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("ReloadScene");

        private Player player = null;
        private bool active = false;

        private static SceneController _instance;
        public static SceneController instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SceneController>();
                }
                return _instance;
            }
        }
        private IEnumerator Start()
        {
            _instance = this;
            player = InteractionSystem.Player.instance;

            while (SteamVR.initializedState == SteamVR.InitializedStates.None || SteamVR.initializedState == SteamVR.InitializedStates.Initializing)
                yield return null;
        }

        void Update()
        {
            active = controllerAction.GetState(player.leftHand.handType) ^ controllerAction.GetState(player.rightHand.handType);
            if (active)
            {
                // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe")); //new program
                Application.Quit(); //kill current process
            }
        }
    }

}
