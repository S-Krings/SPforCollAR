Shader "Unlit/OutlineTest"
{
    Properties
    {
        _OutlineWidth("OutlineWidth", Float) = 0.1
        [HideInInspector]_BUILTIN_Surface("Float", Float) = 0
        [HideInInspector]_BUILTIN_Blend("Float", Float) = 0
        [HideInInspector]_BUILTIN_AlphaClip("Float", Float) = 1
        [HideInInspector]_BUILTIN_SrcBlend("Float", Float) = 1
        [HideInInspector]_BUILTIN_DstBlend("Float", Float) = 0
        [HideInInspector]_BUILTIN_ZWrite("Float", Float) = 1
        [HideInInspector]_BUILTIN_ZWriteControl("Float", Float) = 0
        [HideInInspector]_BUILTIN_ZTest("Float", Float) = 4
        [HideInInspector]_BUILTIN_CullMode("Float", Float) = 1
        [HideInInspector]_BUILTIN_QueueOffset("Float", Float) = 0
        [HideInInspector]_BUILTIN_QueueControl("Float", Float) = -1
    }
        SubShader
    {
        Tags
        {
            // RenderPipeline: <None>
            "RenderType" = "Opaque"
            "BuiltInMaterialType" = "Unlit"
            "Queue" = "AlphaTest"
            "ShaderGraphShader" = "true"
            "ShaderGraphTargetId" = "BuiltInUnlitSubTarget"
        }
        Pass
        {
            Name "Pass"
            Tags
            {
                "LightMode" = "ForwardBase"
            }

        // Render State
        Cull[_BUILTIN_CullMode]
        Blend[_BUILTIN_SrcBlend][_BUILTIN_DstBlend]
        ZTest[_BUILTIN_ZTest]
        ZWrite[_BUILTIN_ZWrite]

        // Debug
        // <None>

        // --------------------------------------------------
        // Pass

        HLSLPROGRAM

        // Pragmas
        #pragma target 3.0
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma multi_compile_fwdbase
        #pragma vertex vert
        #pragma fragment frag

        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>

        // Keywords
        #pragma shader_feature_local_fragment _ _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local_fragment _ _BUILTIN_ALPHAPREMULTIPLY_ON
        #pragma shader_feature_local_fragment _ _BUILTIN_AlphaClip
        #pragma shader_feature_local_fragment _ _BUILTIN_ALPHATEST_ON
        // GraphKeywords: <None>

        // Defines
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_UNLIT
        #define BUILTIN_TARGET_API 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        #ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
        #endif
        #ifdef _BUILTIN_ALPHATEST_ON
        #define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
        #endif
        #ifdef _BUILTIN_AlphaClip
        #define _AlphaClip _BUILTIN_AlphaClip
        #endif
        #ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
        #define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
        #endif


        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */

        // Includes
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"

        // --------------------------------------------------
        // Structs and Packing

        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */

        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

        PackedVaryings PackVaryings(Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

        Varyings UnpackVaryings(PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }


        // --------------------------------------------------
        // Graph

        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _OutlineWidth;
        CBUFFER_END

            // Object and Global properties

            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif

        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif

        // Graph Includes
        // GraphIncludes: <None>

        // Graph Functions

        void Unity_Normalize_float3(float3 In, out float3 Out)
        {
            Out = normalize(In);
        }

        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }

        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }

        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */

        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            float3 _Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1;
            Unity_Normalize_float3(IN.ObjectSpaceNormal, _Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1);
            float _Property_f641f4b058544303adc0c5c149c29cb1_Out_0 = _OutlineWidth;
            float3 _Multiply_80385af780db41288b04d7db77732fa2_Out_2;
            Unity_Multiply_float3_float3(_Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1, (_Property_f641f4b058544303adc0c5c149c29cb1_Out_0.xxx), _Multiply_80385af780db41288b04d7db77732fa2_Out_2);
            float3 _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2;
            Unity_Add_float3(_Multiply_80385af780db41288b04d7db77732fa2_Out_2, IN.ObjectSpacePosition, _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2);
            description.Position = _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif

        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float Alpha;
            float AlphaClipThreshold;
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            surface.BaseColor = IsGammaSpace() ? float3(0.990566, 0.7952418, 0.006229852) : SRGBToLinear(float3(0.990566, 0.7952418, 0.006229852));
            surface.Alpha = 1;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }

        // --------------------------------------------------
        // Build Graph Inputs

        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal = input.normalOS;
            output.ObjectSpaceTangent = input.tangentOS.xyz;
            output.ObjectSpacePosition = input.positionOS;

            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);







        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

                return output;
        }

        void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
        {
            result.vertex = float4(attributes.positionOS, 1);
            result.tangent = attributes.tangentOS;
            result.normal = attributes.normalOS;
            result.vertex = float4(vertexDescription.Position, 1);
            result.normal = vertexDescription.Normal;
            result.tangent = float4(vertexDescription.Tangent, 0);
            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
        }

        void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
        {
            result.pos = varyings.positionCS;
            // World Tangent isn't an available input on v2f_surf


            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogCoord = varyings.fogFactorAndVertexLight.x;
                COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
            #endif

            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
        }

        void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
        {
            result.positionCS = surfVertex.pos;
            // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
            // World Tangent isn't an available input on v2f_surf

            #if UNITY_ANY_INSTANCING_ENABLED
            #endif
            #if !defined(LIGHTMAP_ON)
            #if UNITY_SHOULD_SAMPLE_SH
            #endif
            #endif
            #if defined(LIGHTMAP_ON)
            #endif
            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
            #endif

            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
        }

        // --------------------------------------------------
        // Main

        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/UnlitPass.hlsl"

        ENDHLSL
        }
        Pass
        {
            Name "DepthOnly"
            Tags
            {
                "LightMode" = "DepthOnly"
            }

            // Render State
            Cull[_BUILTIN_CullMode]
            Blend[_BUILTIN_SrcBlend][_BUILTIN_DstBlend]
            ZTest LEqual
            ZWrite On
            ColorMask 0

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 3.0
            #pragma multi_compile_instancing
            #pragma vertex vert
            #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            #pragma shader_feature_local_fragment _ _BUILTIN_AlphaClip
            #pragma shader_feature_local_fragment _ _BUILTIN_ALPHATEST_ON
            // GraphKeywords: <None>

            // Defines
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_DEPTHONLY
            #define BUILTIN_TARGET_API 1
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
            #ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
            #define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
            #endif
            #ifdef _BUILTIN_ALPHATEST_ON
            #define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
            #endif
            #ifdef _BUILTIN_AlphaClip
            #define _AlphaClip _BUILTIN_AlphaClip
            #endif
            #ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
            #define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
            #endif


            // custom interpolator pre-include
            /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */

            // Includes
            #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
            #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            // custom interpolators pre packing
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */

            struct Attributes
            {
                 float3 positionOS : POSITION;
                 float3 normalOS : NORMAL;
                 float4 tangentOS : TANGENT;
                #if UNITY_ANY_INSTANCING_ENABLED
                 uint instanceID : INSTANCEID_SEMANTIC;
                #endif
            };
            struct Varyings
            {
                 float4 positionCS : SV_POSITION;
                #if UNITY_ANY_INSTANCING_ENABLED
                 uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                 uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                 uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                 FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };
            struct SurfaceDescriptionInputs
            {
            };
            struct VertexDescriptionInputs
            {
                 float3 ObjectSpaceNormal;
                 float3 ObjectSpaceTangent;
                 float3 ObjectSpacePosition;
            };
            struct PackedVaryings
            {
                 float4 positionCS : SV_POSITION;
                #if UNITY_ANY_INSTANCING_ENABLED
                 uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                 uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                 uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                 FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };

            PackedVaryings PackVaryings(Varyings input)
            {
                PackedVaryings output;
                ZERO_INITIALIZE(PackedVaryings, output);
                output.positionCS = input.positionCS;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }

            Varyings UnpackVaryings(PackedVaryings input)
            {
                Varyings output;
                output.positionCS = input.positionCS;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }


            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
            float _OutlineWidth;
            CBUFFER_END

                // Object and Global properties

                // -- Property used by ScenePickingPass
                #ifdef SCENEPICKINGPASS
                float4 _SelectionID;
                #endif

            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif

            // Graph Includes
            // GraphIncludes: <None>

            // Graph Functions

            void Unity_Normalize_float3(float3 In, out float3 Out)
            {
                Out = normalize(In);
            }

            void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
            {
                Out = A * B;
            }

            void Unity_Add_float3(float3 A, float3 B, out float3 Out)
            {
                Out = A + B;
            }

            // Custom interpolators pre vertex
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */

            // Graph Vertex
            struct VertexDescription
            {
                float3 Position;
                float3 Normal;
                float3 Tangent;
            };

            VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
            {
                VertexDescription description = (VertexDescription)0;
                float3 _Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1;
                Unity_Normalize_float3(IN.ObjectSpaceNormal, _Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1);
                float _Property_f641f4b058544303adc0c5c149c29cb1_Out_0 = _OutlineWidth;
                float3 _Multiply_80385af780db41288b04d7db77732fa2_Out_2;
                Unity_Multiply_float3_float3(_Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1, (_Property_f641f4b058544303adc0c5c149c29cb1_Out_0.xxx), _Multiply_80385af780db41288b04d7db77732fa2_Out_2);
                float3 _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2;
                Unity_Add_float3(_Multiply_80385af780db41288b04d7db77732fa2_Out_2, IN.ObjectSpacePosition, _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2);
                description.Position = _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2;
                description.Normal = IN.ObjectSpaceNormal;
                description.Tangent = IN.ObjectSpaceTangent;
                return description;
            }

            // Custom interpolators, pre surface
            #ifdef FEATURES_GRAPH_VERTEX
            Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
            {
            return output;
            }
            #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
            #endif

            // Graph Pixel
            struct SurfaceDescription
            {
                float Alpha;
                float AlphaClipThreshold;
            };

            SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
            {
                SurfaceDescription surface = (SurfaceDescription)0;
                surface.Alpha = 1;
                surface.AlphaClipThreshold = 0.5;
                return surface;
            }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
            {
                VertexDescriptionInputs output;
                ZERO_INITIALIZE(VertexDescriptionInputs, output);

                output.ObjectSpaceNormal = input.normalOS;
                output.ObjectSpaceTangent = input.tangentOS.xyz;
                output.ObjectSpacePosition = input.positionOS;

                return output;
            }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
            {
                SurfaceDescriptionInputs output;
                ZERO_INITIALIZE(SurfaceDescriptionInputs, output);







            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
            #else
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
            #endif
            #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

                    return output;
            }

            void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
            {
                result.vertex = float4(attributes.positionOS, 1);
                result.tangent = attributes.tangentOS;
                result.normal = attributes.normalOS;
                result.vertex = float4(vertexDescription.Position, 1);
                result.normal = vertexDescription.Normal;
                result.tangent = float4(vertexDescription.Tangent, 0);
                #if UNITY_ANY_INSTANCING_ENABLED
                #endif
            }

            void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
            {
                result.pos = varyings.positionCS;
                // World Tangent isn't an available input on v2f_surf


                #if UNITY_ANY_INSTANCING_ENABLED
                #endif
                #if !defined(LIGHTMAP_ON)
                #if UNITY_SHOULD_SAMPLE_SH
                #endif
                #endif
                #if defined(LIGHTMAP_ON)
                #endif
                #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                    result.fogCoord = varyings.fogFactorAndVertexLight.x;
                    COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
                #endif

                DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
            }

            void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
            {
                result.positionCS = surfVertex.pos;
                // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
                // World Tangent isn't an available input on v2f_surf

                #if UNITY_ANY_INSTANCING_ENABLED
                #endif
                #if !defined(LIGHTMAP_ON)
                #if UNITY_SHOULD_SAMPLE_SH
                #endif
                #endif
                #if defined(LIGHTMAP_ON)
                #endif
                #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                    result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                    COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
                #endif

                DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
            }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
            #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/DepthOnlyPass.hlsl"

            ENDHLSL
            }
            Pass
            {
                Name "ShadowCaster"
                Tags
                {
                    "LightMode" = "ShadowCaster"
                }

                // Render State
                Cull[_BUILTIN_CullMode]
                Blend[_BUILTIN_SrcBlend][_BUILTIN_DstBlend]
                ZTest LEqual
                ZWrite On
                ColorMask 0

                // Debug
                // <None>

                // --------------------------------------------------
                // Pass

                HLSLPROGRAM

                // Pragmas
                #pragma target 3.0
                #pragma multi_compile_shadowcaster
                #pragma vertex vert
                #pragma fragment frag

                // DotsInstancingOptions: <None>
                // HybridV1InjectedBuiltinProperties: <None>

                // Keywords
                #pragma multi_compile _ _CASTING_PUNCTUAL_LIGHT_SHADOW
                #pragma shader_feature_local_fragment _ _BUILTIN_SURFACE_TYPE_TRANSPARENT
                #pragma shader_feature_local_fragment _ _BUILTIN_AlphaClip
                #pragma shader_feature_local_fragment _ _BUILTIN_ALPHATEST_ON
                // GraphKeywords: <None>

                // Defines
                #define ATTRIBUTES_NEED_NORMAL
                #define ATTRIBUTES_NEED_TANGENT
                #define FEATURES_GRAPH_VERTEX
                /* WARNING: $splice Could not find named fragment 'PassInstancing' */
                #define SHADERPASS SHADERPASS_SHADOWCASTER
                #define BUILTIN_TARGET_API 1
                /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
                #ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
                #define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
                #endif
                #ifdef _BUILTIN_ALPHATEST_ON
                #define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
                #endif
                #ifdef _BUILTIN_AlphaClip
                #define _AlphaClip _BUILTIN_AlphaClip
                #endif
                #ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
                #define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
                #endif


                // custom interpolator pre-include
                /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */

                // Includes
                #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
                #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
                #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
                #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
                #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
                #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
                #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"

                // --------------------------------------------------
                // Structs and Packing

                // custom interpolators pre packing
                /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */

                struct Attributes
                {
                     float3 positionOS : POSITION;
                     float3 normalOS : NORMAL;
                     float4 tangentOS : TANGENT;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : INSTANCEID_SEMANTIC;
                    #endif
                };
                struct Varyings
                {
                     float4 positionCS : SV_POSITION;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };
                struct SurfaceDescriptionInputs
                {
                };
                struct VertexDescriptionInputs
                {
                     float3 ObjectSpaceNormal;
                     float3 ObjectSpaceTangent;
                     float3 ObjectSpacePosition;
                };
                struct PackedVaryings
                {
                     float4 positionCS : SV_POSITION;
                    #if UNITY_ANY_INSTANCING_ENABLED
                     uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                     uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                     uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                     FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };

                PackedVaryings PackVaryings(Varyings input)
                {
                    PackedVaryings output;
                    ZERO_INITIALIZE(PackedVaryings, output);
                    output.positionCS = input.positionCS;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }

                Varyings UnpackVaryings(PackedVaryings input)
                {
                    Varyings output;
                    output.positionCS = input.positionCS;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }


                // --------------------------------------------------
                // Graph

                // Graph Properties
                CBUFFER_START(UnityPerMaterial)
                float _OutlineWidth;
                CBUFFER_END

                    // Object and Global properties

                    // -- Property used by ScenePickingPass
                    #ifdef SCENEPICKINGPASS
                    float4 _SelectionID;
                    #endif

                // -- Properties used by SceneSelectionPass
                #ifdef SCENESELECTIONPASS
                int _ObjectId;
                int _PassValue;
                #endif

                // Graph Includes
                // GraphIncludes: <None>

                // Graph Functions

                void Unity_Normalize_float3(float3 In, out float3 Out)
                {
                    Out = normalize(In);
                }

                void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
                {
                    Out = A * B;
                }

                void Unity_Add_float3(float3 A, float3 B, out float3 Out)
                {
                    Out = A + B;
                }

                // Custom interpolators pre vertex
                /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */

                // Graph Vertex
                struct VertexDescription
                {
                    float3 Position;
                    float3 Normal;
                    float3 Tangent;
                };

                VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                {
                    VertexDescription description = (VertexDescription)0;
                    float3 _Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1;
                    Unity_Normalize_float3(IN.ObjectSpaceNormal, _Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1);
                    float _Property_f641f4b058544303adc0c5c149c29cb1_Out_0 = _OutlineWidth;
                    float3 _Multiply_80385af780db41288b04d7db77732fa2_Out_2;
                    Unity_Multiply_float3_float3(_Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1, (_Property_f641f4b058544303adc0c5c149c29cb1_Out_0.xxx), _Multiply_80385af780db41288b04d7db77732fa2_Out_2);
                    float3 _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2;
                    Unity_Add_float3(_Multiply_80385af780db41288b04d7db77732fa2_Out_2, IN.ObjectSpacePosition, _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2);
                    description.Position = _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2;
                    description.Normal = IN.ObjectSpaceNormal;
                    description.Tangent = IN.ObjectSpaceTangent;
                    return description;
                }

                // Custom interpolators, pre surface
                #ifdef FEATURES_GRAPH_VERTEX
                Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
                {
                return output;
                }
                #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
                #endif

                // Graph Pixel
                struct SurfaceDescription
                {
                    float Alpha;
                    float AlphaClipThreshold;
                };

                SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                {
                    SurfaceDescription surface = (SurfaceDescription)0;
                    surface.Alpha = 1;
                    surface.AlphaClipThreshold = 0.5;
                    return surface;
                }

                // --------------------------------------------------
                // Build Graph Inputs

                VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
                {
                    VertexDescriptionInputs output;
                    ZERO_INITIALIZE(VertexDescriptionInputs, output);

                    output.ObjectSpaceNormal = input.normalOS;
                    output.ObjectSpaceTangent = input.tangentOS.xyz;
                    output.ObjectSpacePosition = input.positionOS;

                    return output;
                }
                SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);







                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
                #else
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                #endif
                #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

                        return output;
                }

                void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
                {
                    result.vertex = float4(attributes.positionOS, 1);
                    result.tangent = attributes.tangentOS;
                    result.normal = attributes.normalOS;
                    result.vertex = float4(vertexDescription.Position, 1);
                    result.normal = vertexDescription.Normal;
                    result.tangent = float4(vertexDescription.Tangent, 0);
                    #if UNITY_ANY_INSTANCING_ENABLED
                    #endif
                }

                void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
                {
                    result.pos = varyings.positionCS;
                    // World Tangent isn't an available input on v2f_surf


                    #if UNITY_ANY_INSTANCING_ENABLED
                    #endif
                    #if !defined(LIGHTMAP_ON)
                    #if UNITY_SHOULD_SAMPLE_SH
                    #endif
                    #endif
                    #if defined(LIGHTMAP_ON)
                    #endif
                    #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                        result.fogCoord = varyings.fogFactorAndVertexLight.x;
                        COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
                    #endif

                    DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
                }

                void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
                {
                    result.positionCS = surfVertex.pos;
                    // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
                    // World Tangent isn't an available input on v2f_surf

                    #if UNITY_ANY_INSTANCING_ENABLED
                    #endif
                    #if !defined(LIGHTMAP_ON)
                    #if UNITY_SHOULD_SAMPLE_SH
                    #endif
                    #endif
                    #if defined(LIGHTMAP_ON)
                    #endif
                    #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                        result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                        COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
                    #endif

                    DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
                }

                // --------------------------------------------------
                // Main

                #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
                #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
                #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShadowCasterPass.hlsl"

                ENDHLSL
                }
                Pass
                {
                    Name "SceneSelectionPass"
                    Tags
                    {
                        "LightMode" = "SceneSelectionPass"
                    }

                    // Render State
                    Cull Off

                    // Debug
                    // <None>

                    // --------------------------------------------------
                    // Pass

                    HLSLPROGRAM

                    // Pragmas
                    #pragma target 3.0
                    #pragma multi_compile_instancing
                    #pragma vertex vert
                    #pragma fragment frag

                    // DotsInstancingOptions: <None>
                    // HybridV1InjectedBuiltinProperties: <None>

                    // Keywords
                    #pragma shader_feature_local_fragment _ _BUILTIN_SURFACE_TYPE_TRANSPARENT
                    #pragma shader_feature_local_fragment _ _BUILTIN_AlphaClip
                    #pragma shader_feature_local_fragment _ _BUILTIN_ALPHATEST_ON
                    // GraphKeywords: <None>

                    // Defines
                    #define ATTRIBUTES_NEED_NORMAL
                    #define ATTRIBUTES_NEED_TANGENT
                    #define FEATURES_GRAPH_VERTEX
                    /* WARNING: $splice Could not find named fragment 'PassInstancing' */
                    #define SHADERPASS SceneSelectionPass
                    #define BUILTIN_TARGET_API 1
                    #define SCENESELECTIONPASS 1
                    /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
                    #ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
                    #define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
                    #endif
                    #ifdef _BUILTIN_ALPHATEST_ON
                    #define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
                    #endif
                    #ifdef _BUILTIN_AlphaClip
                    #define _AlphaClip _BUILTIN_AlphaClip
                    #endif
                    #ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
                    #define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
                    #endif


                    // custom interpolator pre-include
                    /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */

                    // Includes
                    #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
                    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
                    #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
                    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
                    #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
                    #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
                    #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"

                    // --------------------------------------------------
                    // Structs and Packing

                    // custom interpolators pre packing
                    /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */

                    struct Attributes
                    {
                         float3 positionOS : POSITION;
                         float3 normalOS : NORMAL;
                         float4 tangentOS : TANGENT;
                        #if UNITY_ANY_INSTANCING_ENABLED
                         uint instanceID : INSTANCEID_SEMANTIC;
                        #endif
                    };
                    struct Varyings
                    {
                         float4 positionCS : SV_POSITION;
                        #if UNITY_ANY_INSTANCING_ENABLED
                         uint instanceID : CUSTOM_INSTANCE_ID;
                        #endif
                        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                         uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                        #endif
                        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                         uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                        #endif
                        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                         FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                        #endif
                    };
                    struct SurfaceDescriptionInputs
                    {
                    };
                    struct VertexDescriptionInputs
                    {
                         float3 ObjectSpaceNormal;
                         float3 ObjectSpaceTangent;
                         float3 ObjectSpacePosition;
                    };
                    struct PackedVaryings
                    {
                         float4 positionCS : SV_POSITION;
                        #if UNITY_ANY_INSTANCING_ENABLED
                         uint instanceID : CUSTOM_INSTANCE_ID;
                        #endif
                        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                         uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                        #endif
                        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                         uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                        #endif
                        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                         FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                        #endif
                    };

                    PackedVaryings PackVaryings(Varyings input)
                    {
                        PackedVaryings output;
                        ZERO_INITIALIZE(PackedVaryings, output);
                        output.positionCS = input.positionCS;
                        #if UNITY_ANY_INSTANCING_ENABLED
                        output.instanceID = input.instanceID;
                        #endif
                        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                        #endif
                        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                        #endif
                        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                        output.cullFace = input.cullFace;
                        #endif
                        return output;
                    }

                    Varyings UnpackVaryings(PackedVaryings input)
                    {
                        Varyings output;
                        output.positionCS = input.positionCS;
                        #if UNITY_ANY_INSTANCING_ENABLED
                        output.instanceID = input.instanceID;
                        #endif
                        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                        #endif
                        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                        #endif
                        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                        output.cullFace = input.cullFace;
                        #endif
                        return output;
                    }


                    // --------------------------------------------------
                    // Graph

                    // Graph Properties
                    CBUFFER_START(UnityPerMaterial)
                    float _OutlineWidth;
                    CBUFFER_END

                        // Object and Global properties

                        // -- Property used by ScenePickingPass
                        #ifdef SCENEPICKINGPASS
                        float4 _SelectionID;
                        #endif

                    // -- Properties used by SceneSelectionPass
                    #ifdef SCENESELECTIONPASS
                    int _ObjectId;
                    int _PassValue;
                    #endif

                    // Graph Includes
                    // GraphIncludes: <None>

                    // Graph Functions

                    void Unity_Normalize_float3(float3 In, out float3 Out)
                    {
                        Out = normalize(In);
                    }

                    void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
                    {
                        Out = A * B;
                    }

                    void Unity_Add_float3(float3 A, float3 B, out float3 Out)
                    {
                        Out = A + B;
                    }

                    // Custom interpolators pre vertex
                    /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */

                    // Graph Vertex
                    struct VertexDescription
                    {
                        float3 Position;
                        float3 Normal;
                        float3 Tangent;
                    };

                    VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                    {
                        VertexDescription description = (VertexDescription)0;
                        float3 _Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1;
                        Unity_Normalize_float3(IN.ObjectSpaceNormal, _Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1);
                        float _Property_f641f4b058544303adc0c5c149c29cb1_Out_0 = _OutlineWidth;
                        float3 _Multiply_80385af780db41288b04d7db77732fa2_Out_2;
                        Unity_Multiply_float3_float3(_Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1, (_Property_f641f4b058544303adc0c5c149c29cb1_Out_0.xxx), _Multiply_80385af780db41288b04d7db77732fa2_Out_2);
                        float3 _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2;
                        Unity_Add_float3(_Multiply_80385af780db41288b04d7db77732fa2_Out_2, IN.ObjectSpacePosition, _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2);
                        description.Position = _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2;
                        description.Normal = IN.ObjectSpaceNormal;
                        description.Tangent = IN.ObjectSpaceTangent;
                        return description;
                    }

                    // Custom interpolators, pre surface
                    #ifdef FEATURES_GRAPH_VERTEX
                    Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
                    {
                    return output;
                    }
                    #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
                    #endif

                    // Graph Pixel
                    struct SurfaceDescription
                    {
                        float Alpha;
                        float AlphaClipThreshold;
                    };

                    SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                    {
                        SurfaceDescription surface = (SurfaceDescription)0;
                        surface.Alpha = 1;
                        surface.AlphaClipThreshold = 0.5;
                        return surface;
                    }

                    // --------------------------------------------------
                    // Build Graph Inputs

                    VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
                    {
                        VertexDescriptionInputs output;
                        ZERO_INITIALIZE(VertexDescriptionInputs, output);

                        output.ObjectSpaceNormal = input.normalOS;
                        output.ObjectSpaceTangent = input.tangentOS.xyz;
                        output.ObjectSpacePosition = input.positionOS;

                        return output;
                    }
                    SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                    {
                        SurfaceDescriptionInputs output;
                        ZERO_INITIALIZE(SurfaceDescriptionInputs, output);







                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
                    #else
                    #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                    #endif
                    #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

                            return output;
                    }

                    void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
                    {
                        result.vertex = float4(attributes.positionOS, 1);
                        result.tangent = attributes.tangentOS;
                        result.normal = attributes.normalOS;
                        result.vertex = float4(vertexDescription.Position, 1);
                        result.normal = vertexDescription.Normal;
                        result.tangent = float4(vertexDescription.Tangent, 0);
                        #if UNITY_ANY_INSTANCING_ENABLED
                        #endif
                    }

                    void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
                    {
                        result.pos = varyings.positionCS;
                        // World Tangent isn't an available input on v2f_surf


                        #if UNITY_ANY_INSTANCING_ENABLED
                        #endif
                        #if !defined(LIGHTMAP_ON)
                        #if UNITY_SHOULD_SAMPLE_SH
                        #endif
                        #endif
                        #if defined(LIGHTMAP_ON)
                        #endif
                        #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                            result.fogCoord = varyings.fogFactorAndVertexLight.x;
                            COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
                        #endif

                        DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
                    }

                    void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
                    {
                        result.positionCS = surfVertex.pos;
                        // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
                        // World Tangent isn't an available input on v2f_surf

                        #if UNITY_ANY_INSTANCING_ENABLED
                        #endif
                        #if !defined(LIGHTMAP_ON)
                        #if UNITY_SHOULD_SAMPLE_SH
                        #endif
                        #endif
                        #if defined(LIGHTMAP_ON)
                        #endif
                        #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                            result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                            COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
                        #endif

                        DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
                    }

                    // --------------------------------------------------
                    // Main

                    #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
                    #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
                    #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/DepthOnlyPass.hlsl"

                    ENDHLSL
                    }
                    Pass
                    {
                        Name "ScenePickingPass"
                        Tags
                        {
                            "LightMode" = "Picking"
                        }

                        // Render State
                        Cull[_BUILTIN_CullMode]

                        // Debug
                        // <None>

                        // --------------------------------------------------
                        // Pass

                        HLSLPROGRAM

                        // Pragmas
                        #pragma target 3.0
                        #pragma multi_compile_instancing
                        #pragma vertex vert
                        #pragma fragment frag

                        // DotsInstancingOptions: <None>
                        // HybridV1InjectedBuiltinProperties: <None>

                        // Keywords
                        #pragma shader_feature_local_fragment _ _BUILTIN_SURFACE_TYPE_TRANSPARENT
                        #pragma shader_feature_local_fragment _ _BUILTIN_AlphaClip
                        #pragma shader_feature_local_fragment _ _BUILTIN_ALPHATEST_ON
                        // GraphKeywords: <None>

                        // Defines
                        #define ATTRIBUTES_NEED_NORMAL
                        #define ATTRIBUTES_NEED_TANGENT
                        #define FEATURES_GRAPH_VERTEX
                        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
                        #define SHADERPASS ScenePickingPass
                        #define BUILTIN_TARGET_API 1
                        #define SCENEPICKINGPASS 1
                        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
                        #ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
                        #define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
                        #endif
                        #ifdef _BUILTIN_ALPHATEST_ON
                        #define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
                        #endif
                        #ifdef _BUILTIN_AlphaClip
                        #define _AlphaClip _BUILTIN_AlphaClip
                        #endif
                        #ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
                        #define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
                        #endif


                        // custom interpolator pre-include
                        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */

                        // Includes
                        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
                        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
                        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
                        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
                        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
                        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
                        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"

                        // --------------------------------------------------
                        // Structs and Packing

                        // custom interpolators pre packing
                        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */

                        struct Attributes
                        {
                             float3 positionOS : POSITION;
                             float3 normalOS : NORMAL;
                             float4 tangentOS : TANGENT;
                            #if UNITY_ANY_INSTANCING_ENABLED
                             uint instanceID : INSTANCEID_SEMANTIC;
                            #endif
                        };
                        struct Varyings
                        {
                             float4 positionCS : SV_POSITION;
                            #if UNITY_ANY_INSTANCING_ENABLED
                             uint instanceID : CUSTOM_INSTANCE_ID;
                            #endif
                            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                            #endif
                            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                            #endif
                            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                            #endif
                        };
                        struct SurfaceDescriptionInputs
                        {
                        };
                        struct VertexDescriptionInputs
                        {
                             float3 ObjectSpaceNormal;
                             float3 ObjectSpaceTangent;
                             float3 ObjectSpacePosition;
                        };
                        struct PackedVaryings
                        {
                             float4 positionCS : SV_POSITION;
                            #if UNITY_ANY_INSTANCING_ENABLED
                             uint instanceID : CUSTOM_INSTANCE_ID;
                            #endif
                            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                            #endif
                            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                            #endif
                            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                            #endif
                        };

                        PackedVaryings PackVaryings(Varyings input)
                        {
                            PackedVaryings output;
                            ZERO_INITIALIZE(PackedVaryings, output);
                            output.positionCS = input.positionCS;
                            #if UNITY_ANY_INSTANCING_ENABLED
                            output.instanceID = input.instanceID;
                            #endif
                            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                            #endif
                            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                            #endif
                            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                            output.cullFace = input.cullFace;
                            #endif
                            return output;
                        }

                        Varyings UnpackVaryings(PackedVaryings input)
                        {
                            Varyings output;
                            output.positionCS = input.positionCS;
                            #if UNITY_ANY_INSTANCING_ENABLED
                            output.instanceID = input.instanceID;
                            #endif
                            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                            #endif
                            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                            #endif
                            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                            output.cullFace = input.cullFace;
                            #endif
                            return output;
                        }


                        // --------------------------------------------------
                        // Graph

                        // Graph Properties
                        CBUFFER_START(UnityPerMaterial)
                        float _OutlineWidth;
                        CBUFFER_END

                            // Object and Global properties

                            // -- Property used by ScenePickingPass
                            #ifdef SCENEPICKINGPASS
                            float4 _SelectionID;
                            #endif

                        // -- Properties used by SceneSelectionPass
                        #ifdef SCENESELECTIONPASS
                        int _ObjectId;
                        int _PassValue;
                        #endif

                        // Graph Includes
                        // GraphIncludes: <None>

                        // Graph Functions

                        void Unity_Normalize_float3(float3 In, out float3 Out)
                        {
                            Out = normalize(In);
                        }

                        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
                        {
                            Out = A * B;
                        }

                        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
                        {
                            Out = A + B;
                        }

                        // Custom interpolators pre vertex
                        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */

                        // Graph Vertex
                        struct VertexDescription
                        {
                            float3 Position;
                            float3 Normal;
                            float3 Tangent;
                        };

                        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
                        {
                            VertexDescription description = (VertexDescription)0;
                            float3 _Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1;
                            Unity_Normalize_float3(IN.ObjectSpaceNormal, _Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1);
                            float _Property_f641f4b058544303adc0c5c149c29cb1_Out_0 = _OutlineWidth;
                            float3 _Multiply_80385af780db41288b04d7db77732fa2_Out_2;
                            Unity_Multiply_float3_float3(_Normalize_8ce885e6a2d54d079d3df826fe887cbe_Out_1, (_Property_f641f4b058544303adc0c5c149c29cb1_Out_0.xxx), _Multiply_80385af780db41288b04d7db77732fa2_Out_2);
                            float3 _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2;
                            Unity_Add_float3(_Multiply_80385af780db41288b04d7db77732fa2_Out_2, IN.ObjectSpacePosition, _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2);
                            description.Position = _Add_cbb2e9af11e042518019bcfa0f7034f4_Out_2;
                            description.Normal = IN.ObjectSpaceNormal;
                            description.Tangent = IN.ObjectSpaceTangent;
                            return description;
                        }

                        // Custom interpolators, pre surface
                        #ifdef FEATURES_GRAPH_VERTEX
                        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
                        {
                        return output;
                        }
                        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
                        #endif

                        // Graph Pixel
                        struct SurfaceDescription
                        {
                            float Alpha;
                            float AlphaClipThreshold;
                        };

                        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                        {
                            SurfaceDescription surface = (SurfaceDescription)0;
                            surface.Alpha = 1;
                            surface.AlphaClipThreshold = 0.5;
                            return surface;
                        }

                        // --------------------------------------------------
                        // Build Graph Inputs

                        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
                        {
                            VertexDescriptionInputs output;
                            ZERO_INITIALIZE(VertexDescriptionInputs, output);

                            output.ObjectSpaceNormal = input.normalOS;
                            output.ObjectSpaceTangent = input.tangentOS.xyz;
                            output.ObjectSpacePosition = input.positionOS;

                            return output;
                        }
                        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                        {
                            SurfaceDescriptionInputs output;
                            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);







                        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
                        #else
                        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                        #endif
                        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

                                return output;
                        }

                        void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
                        {
                            result.vertex = float4(attributes.positionOS, 1);
                            result.tangent = attributes.tangentOS;
                            result.normal = attributes.normalOS;
                            result.vertex = float4(vertexDescription.Position, 1);
                            result.normal = vertexDescription.Normal;
                            result.tangent = float4(vertexDescription.Tangent, 0);
                            #if UNITY_ANY_INSTANCING_ENABLED
                            #endif
                        }

                        void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
                        {
                            result.pos = varyings.positionCS;
                            // World Tangent isn't an available input on v2f_surf


                            #if UNITY_ANY_INSTANCING_ENABLED
                            #endif
                            #if !defined(LIGHTMAP_ON)
                            #if UNITY_SHOULD_SAMPLE_SH
                            #endif
                            #endif
                            #if defined(LIGHTMAP_ON)
                            #endif
                            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                                result.fogCoord = varyings.fogFactorAndVertexLight.x;
                                COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
                            #endif

                            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
                        }

                        void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
                        {
                            result.positionCS = surfVertex.pos;
                            // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
                            // World Tangent isn't an available input on v2f_surf

                            #if UNITY_ANY_INSTANCING_ENABLED
                            #endif
                            #if !defined(LIGHTMAP_ON)
                            #if UNITY_SHOULD_SAMPLE_SH
                            #endif
                            #endif
                            #if defined(LIGHTMAP_ON)
                            #endif
                            #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                                result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                                COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
                            #endif

                            DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
                        }

                        // --------------------------------------------------
                        // Main

                        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
                        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
                        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/DepthOnlyPass.hlsl"

                        ENDHLSL
                        }
    }
        CustomEditorForRenderPipeline "UnityEditor.Rendering.BuiltIn.ShaderGraph.BuiltInUnlitGUI" ""
                            CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
                            FallBack "Hidden/Shader Graph/FallbackError"
}