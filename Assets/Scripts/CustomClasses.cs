using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace LindenmayerSystem
{
    /// <summary>
    /// class for storing the position
    /// </summary>
    public class TransformInfo
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    /// <summary>
    /// The base class for the tree to add their data
    /// </summary>
    [System.Serializable]
    public class TreeSettings
    {
        public string axiom;
        public int n;
        public float angle;
        public char F;
        public string rule;

        public TreeSettings(string axiom, int n, float angle, char F, string rule)
        {
            this.axiom = axiom;
            this.angle = angle;
            this.n = n;
            this.F = F;
            this.rule = rule;
        }
    }

    /// <summary>
    /// This class holds every tree data and few commons data
    /// </summary>
    [System.Serializable]
    public class TreesData
    {
        public float length = 5f;

        //Four references in the code to make changes for another tree
        public TreeSettings tree1 = new TreeSettings("F", 5, 25.7f, 'F', "F[+F]F[-F]F");
        public TreeSettings tree2 = new TreeSettings("F", 5, 20f, 'F', "F[+F]F[-F][F]");
        public TreeSettings tree3 = new TreeSettings("F", 4, 22.5f, 'F', "FF-[-F+F+F]+[+F-F-F]");
        public TreeSettings tree4 = new TreeSettings("X", 7, 20, 'X', "F[+X]F[-X]+X");
        public TreeSettings tree5 = new TreeSettings("X", 7, 25.7f, 'X', "F[+X][-X]FX");
        public TreeSettings tree6 = new TreeSettings("X", 5, 22.5F, 'X', "F-[[X]+X]+F[+FX]-X");
    }

    [System.Serializable]
    public enum Trees
    {
        Tree1,
        Tree2,
        Tree3,
        Tree4,
        Tree5,
        Tree6
    }

    [System.Serializable]
    public class TreesTextData
    {
        public TMP_InputField generationsText;
        public TMP_InputField angleText;
        public TMP_InputField axiomText;
        public TMP_InputField ruleOneText;
        public TMP_InputField ruleTwoText;
        public TMP_InputField lengthText;
    }

    [System.Serializable]
    public class ParameterData
    {
        public TextMeshProUGUI generationsText;
        public TextMeshProUGUI angleText;
        public TextMeshProUGUI lengthText;
    }
}
