
using Godot;
using Godot.Collections;


public partial class DialogLoader:Node{
    private string charName;
    private string sceneName;
    public  string currKey;
    private Dictionary<string, Variant> CharDialogs;
    private string filePath="res://_Asset/Database/output_file.json";

    public DialogLoader(){
    }

    public void LoadAllDialog(){
        if(!FileAccess.FileExists(filePath)){
            GD.Print("File doesn't exist");
        }
        var json=new Json();
        using var dataLoader = FileAccess.Open(filePath, FileAccess.ModeFlags.Read); 
        var jsonData=Json.ParseString(dataLoader.GetAsText());
        CharDialogs=new Dictionary<string, Variant>((Dictionary)jsonData); //Load whole data from sheet

    } 
    public string[] GetLines(){
        var tmp=(Dictionary<string,string[]>)CharDialogs[currKey];
        var lines=tmp["Lines"];
        return lines;
    }
    public string[] GetOptions(){
        var tmp=(Dictionary<string,string[]>)CharDialogs[currKey];
        var options=tmp["Options"];
        return options;
    }
    public string[] GetResults(){
        var tmp=(Dictionary<string,string[]>)CharDialogs[currKey];
        var res=tmp["Results"];
        GD.Print("Results "+res);
        return res;
    }
    public void SetLinesKey(string key){
        this.currKey=key;
    } 

}