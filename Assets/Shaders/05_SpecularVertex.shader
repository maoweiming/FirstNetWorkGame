﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader"SSM/05_SpecularVertex"{

	Properties{

		_DiffuseColor("Color",Color) = (1,1,1,1)
		_Gloss("Gloss",Range(8,200)) = 10

	}

	SubShader{

		pass{

		Tags{"LightMode" = "ForwardBase"}

		CGPROGRAM

		#include "Lighting.cginc"

		#pragma vertex vert
		#pragma fragment frag

		fixed4 _DiffuseColor;
		half _Gloss;

		struct a2v{
			float4 vertex:POSITION;
			float3 normal:NORMAL;
		};

		struct v2f{
			float4 position : SV_POSITION;
			fixed3 color : COLOR;
		};
	
		v2f vert(a2v v){
			v2f f;

			f.position = UnityObjectToClipPos(v.vertex);

			fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb;

			float3 normalDir = normalize( mul(v.normal , (float3x3) unity_WorldToObject));

			fixed3 lightDir = normalize( _WorldSpaceLightPos0.xyz );

			fixed3 diffuse = _LightColor0.rgb * max( dot( normalDir, lightDir ), 0 ) * _DiffuseColor.rgb;

			fixed3 reflectDir = normalize( reflect(-lightDir, normalDir) );

			fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - mul( v.vertex , unity_WorldToObject ).xyz);

			fixed3 specular = _LightColor0.rgb * pow( max( dot( reflectDir, viewDir ) , 0 ), _Gloss);

			f.color = diffuse + ambient + specular;

			return f;
		}

		fixed4 frag(v2f f):SV_TARGET{

			return fixed4(f.color,1);
		}


		ENDCG
		}
	}

	Fallback"Diffuse"
}