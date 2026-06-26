shader_type canvas_item;

void vertex() {
	// Called for every vertex the material is visible on.
	if (VERTEX.y < 0.5) {
		if (VERTEX.x < 0.5){
			VERTEX.x = -1080.0;
		}else{
			VERTEX.x = 1080.0;
		}
	}else{
		if (VERTEX.x < 0.5){
			VERTEX.x = -1920.0;
		}else{
			VERTEX.x = 1920.0;
		}
	}
}

void fragment() {
	// Called for every pixel the material is visible on.
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
