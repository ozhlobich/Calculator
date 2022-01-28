using System;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator
{
    public class CalculatorManager : MonoBehaviour
    {
        public Button[] numberButtons;
        public Button plusButton;
        public Button equalButton;
        public Text output;

        private string _firstParam;
        private string _secondParam;
        private InputStates _inputStates;
        private Sign _sign;
    
        // Start is called before the first frame update
        void Start()
        {
            for (var i = 0; i < numberButtons.Length; i++)
            {
                var i1 = i;
                numberButtons[i].onClick.AddListener(() => OnNumberButtonClick(i1));
            }
            plusButton.onClick.AddListener(OnPlusButtonClick);
            equalButton.onClick.AddListener(OnEqualButtonClick);
        }
        
        private void OnNumberButtonClick(int number)
        {
            switch (_inputStates)
            {
                case InputStates.FirstParam:
                    _firstParam += number;
                    output.text = _firstParam;
                    break;
                case InputStates.SecondParam:
                    _secondParam += number;
                    output.text = _secondParam;
                    break;
                case InputStates.Result:
                    _inputStates = InputStates.FirstParam;
                    _firstParam += number;
                    output.text = _firstParam;
                    break;
            }
        }

        private void OnPlusButtonClick()
        {
            _sign = Sign.Plus;
            switch (_inputStates)
            {
                case InputStates.FirstParam:
                    _inputStates = InputStates.SecondParam;
                    break;
                case InputStates.SecondParam:
                    _firstParam = _secondParam;
                    break;
                case InputStates.Result:
                    _firstParam = output.text;
                    _inputStates = InputStates.SecondParam;
                    break;
            }
        }

        private void OnEqualButtonClick()
        {
            
            switch (_sign)
            {
                case Sign.Plus:
                    _sign = Sign.Equal;
                    _inputStates = InputStates.Result;
                    var p1 = string.IsNullOrEmpty(_firstParam) ? 0 : Convert.ToInt32(_firstParam);
                    var p2 = string.IsNullOrEmpty(_secondParam) ? 0 : Convert.ToInt32(_secondParam);
                    output.text = (p1 + p2).ToString();
                    break;
                case Sign.Equal:
                    break;
            }

            _firstParam = string.Empty;
            _secondParam = string.Empty;
        }
    }
}
