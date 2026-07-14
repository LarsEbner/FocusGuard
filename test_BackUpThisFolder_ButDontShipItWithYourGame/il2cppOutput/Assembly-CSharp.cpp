#include "pch-cpp.hpp"





template <typename R, typename T1>
struct VirtualFuncInvoker1
{
	typedef R (*Func)(void*, T1, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1>
struct InterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};

struct Action_1_tEB0353AA1A112B6F2D921B58DCC9D9D4C0498E6E;
struct List_1_t58F89DEDCD7DABB0CFB009AAD9C0CFE061592252;
struct List_1_t602BCD639AA637A6C0BB45C136DD5458DBE18064;
struct ReadOnlyCollection_1_tE414953665CCBE1BFF28E8E32C184621ADDA4B76;
struct VolumeParameter_1_t18B35E30089EFE0C2751A53FE6143F972EC9F9B8;
struct Action_1U5BU5D_tB846E6FE2326CCD34124D1E5D70117C9D33DEE76;
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6;
struct InputBindingU5BU5D_t7E47E87B9CAE12B6F6A0659008B425C58D84BB57;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct VolumeParameterU5BU5D_t7025A98CA20F310D68D653DE8E37EA31FF25E103;
struct XrSingleEyeGazeDataHTCU5BU5D_t8D4B6A3A120DF3744FF941025D21175D2EC0428A;
struct XrSingleEyeGeometricDataHTCU5BU5D_t90FECCAA11F87536897187EA64024AECC7AAA12B;
struct XrSingleEyePupilDataHTCU5BU5D_tF46D1C63585D0762116A95FF45EB5FE4DA3CF532;
struct SectionU5BU5D_t9D3017555FFF42E71BE91904A2486EAF429F24C4;
struct BackgroundColorEffect_t1180F0D3625E7EB5CCB01922FD4B090D1B8C2086;
struct BoolParameter_tAA460C5A72ADBDDB4519BFF0BA040EC202E14E95;
struct Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184;
struct CancellationTokenSource_tAAE1E0033BCFC233801F8CB4CED5C852B350CB7B;
struct ClampedFloatParameter_tCD9F742962EAA50F658BC77595AB025D9EF8DEB8;
struct ColorParameter_t367FD9EBE5DAA0ADB44F7DD0C260E9CDE3827CC0;
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3;
struct Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B;
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA;
struct IFocusEffect_t517DE4A02353DDF0DAC8AEC1CE2EB967EC943A21;
struct InputAction_t1B550AD2B55AF322AFB53CD28DA64081220D01CD;
struct InputActionAsset_tF217AC5223B4AAA46EBCB44B33E9259FB117417D;
struct InputActionMap_tFCE82E0E014319D4DED9F8962B06655DD0420A09;
struct InputActionReference_t64730C6B41271E0983FC21BFB416169F5D6BC4A1;
struct LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9;
struct LogStrengthEffect_tD96784BDDB9D6BB7D175A37049EEE511A025E009;
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71;
struct NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A;
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
struct Readme_tE17B99201D0F52BD5727638AD3F41072A65B3BBB;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A;
struct SphereControl_tECCC958757C50C07C60A0C29364FDF3734934843;
struct String_t;
struct Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4;
struct Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1;
struct UISwap_t1566756761DF5D095F3E89F6F960635CFCA36AED;
struct UnionEffect_t37D758ED9E7C2676C2DF98C7E4C2A01CF88FB46C;
struct UnitySourceGeneratedAssemblyMonoScriptTypes_v1_tC95F24D0C6E6B77389433852BB389F39C692926E;
struct UpdateLeftEyeGeometric_t436A987A8B4940F3946127A04CEF264E5C9ADFD0;
struct UpdateLeftGaze_t29AEEF8C212103AB4C6BDC8A0278B3348944B4CB;
struct UpdateLeftPupil_t34F9E3A9EB44660D3F4704A3754576F4879F930C;
struct UpdateRightEyeGeometric_t94E0DBDA782988C8A25AEE08F79B9B0002E7693D;
struct UpdateRightGaze_t22DB02558C008C31DB4679D0FA1D47A12C256666;
struct UpdateRightPupil_tD6654BBDC60B3BCE50324C6B5E97614E44D2F05B;
struct Vector2Parameter_tA29C9FAC53EDB2E0996430A461F9CC59B6C4288A;
struct Vignette_t77147DD5FEEB4476AF22BD98255F8010738985DC;
struct VignetteEffect_t4D2F583DF4E25F5E3B9A45F740DC17D4B18F058F;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct Volume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377;
struct VolumeProfile_t9B5F2005F575A710F38A124EF81A6228CCACACE1;
struct WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3;
struct XR_HTC_eye_tracker_defs_tD0DA3CB49A68FE8A0A556D6FF6B02CCE35043178;
struct CameraCallback_t844E527BFE37BC0495E7F67993E43C07642DA9DD;
struct U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83;
struct Section_t50C894D0A717C2368EBAAE5477D4E8626D0B5401;

IL2CPP_EXTERN_C RuntimeClass* BackgroundColorEffect_t1180F0D3625E7EB5CCB01922FD4B090D1B8C2086_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IFocusEffect_t517DE4A02353DDF0DAC8AEC1CE2EB967EC943A21_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* LogStrengthEffect_tD96784BDDB9D6BB7D175A37049EEE511A025E009_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* OpenXRHelper_t4BE36310EB51760ADFEF668D8C1E7C00DF452063_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* UnionEffect_t37D758ED9E7C2676C2DF98C7E4C2A01CF88FB46C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* VignetteEffect_t4D2F583DF4E25F5E3B9A45F740DC17D4B18F058F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeField* U3CPrivateImplementationDetailsU3E_t0F5473E849A5A5185A9F4C5246F0C32816C49FCA____85D7278D419C72BF2DEB441B46289F21373660B0A1DB767934BA021ADE535AEE_FieldInfo_var;
IL2CPP_EXTERN_C RuntimeField* U3CPrivateImplementationDetailsU3E_t0F5473E849A5A5185A9F4C5246F0C32816C49FCA____CDBAB22DCDC18D8C38C5A43786143948C2F22B5E15277197F82E7141ECDB1C11_FieldInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral2EA74FD1B45A765749965AACFEEC38B63CB85E56;
IL2CPP_EXTERN_C String_t* _stringLiteral97AD4ACE67E943B106D9FFF8E32C1FD79B0BF1B8;
IL2CPP_EXTERN_C String_t* _stringLiteralFC59CEB920AC68F134A1598B64F3B285BD6AE4BA;
IL2CPP_EXTERN_C const RuntimeMethod* InputAction_ReadValue_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m37FC749080A83C05777D1F779F38B8A27BAFA97B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Object_FindAnyObjectByType_TisVolume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377_mDF2A08B67822C1ABAB89B34865D4B6D72E2F7170_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CApplyLinearEffectU3Ed__8_System_Collections_IEnumerator_Reset_m659D61F25322F744AC5DDD6AE2A301BC7BA1B7A3_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* VolumeParameter_1_Override_mC346E68FEB631DC3D367126E958B371099C991E0_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* VolumeProfile_TryGet_TisVignette_t77147DD5FEEB4476AF22BD98255F8010738985DC_mE8244CE93377BC8821F5278FE70551C4B60D7D7E_RuntimeMethod_var;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;
struct XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E;
struct XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138;
struct XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC;

struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6;
struct XrSingleEyeGazeDataHTCU5BU5D_t8D4B6A3A120DF3744FF941025D21175D2EC0428A;
struct XrSingleEyeGeometricDataHTCU5BU5D_t90FECCAA11F87536897187EA64024AECC7AAA12B;
struct XrSingleEyePupilDataHTCU5BU5D_tF46D1C63585D0762116A95FF45EB5FE4DA3CF532;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_tBB65183F1134474D09FF49B95625D25472B9BA8B 
{
};
struct U3CPrivateImplementationDetailsU3E_t0F5473E849A5A5185A9F4C5246F0C32816C49FCA  : public RuntimeObject
{
};
struct LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9  : public RuntimeObject
{
	MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* ____coroutineTrigger;
	RuntimeObject* ____effect;
	float ____effectLength;
	float ____tickDuration;
	double ___lastStrength;
	double ___currentStrength;
};
struct LogStrengthEffect_tD96784BDDB9D6BB7D175A37049EEE511A025E009  : public RuntimeObject
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct UnionEffect_t37D758ED9E7C2676C2DF98C7E4C2A01CF88FB46C  : public RuntimeObject
{
	IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6* ____effects;
};
struct UnitySourceGeneratedAssemblyMonoScriptTypes_v1_tC95F24D0C6E6B77389433852BB389F39C692926E  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct VignetteEffect_t4D2F583DF4E25F5E3B9A45F740DC17D4B18F058F  : public RuntimeObject
{
	Vignette_t77147DD5FEEB4476AF22BD98255F8010738985DC* ___vignette;
};
struct VolumeParameter_t95994C89644D2CC4C11F666571492420D16BED72  : public RuntimeObject
{
	bool ___m_OverrideState;
};
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D  : public RuntimeObject
{
};
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
};
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
};
struct U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83  : public RuntimeObject
{
	int32_t ___U3CU3E1__state;
	RuntimeObject* ___U3CU3E2__current;
	LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* ___U3CU3E4__this;
	double ___strength;
	float ___U3CtickEffectDifferenceU3E5__2;
	float ___U3CcurrentDifferenceU3E5__3;
};
struct Section_t50C894D0A717C2368EBAAE5477D4E8626D0B5401  : public RuntimeObject
{
	String_t* ___heading;
	String_t* ___text;
	String_t* ___linkText;
	String_t* ___url;
};
struct InlinedArray_1_tC208D319D19C2B3DF550BD9CDC11549F23D8F91B 
{
	int32_t ___length;
	Action_1_tEB0353AA1A112B6F2D921B58DCC9D9D4C0498E6E* ___firstValue;
	Action_1U5BU5D_tB846E6FE2326CCD34124D1E5D70117C9D33DEE76* ___additionalValues;
};
struct VolumeParameter_1_t18B35E30089EFE0C2751A53FE6143F972EC9F9B8  : public VolumeParameter_t95994C89644D2CC4C11F666571492420D16BED72
{
	float ___m_Value;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3 
{
	uint8_t ___m_value;
};
struct Color_tD001788D726C3A7F1379BEED0260B9591F440C1F 
{
	float ___r;
	float ___g;
	float ___b;
	float ___a;
};
struct Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F 
{
	double ___m_value;
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 
{
	float ___x;
	float ___y;
	float ___z;
	float ___w;
};
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	float ___m_value;
};
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 
{
	float ___x;
	float ___y;
	float ___z;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3  : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D
{
	float ___m_Seconds;
};
struct WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3_marshaled_pinvoke : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
	float ___m_Seconds;
};
struct WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3_marshaled_com : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
	float ___m_Seconds;
};
struct XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 
{
	uint32_t ___value;
};
struct XrQuaternionf_tCAC179EA55B9A02857B046051F3E115E926E1837 
{
	float ___x;
	float ___y;
	float ___z;
	float ___w;
};
struct XrTime_tAA4642192BA7C50D52CCF171F78B128D7FE096E2 
{
	int64_t ___value;
};
struct XrVector2f_t2B4FBA5E1A3BFD47512B734B5359BBA61989FD20 
{
	float ___x;
	float ___y;
};
struct XrVector3f_t5A6C3732E24CADBBE5C99FC58D6A1C17E67C9AC1 
{
	float ___x;
	float ___y;
	float ___z;
};
#pragma pack(push, tp, 1)
struct __StaticArrayInitTypeSizeU3D404_tF4DBCC53E3B0C5CC8D3C5222F8FFB7116F8879ED 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D404_tF4DBCC53E3B0C5CC8D3C5222F8FFB7116F8879ED__padding[404];
	};
};
#pragma pack(pop, tp)
#pragma pack(push, tp, 1)
struct __StaticArrayInitTypeSizeU3D663_tFE041C7CCF4E687DD084B48E8C2A902F295FC778 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D663_tFE041C7CCF4E687DD084B48E8C2A902F295FC778__padding[663];
	};
};
#pragma pack(pop, tp)
struct MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E 
{
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___FilePathsData;
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___TypesData;
	int32_t ___TotalTypes;
	int32_t ___TotalFiles;
	bool ___IsEditorOnly;
};
struct MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshaled_pinvoke
{
	Il2CppSafeArray* ___FilePathsData;
	Il2CppSafeArray* ___TypesData;
	int32_t ___TotalTypes;
	int32_t ___TotalFiles;
	int32_t ___IsEditorOnly;
};
struct MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshaled_com
{
	Il2CppSafeArray* ___FilePathsData;
	Il2CppSafeArray* ___TypesData;
	int32_t ___TotalTypes;
	int32_t ___TotalFiles;
	int32_t ___IsEditorOnly;
};
struct CallbackArray_1_tDFF8C4C6015023B6C2E70BAD26D8BC6BF00D8775 
{
	bool ___m_CannotMutateCallbacksArray;
	InlinedArray_1_tC208D319D19C2B3DF550BD9CDC11549F23D8F91B ___m_Callbacks;
	InlinedArray_1_tC208D319D19C2B3DF550BD9CDC11549F23D8F91B ___m_CallbacksToAdd;
	InlinedArray_1_tC208D319D19C2B3DF550BD9CDC11549F23D8F91B ___m_CallbacksToRemove;
};
struct BackgroundColorEffect_t1180F0D3625E7EB5CCB01922FD4B090D1B8C2086  : public RuntimeObject
{
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ____color;
};
struct Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B  : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D
{
	intptr_t ___m_Ptr;
};
struct Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B_marshaled_pinvoke : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
	intptr_t ___m_Ptr;
};
struct Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B_marshaled_com : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
	intptr_t ___m_Ptr;
};
struct Exception_t  : public RuntimeObject
{
	String_t* ____className;
	String_t* ____message;
	RuntimeObject* ____data;
	Exception_t* ____innerException;
	String_t* ____helpURL;
	RuntimeObject* ____stackTrace;
	String_t* ____stackTraceString;
	String_t* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	RuntimeObject* ____dynamicMethods;
	int32_t ____HResult;
	String_t* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_pinvoke
{
	char* ____className;
	char* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_pinvoke* ____innerException;
	char* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	char* ____stackTraceString;
	char* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	char* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className;
	Il2CppChar* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_com* ____innerException;
	Il2CppChar* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	Il2CppChar* ____stackTraceString;
	Il2CppChar* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	Il2CppChar* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct FindObjectsInactive_t10C7BE036CAD0178142374F945283DA50D02B87A 
{
	int32_t ___value__;
};
struct FloatParameter_t566B623CD21B2F957A20BA790ACEF6684A712106  : public VolumeParameter_1_t18B35E30089EFE0C2751A53FE6143F972EC9F9B8
{
};
struct InputActionType_t7E3615BDDF3C84F39712E5889559D3AD8E773108 
{
	int32_t ___value__;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr;
};
struct ProfilerMarker_tA256E18DA86EDBC5528CE066FC91C96EE86501AD 
{
	intptr_t ___m_Ptr;
};
struct RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 
{
	intptr_t ___value;
};
struct XrPosef_t0821C076127F41D248AA6A56C7EB9D2A36BD34D9 
{
	XrQuaternionf_tCAC179EA55B9A02857B046051F3E115E926E1837 ___orientation;
	XrVector3f_t5A6C3732E24CADBBE5C99FC58D6A1C17E67C9AC1 ___position;
};
struct XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 
{
	XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 ___isValid;
	float ___eyeOpenness;
	float ___eyeWide;
	float ___eyeSqueeze;
};
struct XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC 
{
	XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 ___isDiameterValid;
	XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 ___isPositionValid;
	float ___pupilDiameter;
	XrVector2f_t2B4FBA5E1A3BFD47512B734B5359BBA61989FD20 ___pupilPosition;
};
struct XrStructureType_t8F52235F3D60F37AD3797ADC60C32D15B8923DD1 
{
	int32_t ___value__;
};
struct ActionFlags_t639BD2944E073F8DD263CE2CA581FC62C401AB1E 
{
	int32_t ___value__;
};
struct Flags_t2ED4EFE461994B03533B3B524C8C2EA71315AAE6 
{
	int32_t ___value__;
};
struct DirtyState_tA2B5B7213371CBEB225ADD2494E395A7C3AC8BA9 
{
	int32_t ___value__;
};
struct ClampedFloatParameter_tCD9F742962EAA50F658BC77595AB025D9EF8DEB8  : public FloatParameter_t566B623CD21B2F957A20BA790ACEF6684A712106
{
	float ___min;
	float ___max;
};
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct InputBinding_t0D75BD1538CF81D29450D568D5C938E111633EC5 
{
	String_t* ___m_Name;
	String_t* ___m_Id;
	String_t* ___m_Path;
	String_t* ___m_Interactions;
	String_t* ___m_Processors;
	String_t* ___m_Groups;
	String_t* ___m_Action;
	int32_t ___m_Flags;
	String_t* ___m_OverridePath;
	String_t* ___m_OverrideInteractions;
	String_t* ___m_OverrideProcessors;
};
struct InputBinding_t0D75BD1538CF81D29450D568D5C938E111633EC5_marshaled_pinvoke
{
	char* ___m_Name;
	char* ___m_Id;
	char* ___m_Path;
	char* ___m_Interactions;
	char* ___m_Processors;
	char* ___m_Groups;
	char* ___m_Action;
	int32_t ___m_Flags;
	char* ___m_OverridePath;
	char* ___m_OverrideInteractions;
	char* ___m_OverrideProcessors;
};
struct InputBinding_t0D75BD1538CF81D29450D568D5C938E111633EC5_marshaled_com
{
	Il2CppChar* ___m_Name;
	Il2CppChar* ___m_Id;
	Il2CppChar* ___m_Path;
	Il2CppChar* ___m_Interactions;
	Il2CppChar* ___m_Processors;
	Il2CppChar* ___m_Groups;
	Il2CppChar* ___m_Action;
	int32_t ___m_Flags;
	Il2CppChar* ___m_OverridePath;
	Il2CppChar* ___m_OverrideInteractions;
	Il2CppChar* ___m_OverrideProcessors;
};
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_pinvoke : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
};
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_com : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
};
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};
struct XrEyeGeometricDataHTC_tC2F903C50B430CDFDCE42B328646E5F19929C6CB 
{
	int32_t ___type;
	intptr_t ___next;
	XrTime_tAA4642192BA7C50D52CCF171F78B128D7FE096E2 ___time;
	XrSingleEyeGeometricDataHTCU5BU5D_t90FECCAA11F87536897187EA64024AECC7AAA12B* ___eyeGeometricData;
};
struct XrEyeGeometricDataHTC_tC2F903C50B430CDFDCE42B328646E5F19929C6CB_marshaled_pinvoke
{
	int32_t ___type;
	intptr_t ___next;
	XrTime_tAA4642192BA7C50D52CCF171F78B128D7FE096E2 ___time;
	XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 ___eyeGeometricData[2];
};
struct XrEyeGeometricDataHTC_tC2F903C50B430CDFDCE42B328646E5F19929C6CB_marshaled_com
{
	int32_t ___type;
	intptr_t ___next;
	XrTime_tAA4642192BA7C50D52CCF171F78B128D7FE096E2 ___time;
	XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 ___eyeGeometricData[2];
};
struct XrEyePupilDataHTC_t775E19D45C8485C893FD291C9F8194CB59160213 
{
	int32_t ___type;
	intptr_t ___next;
	XrTime_tAA4642192BA7C50D52CCF171F78B128D7FE096E2 ___time;
	XrSingleEyePupilDataHTCU5BU5D_tF46D1C63585D0762116A95FF45EB5FE4DA3CF532* ___pupilData;
};
struct XrEyePupilDataHTC_t775E19D45C8485C893FD291C9F8194CB59160213_marshaled_pinvoke
{
	int32_t ___type;
	intptr_t ___next;
	XrTime_tAA4642192BA7C50D52CCF171F78B128D7FE096E2 ___time;
	XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC ___pupilData[2];
};
struct XrEyePupilDataHTC_t775E19D45C8485C893FD291C9F8194CB59160213_marshaled_com
{
	int32_t ___type;
	intptr_t ___next;
	XrTime_tAA4642192BA7C50D52CCF171F78B128D7FE096E2 ___time;
	XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC ___pupilData[2];
};
struct XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E 
{
	XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 ___isValid;
	XrPosef_t0821C076127F41D248AA6A56C7EB9D2A36BD34D9 ___gazePose;
};
struct Nullable_1_t11786EE914FE65E70B9671129B0DFC4D0DE80C44 
{
	bool ___hasValue;
	InputBinding_t0D75BD1538CF81D29450D568D5C938E111633EC5 ___value;
};
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};
struct InputActionReference_t64730C6B41271E0983FC21BFB416169F5D6BC4A1  : public ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A
{
	InputActionAsset_tF217AC5223B4AAA46EBCB44B33E9259FB117417D* ___m_Asset;
	String_t* ___m_ActionId;
	InputAction_t1B550AD2B55AF322AFB53CD28DA64081220D01CD* ___m_Action;
};
struct NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct Readme_tE17B99201D0F52BD5727638AD3F41072A65B3BBB  : public ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A
{
	Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* ___icon;
	String_t* ___title;
	SectionU5BU5D_t9D3017555FFF42E71BE91904A2486EAF429F24C4* ___sections;
	bool ___loadedLayout;
};
struct Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};
struct VolumeComponent_t8121D1F6054A9DFB3A596EE451FD65A2BFE2D7E1  : public ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A
{
	bool ___active;
	String_t* ___U3CdisplayNameU3Ek__BackingField;
	VolumeParameterU5BU5D_t7025A98CA20F310D68D653DE8E37EA31FF25E103* ___parameterList;
	ReadOnlyCollection_1_tE414953665CCBE1BFF28E8E32C184621ADDA4B76* ___m_ParameterReadOnlyCollection;
};
struct VolumeProfile_t9B5F2005F575A710F38A124EF81A6228CCACACE1  : public ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A
{
	List_1_t602BCD639AA637A6C0BB45C136DD5458DBE18064* ___components;
	int32_t ___dirtyState;
};
struct XrEyeGazeDataHTC_t6CE5F798DECBC50A0783E4540AA1D5C857817A0D 
{
	int32_t ___type;
	intptr_t ___next;
	XrTime_tAA4642192BA7C50D52CCF171F78B128D7FE096E2 ___time;
	XrSingleEyeGazeDataHTCU5BU5D_t8D4B6A3A120DF3744FF941025D21175D2EC0428A* ___gaze;
};
struct XrEyeGazeDataHTC_t6CE5F798DECBC50A0783E4540AA1D5C857817A0D_marshaled_pinvoke
{
	int32_t ___type;
	intptr_t ___next;
	XrTime_tAA4642192BA7C50D52CCF171F78B128D7FE096E2 ___time;
	XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E ___gaze[2];
};
struct XrEyeGazeDataHTC_t6CE5F798DECBC50A0783E4540AA1D5C857817A0D_marshaled_com
{
	int32_t ___type;
	intptr_t ___next;
	XrTime_tAA4642192BA7C50D52CCF171F78B128D7FE096E2 ___time;
	XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E ___gaze[2];
};
struct Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
	uint32_t ___m_NonSerializedVersion;
};
struct InputAction_t1B550AD2B55AF322AFB53CD28DA64081220D01CD  : public RuntimeObject
{
	String_t* ___m_Name;
	int32_t ___m_Type;
	String_t* ___m_ExpectedControlType;
	String_t* ___m_Id;
	String_t* ___m_Processors;
	String_t* ___m_Interactions;
	InputBindingU5BU5D_t7E47E87B9CAE12B6F6A0659008B425C58D84BB57* ___m_SingletonActionBindings;
	int32_t ___m_Flags;
	Nullable_1_t11786EE914FE65E70B9671129B0DFC4D0DE80C44 ___m_BindingMask;
	int32_t ___m_BindingsStartIndex;
	int32_t ___m_BindingsCount;
	int32_t ___m_ControlStartIndex;
	int32_t ___m_ControlCount;
	int32_t ___m_ActionIndexInState;
	InputActionMap_tFCE82E0E014319D4DED9F8962B06655DD0420A09* ___m_ActionMap;
	CallbackArray_1_tDFF8C4C6015023B6C2E70BAD26D8BC6BF00D8775 ___m_OnStarted;
	CallbackArray_1_tDFF8C4C6015023B6C2E70BAD26D8BC6BF00D8775 ___m_OnCanceled;
	CallbackArray_1_tDFF8C4C6015023B6C2E70BAD26D8BC6BF00D8775 ___m_OnPerformed;
};
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
	CancellationTokenSource_tAAE1E0033BCFC233801F8CB4CED5C852B350CB7B* ___m_CancellationTokenSource;
};
struct Vignette_t77147DD5FEEB4476AF22BD98255F8010738985DC  : public VolumeComponent_t8121D1F6054A9DFB3A596EE451FD65A2BFE2D7E1
{
	ColorParameter_t367FD9EBE5DAA0ADB44F7DD0C260E9CDE3827CC0* ___color;
	Vector2Parameter_tA29C9FAC53EDB2E0996430A461F9CC59B6C4288A* ___center;
	ClampedFloatParameter_tCD9F742962EAA50F658BC77595AB025D9EF8DEB8* ___intensity;
	ClampedFloatParameter_tCD9F742962EAA50F658BC77595AB025D9EF8DEB8* ___smoothness;
	BoolParameter_tAA460C5A72ADBDDB4519BFF0BA040EC202E14E95* ___rounded;
};
struct XR_HTC_eye_tracker_defs_tD0DA3CB49A68FE8A0A556D6FF6B02CCE35043178  : public RuntimeObject
{
	XrEyeGazeDataHTC_t6CE5F798DECBC50A0783E4540AA1D5C857817A0D ___m_eyeGazes;
	XrEyePupilDataHTC_t775E19D45C8485C893FD291C9F8194CB59160213 ___m_pupilData;
	XrEyeGeometricDataHTC_tC2F903C50B430CDFDCE42B328646E5F19929C6CB ___m_eyeGeometricData;
};
struct SphereControl_tECCC958757C50C07C60A0C29364FDF3734934843  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___RightSphere;
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___LeftSphere;
	InputActionReference_t64730C6B41271E0983FC21BFB416169F5D6BC4A1* ___TriggerR;
};
struct UISwap_t1566756761DF5D095F3E89F6F960635CFCA36AED  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___Focused;
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___Distracted;
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___GazeTrigger;
	RuntimeObject* ___focusEffect;
};
struct UpdateLeftEyeGeometric_t436A987A8B4940F3946127A04CEF264E5C9ADFD0  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
};
struct UpdateLeftGaze_t29AEEF8C212103AB4C6BDC8A0278B3348944B4CB  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
};
struct UpdateLeftPupil_t34F9E3A9EB44660D3F4704A3754576F4879F930C  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
};
struct UpdateRightEyeGeometric_t94E0DBDA782988C8A25AEE08F79B9B0002E7693D  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
};
struct UpdateRightGaze_t22DB02558C008C31DB4679D0FA1D47A12C256666  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
};
struct UpdateRightPupil_tD6654BBDC60B3BCE50324C6B5E97614E44D2F05B  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
};
struct Volume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	bool ___m_IsGlobal;
	float ___priority;
	float ___blendDistance;
	float ___weight;
	VolumeProfile_t9B5F2005F575A710F38A124EF81A6228CCACACE1* ___sharedProfile;
	List_1_t58F89DEDCD7DABB0CFB009AAD9C0CFE061592252* ___m_Colliders;
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___m_CachedGameObject;
	int32_t ___m_PreviousLayer;
	float ___m_PreviousPriority;
	VolumeProfile_t9B5F2005F575A710F38A124EF81A6228CCACACE1* ___m_InternalProfile;
};
struct U3CPrivateImplementationDetailsU3E_t0F5473E849A5A5185A9F4C5246F0C32816C49FCA_StaticFields
{
	__StaticArrayInitTypeSizeU3D404_tF4DBCC53E3B0C5CC8D3C5222F8FFB7116F8879ED ___85D7278D419C72BF2DEB441B46289F21373660B0A1DB767934BA021ADE535AEE;
	__StaticArrayInitTypeSizeU3D663_tFE041C7CCF4E687DD084B48E8C2A902F295FC778 ___CDBAB22DCDC18D8C38C5A43786143948C2F22B5E15277197F82E7141ECDB1C11;
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974_StaticFields
{
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___identityQuaternion;
};
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___zeroVector;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___oneVector;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___upVector;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___downVector;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___leftVector;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rightVector;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___forwardVector;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___backVector;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___positiveInfinityVector;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___negativeInfinityVector;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject;
};
struct Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184_StaticFields
{
	CameraCallback_t844E527BFE37BC0495E7F67993E43C07642DA9DD* ___onPreCull;
	CameraCallback_t844E527BFE37BC0495E7F67993E43C07642DA9DD* ___onPreRender;
	CameraCallback_t844E527BFE37BC0495E7F67993E43C07642DA9DD* ___onPostRender;
};
struct InputAction_t1B550AD2B55AF322AFB53CD28DA64081220D01CD_StaticFields
{
	ProfilerMarker_tA256E18DA86EDBC5528CE066FC91C96EE86501AD ___k_InputActionEnableProfilerMarker;
	ProfilerMarker_tA256E18DA86EDBC5528CE066FC91C96EE86501AD ___k_InputActionDisableProfilerMarker;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct XrSingleEyeGeometricDataHTCU5BU5D_t90FECCAA11F87536897187EA64024AECC7AAA12B  : public RuntimeArray
{
	ALIGN_FIELD (8) XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 m_Items[1];

	inline XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 value)
	{
		m_Items[index] = value;
	}
};
struct XrSingleEyeGazeDataHTCU5BU5D_t8D4B6A3A120DF3744FF941025D21175D2EC0428A  : public RuntimeArray
{
	ALIGN_FIELD (8) XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E m_Items[1];

