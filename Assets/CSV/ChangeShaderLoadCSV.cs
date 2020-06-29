using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeShaderLoadCSV : MonoBehaviour
{
    public TextAsset data;
    public GameObject currentCountryUIName;
    public GameObject currentWasteAmount;
    public GameObject plasticWasteUIAmount;

    //create blocks in ocean
    public GameObject block;
    public int width = 6;
    public int height =  5;

    public GameObject countryUISelectors;

    //get size of plane
    Collider m_Collider;
    Vector3 m_Size;

    public List<CountryData> cDataList = new List<CountryData>();
    public List<string> nationNames = new List<string>();

    public Material waterColor;
    public int currentValue = 0;
    public float currentRawValue = 0;
    float currentLerpValue = 0;
    float targetLerpValue;

    public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue){
    
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
    
        return(NewValue);
    }
    
    void Start()
    {       
        //m_Size is equal to the size of the plane gameobject attached 
        m_Collider = GetComponent<Collider>();
        m_Size = m_Collider.bounds.size;

        string[] dataRows = data.text.Split(new char[] { '\n' });

        for(int i = 4; i < dataRows.Length - 1; i++) 
        {
            string[] row = dataRows[i].Split(new char[] {';'});
            CountryData cData = new CountryData();
            nationNames.Add(row[0]);
            cData.countryName = row[0];
            cData.economicStatus = row[1];
            float.TryParse(row[2], out cData.coastalPopulation);
            float.TryParse(row[3], out cData.wasteGenerationPerCapita);
            float.TryParse(row[4], out cData.percentPlasticInWasteStream);
            float.TryParse(row[5], out cData.percentInadequatelyManagedWaste);
            float.TryParse(row[6], out cData.percentLitteredWaste);
            float.TryParse(row[7], out cData.wasteGeneratedPerDay);
            float.TryParse(row[8], out cData.plasticWasteGeneration);
            float.TryParse(row[9], out cData.inadequatelyManagedPlasticPerDay);
            float.TryParse(row[10], out cData.plasticWasteLittered);
            float.TryParse(row[11], out cData.mismanagedPlasticeWaste);
            float.TryParse(row[12], out cData.mismanagedPlastWaste2010);
            float.TryParse(row[13], out cData.mismanagedPlastWaste2020);
            float.TryParse(row[14], out cData.perCapitaPlasticWaste);
            cDataList.Add(cData);
        }

        currentCountryUIName.GetComponent<Text>().text = cDataList[0].countryName;
        currentWasteAmount.GetComponent<Text>().text = cDataList[0].wasteGenerationPerCapita.ToString();
        plasticWasteUIAmount.GetComponent<Text>().text = cDataList[0].perCapitaPlasticWaste.ToString();

        // waterColor.SetFloat("_RedValue",cDataList[6].wasteGenerationPerCapita);
        waterColor.SetFloat("_BlueValue", currentValue);
    }

    public List<string> ReturnNames() {
        return nationNames;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            currentValue++;
            targetLerpValue = scale(0, 4, 1.0f, 0.0f, cDataList[currentValue].wasteGenerationPerCapita);
            currentCountryUIName.GetComponent<Text>().text = cDataList[currentValue].countryName;
            currentWasteAmount.GetComponent<Text>().text = cDataList[currentValue].wasteGenerationPerCapita.ToString();
            plasticWasteUIAmount.GetComponent<Text>().text = cDataList[currentValue].perCapitaPlasticWaste.ToString();

            GameObject[] bottles;
 
            bottles = GameObject.FindGameObjectsWithTag("Bottle");
            
            foreach(GameObject bottle in bottles)
            {   
                if(bottle.transform.name == "BottleClone") {
                    Destroy(bottle);
                }
            }

        //render blocks
            // int divider = (int)cDataList[currentValue].perCapitaPlasticWaste;
            float divider = scale(0, 600, 4, 0.05f, cDataList[currentValue].perCapitaPlasticWaste);

            Debug.Log(divider);
            for (float y=0; y<m_Size.x / 5; y+= (divider + Random.Range(-1.0f, 1f)))
            {
                for (float x=0; x<m_Size.z / 5; x+=(divider + Random.Range(-1.0f, 1f)))
                {
                var newObject = (GameObject) Instantiate(block, new Vector3(x,-1,y), Quaternion.Euler(new Vector3(-20, Random.Range(0.0f, 350.0f), 0)));
                newObject.name = "BottleClone";
                // newObject.transform.rotation = new Vector3(-30, Random.Range(0.0f, 350.0f), -1);
                }
            } 

        }

        currentLerpValue = Mathf.Lerp(currentLerpValue, targetLerpValue, 0.05f);
        waterColor.SetFloat("_LerpValue", currentLerpValue);

        currentRawValue = cDataList[currentValue].wasteGenerationPerCapita;
    }
}
