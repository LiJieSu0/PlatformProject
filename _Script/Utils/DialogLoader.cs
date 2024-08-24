
using Godot;
using Godot.Collections;



public partial class DialogLoader:Node{
    private string sceneName;
    private string currKey;
    private Dictionary<string, Variant> CharDialogs;
    private Dictionary<string, Variant> _currSectionDict;    
    private string filePath="res://_Asset/Database/output_file.json"; //TODO change file path dynamically
    public DialogLoader(){
        LoadAllDialog();
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
    public Array<string> GetCharNames(){
        var charNames=new Array<string>((string[])_currSectionDict["CharNames"]);
        return charNames;
    }
    public Array<string> GetLines(){
        var lines=new Array<string>((string[])_currSectionDict["Lines"]);
        return lines;
    }
    public Array<Array<string>> GetOptions(){
        Array<Array<string>> res= (Array<Array<string>>)_currSectionDict["Options"];
        return res;
    }
    public Array<Array<string>> GetResults(){
        Array<Array<string>> res= (Array<Array<string>>)_currSectionDict["Results"];
        return res;
    }
    public Array<string> GetFinalResults(){
        Array<string>res= (Array<string>)_currSectionDict["FinalResults"];
        return res;
    }
    public void SetLinesKey(string key){
        this.currKey=key;
        _currSectionDict=(Dictionary<string,Variant>)CharDialogs[currKey];
    } 

}