	inline XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E value)
	{
		m_Items[index] = value;
	}
};
struct XrSingleEyePupilDataHTCU5BU5D_tF46D1C63585D0762116A95FF45EB5FE4DA3CF532  : public RuntimeArray
{
	ALIGN_FIELD (8) XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC m_Items[1];

	inline XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC value)
	{
		m_Items[index] = value;
	}
};
struct IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6  : public RuntimeArray
{
	ALIGN_FIELD (8) RuntimeObject* m_Items[1];

	inline RuntimeObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, RuntimeObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline RuntimeObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, RuntimeObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, uint8_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline uint8_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, uint8_t value)
	{
		m_Items[index] = value;
	}
};


IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float InputAction_ReadValue_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m37FC749080A83C05777D1F779F38B8A27BAFA97B_gshared (InputAction_t1B550AD2B55AF322AFB53CD28DA64081220D01CD* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void VolumeParameter_1_Override_mC346E68FEB631DC3D367126E958B371099C991E0_gshared (VolumeParameter_1_t18B35E30089EFE0C2751A53FE6143F972EC9F9B8* __this, float ___0_x, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Object_FindAnyObjectByType_TisRuntimeObject_m7D4F7F238C71E2755C59C772E595368B03B9F9BE_gshared (int32_t ___0_findObjectsInactive, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool VolumeProfile_TryGet_TisRuntimeObject_m6394BC3A9A95358ECE114A783689654E1E2DAC44_gshared (VolumeProfile_t9B5F2005F575A710F38A124EF81A6228CCACACE1* __this, RuntimeObject** ___0_component, const RuntimeMethod* method) ;

IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR InputAction_t1B550AD2B55AF322AFB53CD28DA64081220D01CD* InputActionReference_get_action_m395EDEA6A93B54555D22323FDA6E1B1E931CE6EF (InputActionReference_t64730C6B41271E0983FC21BFB416169F5D6BC4A1* __this, const RuntimeMethod* method) ;
inline float InputAction_ReadValue_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m37FC749080A83C05777D1F779F38B8A27BAFA97B (InputAction_t1B550AD2B55AF322AFB53CD28DA64081220D01CD* __this, const RuntimeMethod* method)
{
	return ((  float (*) (InputAction_t1B550AD2B55AF322AFB53CD28DA64081220D01CD*, const RuntimeMethod*))InputAction_ReadValue_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m37FC749080A83C05777D1F779F38B8A27BAFA97B_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GameObject_SetActive_m638E92E1E75E519E5B24CF150B08CA8E0CDFAB92 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, bool ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XR_HTC_eye_tracker_defs_tD0DA3CB49A68FE8A0A556D6FF6B02CCE35043178* XR_HTC_eye_tracker_get_Interop_m60324318612CAA6B79D6E96937F98D5E7FC978F8 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool XrBool32_op_Implicit_mCAB79FC4DEA0C3928AC775D9C24BB7F1752B70CD (XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 ___0_equatable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371 (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 OpenXRHelper_ToUnityVector_m03FEFEB90A27066A97FFA57E1C163DF522372ED9 (XrVector3f_t5A6C3732E24CADBBE5C99FC58D6A1C17E67C9AC1 ___0_xrVec, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156 (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 OpenXRHelper_ToUnityQuaternion_m19B2BEF4CCBD41E59D0E3963D5A1B6259C98BF9C (XrQuaternionf_tCAC179EA55B9A02857B046051F3E115E926E1837 ___0_xrQuat, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ScriptableObject__ctor_mD037FDB0B487295EA47F79A4DB1BF1846C9087FF (ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LogStrengthEffect__ctor_m217F0B971A45BDA6275C3E0B7BD49132A8DDFAF9 (LogStrengthEffect_tD96784BDDB9D6BB7D175A37049EEE511A025E009* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Color__ctor_mCD6889CDE39F18704CD6EA8E2EFBFA48BA3E13B0_inline (Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* __this, float ___0_r, float ___1_g, float ___2_b, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BackgroundColorEffect__ctor_mA62C2D38AEB4954F7F1AAB5C892987273361DA7E (BackgroundColorEffect_t1180F0D3625E7EB5CCB01922FD4B090D1B8C2086* __this, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___0_color, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void VignetteEffect__ctor_mFDA42B78E6C96AE92BEB7DBDF1E0605373941890 (VignetteEffect_t4D2F583DF4E25F5E3B9A45F740DC17D4B18F058F* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnionEffect__ctor_mB7C9EDB10A3F96C0BBDC3F074486C2991DBF7E58 (UnionEffect_t37D758ED9E7C2676C2DF98C7E4C2A01CF88FB46C* __this, IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6* ___0_effects, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LinearEffect__ctor_m032F01336356FC5B1A26A343BBAA53786E23C417 (LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* __this, MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* ___0_coroutineTrigger, RuntimeObject* ___1_effect, float ___2_effectLength, float ___3_tickDuration, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B (RuntimeArray* ___0_array, RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 ___1_fldHandle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Convert_ToSingle_mF6ADEF60A6A97E9E7E410D8D15B26F2D5995151E (double ___0_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Color__ctor_m3786F0D6E510D9CFA544523A955870BD2A514C8C_inline (Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* __this, float ___0_r, float ___1_g, float ___2_b, float ___3_a, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* Camera_get_main_m52C992F18E05355ABB9EEB64A4BF2215E12762DF (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Camera_set_backgroundColor_m036FD8C316A93A0B168ACC89AFF16D396B872138 (Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* __this, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MonoBehaviour_StopAllCoroutines_m872033451D42013A99867D09337490017E9ED318 (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* LinearEffect_ApplyLinearEffect_m43EBBB28B5D80C2651A471E8122492A095452F5D (LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* __this, double ___0_strength, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* MonoBehaviour_StartCoroutine_m4CAFF732AA28CD3BDC5363B44A863575530EC812 (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* __this, RuntimeObject* ___0_routine, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CApplyLinearEffectU3Ed__8__ctor_mF5D90255DF52826C5CC7FA361199FDD0F7744370 (U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83* __this, int32_t ___0_U3CU3E1__state, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WaitForSeconds__ctor_m579F95BADEDBAB4B3A7E302C6EE3995926EF2EFC (WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3* __this, float ___0_seconds, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double Math_Min_mA3310F1FF7876DA2FC7F37B822E6DD66410565C1 (double ___0_val1, double ___1_val2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double Math_Max_m7BAC743E1752A51F258BB82DEBDD13E7C6D3ED26 (double ___0_val1, double ___1_val2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NotSupportedException__ctor_m1398D0CDE19B36AA3DE9392879738C1EA2439CDF (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Double_ToString_m7499A5D792419537DCB9470A3675CEF5117DE339 (double* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m9E3155FB84015C823606188F53B47CB44C444991 (String_t* ___0_str0, String_t* ___1_str1, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void VignetteEffect_TryFindVignette_m1E49C5314CC03573E00A08A8509280FAC3B7ED81 (VignetteEffect_t4D2F583DF4E25F5E3B9A45F740DC17D4B18F058F* __this, const RuntimeMethod* method) ;
inline void VolumeParameter_1_Override_mC346E68FEB631DC3D367126E958B371099C991E0 (VolumeParameter_1_t18B35E30089EFE0C2751A53FE6143F972EC9F9B8* __this, float ___0_x, const RuntimeMethod* method)
{
	((  void (*) (VolumeParameter_1_t18B35E30089EFE0C2751A53FE6143F972EC9F9B8*, float, const RuntimeMethod*))VolumeParameter_1_Override_mC346E68FEB631DC3D367126E958B371099C991E0_gshared)(__this, ___0_x, method);
}
inline Volume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377* Object_FindAnyObjectByType_TisVolume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377_mDF2A08B67822C1ABAB89B34865D4B6D72E2F7170 (int32_t ___0_findObjectsInactive, const RuntimeMethod* method)
{
	return ((  Volume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377* (*) (int32_t, const RuntimeMethod*))Object_FindAnyObjectByType_TisRuntimeObject_m7D4F7F238C71E2755C59C772E595368B03B9F9BE_gshared)(___0_findObjectsInactive, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR VolumeProfile_t9B5F2005F575A710F38A124EF81A6228CCACACE1* Volume_get_profile_mB157C4D67D52CE6D3E8211D6587B0EF71102E43D (Volume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377* __this, const RuntimeMethod* method) ;
inline bool VolumeProfile_TryGet_TisVignette_t77147DD5FEEB4476AF22BD98255F8010738985DC_mE8244CE93377BC8821F5278FE70551C4B60D7D7E (VolumeProfile_t9B5F2005F575A710F38A124EF81A6228CCACACE1* __this, Vignette_t77147DD5FEEB4476AF22BD98255F8010738985DC** ___0_component, const RuntimeMethod* method)
{
	return ((  bool (*) (VolumeProfile_t9B5F2005F575A710F38A124EF81A6228CCACACE1*, Vignette_t77147DD5FEEB4476AF22BD98255F8010738985DC**, const RuntimeMethod*))VolumeProfile_TryGet_TisRuntimeObject_m6394BC3A9A95358ECE114A783689654E1E2DAC44_gshared)(__this, ___0_component, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88563
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereControl_Update_m2C7B52D664DF3115E65EC3570025FC244B4D3FD0 (SphereControl_tECCC958757C50C07C60A0C29364FDF3734934843* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&InputAction_ReadValue_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m37FC749080A83C05777D1F779F38B8A27BAFA97B_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/SphereControl.cs:13>
		InputActionReference_t64730C6B41271E0983FC21BFB416169F5D6BC4A1* L_0 = __this->___TriggerR;
		NullCheck(L_0);
		InputAction_t1B550AD2B55AF322AFB53CD28DA64081220D01CD* L_1;
		L_1 = InputActionReference_get_action_m395EDEA6A93B54555D22323FDA6E1B1E931CE6EF(L_0, NULL);
		NullCheck(L_1);
		float L_2;
		L_2 = InputAction_ReadValue_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m37FC749080A83C05777D1F779F38B8A27BAFA97B(L_1, InputAction_ReadValue_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m37FC749080A83C05777D1F779F38B8A27BAFA97B_RuntimeMethod_var);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/SphereControl.cs:14>
		if (!((((float)L_2) > ((float)(0.5f)))? 1 : 0))
		{
			goto IL_0032;
		}
	}
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/SphereControl.cs:16>
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_3 = __this->___RightSphere;
		NullCheck(L_3);
		GameObject_SetActive_m638E92E1E75E519E5B24CF150B08CA8E0CDFAB92(L_3, (bool)0, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/SphereControl.cs:17>
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_4 = __this->___LeftSphere;
		NullCheck(L_4);
		GameObject_SetActive_m638E92E1E75E519E5B24CF150B08CA8E0CDFAB92(L_4, (bool)0, NULL);
		return;
	}

IL_0032:
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/SphereControl.cs:20>
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_5 = __this->___LeftSphere;
		NullCheck(L_5);
		GameObject_SetActive_m638E92E1E75E519E5B24CF150B08CA8E0CDFAB92(L_5, (bool)1, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/SphereControl.cs:21>
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_6 = __this->___RightSphere;
		NullCheck(L_6);
		GameObject_SetActive_m638E92E1E75E519E5B24CF150B08CA8E0CDFAB92(L_6, (bool)1, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/SphereControl.cs:23>
		return;
	}
}
// Method Definition Index: 88564
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereControl__ctor_mCA64CF5625A23BAC067A35057FE3B3578CD6C4BD (SphereControl_tECCC958757C50C07C60A0C29364FDF3734934843* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88565
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateLeftEyeGeometric_Start_mC178D45B9032015A2D6A30C9FD9DEC80D6AD8558 (UpdateLeftEyeGeometric_t436A987A8B4940F3946127A04CEF264E5C9ADFD0* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftEyeGeometric.cs:11>
		return;
	}
}
// Method Definition Index: 88566
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateLeftEyeGeometric_Update_m4D5C5F185D8F99707A1C6764B6BC0EED8BB1E5CB (UpdateLeftEyeGeometric_t436A987A8B4940F3946127A04CEF264E5C9ADFD0* __this, const RuntimeMethod* method) 
{
	XrSingleEyeGeometricDataHTCU5BU5D_t90FECCAA11F87536897187EA64024AECC7AAA12B* V_0 = NULL;
	XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftEyeGeometric.cs:16>
		XR_HTC_eye_tracker_defs_tD0DA3CB49A68FE8A0A556D6FF6B02CCE35043178* L_0;
		L_0 = XR_HTC_eye_tracker_get_Interop_m60324318612CAA6B79D6E96937F98D5E7FC978F8(NULL);
		NullCheck(L_0);
		bool L_1;
		L_1 = VirtualFuncInvoker1< bool, XrSingleEyeGeometricDataHTCU5BU5D_t90FECCAA11F87536897187EA64024AECC7AAA12B** >::Invoke(11, L_0, (&V_0));
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftEyeGeometric.cs:17>
		XrSingleEyeGeometricDataHTCU5BU5D_t90FECCAA11F87536897187EA64024AECC7AAA12B* L_2 = V_0;
		NullCheck(L_2);
		int32_t L_3 = 0;
		XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 L_4 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_3));
		V_1 = L_4;
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftEyeGeometric.cs:18>
		XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 L_5 = V_1;
		XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 L_6 = L_5.___isValid;
		bool L_7;
		L_7 = XrBool32_op_Implicit_mCAB79FC4DEA0C3928AC775D9C24BB7F1752B70CD(L_6, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftEyeGeometric.cs:25>
		return;
	}
}
// Method Definition Index: 88567
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateLeftEyeGeometric__ctor_m323DAB4A153F039AD77892CF8062077812A2C3CC (UpdateLeftEyeGeometric_t436A987A8B4940F3946127A04CEF264E5C9ADFD0* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88568
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateLeftGaze_Start_m8CADBB15A3FFB6209444525E8C7EDC80FF18FFAB (UpdateLeftGaze_t29AEEF8C212103AB4C6BDC8A0278B3348944B4CB* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftGaze.cs:11>
		return;
	}
}
// Method Definition Index: 88569
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateLeftGaze_Update_m5E6ABDFB7C96095A361DC9B8F98FBED0AFE1783F (UpdateLeftGaze_t29AEEF8C212103AB4C6BDC8A0278B3348944B4CB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OpenXRHelper_t4BE36310EB51760ADFEF668D8C1E7C00DF452063_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	XrSingleEyeGazeDataHTCU5BU5D_t8D4B6A3A120DF3744FF941025D21175D2EC0428A* V_0 = NULL;
	XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftGaze.cs:16>
		XR_HTC_eye_tracker_defs_tD0DA3CB49A68FE8A0A556D6FF6B02CCE35043178* L_0;
		L_0 = XR_HTC_eye_tracker_get_Interop_m60324318612CAA6B79D6E96937F98D5E7FC978F8(NULL);
		NullCheck(L_0);
		bool L_1;
		L_1 = VirtualFuncInvoker1< bool, XrSingleEyeGazeDataHTCU5BU5D_t8D4B6A3A120DF3744FF941025D21175D2EC0428A** >::Invoke(7, L_0, (&V_0));
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftGaze.cs:17>
		XrSingleEyeGazeDataHTCU5BU5D_t8D4B6A3A120DF3744FF941025D21175D2EC0428A* L_2 = V_0;
		NullCheck(L_2);
		int32_t L_3 = 0;
		XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E L_4 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_3));
		V_1 = L_4;
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftGaze.cs:18>
		XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E L_5 = V_1;
		XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 L_6 = L_5.___isValid;
		bool L_7;
		L_7 = XrBool32_op_Implicit_mCAB79FC4DEA0C3928AC775D9C24BB7F1752B70CD(L_6, NULL);
		if (!L_7)
		{
			goto IL_0058;
		}
	}
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftGaze.cs:20>
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_8;
		L_8 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(__this, NULL);
		XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E L_9 = V_1;
		XrPosef_t0821C076127F41D248AA6A56C7EB9D2A36BD34D9 L_10 = L_9.___gazePose;
		XrVector3f_t5A6C3732E24CADBBE5C99FC58D6A1C17E67C9AC1 L_11 = L_10.___position;
		il2cpp_codegen_runtime_class_init_inline(OpenXRHelper_t4BE36310EB51760ADFEF668D8C1E7C00DF452063_il2cpp_TypeInfo_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
		L_12 = OpenXRHelper_ToUnityVector_m03FEFEB90A27066A97FFA57E1C163DF522372ED9(L_11, NULL);
		NullCheck(L_8);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_8, L_12, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftGaze.cs:21>
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_13;
		L_13 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(__this, NULL);
		XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E L_14 = V_1;
		XrPosef_t0821C076127F41D248AA6A56C7EB9D2A36BD34D9 L_15 = L_14.___gazePose;
		XrQuaternionf_tCAC179EA55B9A02857B046051F3E115E926E1837 L_16 = L_15.___orientation;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_17;
		L_17 = OpenXRHelper_ToUnityQuaternion_m19B2BEF4CCBD41E59D0E3963D5A1B6259C98BF9C(L_16, NULL);
		NullCheck(L_13);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_13, L_17, NULL);
	}

IL_0058:
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftGaze.cs:23>
		return;
	}
}
// Method Definition Index: 88570
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateLeftGaze__ctor_mE81094D4624084E0A869B1A8F222241D43054CF3 (UpdateLeftGaze_t29AEEF8C212103AB4C6BDC8A0278B3348944B4CB* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88571
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateLeftPupil_Start_m235BE0E3B8D03607C146A56AD807CD564938DA57 (UpdateLeftPupil_t34F9E3A9EB44660D3F4704A3754576F4879F930C* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftPupil.cs:11>
		return;
	}
}
// Method Definition Index: 88572
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateLeftPupil_Update_mA442A8E3E0C9CEF5C8F4F570420B8B450937C59B (UpdateLeftPupil_t34F9E3A9EB44660D3F4704A3754576F4879F930C* __this, const RuntimeMethod* method) 
{
	XrSingleEyePupilDataHTCU5BU5D_tF46D1C63585D0762116A95FF45EB5FE4DA3CF532* V_0 = NULL;
	XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftPupil.cs:16>
		XR_HTC_eye_tracker_defs_tD0DA3CB49A68FE8A0A556D6FF6B02CCE35043178* L_0;
		L_0 = XR_HTC_eye_tracker_get_Interop_m60324318612CAA6B79D6E96937F98D5E7FC978F8(NULL);
		NullCheck(L_0);
		bool L_1;
		L_1 = VirtualFuncInvoker1< bool, XrSingleEyePupilDataHTCU5BU5D_tF46D1C63585D0762116A95FF45EB5FE4DA3CF532** >::Invoke(9, L_0, (&V_0));
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftPupil.cs:17>
		XrSingleEyePupilDataHTCU5BU5D_tF46D1C63585D0762116A95FF45EB5FE4DA3CF532* L_2 = V_0;
		NullCheck(L_2);
		int32_t L_3 = 0;
		XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC L_4 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_3));
		V_1 = L_4;
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftPupil.cs:18>
		XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC L_5 = V_1;
		XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 L_6 = L_5.___isDiameterValid;
		bool L_7;
		L_7 = XrBool32_op_Implicit_mCAB79FC4DEA0C3928AC775D9C24BB7F1752B70CD(L_6, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftPupil.cs:23>
		XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC L_8 = V_1;
		XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 L_9 = L_8.___isPositionValid;
		bool L_10;
		L_10 = XrBool32_op_Implicit_mCAB79FC4DEA0C3928AC775D9C24BB7F1752B70CD(L_9, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateLeftPupil.cs:29>
		return;
	}
}
// Method Definition Index: 88573
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateLeftPupil__ctor_m9213D951898626DC58F0CA5B518FF4C262B0BA62 (UpdateLeftPupil_t34F9E3A9EB44660D3F4704A3754576F4879F930C* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88574
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateRightEyeGeometric_Start_m77405C7B35BBF82A9C0D3C5874C274DE119F82B5 (UpdateRightEyeGeometric_t94E0DBDA782988C8A25AEE08F79B9B0002E7693D* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightEyeGeometric.cs:11>
		return;
	}
}
// Method Definition Index: 88575
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateRightEyeGeometric_Update_m41FB79D2ABF1D62B5B9EE816B60EB13E4105DB27 (UpdateRightEyeGeometric_t94E0DBDA782988C8A25AEE08F79B9B0002E7693D* __this, const RuntimeMethod* method) 
{
	XrSingleEyeGeometricDataHTCU5BU5D_t90FECCAA11F87536897187EA64024AECC7AAA12B* V_0 = NULL;
	XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightEyeGeometric.cs:16>
		XR_HTC_eye_tracker_defs_tD0DA3CB49A68FE8A0A556D6FF6B02CCE35043178* L_0;
		L_0 = XR_HTC_eye_tracker_get_Interop_m60324318612CAA6B79D6E96937F98D5E7FC978F8(NULL);
		NullCheck(L_0);
		bool L_1;
		L_1 = VirtualFuncInvoker1< bool, XrSingleEyeGeometricDataHTCU5BU5D_t90FECCAA11F87536897187EA64024AECC7AAA12B** >::Invoke(11, L_0, (&V_0));
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightEyeGeometric.cs:17>
		XrSingleEyeGeometricDataHTCU5BU5D_t90FECCAA11F87536897187EA64024AECC7AAA12B* L_2 = V_0;
		NullCheck(L_2);
		int32_t L_3 = 1;
		XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 L_4 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_3));
		V_1 = L_4;
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightEyeGeometric.cs:18>
		XrSingleEyeGeometricDataHTC_tD5E62379D78B54BC3C34A063DF7BFACC021EF138 L_5 = V_1;
		XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 L_6 = L_5.___isValid;
		bool L_7;
		L_7 = XrBool32_op_Implicit_mCAB79FC4DEA0C3928AC775D9C24BB7F1752B70CD(L_6, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightEyeGeometric.cs:25>
		return;
	}
}
// Method Definition Index: 88576
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateRightEyeGeometric__ctor_m3F2B6EDB0F23CCA1062C96823B723770AF6D56DD (UpdateRightEyeGeometric_t94E0DBDA782988C8A25AEE08F79B9B0002E7693D* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88577
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateRightGaze_Start_m89A8760FE4FB36405DC13AFC63FB607CA75AB1E8 (UpdateRightGaze_t22DB02558C008C31DB4679D0FA1D47A12C256666* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightGaze.cs:11>
		return;
	}
}
// Method Definition Index: 88578
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateRightGaze_Update_m514E736BAE68214CCE4B0ECF3474474D3AE70CBE (UpdateRightGaze_t22DB02558C008C31DB4679D0FA1D47A12C256666* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OpenXRHelper_t4BE36310EB51760ADFEF668D8C1E7C00DF452063_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	XrSingleEyeGazeDataHTCU5BU5D_t8D4B6A3A120DF3744FF941025D21175D2EC0428A* V_0 = NULL;
	XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightGaze.cs:16>
		XR_HTC_eye_tracker_defs_tD0DA3CB49A68FE8A0A556D6FF6B02CCE35043178* L_0;
		L_0 = XR_HTC_eye_tracker_get_Interop_m60324318612CAA6B79D6E96937F98D5E7FC978F8(NULL);
		NullCheck(L_0);
		bool L_1;
		L_1 = VirtualFuncInvoker1< bool, XrSingleEyeGazeDataHTCU5BU5D_t8D4B6A3A120DF3744FF941025D21175D2EC0428A** >::Invoke(7, L_0, (&V_0));
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightGaze.cs:17>
		XrSingleEyeGazeDataHTCU5BU5D_t8D4B6A3A120DF3744FF941025D21175D2EC0428A* L_2 = V_0;
		NullCheck(L_2);
		int32_t L_3 = 1;
		XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E L_4 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_3));
		V_1 = L_4;
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightGaze.cs:18>
		XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E L_5 = V_1;
		XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 L_6 = L_5.___isValid;
		bool L_7;
		L_7 = XrBool32_op_Implicit_mCAB79FC4DEA0C3928AC775D9C24BB7F1752B70CD(L_6, NULL);
		if (!L_7)
		{
			goto IL_0058;
		}
	}
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightGaze.cs:20>
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_8;
		L_8 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(__this, NULL);
		XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E L_9 = V_1;
		XrPosef_t0821C076127F41D248AA6A56C7EB9D2A36BD34D9 L_10 = L_9.___gazePose;
		XrVector3f_t5A6C3732E24CADBBE5C99FC58D6A1C17E67C9AC1 L_11 = L_10.___position;
		il2cpp_codegen_runtime_class_init_inline(OpenXRHelper_t4BE36310EB51760ADFEF668D8C1E7C00DF452063_il2cpp_TypeInfo_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
		L_12 = OpenXRHelper_ToUnityVector_m03FEFEB90A27066A97FFA57E1C163DF522372ED9(L_11, NULL);
		NullCheck(L_8);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_8, L_12, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightGaze.cs:21>
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_13;
		L_13 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(__this, NULL);
		XrSingleEyeGazeDataHTC_tCB814CBF112A1183CC3205D39A9EB3DAFC3FB73E L_14 = V_1;
		XrPosef_t0821C076127F41D248AA6A56C7EB9D2A36BD34D9 L_15 = L_14.___gazePose;
		XrQuaternionf_tCAC179EA55B9A02857B046051F3E115E926E1837 L_16 = L_15.___orientation;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_17;
		L_17 = OpenXRHelper_ToUnityQuaternion_m19B2BEF4CCBD41E59D0E3963D5A1B6259C98BF9C(L_16, NULL);
		NullCheck(L_13);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_13, L_17, NULL);
	}

IL_0058:
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightGaze.cs:23>
		return;
	}
}
// Method Definition Index: 88579
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateRightGaze__ctor_m579D2C300ADB77B3982AAC67CC4426D10B825438 (UpdateRightGaze_t22DB02558C008C31DB4679D0FA1D47A12C256666* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88580
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateRightPupil_Start_mDFAD8C49C2F17344B2F08CF84BE5036F7442FC2D (UpdateRightPupil_tD6654BBDC60B3BCE50324C6B5E97614E44D2F05B* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightPupil.cs:11>
		return;
	}
}
// Method Definition Index: 88581
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateRightPupil_Update_m18E5BFDED3F61E9CB72791E30E6D380C8832FEDC (UpdateRightPupil_tD6654BBDC60B3BCE50324C6B5E97614E44D2F05B* __this, const RuntimeMethod* method) 
{
	XrSingleEyePupilDataHTCU5BU5D_tF46D1C63585D0762116A95FF45EB5FE4DA3CF532* V_0 = NULL;
	XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightPupil.cs:16>
		XR_HTC_eye_tracker_defs_tD0DA3CB49A68FE8A0A556D6FF6B02CCE35043178* L_0;
		L_0 = XR_HTC_eye_tracker_get_Interop_m60324318612CAA6B79D6E96937F98D5E7FC978F8(NULL);
		NullCheck(L_0);
		bool L_1;
		L_1 = VirtualFuncInvoker1< bool, XrSingleEyePupilDataHTCU5BU5D_tF46D1C63585D0762116A95FF45EB5FE4DA3CF532** >::Invoke(9, L_0, (&V_0));
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightPupil.cs:17>
		XrSingleEyePupilDataHTCU5BU5D_tF46D1C63585D0762116A95FF45EB5FE4DA3CF532* L_2 = V_0;
		NullCheck(L_2);
		int32_t L_3 = 1;
		XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC L_4 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_3));
		V_1 = L_4;
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightPupil.cs:18>
		XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC L_5 = V_1;
		XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 L_6 = L_5.___isDiameterValid;
		bool L_7;
		L_7 = XrBool32_op_Implicit_mCAB79FC4DEA0C3928AC775D9C24BB7F1752B70CD(L_6, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightPupil.cs:23>
		XrSingleEyePupilDataHTC_t45D05A886690D1BC02D505C9AFD2FCA6D9128FCC L_8 = V_1;
		XrBool32_t097064783F5A5A7A7CDD07E19DDA8E22D1FECEE6 L_9 = L_8.___isPositionValid;
		bool L_10;
		L_10 = XrBool32_op_Implicit_mCAB79FC4DEA0C3928AC775D9C24BB7F1752B70CD(L_9, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/Eye Tracking/UpdateRightPupil.cs:29>
		return;
	}
}
// Method Definition Index: 88582
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdateRightPupil__ctor_mCA147073986A49EA6D1C122221CD310ABAFDB699 (UpdateRightPupil_tD6654BBDC60B3BCE50324C6B5E97614E44D2F05B* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88583
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Readme__ctor_m69C325C4C171DCB0312B646A9034AA91EA8C39C6 (Readme_tE17B99201D0F52BD5727638AD3F41072A65B3BBB* __this, const RuntimeMethod* method) 
{
	{
		ScriptableObject__ctor_mD037FDB0B487295EA47F79A4DB1BF1846C9087FF(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88584
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Section__ctor_m5F732533E4DFC0167D965E5F5DB332E46055399B (Section_t50C894D0A717C2368EBAAE5477D4E8626D0B5401* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88585
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UISwap__ctor_mF5B315ACED056A69F26B10EC8D5223FE58436B3E (UISwap_t1566756761DF5D095F3E89F6F960635CFCA36AED* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BackgroundColorEffect_t1180F0D3625E7EB5CCB01922FD4B090D1B8C2086_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LogStrengthEffect_tD96784BDDB9D6BB7D175A37049EEE511A025E009_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnionEffect_t37D758ED9E7C2676C2DF98C7E4C2A01CF88FB46C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&VignetteEffect_t4D2F583DF4E25F5E3B9A45F740DC17D4B18F058F_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UISwap.cs:14>
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UISwap.cs:16>
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UISwap.cs:17>
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UISwap.cs:18>
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UISwap.cs:19>
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UISwap.cs:20>
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UISwap.cs:21>
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UISwap.cs:22>
		IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6* L_0 = (IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6*)(IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6*)SZArrayNew(IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6_il2cpp_TypeInfo_var, (uint32_t)3);
		IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6* L_1 = L_0;
		LogStrengthEffect_tD96784BDDB9D6BB7D175A37049EEE511A025E009* L_2 = (LogStrengthEffect_tD96784BDDB9D6BB7D175A37049EEE511A025E009*)il2cpp_codegen_object_new(LogStrengthEffect_tD96784BDDB9D6BB7D175A37049EEE511A025E009_il2cpp_TypeInfo_var);
		LogStrengthEffect__ctor_m217F0B971A45BDA6275C3E0B7BD49132A8DDFAF9(L_2, NULL);
		NullCheck(L_1);
		ArrayElementTypeCheck (L_1, L_2);
		(L_1)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_2);
		IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6* L_3 = L_1;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_4;
		memset((&L_4), 0, sizeof(L_4));
		Color__ctor_mCD6889CDE39F18704CD6EA8E2EFBFA48BA3E13B0_inline((&L_4), (0.0f), (0.588235319f), (0.509803951f), NULL);
		BackgroundColorEffect_t1180F0D3625E7EB5CCB01922FD4B090D1B8C2086* L_5 = (BackgroundColorEffect_t1180F0D3625E7EB5CCB01922FD4B090D1B8C2086*)il2cpp_codegen_object_new(BackgroundColorEffect_t1180F0D3625E7EB5CCB01922FD4B090D1B8C2086_il2cpp_TypeInfo_var);
		BackgroundColorEffect__ctor_mA62C2D38AEB4954F7F1AAB5C892987273361DA7E(L_5, L_4, NULL);
		NullCheck(L_3);
		ArrayElementTypeCheck (L_3, L_5);
		(L_3)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_5);
		IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6* L_6 = L_3;
		VignetteEffect_t4D2F583DF4E25F5E3B9A45F740DC17D4B18F058F* L_7 = (VignetteEffect_t4D2F583DF4E25F5E3B9A45F740DC17D4B18F058F*)il2cpp_codegen_object_new(VignetteEffect_t4D2F583DF4E25F5E3B9A45F740DC17D4B18F058F_il2cpp_TypeInfo_var);
		VignetteEffect__ctor_mFDA42B78E6C96AE92BEB7DBDF1E0605373941890(L_7, NULL);
		NullCheck(L_6);
		ArrayElementTypeCheck (L_6, L_7);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(2), (RuntimeObject*)L_7);
		UnionEffect_t37D758ED9E7C2676C2DF98C7E4C2A01CF88FB46C* L_8 = (UnionEffect_t37D758ED9E7C2676C2DF98C7E4C2A01CF88FB46C*)il2cpp_codegen_object_new(UnionEffect_t37D758ED9E7C2676C2DF98C7E4C2A01CF88FB46C_il2cpp_TypeInfo_var);
		UnionEffect__ctor_mB7C9EDB10A3F96C0BBDC3F074486C2991DBF7E58(L_8, L_6, NULL);
		LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* L_9 = (LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9*)il2cpp_codegen_object_new(LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9_il2cpp_TypeInfo_var);
		LinearEffect__ctor_m032F01336356FC5B1A26A343BBAA53786E23C417(L_9, __this, L_8, (5.0f), (0.0399999991f), NULL);
		__this->___focusEffect = L_9;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___focusEffect), (void*)L_9);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UISwap.cs:23>
		return;
	}
}
// Method Definition Index: 88586
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UISwap_LooksAtScreen_m3A9E90DBF63A89D216E1F34F078844B2B978F5EA (UISwap_t1566756761DF5D095F3E89F6F960635CFCA36AED* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IFocusEffect_t517DE4A02353DDF0DAC8AEC1CE2EB967EC943A21_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UISwap.cs:27>
		RuntimeObject* L_0 = __this->___focusEffect;
		NullCheck(L_0);
		InterfaceActionInvoker1< double >::Invoke(0, IFocusEffect_t517DE4A02353DDF0DAC8AEC1CE2EB967EC943A21_il2cpp_TypeInfo_var, L_0, (0.0));
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UISwap.cs:28>
		return;
	}
}
// Method Definition Index: 88587
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UISwap_LooksAway_m36BCD8A72A747BBC9D051198C8BEB2DB5D3778F8 (UISwap_t1566756761DF5D095F3E89F6F960635CFCA36AED* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IFocusEffect_t517DE4A02353DDF0DAC8AEC1CE2EB967EC943A21_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UISwap.cs:32>
		RuntimeObject* L_0 = __this->___focusEffect;
		NullCheck(L_0);
		InterfaceActionInvoker1< double >::Invoke(0, IFocusEffect_t517DE4A02353DDF0DAC8AEC1CE2EB967EC943A21_il2cpp_TypeInfo_var, L_0, (1.0));
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UISwap.cs:33>
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88588
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E UnitySourceGeneratedAssemblyMonoScriptTypes_v1_Get_mBEB95BEB954BB63E9710BBC7AD5E78C4CB0A0033 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CPrivateImplementationDetailsU3E_t0F5473E849A5A5185A9F4C5246F0C32816C49FCA____85D7278D419C72BF2DEB441B46289F21373660B0A1DB767934BA021ADE535AEE_FieldInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CPrivateImplementationDetailsU3E_t0F5473E849A5A5185A9F4C5246F0C32816C49FCA____CDBAB22DCDC18D8C38C5A43786143948C2F22B5E15277197F82E7141ECDB1C11_FieldInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		il2cpp_codegen_initobj((&V_0), sizeof(MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E));
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)SZArrayNew(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var, (uint32_t)((int32_t)663));
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_1 = L_0;
		RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 L_2 = { reinterpret_cast<intptr_t> (U3CPrivateImplementationDetailsU3E_t0F5473E849A5A5185A9F4C5246F0C32816C49FCA____CDBAB22DCDC18D8C38C5A43786143948C2F22B5E15277197F82E7141ECDB1C11_FieldInfo_var) };
		RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B((RuntimeArray*)L_1, L_2, NULL);
		(&V_0)->___FilePathsData = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&(&V_0)->___FilePathsData), (void*)L_1);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_3 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)SZArrayNew(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var, (uint32_t)((int32_t)404));
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_4 = L_3;
		RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 L_5 = { reinterpret_cast<intptr_t> (U3CPrivateImplementationDetailsU3E_t0F5473E849A5A5185A9F4C5246F0C32816C49FCA____85D7278D419C72BF2DEB441B46289F21373660B0A1DB767934BA021ADE535AEE_FieldInfo_var) };
		RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B((RuntimeArray*)L_4, L_5, NULL);
		(&V_0)->___TypesData = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&(&V_0)->___TypesData), (void*)L_4);
		(&V_0)->___TotalFiles = ((int32_t)15);
		(&V_0)->___TotalTypes = ((int32_t)16);
		(&V_0)->___IsEditorOnly = (bool)0;
		MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E L_6 = V_0;
		return L_6;
	}
}
// Method Definition Index: 88589
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnitySourceGeneratedAssemblyMonoScriptTypes_v1__ctor_mE70FB23ACC1EA12ABC948AA22C2E78B2D0AA39B1 (UnitySourceGeneratedAssemblyMonoScriptTypes_v1_tC95F24D0C6E6B77389433852BB389F39C692926E* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C void MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshal_pinvoke(const MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E& unmarshaled, MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshaled_pinvoke& marshaled)
{
	marshaled.___FilePathsData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___FilePathsData);
	marshaled.___TypesData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___TypesData);
	marshaled.___TotalTypes = unmarshaled.___TotalTypes;
	marshaled.___TotalFiles = unmarshaled.___TotalFiles;
	marshaled.___IsEditorOnly = static_cast<int32_t>(unmarshaled.___IsEditorOnly);
}
IL2CPP_EXTERN_C void MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshal_pinvoke_back(const MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshaled_pinvoke& marshaled, MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E& unmarshaled)
{
	unmarshaled.___FilePathsData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___FilePathsData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___FilePathsData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___FilePathsData));
	unmarshaled.___TypesData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___TypesData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___TypesData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___TypesData));
	int32_t unmarshaledTotalTypes_temp_2 = 0;
	unmarshaledTotalTypes_temp_2 = marshaled.___TotalTypes;
	unmarshaled.___TotalTypes = unmarshaledTotalTypes_temp_2;
	int32_t unmarshaledTotalFiles_temp_3 = 0;
	unmarshaledTotalFiles_temp_3 = marshaled.___TotalFiles;
	unmarshaled.___TotalFiles = unmarshaledTotalFiles_temp_3;
	bool unmarshaledIsEditorOnly_temp_4 = false;
	unmarshaledIsEditorOnly_temp_4 = static_cast<bool>(marshaled.___IsEditorOnly);
	unmarshaled.___IsEditorOnly = unmarshaledIsEditorOnly_temp_4;
}
IL2CPP_EXTERN_C void MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshal_pinvoke_cleanup(MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshaled_pinvoke& marshaled)
{
	il2cpp_codegen_com_destroy_safe_array(marshaled.___FilePathsData);
	marshaled.___FilePathsData = NULL;
	il2cpp_codegen_com_destroy_safe_array(marshaled.___TypesData);
	marshaled.___TypesData = NULL;
}
IL2CPP_EXTERN_C void MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshal_com(const MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E& unmarshaled, MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshaled_com& marshaled)
{
	marshaled.___FilePathsData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___FilePathsData);
	marshaled.___TypesData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___TypesData);
	marshaled.___TotalTypes = unmarshaled.___TotalTypes;
	marshaled.___TotalFiles = unmarshaled.___TotalFiles;
	marshaled.___IsEditorOnly = static_cast<int32_t>(unmarshaled.___IsEditorOnly);
}
IL2CPP_EXTERN_C void MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshal_com_back(const MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshaled_com& marshaled, MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E& unmarshaled)
{
	unmarshaled.___FilePathsData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___FilePathsData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___FilePathsData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___FilePathsData));
	unmarshaled.___TypesData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___TypesData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___TypesData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___TypesData));
	int32_t unmarshaledTotalTypes_temp_2 = 0;
	unmarshaledTotalTypes_temp_2 = marshaled.___TotalTypes;
	unmarshaled.___TotalTypes = unmarshaledTotalTypes_temp_2;
	int32_t unmarshaledTotalFiles_temp_3 = 0;
	unmarshaledTotalFiles_temp_3 = marshaled.___TotalFiles;
	unmarshaled.___TotalFiles = unmarshaledTotalFiles_temp_3;
	bool unmarshaledIsEditorOnly_temp_4 = false;
	unmarshaledIsEditorOnly_temp_4 = static_cast<bool>(marshaled.___IsEditorOnly);
	unmarshaled.___IsEditorOnly = unmarshaledIsEditorOnly_temp_4;
}
IL2CPP_EXTERN_C void MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshal_com_cleanup(MonoScriptData_t8F50E352855B96FFFC1D9CB07EACC90C99D73A3E_marshaled_com& marshaled)
{
	il2cpp_codegen_com_destroy_safe_array(marshaled.___FilePathsData);
	marshaled.___FilePathsData = NULL;
	il2cpp_codegen_com_destroy_safe_array(marshaled.___TypesData);
	marshaled.___TypesData = NULL;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88590
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BackgroundColorEffect__ctor_mA62C2D38AEB4954F7F1AAB5C892987273361DA7E (BackgroundColorEffect_t1180F0D3625E7EB5CCB01922FD4B090D1B8C2086* __this, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___0_color, const RuntimeMethod* method) 
{
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/BackgroundColorEffect.cs:9>
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/BackgroundColorEffect.cs:11>
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_0 = ___0_color;
		__this->____color = L_0;
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/BackgroundColorEffect.cs:12>
		return;
	}
}
// Method Definition Index: 88591
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BackgroundColorEffect_ApplyEffect_mC03EF6BCE3A88F4B9CEFC40C939FB359081013D9 (BackgroundColorEffect_t1180F0D3625E7EB5CCB01922FD4B090D1B8C2086* __this, double ___0_strength, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/BackgroundColorEffect.cs:16>
		double L_0 = ___0_strength;
		il2cpp_codegen_runtime_class_init_inline(Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		float L_1;
		L_1 = Convert_ToSingle_mF6ADEF60A6A97E9E7E410D8D15B26F2D5995151E(L_0, NULL);
		V_0 = L_1;
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/BackgroundColorEffect.cs:17>
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_2 = (Color_tD001788D726C3A7F1379BEED0260B9591F440C1F*)(&__this->____color);
		float L_3 = L_2->___r;
		float L_4 = V_0;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_5 = (Color_tD001788D726C3A7F1379BEED0260B9591F440C1F*)(&__this->____color);
		float L_6 = L_5->___g;
		float L_7 = V_0;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_8 = (Color_tD001788D726C3A7F1379BEED0260B9591F440C1F*)(&__this->____color);
		float L_9 = L_8->___b;
		float L_10 = V_0;
		float L_11 = V_0;
		Color__ctor_m3786F0D6E510D9CFA544523A955870BD2A514C8C_inline((&V_1), ((float)il2cpp_codegen_multiply(L_3, L_4)), ((float)il2cpp_codegen_multiply(L_6, L_7)), ((float)il2cpp_codegen_multiply(L_9, L_10)), L_11, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/BackgroundColorEffect.cs:18>
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_12;
		L_12 = Camera_get_main_m52C992F18E05355ABB9EEB64A4BF2215E12762DF(NULL);
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_13 = V_1;
		NullCheck(L_12);
		Camera_set_backgroundColor_m036FD8C316A93A0B168ACC89AFF16D396B872138(L_12, L_13, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/BackgroundColorEffect.cs:19>
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88593
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LinearEffect__ctor_m032F01336356FC5B1A26A343BBAA53786E23C417 (LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* __this, MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* ___0_coroutineTrigger, RuntimeObject* ___1_effect, float ___2_effectLength, float ___3_tickDuration, const RuntimeMethod* method) 
{
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:30>
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:32>
		MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* L_0 = ___0_coroutineTrigger;
		__this->____coroutineTrigger = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____coroutineTrigger), (void*)L_0);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:33>
		RuntimeObject* L_1 = ___1_effect;
		__this->____effect = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____effect), (void*)L_1);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:34>
		float L_2 = ___2_effectLength;
		__this->____effectLength = L_2;
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:35>
		float L_3 = ___3_tickDuration;
		__this->____tickDuration = L_3;
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:36>
		return;
	}
}
// Method Definition Index: 88594
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LinearEffect_ApplyEffect_m62D1F82687DF690E85E06EE87EB777CABF9DF608 (LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* __this, double ___0_strength, const RuntimeMethod* method) 
{
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:40>
		double L_0 = __this->___currentStrength;
		__this->___lastStrength = L_0;
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:41>
		MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* L_1 = __this->____coroutineTrigger;
		NullCheck(L_1);
		MonoBehaviour_StopAllCoroutines_m872033451D42013A99867D09337490017E9ED318(L_1, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:42>
		MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* L_2 = __this->____coroutineTrigger;
		double L_3 = ___0_strength;
		RuntimeObject* L_4;
		L_4 = LinearEffect_ApplyLinearEffect_m43EBBB28B5D80C2651A471E8122492A095452F5D(__this, L_3, NULL);
		NullCheck(L_2);
		Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* L_5;
		L_5 = MonoBehaviour_StartCoroutine_m4CAFF732AA28CD3BDC5363B44A863575530EC812(L_2, L_4, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:43>
		return;
	}
}
// Method Definition Index: 88595
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* LinearEffect_ApplyLinearEffect_m43EBBB28B5D80C2651A471E8122492A095452F5D (LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* __this, double ___0_strength, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83* L_0 = (U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83*)il2cpp_codegen_object_new(U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83_il2cpp_TypeInfo_var);
		U3CApplyLinearEffectU3Ed__8__ctor_mF5D90255DF52826C5CC7FA361199FDD0F7744370(L_0, 0, NULL);
		U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83* L_1 = L_0;
		NullCheck(L_1);
		L_1->___U3CU3E4__this = __this;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___U3CU3E4__this), (void*)__this);
		U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83* L_2 = L_1;
		double L_3 = ___0_strength;
		NullCheck(L_2);
		L_2->___strength = L_3;
		return L_2;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88596
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CApplyLinearEffectU3Ed__8__ctor_mF5D90255DF52826C5CC7FA361199FDD0F7744370 (U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83* __this, int32_t ___0_U3CU3E1__state, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		int32_t L_0 = ___0_U3CU3E1__state;
		__this->___U3CU3E1__state = L_0;
		return;
	}
}
// Method Definition Index: 88597
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CApplyLinearEffectU3Ed__8_System_IDisposable_Dispose_m0F643F022C7F398ACC6BD80C1460D06FFF423270 (U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
// Method Definition Index: 88598
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CApplyLinearEffectU3Ed__8_MoveNext_mCAF8B9E593C2DB1B4F81AB64C1E0C6E91FD225CF (U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IFocusEffect_t517DE4A02353DDF0DAC8AEC1CE2EB967EC943A21_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* V_1 = NULL;
	double V_2 = 0.0;
	double G_B7_0 = 0.0;
	double G_B6_0 = 0.0;
	int32_t G_B8_0 = 0;
	double G_B8_1 = 0.0;
	LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* G_B10_0 = NULL;
	LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* G_B9_0 = NULL;
	double G_B11_0 = 0.0;
	LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* G_B11_1 = NULL;
	{
		int32_t L_0 = __this->___U3CU3E1__state;
		V_0 = L_0;
		LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* L_1 = __this->___U3CU3E4__this;
		V_1 = L_1;
		int32_t L_2 = V_0;
		if (!L_2)
		{
			goto IL_0017;
		}
	}
	{
		int32_t L_3 = V_0;
		if ((((int32_t)L_3) == ((int32_t)1)))
		{
			goto IL_0061;
		}
	}
	{
		return (bool)0;
	}

IL_0017:
	{
		__this->___U3CU3E1__state = (-1);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:47>
		LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* L_4 = V_1;
		NullCheck(L_4);
		float L_5 = L_4->____effectLength;
		LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* L_6 = V_1;
		NullCheck(L_6);
		float L_7 = L_6->____tickDuration;
		__this->___U3CtickEffectDifferenceU3E5__2 = ((float)((1.0f)/((float)(L_5/L_7))));
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:48>
		__this->___U3CcurrentDifferenceU3E5__3 = (0.0f);
		goto IL_00dd;
	}

IL_0047:
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:52>
		LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* L_8 = V_1;
		NullCheck(L_8);
		float L_9 = L_8->____tickDuration;
		WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3* L_10 = (WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3*)il2cpp_codegen_object_new(WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3_il2cpp_TypeInfo_var);
		WaitForSeconds__ctor_m579F95BADEDBAB4B3A7E302C6EE3995926EF2EFC(L_10, L_9, NULL);
		__this->___U3CU3E2__current = L_10;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CU3E2__current), (void*)L_10);
		__this->___U3CU3E1__state = 1;
		return (bool)1;
	}

