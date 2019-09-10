using MidiJack;
using UnityEngine;

namespace Code.ControlScript
{
    public abstract class AbstractControlScript : MonoBehaviour
    {
        public MidiChannel Channel = MidiChannel.Ch1;
        public int KnobNumber = 0;

        public float GetCurrentFloatFromKnob() => MidiMaster.GetKnob(Channel, KnobNumber);
    }
}
