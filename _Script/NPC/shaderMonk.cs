using Godot;
using System;

public partial class shaderMonk : Node2D
{
	ShaderMaterial shaderMaterial;
	bool isIncrease=true;
	float currValue=0.4f;
	public override void _Ready(){
		shaderMaterial=(ShaderMaterial)GetNode<Sprite2D>("Sprite2D").Material;
		shaderMaterial.SetShaderParameter("flash_modifier",currValue);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		if(isIncrease){
			currValue+=(float)delta*0.5f;
			if(currValue>=0.8)
				isIncrease=false;
		}
		else{
			currValue-=(float)delta*0.5f;
			if(currValue<=0.4)
				isIncrease=true;
		}
		shaderMaterial.SetShaderParameter("flash_modifier",currValue);

	}
}