IL_0061:
	{
		__this->___U3CU3E1__state = (-1);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:53>
		float L_11 = __this->___U3CcurrentDifferenceU3E5__3;
		float L_12 = __this->___U3CtickEffectDifferenceU3E5__2;
		__this->___U3CcurrentDifferenceU3E5__3 = ((float)il2cpp_codegen_add(L_11, L_12));
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:55>
		LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* L_13 = V_1;
		NullCheck(L_13);
		double L_14 = L_13->___lastStrength;
		double L_15 = __this->___strength;
		LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* L_16 = V_1;
		NullCheck(L_16);
		double L_17 = L_16->___lastStrength;
		if ((((double)L_15) < ((double)L_17)))
		{
			G_B7_0 = L_14;
			goto IL_0092;
		}
		G_B6_0 = L_14;
	}
	{
		G_B8_0 = 1;
		G_B8_1 = G_B6_0;
		goto IL_0093;
	}

IL_0092:
	{
		G_B8_0 = (-1);
		G_B8_1 = G_B7_0;
	}

IL_0093:
	{
		float L_18 = __this->___U3CcurrentDifferenceU3E5__3;
		V_2 = ((double)il2cpp_codegen_add(G_B8_1, ((double)((float)il2cpp_codegen_multiply(((float)G_B8_0), L_18)))));
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:56>
		LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* L_19 = V_1;
		double L_20 = __this->___strength;
		LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* L_21 = V_1;
		NullCheck(L_21);
		double L_22 = L_21->___lastStrength;
		if ((((double)L_20) < ((double)L_22)))
		{
			G_B10_0 = L_19;
			goto IL_00bb;
		}
		G_B9_0 = L_19;
	}
	{
		double L_23 = __this->___strength;
		double L_24 = V_2;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_25;
		L_25 = Math_Min_mA3310F1FF7876DA2FC7F37B822E6DD66410565C1(L_23, L_24, NULL);
		G_B11_0 = L_25;
		G_B11_1 = G_B9_0;
		goto IL_00c7;
	}

