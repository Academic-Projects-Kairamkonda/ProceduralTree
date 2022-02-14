using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

namespace LindenmayerSystem
{
    public class LSystemsDataManager : MonoBehaviour
    {
        public KochCurve kochCurve;

        public TreesTextData textData;

        private int gen;
        private float angle;
        private string axiom;
        private string ruleOne;
        private string ruleTw0;
        private float length;
        private Dictionary<char, string> rules;

        private const string checker1 = "F[+F]F[-F][F]";
        private const string checker2 = "FF-[-F+F+F]+[+F-F-F]";


        private const string Xcheck1 = "F[+X]F[-X]+X";
        private const string Xcheck2 = "FF";


        private void ProcessInputData()
        {
            gen = int.Parse(textData.generationsText.text.ToString());
            angle = float.Parse(textData.angleText.text.ToString());
            axiom = textData.axiomText.text.ToString().ToUpper();
            ruleOne = textData.ruleOneText.text.ToString().ToUpper();
            ruleTw0 = textData.ruleTwoText.text.ToString().ToUpper();
            length = float.Parse(textData.lengthText.text.ToString());
        }

        public void GenerateData()
        {
            ProcessInputData();

            if(string.IsNullOrEmpty(textData.ruleTwoText.text.ToString()))
            {
                Debug.Log("Rule 2 is Empty");

                kochCurve.GenerateNodeRewriting(gen, angle, axiom, rules = new Dictionary<char, string>
                {
                    {'F',ruleOne}
                }, length);
            }
            else
            {
                Debug.Log("Rule 2 is assigned");

                rules = new Dictionary<char, string>
                {
                    {'X',ruleOne},
                    {'F',ruleTw0}
                };

                kochCurve.GenerateNodeRewriting(gen, angle, axiom, rules, length) ;
            }
        }


        string DataText(string dataName,TextMeshProUGUI nameTemp)
        {
            string name;
            name = nameTemp.text.ToString();
            name = dataName + ": " + name;
            return name;

            /* Debug Data
            Debug.Log(DataText("n", generationsText));
            Debug.Log(DataText("angle", angleText));
            Debug.Log(DataText("axiom", axiomText));
            Debug.Log(DataText("rule1", ruleOneText));
            Debug.Log(DataText("rule2", ruleTwoText));
            */
        }


    }
}