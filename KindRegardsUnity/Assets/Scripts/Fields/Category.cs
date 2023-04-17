using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Category : MonoBehaviour
{
    public int id { get; set; } = 0;
    public string name { get; set; } = "";
    public bool main_category { get; set; } = true;
    public bool sub_category { get; set; } = false;
    public List<Sentences>? sentences { get; set; }

    public Category() { }

    public Category(int id, string name, bool main_category, bool sub_category)
    {
        this.id = id;
        this.name = name;
        this.main_category = main_category;
        this.sub_category = sub_category;
    }

    public Category(int id, string name, bool main_category, bool sub_category, List<Sentences> s)
    {
        this.id = id;
        this.name = name;
        this.main_category = main_category;
        this.sub_category = sub_category;
        sentences = s;
    }
}
