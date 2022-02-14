using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;
using TMPro;

namespace LindenmayerSystem
{
    public class KochCurve : MonoBehaviour
    {
        [SerializeField]private GameObject branch;
        [SerializeField]private GameObject tree;
        private GameObject tempTree;

        private float length;

        //Tree data
        private int n;
        private float angle;
        private string axiom;
        private Dictionary<char, string> rules;

        //This string hold the whole iteration data
        private string currentString;

        //Storing the position of the branch
        private Stack<TransformInfo> transformStack = new Stack<TransformInfo>();

        //Enum Types to set data of the trees
        public Trees trees;

        //Parameters text
        public ParameterData parameterData;

        private void Awake()
        {
            TreeType();
        }

        public void AddGeneration()
        {
            if (n >=0)
            {
                ResetTree();
                n++;
                GenerateNodeRewriting(n, angle, axiom, rules, length);
            }
        }

        public void SubGeneration()
        {
            if (n >0)
            {
                ResetTree();
                n--;
                GenerateNodeRewriting(n, angle, axiom, rules, length);
            }
        }

        public void AddAngle()
        {
            if (angle >= 1)
            {
                ResetTree();
                angle++;
                GenerateNodeRewriting(n, angle, axiom, rules, length);
            }
        }

        public void SubAngle()
        {
            if (angle > 0.1)
            {
                ResetTree();
                angle--;
                GenerateNodeRewriting(n, angle, axiom, rules, length);
            }
        }

        public void AddLength()
        {
            if (length >= 0)
            {
                ResetTree();
                length += 0.1f;
                GenerateNodeRewriting(n, angle, axiom, rules, length);
            }
        }

        public void SubLength()
        {
            if (length > 0.1)
            {
                ResetTree();
                length -= 0.1f;
                GenerateNodeRewriting(n, angle, axiom, rules, length);
            }
        }

        private void Update()
        {
            parameterData.generationsText.text = n.ToString();
            parameterData.angleText.text = angle.ToString();
            parameterData.lengthText.text = length.ToString("F1");
        }

        public void ResetTree()
        {
            Destroy(tempTree);
            this.transform.position = Vector3.zero;
            this.transform.rotation = Quaternion.identity;
        }

        private void TreeType()
        {
            if (tempTree!=null)
            {
                Destroy(tempTree);
            }

            switch (trees)
            {
                case Trees.Tree1:
                    n = 5;
                    angle = 25.7f;
                    axiom = "F";

                    rules = new Dictionary<char, string>
                    {
                        {'F', "F[+F]F[-F]F"}
                    };

                    length = 1.25f;
                    break;

                case Trees.Tree2:
                    n = 5;
                    angle = 20f;
                    axiom = "F";

                    rules = new Dictionary<char, string>
                    {
                        { 'F', "F[+F]F[-F][F]"}
                    };

                    length = 4.8f;
                    break;

                case Trees.Tree3:
                    n = 4;
                    angle = 22.5f;
                    axiom = "F";

                    rules = new Dictionary<char, string>
                    {
                        { 'F', "FF-[-F+F+F]+[+F-F-F]"}
                    };

                    length =  5.5f;
                    break;


                case Trees.Tree4:
                    n = 7;
                    angle = 20f;
                    axiom = "X";
                    
                    rules = new Dictionary<char, string>
                    {
                        { 'X',"F[+X]F[-X]+X" },
                        {'F',"FF" }
                    };
                    
                    length = 1.2f;
                    break;

                case Trees.Tree5:
                    n = 7;
                    angle = 25.7f;
                    axiom = "X";

                    rules = new Dictionary<char, string>
                    {
                        {'X',"F[+X][-X]FX"},
                        {'F',"FF" }
                    };

                    length = 1.2f;
                    break;

                case Trees.Tree6:
                    n = 5;
                    angle = 22.5f;
                    axiom = "X";

                    rules = new Dictionary<char, string>
                    {
                        {'X', "F-[[X]+X]+F[+FX]-X"},
                        {'F',"FF" }
                    };

                    length = 3.8f;
                    break;
            }

            GenerateNodeRewriting();
        }

