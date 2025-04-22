using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;
    public Web Web;
    public Login loginScript;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this; 
        Web = GetComponent<Web>();
        loginScript = GetComponent<Login>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
