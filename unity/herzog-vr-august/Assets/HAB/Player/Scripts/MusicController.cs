using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class MusicController : MonoBehaviour
    {
        public SteamVR_Action_Boolean controllerAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("ToggleMusic");
        public AudioSource audioSource;

        public bool active = false;
        public bool overwriteActiveWithInput = true;
        private Player player = null;

        private static MusicController _instance;
        public static MusicController instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MusicController>();
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
            if (overwriteActiveWithInput && audioSource != null)
            {
                active = controllerAction.GetState(player.leftHand.handType) ^ controllerAction.GetState(player.rightHand.handType);
                if (active)
                {
                    if (audioSource.isPlaying)
                    {
                        audioSource.Pause();
                    }
                }
                else
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.UnPause();
                    }
                }
            }
        }
    }
}
