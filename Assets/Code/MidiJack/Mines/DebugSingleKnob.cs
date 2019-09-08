using System;
using UnityEngine;
using UnityEngine.UI;

namespace MidiJack.Mines
{
    public class DebugSingleKnob : MonoBehaviour
    {
        private Text _textObject;
        public MidiChannel Channel;
        public int KnobNumber = 0;

        private void Awake()
        {
            _textObject = GetComponent<Text>();
#if !DEBUG
            _textObject.gameObject.SetActive(false);
#endif
        }

        public void Update()
        {
            _textObject.text = MidiMaster.GetKnob(Channel, KnobNumber).ToString();
        }
    }
}