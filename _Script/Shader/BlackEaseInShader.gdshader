shader_type canvas_item;
uniform vec4 CanvasColor:source_color=vec4(1.0);
uniform float Opacity:hint_range(0.0, 255, 0.1);

void fragment() {
	vec4 texture_color = texture(TEXTURE, UV);
	vec4 ChangeOpacity=vec4(0,0,0,Opacity);
	texture_color.rgb=mix(texture_color.rgb,CanvasColor.rgb,0.7);
	COLOR=ChangeOpacity;
	// Called for every pixel the material is visible on.
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
