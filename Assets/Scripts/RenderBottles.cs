using UnityEngine;
public class RenderBottles : MonoBehaviour
{
   public GameObject block;
   public int width = 10;
   public int height = 4;
  
   void Start()
   {
       for (int y=0; y<height; ++y)
       {
           for (int x=0; x<width; ++x)
           {
               Instantiate(block, new Vector3(x,0,y), Quaternion.identity);
           }
       }       
   }
}