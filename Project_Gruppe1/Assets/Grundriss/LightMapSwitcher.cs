using UnityEngine;
using System.Collections;
using System.Linq;

public class LightMapSwitcher : MonoBehaviour
{

	private Object[] Day = new Texture2D[14];
	private Object[] Night = new Texture2D[14];

	private Texture2D[] DayNear = new Texture2D[7];
	private Texture2D[] DayFar = new Texture2D[7];
	private Texture2D[] NightNear = new Texture2D[7];
	private Texture2D[] NightFar = new Texture2D[7];

	private LightmapData[] dayLightMaps;
	private LightmapData[] nightLightMaps;
	
	void Start ()
	{
		if ((DayNear.Length != DayFar.Length) || (NightNear.Length != NightFar.Length))
		{
			Debug.Log("In order for LightMapSwitcher to work, the Near and Far LightMap lists must be of equal length");
			return;
		}

		int x = 0, z = 0;
		Day = Resources.LoadAll("Lightmaps_Day");
		foreach (Object item in Day) {
			if (item.name.Contains("_dir")){
				DayNear[x] = item as Texture2D;
				x++;
			} else if (item.name.Contains("_light")){
				DayFar[z] = item as Texture2D;
				z++;
			}
		}

		x = 0;
		z = 0;
		Night = Resources.LoadAll("Lightmaps_Night");
		foreach (Object item in Night) {
			if (item.name.Contains("_dir")){
				NightNear[x] = item as Texture2D;
				x++;
			} else if (item.name.Contains("_light")){
				NightFar[z] = item as Texture2D;
				z++;
			}
		}

		//DayNear = Resources.LoadAll<Texture2D>("Lightmaps_Day/Dir");
		//DayFar = Resources.LoadAll<Texture2D>("Lightmaps_Day/Light");
		//NightNear = Resources.LoadAll<Texture2D>("GLightmaps_Night/Dir");
		//NightFar =  Resources.LoadAll<Texture2D>("Lightmaps_Night/Light");
		
		// Sort the Day and Night arrays in numerical order, so you can just blindly drag and drop them into the inspector
		DayNear = DayNear.OrderBy(t2d => t2d.name, new NaturalSortComparer<string>()).ToArray();
		DayFar = DayFar.OrderBy(t2d => t2d.name, new NaturalSortComparer<string>()).ToArray();
		NightNear = NightNear.OrderBy(t2d => t2d.name, new NaturalSortComparer<string>()).ToArray();
		NightFar = NightFar.OrderBy(t2d => t2d.name, new NaturalSortComparer<string>()).ToArray();
		
		// Put them in a LightMapData structure
		dayLightMaps = new LightmapData[DayNear.Length];
		for (int i=0; i<DayNear.Length; i++)
		{
			dayLightMaps[i] = new LightmapData();
			dayLightMaps[i].lightmapNear = DayNear[i];
			dayLightMaps[i].lightmapFar = DayFar[i];
		}
		
		nightLightMaps = new LightmapData[NightNear.Length];
		for (int i=0; i<NightNear.Length; i++)
		{
			nightLightMaps[i] = new LightmapData();
			nightLightMaps[i].lightmapNear = NightNear[i];
			nightLightMaps[i].lightmapFar = NightFar[i];
		}

		SetToDay ();
	}
	
	#region Publics
	public void SetToDay()
	{
		LightmapSettings.lightmaps = dayLightMaps;
	}
	
	public void SetToNight()
	{
		LightmapSettings.lightmaps = nightLightMaps;
	}
	#endregion
	
	#region Debug
	[ContextMenu ("Set to Night")]
	void Debug00()
	{
		SetToNight();
	}
	
	[ContextMenu ("Set to Day")]
	void Debug01()
	{
		SetToDay();
	}
	#endregion
}
