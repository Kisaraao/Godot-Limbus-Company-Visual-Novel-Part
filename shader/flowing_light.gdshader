shader_type canvas_item;

uniform sampler2D light;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	float speed = 0.25;
	vec2 light_uv = UV;
	
	light_uv.y = fract(light_uv.y + TIME * speed);
	
	vec4 gold_color = vec4(1.0, 0.6, 0.0, 0.5);
	//vec4 gold_color = vec4(0.75, 0.75, 1.0, 0.5);
	vec4 light_color = texture(light, light_uv);
	vec4 res = light_color;
	float glow = res.r;
	glow = smoothstep(0.0, 0.35, glow);
	res = glow * gold_color * 1.75;
	COLOR = res * COLOR.a;
}
