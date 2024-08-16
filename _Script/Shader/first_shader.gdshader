shader_type canvas_item;

uniform vec4 flash_color:source_color=vec4(1.0);
uniform float flash_modifier:hint_range(0.0, 1.0, 0.1)=1;


void fragment() {
	vec4 color=texture(TEXTURE,UV);
	color.rgb=mix(color.rgb,flash_color.rgb,0.6 + 0.2 *sin(TIME * 15.0));
	COLOR=color;
}
