
using System;
using Godot;

public interface ISaveable{
    string Save();
    void Load(Variant variant);
    string NewSave();

}