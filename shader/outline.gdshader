shader_type canvas_item;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
	vec2 strength = vec2(15, 0);
	vec2 right_uv = UV + TEXTURE_PIXEL_SIZE * strength;
	vec4 right_color = texture(TEXTURE, right_uv);
	vec2 left_uv = UV - TEXTURE_PIXEL_SIZE * strength;
	vec4 left_color = texture(TEXTURE, left_uv);
	if(right_color.a > 0.9f && COLOR.a < 0.5f){
		COLOR = vec4(0, 0, 0, 1);
	}
	if(left_color.a > 0.9f && COLOR.a < 0.5f){
		COLOR = vec4(0, 0, 0, 1);
	}
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
