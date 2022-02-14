using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LindenmayerSystem
{
    public class LSystems : MonoBehaviour
    {
        //Has the whole tree data
        TreesData treesData = new TreesData();

        //stacking of a string position
        private Stack<TransformInfo> transformStack;

        // Rules are mentioned 
        [SerializeField] public Dictionary<char, string> rules= new Dictionary<char, string>();

        //An empty string to load the axiom before iteration
        private string currentString = string.Empty;

        private string axiom = "X";

        //This branch is used to draw line on Game Scene 
        public GameObject Branch;

        //Angle for rotation of a branch;
        private float angle;

        void Start()
        {
            TreeSettings temp = treesData.tree1;
            rules.Add(temp.F, temp.rule);
            rules.Add('F', "FF");
            angle = temp.angle;
            axiom = temp.axiom;

            for (int i = 0; i < temp.n; i++)
            {
                Generate();
            }

            /*
            rules = new Dictionary<char, string>
            {
               {'X', "[FX][-FX][+FX]"},
               {'F',"FFF" }
            };
            */

            //rules.Add(temp.F, temp.rule);
        }

        public void Generate()
        {
            currentString = axiom;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < sb.Length; i++)
            {
                foreach (var c in currentString)
                {
                    sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
                }

                currentString = sb.ToString();
            }

            foreach (var c in currentString)
            {
                switch (c)
                {
                    case 'F':
                        Vector3 initialPosition = transform.position;
                        transform.Translate(Vector3.up * treesData.length);

                        GameObject treeSegment = Instantiate(Branch);
                        treeSegment.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                        treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                        break;
                    case 'X':
                        break;
                    case '+':
                        transform.Rotate(Vector3.back * angle);
                        break;

                    case '-':
                        transform.Rotate(Vector3.forward * angle);
                        break;

                    case '[':
                        transformStack.Push(new TransformInfo()
                        {
                            position = transform.position,
                            rotation = transform.rotation
                        });
                        break;

                    case ']':
                        TransformInfo ti = transformStack.Pop();
                        transform.position = ti.position;
                        transform.rotation = ti.rotation;
                        break;

                    default:
                        Debug.LogWarning("Error found");
                        break;
                }
            }
        }
    }
}