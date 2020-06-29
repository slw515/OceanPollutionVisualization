using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCVFile : MonoBehaviour
{
    public TextAsset data;
    public List<CountryData> cDataList = new List<CountryData>();
    public Material waterColor;
    public int currentValue = 2;
    public int currentValueTwo = 2;

    void Start()
    {
        string[] dataRows = data.text.Split(new char[] { '\n' });
        Debug.Log(dataRows[dataRows.Length - 1]);

        for(int i = 4; i < dataRows.Length - 1; i++) 
        {
            string[] row = dataRows[i].Split(new char[] {';'});
            CountryData cData = new CountryData();

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

            cDataList.Add(cData);
        }
        foreach (CountryData cData in cDataList){
            Debug.Log(cData.countryName);
        }

        // waterColor.SetFloat("_RedValue",cDataList[6].wasteGenerationPerCapita);
        waterColor.SetFloat("_RedValue", currentValue);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            currentValue++;
            // waterColor.SetFloat("_RedValue",cDataList[currentValue].wasteGenerationPerCapita);
        }
        waterColor.SetFloat("_RedValue",currentValue);
        Debug.Log("HEllo1111");

    }
}
