using FairyGUI.Utils;

namespace FairyGUI
{
	public class PlayTransitionAction : ControllerAction
	{
		public string transitionName;
		public int playTimes;
		public float delay;
		public bool stopOnExit;

		private Transition _currentTransition;

		public PlayTransitionAction()
		{
			playTimes = 1;
			delay = 0;
		}

		override protected void Enter(Controller controller)
		{
			Transition trans = controller.parent.GetTransition(transitionName);
			if (trans != null)
			{
				if (_currentTransition != null && _currentTransition.playing)
					trans.ChangePlayTimes(playTimes);
				else
					trans.Play(playTimes, delay, null);
				_currentTransition = trans;
			}
		}

		override protected void Leave(Controller controller)
		{
			if (stopOnExit && _currentTransition != null)
			{
				_currentTransition.Stop();
				_currentTransition = null;
			}
		}

		override public void Setup(XML xml)
		{
			base.Setup(xml);

			transitionName = xml.GetAttribute("transition");
			playTimes = xml.GetAttributeInt("repeat", 1);
			delay = xml.GetAttributeFloat("delay", 0);
			stopOnExit = xml.GetAttributeBool("stopOnExit", false);
		}
	}
}
