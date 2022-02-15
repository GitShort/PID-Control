

using UnityEngine;
using UnityEngine.UI;
using System.Collections;


	//-------------------------------------------------------------------------
	public class DebugUI : MonoBehaviour
	{
		// private Player player;
		private IntensityUI intensityUI;

		//-------------------------------------------------
		static private DebugUI _instance;
		static public DebugUI instance
		{
			get
			{
				if ( _instance == null )
				{
					_instance = GameObject.FindObjectOfType<DebugUI>();
				}
				return _instance;
			}
		}


		//-------------------------------------------------
		void Start()
		{
			intensityUI = IntensityUI.intensityUI;
			// player = Player.instance;
		}


// #if !HIDE_DEBUG_UI
        //-------------------------------------------------
        private void OnGUI()
		{
			if (intensityUI != null)
			{
				intensityUI.DrawIntensity();
			}
			// player.Draw2DDebug();
		}
// #endif
    }

