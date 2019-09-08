using System;
using UnityEngine;
using UnityEngine.UI;

namespace MidiJack.Mines
{
    public class DebugMidi : MonoBehaviour
    {
        public Text[] DebugTextObjects;

        private void Update()
        {
            var knobNumbers = MidiMaster.GetKnobNumbers();
            for (var i = 0; i < knobNumbers.Length; i++)
            {
                Debug.Log($"Update debug [{i}] for {knobNumbers[i]}");
                var debugTextObject = DebugTextObjects[i];
                debugTextObject.text = MidiMaster.GetKnob(knobNumbers[i]).ToString();
            }
        }
    }
}