        public void GenerateNodeRewriting(int n, float angle, string axiom, Dictionary<char, string> rules, float length)
        {
            if (tempTree != null)
            {
                Destroy(tempTree);
            }


            //Resetting position and rotation
            this.transform.position = Vector3.zero;
            this.transform.rotation = Quaternion.identity;
            tempTree = Instantiate(tree);

            this.n = n;
            this.angle = angle;
            this.axiom = axiom;
            this.rules = rules;
            this.length = length;

            this.currentString = this.axiom;

            currentString = TreeFormula(currentString);

            SpawnTree(currentString);
        }

        //Node rewriting
        void GenerateNodeRewriting()
        {
            //Resetting position and rotation
            this.transform.position = Vector3.zero;
            this.transform.rotation = Quaternion.identity;

            tempTree = Instantiate(tree);

            currentString = axiom;

            currentString = TreeFormula(currentString);

            SpawnTree(currentString);
        }

        string TreeFormula(string currentString)
        {

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < n; i++)
            {
                foreach (char c in currentString)
                {
                    sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
                }

                currentString = sb.ToString();
                sb = new StringBuilder();
            }

            return currentString;
        }

        void SpawnTree(string currentString)
        {
            for (int i = 0; i < currentString.Length; i++)
            {
                char currentCharacter = currentString[i];

                if (currentCharacter == 'X')
                {

                }

                else if (currentCharacter == 'F')
                {
                    Vector3 intialPosition = transform.position;
                    transform.Translate(Vector3.up * length);

                    GameObject treeSegment = Instantiate(branch);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, intialPosition);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    treeSegment.gameObject.transform.SetParent(tempTree.transform);

                    //Debug.DrawLine(intialPosition, transform.position, Color.green, 100000f);
                }

                else if (currentCharacter == '+')
                {
                    transform.Rotate(Vector3.forward * angle);
                }

                else if (currentCharacter == '-')
                {
                    transform.Rotate(Vector3.forward * -angle);
                }

                else if (currentCharacter == '[')
                {
                    TransformInfo ti = new TransformInfo();
                    ti.position = transform.position;
                    ti.rotation = transform.rotation;
                    transformStack.Push(ti);
                }

                else if (currentCharacter == ']')
                {
                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                }
            }
        }

        public void Tree1()
        {
            trees = Trees.Tree1;
            TreeType();
        }

        public void Tree2()
        {
            trees = Trees.Tree2;
            TreeType();
        }

        public void Tree3()
        {
            trees = Trees.Tree3;
            TreeType();
        }

        public void Tree4()
        {
            trees = Trees.Tree4;
            TreeType();
        }

        public void Tree5()
        {
            trees = Trees.Tree5;
            TreeType();
        }

        public void Tree6()
        {
            trees = Trees.Tree6;
            TreeType();
        }

        public void GenerateTree()
        {
            trees = Trees.Tree6;
            TreeType();
        }




        //Edge rewriting
        public void GenerateEdgeRewriting()
        {
            string newString = "";

            char[] stringCharacters = currentString.ToCharArray();

            for (int i = 0; i < stringCharacters.Length; i++)
            {
                char currentCharacter = stringCharacters[i];

                if (rules.ContainsKey(currentCharacter))
                {
                    //Debug.Log(currentCharacter);
                    newString += rules[currentCharacter];
                }
                else
                {
                    //Debug.Log(currentCharacter);
                    newString += currentCharacter.ToString();
                }
            }

            currentString = newString;
            //Debug.Log(currentString);

            stringCharacters = currentString.ToCharArray();

            for (int i = 0; i < stringCharacters.Length; i++)
            {
                char currentCharacter = stringCharacters[i];

                if(currentCharacter=='X')
                {

                }

                else if (currentCharacter=='F')
                {
                    Vector3 intialPosition = transform.position;
                    transform.Translate(Vector3.up * length);

                    /*
                    Debug.DrawLine(intialPosition, transform.position, Color.green, 100000f);
                    */

                    GameObject treeSegment = Instantiate(branch);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, intialPosition);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    treeSegment.gameObject.transform.SetParent(this.transform);
                }

                else if (currentCharacter == '+')
                {
                    transform.Rotate(Vector3.forward * angle);
                }

                else if (currentCharacter == '-')
                {
                    transform.Rotate(Vector3.forward * -angle);
                }

                else if (currentCharacter == '[')
                {
                    TransformInfo ti = new TransformInfo();
                    ti.position = transform.position;
                    ti.rotation = transform.rotation;
                    transformStack.Push(ti);
                }

                else if (currentCharacter == ']')
                { 
                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                }
            }
        }
    }
}
