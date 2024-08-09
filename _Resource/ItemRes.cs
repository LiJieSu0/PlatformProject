using Godot;
using System;

[GlobalClass]
public partial class ItemRes : Resource{
    [Export] public string ItemName="";
    [Export] public string ItemSpritePath="";
    [Export] public int ItemNo=0;
}
