Shader "Jundroo/SR Standard/SrStandardObjectShader1" 
{
    Properties
    {
        [Header(PBR Options)]
        _metallicness("Metallicness", Range(0, 1)) = 0
        _smoothness("Smoothness", Range(0, 1)) = 0
        _texture("Texture", 2D) = "white" {}
        
        [Header(Transparency)]
        [Toggle(USE_OPACITY_MAP)] _UseOpacityMap("Use Opacity Map", Float) = 0
        _OpacityMap("Opacity Map (A)", 2D) = "white" {}
        _Opacity("Opacity", Range(0, 1)) = 1

        [Space(20)][Header(Normal Map Options)]
        _normalMap("Normal Map", 2D) = "bump" {}

        [HDR]
        _emissive("Emission", Color) = (0, 0, 0, 0)

        [Space(20)][Header(Color)]
        _colorMultiplier("Color Multiplier", Color) = (1, 1, 1, 1)

        [Header(Specular)]
        _SpecColor("Specular Color", Color) = (1,1,1,1)
        _SpecGloss("Specular Glossiness", Range(0,1)) = 0.5

        [Space(20)][Header(Atmosphere Options)]
        [KeywordEnum(None, LOW, HIGH)] TERRAIN_ATMOSPHERE("Atmosphere Quality", Float) = 0
    }

    SubShader
    {
        Tags { 
            "Queue"="Transparent" 
            "RenderType"="Transparent" 
            "IgnoreProjector"="True" 
        }

        // ForwardBase Pass (主光源)
        Pass 
        {
            Tags { "LightMode" = "ForwardBase" }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #pragma multi_compile __ TERRAIN_STRUCTURE_NORMAL_MAPS_ON 
            #pragma multi_compile __ OBJECT_ATMOSPHERE
            #pragma multi_compile __ UNDERWATER
            #pragma multi_compile SR_LIGHTING_LOW SR_LIGHTING_MEDIUM SR_LIGHTING_HIGH
            #pragma shader_feature USE_OPACITY_MAP

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            // 声明Properties中的变量
            sampler2D _texture;
            float4 _texture_ST;
            sampler2D _OpacityMap;
            float4 _OpacityMap_ST;
            sampler2D _normalMap;
            float4 _normalMap_ST;
            float _metallicness;
            float _smoothness;
            float4 _emissive;
            float4 _colorMultiplier;
            float _Opacity;
            float _SpecGloss;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 opacityUV : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
                float3 worldNormal : TEXCOORD3;
                float3 worldTangent : TEXCOORD4;
                float3 worldBitangent : TEXCOORD5;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _texture);
                o.opacityUV = TRANSFORM_TEX(v.texcoord, _OpacityMap);
                
                // 计算世界空间坐标和法线
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
                o.worldBitangent = cross(o.worldNormal, o.worldTangent) * v.tangent.w;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 采样主纹理和颜色
                fixed4 texColor = tex2D(_texture, i.uv);
                fixed4 finalColor = texColor * _colorMultiplier;

                // 计算透明度
                #ifdef USE_OPACITY_MAP
                    float opacity = tex2D(_OpacityMap, i.opacityUV).a;
                #else
                    float opacity = texColor.a;
                #endif
                finalColor.a = opacity * _Opacity;

                // 采样法线贴图
                float3 normalMap = UnpackNormal(tex2D(_normalMap, i.uv));
                float3x3 TBN = float3x3(i.worldTangent, i.worldBitangent, i.worldNormal);
                float3 worldNormal = normalize(mul(normalMap, TBN));

                // 计算光照
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 halfDir = normalize(lightDir + viewDir);

                // 高光计算 (Blinn-Phong)
                float spec = pow(max(dot(worldNormal, halfDir), 0.0), _SpecGloss * 128);
                float3 specular = _SpecColor.rgb * spec * _LightColor0.rgb;

                // 合并颜色
                finalColor.rgb += specular;
                finalColor.rgb += _emissive.rgb; // 自发光

                return finalColor;
            }
            ENDCG
        }

        // ForwardAdd Pass (附加光源)
        Pass 
        {
            Tags { "LightMode" = "ForwardAdd" }
            Blend One One
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile __ TERRAIN_STRUCTURE_NORMAL_MAPS_ON 
            #pragma multi_compile __ UNDERWATER
            #pragma multi_compile SR_LIGHTING_LOW SR_LIGHTING_MEDIUM SR_LIGHTING_HIGH

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            // 复用ForwardBase中的变量和结构体定义
            sampler2D _texture;
            float4 _texture_ST;
            sampler2D _normalMap;
            float4 _normalMap_ST;
            float _SpecGloss;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 worldNormal : TEXCOORD2;
                LIGHTING_COORDS(3,4) // 阴影坐标
            };

            v2f vert(appdata_full v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _texture);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                TRANSFER_VERTEX_TO_FRAGMENT(o); // 传递阴影数据
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 光照计算（仅附加光源贡献）
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 halfDir = normalize(lightDir + viewDir);

                // 高光计算
                float spec = pow(max(dot(i.worldNormal, halfDir), 0.0), _SpecGloss * 128);
                float3 specular = _SpecColor.rgb * spec * _LightColor0.rgb;

                return fixed4(specular, 1);
            }
            ENDCG
        }
    }
    Fallback "VertexLit"
}