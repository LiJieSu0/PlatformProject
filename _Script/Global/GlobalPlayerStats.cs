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
  public int PlayerLuck;
  public int PlayerCurrAccumExp;
  public int PlayerMoney;
  public int PlayerUpgradePoints;
  public float PlayerMaxHp;
  public float PlayerCurrHp;
  public float PlayerMaxMp;
  public float PlayerCurrMp;

  //TODO add luck

  //TODO load player save scene
  //TODO load player stats from global
  //TODO Save player stats from global
    public override void _Ready(){
		  Instance=this;
      GlobalSaveManager.Instance.LoadSave();
    }



    public string Save(){
      var saveData=new Dictionary<string, Variant>(){
        {"PlayerLevel",PlayerLevel},
        {"PlayerStrength",PlayerStrength},
        {"PlayerVitality", PlayerVitality},
        {"PlayerIntelligence", PlayerIntelligence},
        {"PlayerDexterity",PlayerDexterity},
        {"PlayerLuck",PlayerLuck},
        {"PlayerCurrAccumExp",PlayerCurrAccumExp},
        {"PlayerMoney",PlayerMoney},
        {"PlayerUpgradePoints",PlayerUpgradePoints},
        {"PlayerMaxHp",PlayerMaxHp},
        {"PlayerMaxMp",PlayerMaxMp},
        {"PlayerCurrHp",PlayerCurrHp},
        {"PlayerCurrMp",PlayerCurrMp},
      };

      return Json.Stringify(new Dictionary<string, Variant>(){
        {this.Name,saveData}
      });
    }

    public void Load(Variant variant){
      var data=new Dictionary<string, Variant>((Dictionary)variant);
      FromDict(data);
    }
    public void FromDict(Dictionary<string, Variant> data){
        PlayerLevel = (int)data["PlayerLevel"];
        PlayerStrength = (int)data["PlayerStrength"];
        PlayerVitality = (int)data["PlayerVitality"];
        PlayerIntelligence = (int)data["PlayerIntelligence"];
        PlayerDexterity=(int)data["PlayerDexterity"];
        PlayerLuck=(int)data["PlayerLuck"];
        PlayerCurrAccumExp=(int)data["PlayerCurrAccumExp"];
        PlayerMoney=(int)data["PlayerMoney"];
        PlayerUpgradePoints=(int)data["PlayerUpgradePoints"];
        PlayerMaxHp=(float)data["PlayerMaxHp"];
        PlayerMaxMp=(float)data["PlayerMaxMp"];
        PlayerCurrHp=(float)data["PlayerCurrHp"];
        PlayerCurrMp=(float)data["PlayerCurrMp"];
    }
    public string NewSave()
    {
      var newSave=new Dictionary<string, Variant>(){
        {"PlayerLevel",1},
        {"PlayerStrength",10},
        {"PlayerVitality",10},
        {"PlayerIntelligence",10},
        {"PlayerDexterity",10},
        {"PlayerLuck",10},
        {"PlayerCurrAccumExp",0},
        {"PlayerMoney",0},
        {"PlayerUpgradePoints",5},
        {"PlayerMaxHp",100}, //TODO read all data from data sheet
        {"PlayerMaxMp",100},
        {"PlayerCurrHp",100},
        {"PlayerCurrMp",100},
      };
      return Json.Stringify(new Dictionary<string, Variant>(){
        {this.Name,newSave}
      });
    }


}
