shader_type canvas_item;
uniform vec4 NotSpeakingColor:source_color=vec4(1.0);

void fragment() {
	vec4 texture_color = texture(TEXTURE, UV);
	texture_color.rgb=mix(texture_color.rgb,NotSpeakingColor.rgb,0.7);
	COLOR=texture_color;
	// Called for every pixel the material is visible on.
}