IL_00bb:
	{
		double L_26 = __this->___strength;
		double L_27 = V_2;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_28;
		L_28 = Math_Max_m7BAC743E1752A51F258BB82DEBDD13E7C6D3ED26(L_26, L_27, NULL);
		G_B11_0 = L_28;
		G_B11_1 = G_B10_0;
	}

IL_00c7:
	{
		NullCheck(G_B11_1);
		G_B11_1->___currentStrength = G_B11_0;
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:57>
		LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* L_29 = V_1;
		NullCheck(L_29);
		RuntimeObject* L_30 = L_29->____effect;
		LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* L_31 = V_1;
		NullCheck(L_31);
		double L_32 = L_31->___currentStrength;
		NullCheck(L_30);
		InterfaceActionInvoker1< double >::Invoke(0, IFocusEffect_t517DE4A02353DDF0DAC8AEC1CE2EB967EC943A21_il2cpp_TypeInfo_var, L_30, L_32);
	}

IL_00dd:
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:50>
		float L_33 = __this->___U3CcurrentDifferenceU3E5__3;
		double L_34 = __this->___strength;
		LinearEffect_t4054B18E4CD941F04BF59B2F792E2EFF6E2CBBB9* L_35 = V_1;
		NullCheck(L_35);
		double L_36 = L_35->___lastStrength;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_37;
		L_37 = fabs(((double)il2cpp_codegen_subtract(L_34, L_36)));
		if ((((double)((double)L_33)) < ((double)L_37)))
		{
			goto IL_0047;
		}
	}
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LinearEffect.cs:59>
		return (bool)0;
	}
}
// Method Definition Index: 88599
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* U3CApplyLinearEffectU3Ed__8_System_Collections_Generic_IEnumeratorU3CSystem_ObjectU3E_get_Current_m25C691DA60164670C04C3BB9F9282BDD6B6C180F (U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___U3CU3E2__current;
		return L_0;
	}
}
// Method Definition Index: 88600
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CApplyLinearEffectU3Ed__8_System_Collections_IEnumerator_Reset_m659D61F25322F744AC5DDD6AE2A301BC7BA1B7A3 (U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83* __this, const RuntimeMethod* method) 
{
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_m1398D0CDE19B36AA3DE9392879738C1EA2439CDF(L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&U3CApplyLinearEffectU3Ed__8_System_Collections_IEnumerator_Reset_m659D61F25322F744AC5DDD6AE2A301BC7BA1B7A3_RuntimeMethod_var)));
	}
}
// Method Definition Index: 88601
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* U3CApplyLinearEffectU3Ed__8_System_Collections_IEnumerator_get_Current_m699BB61BC77AACE8A8CB091BB1F5883F2192C10B (U3CApplyLinearEffectU3Ed__8_t27C1F1B530F0B1E999C99FAA7E9894A5C8DF4D83* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___U3CU3E2__current;
		return L_0;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88602
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LogStrengthEffect_ApplyEffect_m34B5B3D3B4674124C44E9977D37D882E8A251F9E (LogStrengthEffect_tD96784BDDB9D6BB7D175A37049EEE511A025E009* __this, double ___0_strength, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2EA74FD1B45A765749965AACFEEC38B63CB85E56);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LogStrengthEffect.cs:9>
		String_t* L_0;
		L_0 = Double_ToString_m7499A5D792419537DCB9470A3675CEF5117DE339((&___0_strength), NULL);
		String_t* L_1;
		L_1 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral2EA74FD1B45A765749965AACFEEC38B63CB85E56, L_0, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(L_1, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/LogStrengthEffect.cs:10>
		return;
	}
}
// Method Definition Index: 88603
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LogStrengthEffect__ctor_m217F0B971A45BDA6275C3E0B7BD49132A8DDFAF9 (LogStrengthEffect_tD96784BDDB9D6BB7D175A37049EEE511A025E009* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88604
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnionEffect__ctor_mB7C9EDB10A3F96C0BBDC3F074486C2991DBF7E58 (UnionEffect_t37D758ED9E7C2676C2DF98C7E4C2A01CF88FB46C* __this, IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6* ___0_effects, const RuntimeMethod* method) 
{
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UnionEffect.cs:7>
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UnionEffect.cs:9>
		IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6* L_0 = ___0_effects;
		__this->____effects = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____effects), (void*)L_0);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UnionEffect.cs:10>
		return;
	}
}
// Method Definition Index: 88605
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnionEffect_ApplyEffect_m6A8DBBF000F76014075CB58B0955BC660D160D06 (UnionEffect_t37D758ED9E7C2676C2DF98C7E4C2A01CF88FB46C* __this, double ___0_strength, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IFocusEffect_t517DE4A02353DDF0DAC8AEC1CE2EB967EC943A21_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6* V_0 = NULL;
	int32_t V_1 = 0;
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UnionEffect.cs:14>
		IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6* L_0 = __this->____effects;
		V_0 = L_0;
		V_1 = 0;
		goto IL_0018;
	}

