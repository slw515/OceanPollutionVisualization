using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    // [SerializeField]
    // private int[] intArray;

    [SerializeField]
    GameObject csvDataObject;
    // private string[] countryNames;
    private List<string> countryNames = new List<string>();

    
    // public countryNames;

    void Awake() {
        csvDataObject = GameObject.FindGameObjectWithTag("DataSource");
    }

    void Start() {
        countryNames = csvDataObject.GetComponent<ChangeShaderLoadCSV>().ReturnNames();
        Invoke("addCountries", 0.3f);

    }

    void addCountries() {
            for (int i = 0; i <= countryNames.Count; i++) {
                GameObject button = Instantiate(buttonTemplate) as GameObject;
                button.SetActive(true);
                button.name = countryNames[i];
                button.GetComponent<ButtonListButton>().SetText(countryNames[i]);

                button.transform.SetParent(buttonTemplate.transform.parent, false);
            }
    }

    public void ButtonClicked(string myTextString) {
        Debug.Log(myTextString);
    }

    void Update() {
    }
}
