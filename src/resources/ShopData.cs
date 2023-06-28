using Godot;

public partial class ShopData : Resource {
	[Signal] public delegate void UpdateEventHandler();
	[Export] double StartingPrice = 100;
	[Export] double PriceGrowthLinear;
	[Export] double PriceGrowthExponential;
	[Export] double StartingValue = 0;
	[Export] double ValueGrowthLinear;
	[Export] double ValueGrowthExponential;
	[Export(PropertyHint.Range, "1,32768,1")] int MaxLevel;
	int Level = 0;
	int CachedLevel = 0;
	int CachedPrice;
	double CachedValue;

	public int GetPrice() {
		if (CachedLevel == Level) return CachedPrice;
		int returnValue = (int)(Mathf.Pow(Level+1, PriceGrowthExponential)*PriceGrowthLinear+StartingPrice);
		CachedLevel = Level;
		CachedPrice = returnValue;
		return returnValue;
	}

	public double GetValue() {
		if (CachedLevel == Level) return CachedPrice;
		int returnValue =  (int)(Mathf.Pow(Level, ValueGrowthExponential)*ValueGrowthLinear+StartingValue);
		CachedLevel = Level;
		CachedValue = returnValue;
		return returnValue;
	}
}