IL_000b:
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UnionEffect.cs:14>
		IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6* L_1 = V_0;
		int32_t L_2 = V_1;
		NullCheck(L_1);
		int32_t L_3 = L_2;
		RuntimeObject* L_4 = (L_1)->GetAt(static_cast<il2cpp_array_size_t>(L_3));
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UnionEffect.cs:16>
		double L_5 = ___0_strength;
		NullCheck(L_4);
		InterfaceActionInvoker1< double >::Invoke(0, IFocusEffect_t517DE4A02353DDF0DAC8AEC1CE2EB967EC943A21_il2cpp_TypeInfo_var, L_4, L_5);
		int32_t L_6 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_6, 1));
	}

IL_0018:
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UnionEffect.cs:14>
		int32_t L_7 = V_1;
		IFocusEffectU5BU5D_t1E6EF17471668DE8681BCAD5F4DF904C8A807EE6* L_8 = V_0;
		NullCheck(L_8);
		if ((((int32_t)L_7) < ((int32_t)((int32_t)(((RuntimeArray*)L_8)->max_length)))))
		{
			goto IL_000b;
		}
	}
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/UnionEffect.cs:18>
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 88606
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void VignetteEffect__ctor_mFDA42B78E6C96AE92BEB7DBDF1E0605373941890 (VignetteEffect_t4D2F583DF4E25F5E3B9A45F740DC17D4B18F058F* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/VignetteEffect.cs:11>
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/VignetteEffect.cs:14>
		return;
	}
}
// Method Definition Index: 88607
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void VignetteEffect_ApplyEffect_m8DFBA8DC781792B8CA608282AB9038AC49F6947E (VignetteEffect_t4D2F583DF4E25F5E3B9A45F740DC17D4B18F058F* __this, double ___0_strength, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&VolumeParameter_1_Override_mC346E68FEB631DC3D367126E958B371099C991E0_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	Vignette_t77147DD5FEEB4476AF22BD98255F8010738985DC* G_B4_0 = NULL;
	Vignette_t77147DD5FEEB4476AF22BD98255F8010738985DC* G_B3_0 = NULL;
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/VignetteEffect.cs:17>
		Vignette_t77147DD5FEEB4476AF22BD98255F8010738985DC* L_0 = __this->___vignette;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_0014;
		}
	}
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/VignetteEffect.cs:19>
		VignetteEffect_TryFindVignette_m1E49C5314CC03573E00A08A8509280FAC3B7ED81(__this, NULL);
	}

