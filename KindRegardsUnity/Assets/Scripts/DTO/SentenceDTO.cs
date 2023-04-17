using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentenceDTO : MonoBehaviour
{
    public int Id { get; set; }
    public string begin_text { get; set; }
    public string end_text { get; set; }
    public bool variable { get; set; }
    public bool category { get; set; }
    public CategoryDTO categoryID { get; set; }
    public List<VariableDTO> variables { get; set; }
}
