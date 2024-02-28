using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.IO;

[CreateAssetMenu(fileName = "ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
   public List<Sprite> ItemIconLibrary = new List<Sprite>();

   [TextArea] public List<string> ItemNamesLibrary;

   [TextArea] public string fileName;
   
   [TextArea] public List<string> ItemCommonDescriptionLibray = new List<string>();

   [TextArea] public List<string> ItemUnCommonDescriptionLibray = new List<string>();
   
   [TextArea] public List<string> ItemRareDescriptionLibray = new List<string>();
   
   [TextArea] public List<string> ItemEpicDescriptionLibray = new List<string>();

   public Dictionary<Rarity, List<string>> itemDescriptionMapping;

   public void ReadFromTheFile()
   {
      itemDescriptionMapping = new Dictionary<Rarity, List<string>>
      {
         { Rarity.Common , ItemCommonDescriptionLibray},
         { Rarity.Uncommon, ItemUnCommonDescriptionLibray },
         { Rarity.Rare  , ItemRareDescriptionLibray} ,
         { Rarity.Epic , ItemEpicDescriptionLibray}
         
      };
      
      var file = Resources.Load<TextAsset>("Text" + "/" + fileName);
      
      var content = file.text;
      
      var AllWords = content.Split("\n");
      
      //Debug.Log(AllWords.Length);
      
      for (int i = 0; i < AllWords.Length; i++)
      {
         //Debug.Log(AllWords[i]);
      }
      
      ItemNamesLibrary = new List<string>(AllWords);

      if (ItemNamesLibrary == null)
      {
         Debug.Log("No file");
      }
      
      
      
      
      
      
      
   }
   
}
