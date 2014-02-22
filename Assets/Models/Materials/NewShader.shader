Shader "Custom/NewShader" {
	SubShader {
		UsePass "VertexLit/SHADOWCOLLECTOR"
		UsePass "VertexLit/SHADOWCASTER"
	} 
	FallBack off
}
