using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryDTO : MonoBehaviour
{
    public int Id { get; set; }
    public string name { get; set; }
    public bool main_category { get; set; }
    public bool sub_category { get; set; }
    public List<SentenceDTO> sentences { get; set; }
}
