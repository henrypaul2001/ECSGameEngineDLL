#version 330
 
in vec2 v_TexCoord;
in vec3 v_Normal;
in vec3 v_FragPos;
uniform sampler2D s_texture;
uniform vec3 v_diffuse;	// OBJ NEW

out vec4 Color;
 
void main()
{
/*
	vec3 lightColor = vec3(1,1,1);
	vec4 lightAmbient = vec4(0.1, 0.1, 0.1, 1.0);
	vec3 lightPos = vec3(30,10,-25);

	vec3 norm = normalize(v_Normal);
	vec3 lightDir = normalize(lightPos - v_FragPos); 
	float diff = max(dot(norm, lightDir), 0.0);
	vec3 diffuse = diff * lightColor;

    Color = lightAmbient + (vec4(v_diffuse, 1) * texture2D(s_texture, v_TexCoord) * vec4(diffuse, 0));  // OBJ CHANGED
	*/
	vec3 lightColor1 = vec3(0.5,0.5,0.5);
    vec3 lightColor2 = vec3(0.5,0.5,0.5);
    vec3 lightColor3 = vec3(0.5,0.5,0.5);
    vec3 lightColor4 = vec3(0.5,0.5,0.5);
    vec4 lightAmbient = vec4(0.1, 0.1, 0.1, 1.0);

    vec3 lightPos1 = vec3(60,50,-60);
    vec3 lightPos2 = vec3(60,50,0);
    vec3 lightPos3 = vec3(0,50,0);
    vec3 lightPos4 = vec3(0,50,-60);

    vec3 norm = normalize(v_Normal);
    vec3 lightDir1 = normalize(lightPos1 - v_FragPos);
    vec3 lightDir2 = normalize(lightPos2 - v_FragPos);
    vec3 lightDir3 = normalize(lightPos3 - v_FragPos);
    vec3 lightDir4 = normalize(lightPos4 - v_FragPos); 

    float diff1 = max(dot(norm, lightDir1), 0.0);
    float diff2 = max(dot(norm, lightDir2), 0.0);
    float diff3 = max(dot(norm, lightDir3), 0.0);
    float diff4 = max(dot(norm, lightDir4), 0.0);

    vec3 diffuse1 = diff1 * lightColor1;
    vec3 diffuse2 = diff2 * lightColor2;
    vec3 diffuse3 = diff3 * lightColor3;
    vec3 diffuse4 = diff4 * lightColor4;

    vec3 diffuse = diffuse1 + diffuse2 + diffuse3 + diffuse4;

    Color = lightAmbient + (vec4(v_diffuse, 1) * texture2D(s_texture, v_TexCoord) * vec4(diffuse, 0));
}