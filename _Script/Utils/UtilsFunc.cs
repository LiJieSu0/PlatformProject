using System.Reflection.Metadata.Ecma335;
using Godot;
public static class UtilsFunc{
    public static Vector2 DesiredSpriteScale(Vector2 orgin,Vector2 desiredSize){
        Vector2 scale=desiredSize/orgin;
        return scale;
    }
}