IL_0014:
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/VignetteEffect.cs:22>
		Vignette_t77147DD5FEEB4476AF22BD98255F8010738985DC* L_2 = __this->___vignette;
		Vignette_t77147DD5FEEB4476AF22BD98255F8010738985DC* L_3 = L_2;
		if (L_3)
		{
			G_B4_0 = L_3;
			goto IL_001f;
		}
		G_B3_0 = L_3;
	}
	{
		return;
	}

IL_001f:
	{
		NullCheck(G_B4_0);
		ClampedFloatParameter_tCD9F742962EAA50F658BC77595AB025D9EF8DEB8* L_4 = G_B4_0->___intensity;
		double L_5 = ___0_strength;
		NullCheck(L_4);
		VolumeParameter_1_Override_mC346E68FEB631DC3D367126E958B371099C991E0(L_4, ((float)L_5), VolumeParameter_1_Override_mC346E68FEB631DC3D367126E958B371099C991E0_RuntimeMethod_var);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/VignetteEffect.cs:23>
		return;
	}
}
// Method Definition Index: 88608
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void VignetteEffect_TryFindVignette_m1E49C5314CC03573E00A08A8509280FAC3B7ED81 (VignetteEffect_t4D2F583DF4E25F5E3B9A45F740DC17D4B18F058F* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_FindAnyObjectByType_TisVolume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377_mDF2A08B67822C1ABAB89B34865D4B6D72E2F7170_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&VolumeProfile_TryGet_TisVignette_t77147DD5FEEB4476AF22BD98255F8010738985DC_mE8244CE93377BC8821F5278FE70551C4B60D7D7E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral97AD4ACE67E943B106D9FFF8E32C1FD79B0BF1B8);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralFC59CEB920AC68F134A1598B64F3B285BD6AE4BA);
		s_Il2CppMethodInitialized = true;
	}
	Volume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377* V_0 = NULL;
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/VignetteEffect.cs:27>
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Volume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377* L_0;
		L_0 = Object_FindAnyObjectByType_TisVolume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377_mDF2A08B67822C1ABAB89B34865D4B6D72E2F7170(1, Object_FindAnyObjectByType_TisVolume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377_mDF2A08B67822C1ABAB89B34865D4B6D72E2F7170_RuntimeMethod_var);
		V_0 = L_0;
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/VignetteEffect.cs:28>
		Volume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377* L_1 = V_0;
		bool L_2;
		L_2 = Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602(L_1, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_2)
		{
			goto IL_002e;
		}
	}
	{
		Volume_t7CAAEA22D7F13A50FAE114DE7A6986FEAC837377* L_3 = V_0;
		NullCheck(L_3);
		VolumeProfile_t9B5F2005F575A710F38A124EF81A6228CCACACE1* L_4;
		L_4 = Volume_get_profile_mB157C4D67D52CE6D3E8211D6587B0EF71102E43D(L_3, NULL);
		Vignette_t77147DD5FEEB4476AF22BD98255F8010738985DC** L_5 = (Vignette_t77147DD5FEEB4476AF22BD98255F8010738985DC**)(&__this->___vignette);
		NullCheck(L_4);
		bool L_6;
		L_6 = VolumeProfile_TryGet_TisVignette_t77147DD5FEEB4476AF22BD98255F8010738985DC_mE8244CE93377BC8821F5278FE70551C4B60D7D7E(L_4, L_5, VolumeProfile_TryGet_TisVignette_t77147DD5FEEB4476AF22BD98255F8010738985DC_mE8244CE93377BC8821F5278FE70551C4B60D7D7E_RuntimeMethod_var);
		if (!L_6)
		{
			goto IL_002e;
		}
	}
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/VignetteEffect.cs:30>
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(_stringLiteralFC59CEB920AC68F134A1598B64F3B285BD6AE4BA, NULL);
		return;
	}

