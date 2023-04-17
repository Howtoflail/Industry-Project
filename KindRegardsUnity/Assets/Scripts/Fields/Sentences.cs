using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentences : MonoBehaviour
{
    public int id { get; set; } = 0;
    public string begin_text { get; set; } = "";
    public string end_text { get; set; } = "";
    public bool variable { get; set; } = false;
    public bool category { get; set; } = false;
    public Category? categoryID { get; set; }
    //public List<TextVariable>? variables { get; set; }

    public Sentences() { }

    //public Sentences(int id, string begin_text, string end_text, bool variable, bool category, Category categoryID, List<TextVariable> variables)
    //{
    //    this.id = id;
    //    this.begin_text = begin_text;
    //    this.end_text = end_text;
    //    this.variable = variable;
    //    this.category = category;
    //    this.categoryID = categoryID;
    //    this.variables = variables;
    //}

    public Sentences(int id, string begin_text, string end_text, bool variable, bool category, Category categoryID)
    {
        this.id = id;
        this.begin_text = begin_text;
        this.end_text = end_text;
        this.variable = variable;
        this.category = category;
        this.categoryID = categoryID;
    }
}
