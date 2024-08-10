using Godot;
using System;

[GlobalClass]
public partial class DropItemRes : Resource{
    [Export] public string ItemName="";
    [Export] public string ItemSpritePath="";
}