IL_002e:
	{
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/VignetteEffect.cs:34>
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(_stringLiteral97AD4ACE67E943B106D9FFF8E32C1FD79B0BF1B8, NULL);
		//<source_info:C:/Users/Studis/FocusGuard/Assets/UISwap/VignetteEffect.cs:36>
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Method Definition Index: 43026
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Color__ctor_mCD6889CDE39F18704CD6EA8E2EFBFA48BA3E13B0_inline (Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* __this, float ___0_r, float ___1_g, float ___2_b, const RuntimeMethod* method) 
{
	{
		float L_0 = ___0_r;
		__this->___r = L_0;
		float L_1 = ___1_g;
		__this->___g = L_1;
		float L_2 = ___2_b;
		__this->___b = L_2;
		__this->___a = (1.0f);
		return;
	}
}
// Method Definition Index: 43025
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Color__ctor_m3786F0D6E510D9CFA544523A955870BD2A514C8C_inline (Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* __this, float ___0_r, float ___1_g, float ___2_b, float ___3_a, const RuntimeMethod* method) 
{
	{
		float L_0 = ___0_r;
		__this->___r = L_0;
		float L_1 = ___1_g;
		__this->___g = L_1;
		float L_2 = ___2_b;
		__this->___b = L_2;
		float L_3 = ___3_a;
		__this->___a = L_3;
		return;
	}
}
