using Godot;
using Godot.Collections;


public partial class GlobalPlayerStats : Node,ISaveable
{
	public static GlobalPlayerStats Instance;

  public int PlayerLevel;
  public int PlayerStrength;
  public int PlayerVitality;
  public int PlayerIntelligence;
  public int PlayerDexterity;
  public int PlayerExp;
  public int PlayerMoney;

  //TODO load player save scene
  //TODO load player stats from global
  //TODO Save player stats from global
    public override void _Ready(){
		  Instance=this;
      GlobalSaveManager.Instance.LoadSave();
    }

    public void FromDict(Dictionary<string, Variant> data){
        PlayerLevel = (int)data["PlayerLevel"];
        PlayerStrength = (int)data["PlayerStrength"];
        PlayerVitality = (int)data["PlayerVitality"];
        PlayerIntelligence = (int)data["PlayerIntelligence"];
        PlayerDexterity=(int)data["PlayerDexterity"];
        PlayerExp=(int)data["PlayerExp"];
        PlayerMoney=(int)data["PlayerMoney"];

    }

    public string Save(){
      var saveData=new Dictionary<string, Variant>(){
        {"PlayerLevel",PlayerLevel},
        {"PlayerStrength",PlayerStrength},
        {"PlayerVitality", PlayerVitality},
        {"PlayerIntelligence", PlayerIntelligence},
        {"PlayerDexterity",PlayerDexterity},
        {"PlayerExp",PlayerExp},
        {"PlayerMoney",PlayerMoney},
      };

      return Json.Stringify(new Dictionary<string, Variant>(){
        {this.Name,saveData}
      });
    }

    public void Load(Variant variant){
      var data=new Dictionary<string, Variant>((Dictionary)variant);
      FromDict(data);
    }

    public string NewSave()
    {
      var newSave=new Dictionary<string, Variant>(){
        {"PlayerLevel",1},
        {"PlayerStrength",10},
        {"PlayerVitality",10},
        {"PlayerIntelligence",10},
        {"PlayerDexterity",10},
        {"PlayerExp",0},
        {"PlayerMoney",0},
      };
      return Json.Stringify(new Dictionary<string, Variant>(){
        {this.Name,newSave}
      });
    }
}
